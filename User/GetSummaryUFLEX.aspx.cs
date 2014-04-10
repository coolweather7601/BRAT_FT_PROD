using System;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Text;
using System.Web.Configuration;
using System.Text.RegularExpressions;

public partial class GetSummaryUFLEX : System.Web.UI.Page
{
    DBClass dbObj = new DBClass();
    UserInfoClass ucObj = new UserInfoClass();
    AutoCheck acObj = new AutoCheck();
    string Platform;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtDateTo.Text = DateTime.Now.AddDays(+1).ToString("yyyy-MM-dd");
            
        }
        Platform = Request.QueryString["Platform"];
    }
    protected void BTNSearch_Click(object sender, EventArgs e)
    {

        panelResult.Visible = false;  //Default hide the result panel
        btnBrat.Enabled = false;
        txtPassQty.Text = "";
        txtTotalQty.Text = "";

        //Get Pass/Tatal from ACS
        ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["ACS"];
        OracleConnection conn = new OracleConnection();
        conn.ConnectionString = setting.ConnectionString;
        conn.Open();
        string sqlstr = "select qty,qty_pass from batch_record where batchno=upper('" + txtBatch.Text.ToString() + "') and start_date_time between to_date('" + txtDateFrom.Text.ToString() + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + txtDateTo.Text.ToString() + " 23:59:59','yyyy-mm-dd HH24:MI:SS')  and SORTER='f' and  tester like '%FLEX%' order by qty";
        OracleCommand cmd = new OracleCommand(sqlstr, conn);
        OracleDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            txtTotalQty.Text = rd[0].ToString();
            txtPassQty.Text = rd[1].ToString();
        }
        rd.Close();
        conn.Close();

        using (MesData.SFCData service = new MesData.SFCData())
        {
            //service.Url = serviceURL;
            //service.Timeout = serviceTimeout * 1000;

            MesData.CompleteLotDataProxy proxy = service.getCompleteLotData(txtBatch.Text);
            if (proxy != null)
            {
                if (proxy.ErrorNumber == 0)
                {
                    hiddIntrack12NC.Value = proxy.Product;
                    hiddIntrackTypeName.Value = proxy.LocalTypeName;
                    hiddFab.Value = proxy.WaferFabName;
                    labDifussion.Text = proxy.Waferbatch;
                    labLogging.Text = "";
                }
                else
                {
                    labLogging.Text = proxy.ErrorDescription;
                }
            }
            else
                labLogging.Text = "SFC data web service not responsed!";
        }

        //Get SumInfo base on batch
        //string test_stage = "";
        string P_Str_SqlStr = "select rowid, file_location,file_name,df_lotid,product,to_char(start_date,'YYYY mm dd.HH24MISS') start_date,pass_qty,tested_qty,test_code,tester,test_stage,non_retest,pscver " +
        "FROM summary_"+Platform+" where test_code in ('P', 'O') and batch = upper('" + txtBatch.Text.ToString() + "') and start_date between to_date('" + txtDateFrom.Text.ToString() + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + txtDateTo.Text.ToString() + " 23:59:59','yyyy-mm-dd HH24:MI:SS') ";
        OracleConnection myConn = dbObj.GetSumConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "SUM");
        DataTable newtable = new DataTable();
        newtable = ds.Tables["SUM"];

        gvBatchInfo.DataSource = newtable;
        gvBatchInfo.DataBind();

        gvBatchInfo.SelectedIndex = -1; //Unselect the row of sum info


    }

    protected void gvBatchInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Visible = false;
        }

        System.Data.DataRowView drvSum;
        drvSum = (System.Data.DataRowView)e.Row.DataItem;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[12].Visible = false;
            if (drvSum != null)
            {
                if ((e.Row.Cells[10].Text[0] == 'A') || ((e.Row.Cells[10].Text.ToString().CompareTo("Q22") == 0) && (ucObj.CheckSpecialQA(hiddIntrack12NC.Value) == 1)))
                {
                    e.Row.Cells[1].Text = "X";
                }
            }
        }
    }

    protected void gvBatchInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        UpdateRowControl(e.NewSelectedIndex);
    }

    private void UpdateRowControl(int rowIndex)
    {
        if (rowIndex != -1)
        {
            hiddSelectRow.Value = gvBatchInfo.Rows[rowIndex].Cells[12].Text;
            hiddStage.Value = gvBatchInfo.Rows[rowIndex].Cells[10].Text.ToString();
            //hiddStage.Value = gvBatchInfo.Rows[rowIndex].Cells[10].Text.ToString().Remove(1, 1);
            if ((gvBatchInfo.Rows[rowIndex].Cells[10].Text[0] == 'A') || ((gvBatchInfo.Rows[rowIndex].Cells[10].Text.ToString().CompareTo("Q22") == 0) && (ucObj.CheckSpecialQA(hiddIntrack12NC.Value) == 1)))
            {
                btnBrat.Enabled = false;
            }
            else
            {
                btnBrat.Enabled = true;
            }
        }
        else
        {
            hiddStage.Value = "";
            hiddSelectRow.Value = "";
        }
    }

    protected void btnBrat_Click(object sender, EventArgs e)
    {
        panelResult.Visible = true;
        string device_ID = "";
        string commit_Date = "";
        commit_Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        string test_stage = "";
        if ((hiddStage.Value.CompareTo("F105") == 0) || (hiddStage.Value.CompareTo("B105") == 0))
        {
            test_stage = "F1";
        }
        else if (hiddStage.Value.CompareTo("Q22") == 0)
        {
            test_stage = ucObj.ReturnTenInfo(hiddIntrack12NC.Value);
            //test_stage = "F2";
        }
        else
        {
            test_stage = hiddStage.Value.Remove(1, 1);
        }

        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        string sqlstr = "select DEVICE_POOL2.ID device_id,new_expertise_id,Device_name, Suffix,PROD12NC_POOL2.prod_12nc, PROD12NC_POOL2.COMMER_NAME, platform,Temp,Stage,BUDGET_YLD " +
                        "from DEVICE_POOL2  ,PROD12NC_POOL2 where DEVICE_POOL2.id=PROD12NC_POOL2.id and PROD12NC_POOL2.prod_12nc='" + hiddIntrack12NC.Value+ "' and DEVICE_POOL2.platform = '"+Platform+"' " +
                        "and Stage = '" + test_stage + "'";

        OracleCommand myCmd = new OracleCommand(sqlstr, myConn);
        OracleDataReader rd = myCmd.ExecuteReader();
        while (rd.Read())
        {
            device_ID = rd["device_id"].ToString();

            hiddDevice.Value = rd["Device_name"].ToString();
            labDevice.Text = "<a href=\"ViewLimit.aspx?ID=" + device_ID + "\" target=\"_blank\">" + rd["Device_name"].ToString() + "</a>";
            labSuffix.Text = rd["Suffix"].ToString();
            lab12Nc.Text = rd["prod_12nc"].ToString();
            labTemp.Text = rd["Temp"].ToString();
            labPlatform.Text = rd["platform"].ToString();
            labBudgetYield.Text = rd["BUDGET_YLD"].ToString();
            labStage.Text = rd["Stage"].ToString();

        }
        rd.Close();
        myConn.Close();

        labDetailInfo.Text = "<a href=\"Limit_Match_Case_HB.aspx?Batch=" + txtBatch.Text + "&FromDate=" + txtDateFrom.Text + "&EndDate=" + txtDateTo.Text + "&ID=" + device_ID + "&rowid=" + hiddSelectRow.Value + "&pass_acs=" + txtPassQty.Text + "&total_acs=" + txtTotalQty.Text + "&platform=" + labPlatform.Text + "&test_stage=" + hiddStage.Value + "\" target=\"_blank\">BR@T Limit Result</a>";

        WholeResult SumResult = acObj.GetSummaryInfo(txtBatch.Text, txtDateFrom.Text, txtDateTo.Text, hiddStage.Value, Platform, Convert.ToInt32(txtPassQty.Text), Convert.ToInt32(txtTotalQty.Text), 1, hiddSelectRow.Value, "");

        labEng.Text = ucObj.RtnEngName(SumResult.Engineer);

        switch (SumResult.StatusIdx)
        {
            case 0:
                div1.Style.Add("background-color", "Red");
                break;
            case 1:
                div1.Style.Add("background-color", "Yellow");
                break;
            case 2:
                div1.Style.Add("background-color", "Lime");
                break;
            case 3:
                div1.Style.Add("background-color", "Pink");
                break;
            case 4:
                div1.Style.Add("background-color", "#FFB649");
                break;
        }
        /*
        if (SumResult.StatusIdx == 0)
        {
            //labBratResult.BackColor = Color.Red;
            
        }
        else
        {
            //labBratResult.BackColor = Color.Lime;
            div1.Style.Add("background-color", "Lime");
        }
        */
        labBratResult.Text = "<P>"+SumResult.Status;
        labHoldCode.Text = SumResult.HoldCode;
        labComment.Text = SumResult.Comment;
        labActualYield.Text = SumResult.Yield.ToString();
        labAYHCheck.Text = "OK";
        labOpenShortCheck.Text = "OK";
        labHandlerCheck.Text = "OK";
        labBinSpcCheck.Text = "OK";
        labQACheck.Text = "OK";
        labBatchNr.Text = txtBatch.Text;

        string spc_log = "";

        for (int count = 0; count < SumResult.SpecificResult.Count; count++)
        {
            SpecificCheck reporter = (SpecificCheck)SumResult.SpecificResult[count];

            if (reporter.Type.CompareTo("AYHcheck") == 0)
            {
                labAYHCheck.Text = "扣住;" + reporter.HoldCode + ";" + reporter.Comment;
            }

            if (reporter.Type.CompareTo("OScheck") == 0)
            {
                labOpenShortCheck.Text = "扣住;" + reporter.HoldCode + ";" + reporter.Comment;
            }

            if (reporter.Type.CompareTo("QASampling") == 0)
            {
                if (reporter.HoldCode.CompareTo("None") == 0)
                {
                    labQACheck.Text = reporter.Comment;
                }
                else
                {
                    labQACheck.Text = "扣住;" + reporter.HoldCode + ";" + reporter.Comment;
                }
            }
            if (reporter.Type.CompareTo("R2R") == 0)
            {
                labHandlerCheck.Text = "扣住;" + reporter.HoldCode + ";" + reporter.Comment;
            }
            if (reporter.Type.CompareTo("BIN_SPC") == 0)
            {
                if (string.IsNullOrEmpty(reporter.Comment) == false)
                {
                    string[] spcArray = reporter.Comment.Split(',');
                    string tmpbin = "";

                    StringBuilder tmpurl = new StringBuilder();
                    foreach (string cond1 in spcArray)
                    {
                        tmpbin = Regex.Replace(cond1, @"\D", "");
                        tmpurl.Append("<a href=http://klx1001vm/cgi-bin/BinSPC_jnlp.pl?from=brat&type=" + hiddIntrackTypeName.Value + "&fab=" + hiddFab.Value + "&platform=" + labPlatform.Text.ToString() + "&batch=" + labBatchNr.Text.ToString() + "&summ_date=" + "" + "&stage=" + labStage.Text.ToString()[1] + "&bin=" + tmpbin + "\" target=\"_blank\">" + cond1 + "</a>");
                    }
                    spc_log = tmpurl.ToString();
                }

                if (hiddDevice.Value.CompareTo("TDA18273") == 0)
                {
                    labBinSpcCheck.Text = "扣住;" + reporter.HoldCode + ";" + spc_log;
                }
                else
                {
                    labBinSpcCheck.Text = "廢品留下來;" + reporter.HoldCode + ";" + spc_log;
                }
            }
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetFailInfo(string contextKey)
    {

        StringBuilder FailInfo = new StringBuilder();
        string rowid = contextKey.Split('$')[0];
        string total = contextKey.Split('$')[1];
        FailInfo.Append("<table><tr><td>------------&nbsp;&nbsp;<i>Top 20 Fail Test Items</i>------</td></tr></table><b>&nbsp;&nbsp; Item | Qty | Yield | Description</b><br>");
        //return string.Format("[Missing image: {0}]", contextKey);
        string Cmd_Bin = "select top1,top2,top3,top4,top5,top6,top7,top8,top9,top10,top11,top12,top13,top14,top15,top16,top17,top18,top19,top20 from summary_uflex WHERE rowid ='" + rowid + "'";

        OracleConnection conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM"].ConnectionString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(Cmd_Bin, conn);
        OracleDataReader dr1 = cmd.ExecuteReader();
        dr1.Read();

        int fail_len = 0;
        float fail_yield = 0;
        for (int i = 0; i < 20; i++)
        {
            if (string.IsNullOrEmpty(dr1[i].ToString()) == false)
            {
                string FailItem = dr1[i].ToString().Split(';')[0];
                string FailQty = dr1[i].ToString().Split(';')[1];
                string FailDesc = dr1[i].ToString().Split(';')[2];

                fail_len = FailItem.Length;
                for (int j = 7; j > fail_len; j--)
                {
                    FailInfo.Append("&nbsp;");
                }
                FailInfo.Append(FailItem);

                fail_len = FailQty.Length;
                for (int j = 7; j > fail_len; j--)
                {
                    FailInfo.Append("&nbsp;");
                }
                FailInfo.Append(FailQty);

                if (string.IsNullOrEmpty(total) == false)
                {
                    fail_yield = (float)Convert.ToInt16(FailQty) / Convert.ToInt16(total) * 100;
                }
                FailInfo.Append("&nbsp;&nbsp;&nbsp;" + String.Format("{0:#,0.##}", fail_yield) + "&nbsp;&nbsp;&nbsp;" + FailDesc + "<br>");
            }
        }
        dr1.Close();
        conn.Close();

        return FailInfo.ToString();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    //[WebMethod]
    public static string GetDetails(string contextKey)
    {
        StringBuilder BinInfo2 = new StringBuilder();
        string rowid = contextKey.Split('$')[0];
        string total = contextKey.Split('$')[1];
        BinInfo2.Append("<table><tr><td>------------&nbsp;&nbsp;<i>BIN VALUE</i>&nbsp;&nbsp;--------</td></tr></table>&nbsp;&nbsp;&nbsp;<b>BIN |  Qty | Yield </b><br>");
        //return string.Format("[Missing image: {0}]", contextKey);
        string Cmd_Bin = "select bin1,bin2,bin3,bin4,bin5,bin6,bin7,bin8,bin9,bin10,bin11,bin12,bin13,bin14,bin15,bin16,bin17,bin18,bin19,bin20," +
                        "bin21,bin22,bin23,bin24,bin25,bin26,bin27,bin28,bin29,bin30,bin31,bin32,bin33,bin34,bin35,bin36,bin37,bin38,bin39,bin40," +
                        "bin41,bin42,bin43,bin44,bin45,bin46,bin47,bin48,bin49,bin50,bin51,bin52,bin53,bin54,bin55,bin56,bin57,bin58,bin59,bin60," +
                        "bin61,bin62,bin63,bin64,bin65,bin66,bin67,bin68,bin69,bin70,bin71,bin72,bin73,bin74,bin75,bin76,bin77,bin78,bin79,bin80," +
                        "bin81,bin82,bin83,bin84,bin85,bin86,bin87,bin88,bin89,bin90,bin91,bin92,bin93,bin94,bin95,bin96,bin97,bin98,bin99,bin100," +
                        "bin101,bin102,bin103,bin104,bin105,bin106,bin107,bin108,bin109,bin110,bin111,bin112,bin113,bin114,bin115,bin116,bin117,bin118,bin119,bin120," +
                        "bin121,bin122,bin123,bin124,bin125,bin126,bin127,bin128,bin129,bin130,bin131,bin132,bin133,bin134,bin135,bin136,bin137,bin138,bin139,bin140," +
                        "bin141,bin142,bin143,bin144,bin145,bin146,bin147,bin148,bin149,bin150,bin151,bin152,bin153,bin154,bin155,bin156,bin157,bin158,bin159,bin160," +
                        "bin161,bin162,bin163,bin164,bin165,bin166,bin167,bin168,bin169,bin170,bin171,bin172,bin173,bin174,bin175,bin176,bin177,bin178,bin179,bin180," +
                        "bin181,bin182,bin183,bin184,bin185,bin186,bin187,bin188,bin189,bin190,bin191,bin192,bin193,bin194,bin195,bin196,bin197,bin198,bin199,bin200," +
                        "bin201,bin202,bin203,bin204,bin205,bin206,bin207,bin208,bin209,bin210,bin211,bin212,bin213,bin214,bin215,bin216,bin217,bin218,bin219,bin220," +
                        "bin221,bin222,bin223,bin224,bin225,bin226,bin227,bin228,bin229,bin230,bin231,bin232,bin233,bin234,bin235,bin236,bin237,bin238,bin239,bin240," +
                        "bin241,bin242,bin243,bin244,bin245,bin246,bin247,bin248,bin249,bin250,bin251,bin252,bin253,bin254,bin255,bin256 from summary_uflex WHERE rowid ='" + rowid + "'";

        OracleConnection conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM"].ConnectionString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(Cmd_Bin, conn);
        OracleDataReader dr1 = cmd.ExecuteReader();
        dr1.Read();

        int bin_len = 0;
        float bin_yield = 0;
        for (int i = 0; i < 256; i++)
        {
            if (string.IsNullOrEmpty(dr1[i].ToString()) == false)
            {
                if ((i + 1) < 10)
                {
                    BinInfo2.Append("&nbsp;&nbsp;");
                }
                if ((i + 1) > 10 && (i + 1) < 100)
                {
                    BinInfo2.Append("&nbsp;");
                }
                BinInfo2.Append("&nbsp;&nbsp;&nbsp;&nbsp;" + (i + 1));
                bin_len = dr1[i].ToString().Length;
                for (int j = 7; j > bin_len; j--)
                {
                    BinInfo2.Append("&nbsp;");
                }
                BinInfo2.Append(dr1[i].ToString());


                if (string.IsNullOrEmpty(total) == false)
                {
                    bin_yield = (float)Convert.ToInt16(dr1[i].ToString()) / Convert.ToInt16(total) * 100;
                }
                BinInfo2.Append("&nbsp;&nbsp;&nbsp;" + String.Format("{0:#,0.##}", bin_yield) + "<br>");
            }
        }
        dr1.Close();
        conn.Close();

        return BinInfo2.ToString();
    }



}
