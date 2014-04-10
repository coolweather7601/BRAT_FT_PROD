using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Web.Configuration;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

public partial class User_Limit_Match_Case : System.Web.UI.Page
{
    DBClass dbObj = new DBClass();
    UserInfoClass ucObj = new UserInfoClass();
    SummaryCheck scObj = new SummaryCheck();
    AutoCheck acObj = new AutoCheck();
    string Rowid = "";
    string DeviceID = "";
    string ExpID = "";
    string Batch = "";
    string FromDate = "";
    string EndDate = "";
    string Platform = "";
    string SumStage = "";
    int PassQty = 0;
    int TotalQty = 0;
    float Yield = 0;
    float BudgetYield = 0;
    DataSet ds2;
    DataSet ds3;

    Dictionary<string, ArrayList> NonRtBinDict = new Dictionary<string, ArrayList>();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DeviceID = Request.QueryString["ID"];
            Rowid = Request.QueryString["rowid"];
            Rowid = Rowid.Replace(" ", "+");
            PassQty = System.Convert.ToInt32(Request.QueryString["pass_acs"]);
            TotalQty = System.Convert.ToInt32(Request.QueryString["total_acs"]);
            Batch = Request.QueryString["Batch"];
            FromDate = Request.QueryString["FromDate"];
            EndDate = Request.QueryString["EndDate"];
            Platform = Request.QueryString["platform"];
            SumStage = Request.QueryString["test_stage"];
            Yield = (float)PassQty / TotalQty * 100;
            SqlDevicePool.SelectParameters["NEW_EXPERTISE_ID"].DefaultValue = ID;
        }

        string sqlstr = "SELECT DEVICE_NAME,SUFFIX,PLATFORM,STAGE,BUDGET_YLD,NEW_EXPERTISE_ID FROM DEVICE_POOL2 WHERE ID = '" + DeviceID + "'";
        ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["BRAT_TEST"];
        OracleConnection conn = new OracleConnection();
        conn.ConnectionString = setting.ConnectionString;
        conn.Open();

        OracleCommand cmd = new OracleCommand(sqlstr, conn);
        OracleDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            LABDeviceName.Text = rd["DEVICE_NAME"].ToString();
            LABSuffix.Text = rd["SUFFIX"].ToString();
            LABPlatform.Text = rd["PLATFORM"].ToString();
            LABStage.Text = rd["STAGE"].ToString();
            LABBgtYld.Text = rd["BUDGET_YLD"].ToString();
            ExpID = rd["NEW_EXPERTISE_ID"].ToString();
            BudgetYield = float.Parse(rd["BUDGET_YLD"].ToString());
        }
        rd.Close();
        conn.Close();

        ds2 = ucObj.RtnSumFail("UserInfo2", Rowid, Platform);
        ds3 = ucObj.RtnSumBin("UserInfo3", Rowid, Platform);

        int bin11 = 0;
        if (ds3.Tables["UserInfo3"].Rows[0][10].ToString().CompareTo("") != 0)
            bin11 = Convert.ToInt32(ds3.Tables["UserInfo3"].Rows[0][10].ToString());  //Get the bin11 of last record
        int merge_bin11 = 0;

        NonRtBinDict = scObj.NonRetestBinCollect(Batch, Rowid, FromDate, EndDate, SumStage, LABPlatform.Text);

        //Update the bin 11 value of last record
        if (NonRtBinDict.ContainsKey("bin11"))
        {
            merge_bin11 = bin11 + Convert.ToInt32(NonRtBinDict["bin11"][0].ToString());
            ds3.Tables["UserInfo3"].Rows[0][10] = merge_bin11.ToString();  //Update the bin 11 value of last record

        }

        foreach (DataRow row in ds2.Tables["UserInfo2"].Rows)
        {
            foreach (DataColumn col in ds2.Tables["UserInfo2"].Columns)
            {
                if (row[col].ToString().CompareTo("") != 0)
                {
                    string keynum = row[col].ToString().Split(';')[0];
                    string keyqty = row[col].ToString().Split(';')[1];
                    string keydesc = row[col].ToString().Split(';')[2];
                    if (!NonRtBinDict.ContainsKey(keynum))
                    {
                        NonRtBinDict.Add(keynum, new ArrayList());
                        NonRtBinDict[keynum].Add(keyqty);
                        NonRtBinDict[keynum].Add(keydesc);
                    }
                    else
                    {
                        NonRtBinDict[keynum][0] = Convert.ToInt32(NonRtBinDict[keynum][0]) + Convert.ToInt32(keyqty);
                    }
                }
            }
        }


        string P_Str_SqlStr = "SELECT CRITERION, PIORITY, EQUATION, UNIT, QTY, CRITERION2, EQUATION2, UNIT2, QTY2,INDICATE_ID, ACTION, HOLD_CODE, COMMENTS, ENGINEER, PHONE FROM  LIMIT_EXPERTISE WHERE NEW_EXPERTISE_ID = '" + ExpID + "'  order by PIORITY";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "LIMIT");
        DataTable newtable = new DataTable();
        newtable = ds.Tables["LIMIT"];
        newtable.Columns.Add(new DataColumn("STATUS", typeof(bool)));
        newtable.Columns.Add(new DataColumn("VALUE", typeof(string)));

        foreach (DataRow row in newtable.Rows)
        {
            LimitParse(row);
        }
        gvResult.DataSource = newtable;
        gvResult.DataBind();
    }

    public void LimitParse(DataRow limit_array)
    {
        int total_fail = 0;
        float ActYield = 0;

        int total_fail2 = 0;
        float ActYield2 = 0;

        float rtnValue1 = 0;
        float rtnValue2 = 0;

        bool Result1 = true;
        bool Result2 = true;
        bool Result = true;

        int Idx2ndCond = 0;

        if (limit_array[5].ToString().CompareTo("") != 0)
        {
            Idx2ndCond = 1;
        }
        string[] BinArray = limit_array[0].ToString().Split('+');

        foreach (string bin in BinArray)
        {

            if (bin.Substring(0, 1) == "P")
            {
                total_fail += PassQty;
            }

            if (bin.Substring(0, 1) == "B")
            {
                //int ggg = System.Convert.ToInt32(bin.Substring(1));
                //string kkk = ds3.Tables["UserInfo3"].Rows[0][System.Convert.ToInt32(bin.Substring(1))-1].ToString();
                if (ds3.Tables["UserInfo3"].Rows[0][System.Convert.ToInt32(bin.Substring(1)) - 1].ToString().CompareTo("") != 0)
                {
                    total_fail += System.Convert.ToInt32(ds3.Tables["UserInfo3"].Rows[0][System.Convert.ToInt32(bin.Substring(1)) - 1].ToString());
                }
            }

            if (bin.Substring(0, 1) == "F")
            {
                if (NonRtBinDict.ContainsKey(bin.Substring(1)))
                {
                    total_fail += Convert.ToInt32(NonRtBinDict[bin.Substring(1)][0]);
                }

            }
        }
        ActYield = (float)total_fail / TotalQty * 100;

        if (Idx2ndCond == 1)
        {
            string[] BinArray2 = limit_array[5].ToString().Split('+');

            foreach (string bin in BinArray2)
            {

                if (bin.Substring(0, 1) == "P")
                {
                    total_fail2 += PassQty;
                }

                if (bin.Substring(0, 1) == "B")
                {
                    if (ds3.Tables["UserInfo3"].Rows[0][System.Convert.ToInt32(bin.Substring(1)) - 1].ToString().CompareTo("") != 0)
                    {
                        total_fail2 += System.Convert.ToInt32(ds3.Tables["UserInfo3"].Rows[0][System.Convert.ToInt32(bin.Substring(1)) - 1].ToString());
                    }

                }
                if (bin.Substring(0, 1) == "F")
                {
                    if (NonRtBinDict.ContainsKey(bin.Substring(1)))
                    {
                        total_fail2 += Convert.ToInt32(NonRtBinDict[bin.Substring(1)][0]);
                    }

                }
            }
            ActYield2 = (float)total_fail2 / TotalQty * 100;
        }

        switch (limit_array[3].ToString())
        {
            case "pcs":
                switch (limit_array[2].ToString())
                {
                    case ">":
                        if (total_fail > System.Convert.ToDouble(limit_array[4]))
                        {
                            Result1 = true;
                        }
                        else
                        {
                            Result1 = false;
                        }
                        break;
                    case "=":
                        if (total_fail == System.Convert.ToDouble(limit_array[4]))
                        {
                            Result1 = true;
                        }
                        else
                        {
                            Result1 = false;
                        }
                        break;
                    case "<":
                        if (total_fail < System.Convert.ToDouble(limit_array[4]))
                        {
                            Result1 = true;
                        }
                        else
                        {
                            Result1 = false;
                        }
                        break;
                }
                rtnValue1 = total_fail;
                break;
            case "%":
                switch (limit_array[2].ToString())
                {
                    case ">":
                        if (ActYield > System.Convert.ToDouble(limit_array[4]))
                        {
                            Result1 = true;
                        }
                        else
                        {
                            Result1 = false;
                        }
                        break;
                    case "=":
                        if (ActYield == System.Convert.ToDouble(limit_array[4]))
                        {
                            Result1 = true;
                        }
                        else
                        {
                            Result1 = false;
                        }
                        break;
                    case "<":
                        if (ActYield < System.Convert.ToDouble(limit_array[4]))
                        {
                            Result1 = true;
                        }
                        else
                        {
                            Result1 = false;
                        }
                        break;
                }
                rtnValue1 = ActYield;
                break;

        }

        if (Idx2ndCond == 1)
        {

            switch (limit_array[7].ToString())
            {
                case "pcs":
                    switch (limit_array[6].ToString())
                    {
                        case ">":
                            if (total_fail2 > System.Convert.ToDouble(limit_array[8]))
                            {
                                Result2 = true;
                            }
                            else
                            {
                                Result2 = false;
                            }
                            break;
                        case "=":
                            if (total_fail2 == System.Convert.ToDouble(limit_array[8]))
                            {
                                Result2 = true;
                            }
                            else
                            {
                                Result2 = false;
                            }
                            break;
                        case "<":
                            if (total_fail2 < System.Convert.ToDouble(limit_array[8]))
                            {
                                Result2 = true;
                            }
                            else
                            {
                                Result2 = false;
                            }
                            break;
                    }
                    rtnValue2 = total_fail2;
                    break;
                case "%":
                    switch (limit_array[6].ToString())
                    {
                        case ">":
                            if (ActYield2 > System.Convert.ToDouble(limit_array[8]))
                            {
                                Result2 = true;
                            }
                            else
                            {
                                Result2 = false;
                            }
                            break;
                        case "=":
                            if (ActYield2 == System.Convert.ToDouble(limit_array[8]))
                            {
                                Result2 = true;
                            }
                            else
                            {
                                Result2 = false;
                            }
                            break;
                        case "<":
                            if (ActYield2 < System.Convert.ToDouble(limit_array[8]))
                            {
                                Result2 = true;
                            }
                            else
                            {
                                Result2 = false;
                            }
                            break;
                    }
                    rtnValue2 = ActYield2;
                    break;
            }
        }

        if (Idx2ndCond == 1)
        {
            Result = Result1 && Result2;
            limit_array[15] = Result;
            limit_array[16] = String.Format("{0:#,0.#####}", rtnValue1) + ";" + String.Format("{0:#,0.#####}", rtnValue2);
            return;
        }
        else
        {
            limit_array[15] = Result1;
            limit_array[16] = String.Format("{0:#,0.#####}", rtnValue1);
            return;
        }
    }

    /// <summary>
    /// Transfer code to full description and highlight the fail item
    /// </summary>
    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal) | (e.Row.RowState == DataControlRowState.Alternate))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label cmt = (Label)e.Row.FindControl("label3");
                cmt.Text = Server.UrlDecode(cmt.Text.ToString());
                cmt.Text = cmt.Text.ToString().Replace("\r\n", "<br/>");

                Label eng = (Label)e.Row.FindControl("label4");
                eng.Text = Server.UrlDecode(eng.Text.ToString());
                eng.Text = eng.Text.ToString().Replace("\r\n", "<br/>");

                Label Ind = (Label)e.Row.FindControl("label1");
                Ind.Text = ucObj.IndicationRtn(Ind.Text.ToString());
                Label HCode = (Label)e.Row.FindControl("label2");
                HCode.Text = ucObj.HoldCodeRtn(HCode.Text.ToString());

                if (e.Row.Cells[0].Text.ToString().CompareTo("True") == 0)
                {
                    e.Row.BackColor = Color.LightPink;
                }
            }
        }
    }

    /// <summary>
    /// Create crossing head
    /// </summary>
    protected void gvResult_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].Attributes.Add("rowspan", "2");
                tcHeader[0].Text = "Status";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("rowspan", "2");
                tcHeader[1].Text = "Value";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("colspan", "4");
                tcHeader[2].Text = "條件1";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Attributes.Add("rowspan", "2");
                tcHeader[3].Text = "AND";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Attributes.Add("colspan", "4");
                tcHeader[4].Text = "條件2";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("rowspan", "2");
                tcHeader[5].Text = "指示";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Attributes.Add("rowspan", "2");
                tcHeader[6].Text = "Hold Code";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Attributes.Add("rowspan", "2");
                tcHeader[7].Text = "註解";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Attributes.Add("rowspan", "2");
                tcHeader[8].Text = "連絡人";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[9].Attributes.Add("rowspan", "2");
                tcHeader[9].Text = "電話</th></tr><tr>";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[13].Text = "單位";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[14].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[15].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[16].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[17].Text = "單位";
                break;
        }
    }
}
