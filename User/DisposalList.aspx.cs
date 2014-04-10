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
using System.Web.Configuration;
using System.Text;
using System.Text.RegularExpressions;


public partial class DisposalList : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();
    DBClass dbObj = new DBClass();
    public static DataTable dtGv;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtDateFrom.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtDateTo.Text = DateTime.Now.AddDays(+1).ToString("yyyy-MM-dd");
            ucObj.DDLPlatformBind(ddlPlatform);
            //ddlPlatform.Items.Add(new ListItem("All",""));
            //ddlPlatform.Items.Insert(0, new ListItem("All", ""));
            ArrayList AryOpAct = new ArrayList();
            AryOpAct.Add(new ListItem("All", ""));
            AryOpAct.Add(new ListItem("Hold 住", "0"));
            AryOpAct.Add(new ListItem("良品出,廢品留下來", "1"));
            AryOpAct.Add(new ListItem("良品出,廢品丟", "2"));
            AryOpAct.Add(new ListItem("重測", "3"));
            AryOpAct.Add(new ListItem("通知工程師", "4"));
            ddlOpAct.DataSource = AryOpAct;
            ddlOpAct.DataValueField = "Value";
            ddlOpAct.DataTextField = "Text";
            ddlOpAct.DataBind();

        }

    }
    
    protected void GVDisposal2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[4].Text.ToString())
            {
                case "0":
                    e.Row.Cells[4].BackColor = Color.Red;
                    break;
                case "1":
                    e.Row.Cells[4].BackColor = Color.Yellow;
                    break;
                case "2":
                    e.Row.Cells[4].BackColor = Color.Lime;
                    break;
                case "3":
                    e.Row.Cells[4].BackColor = Color.Pink;
                    break;
                case "4":
                    e.Row.Cells[4].BackColor = Color.FromName("#FFB649");
                    e.Row.Cells[0].Text = e.Row.Cells[0].Text.ToString();
                    break;

            }
            e.Row.Cells[4].Text = ucObj.IndicationRtn(e.Row.Cells[4].Text.ToString());

            Label labSpc = (Label)e.Row.FindControl("labSPCurl");
            HiddenField hiddSpc = (HiddenField)e.Row.FindControl("hiddSpcLog");
            HiddenField hiddMisc = (HiddenField)e.Row.FindControl("hiddMisc");

            if (string.IsNullOrEmpty(hiddSpc.Value) == true)
            {
                labSpc.Text = "";
            }
            else
            {
                string[] spcArray = hiddSpc.Value.Split(',');
                string tmpbin = "";

                StringBuilder tmpurl = new StringBuilder();
                foreach (string cond1 in spcArray)
                {
                    tmpbin = Regex.Replace(cond1, @"\D", "");
                    tmpurl.Append("<a href=http://klx1001vm/cgi-bin/BinSPC_jnlp.pl?from=brat&type=" + hiddMisc.Value.Split('|')[0] + "&fab=" + hiddMisc.Value.Split('|')[1] + "&platform=" + e.Row.Cells[9].Text.ToString() + "&batch=" + hiddMisc.Value.Split('|')[2] + "&summ_date=" + hiddMisc.Value.Split('|')[3] + "&stage=" + e.Row.Cells[5].Text.ToString()[1] + "&bin=" + tmpbin + "\" target=\"_blank\">" + cond1 + "</a>");
                }
                labSpc.Text = tmpurl.ToString();
            }
            Label labEnc = (Label)e.Row.FindControl("labEncUrl");
            Label labDept = (Label)e.Row.FindControl("labRespDept");
            Label labStatus = (Label)e.Row.FindControl("labEncStatus");
            Label labDate = (Label)e.Row.FindControl("labStatusDate");
            Label labDefectCode = (Label)e.Row.FindControl("labDefectCode");

            EncResult eNCResult = new EncResult();
            eNCResult = ucObj.ReturnEncInfo(hiddMisc.Value.Split('|')[2]);
            labEnc.Text = "<a href=http://apkbl012.tw-khh01.nxp.com:8080/eNC/form/ea_request.aspx?docno=" + eNCResult.DocNo + "&seqno=1&sign=0 target=\"_blank\">" + eNCResult.RefNo + "</a>";
            labDept.Text = eNCResult.RespDept;
            labStatus.Text = eNCResult.Status;
            labDate.Text = eNCResult.StatusDate;
            labDefectCode.Text = eNCResult.Defect;
        }
    }

    //==========================================================
    //2013/8/26 Alan
    //==========================================================
    protected void O_Output_Click(object sender, EventArgs e)
    {
        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);

        string str = string.Format(@"SELECT o.batch,o.test_code,o.test_code_count as o_count,p.test_code_count as p_count,o.max_date FROM 
                                    (SELECT batch, test_code,count(test_code) test_code_count, max(start_date) max_date from summary_agilent 
                                        WHERE start_date between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') AND to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS')
                                        AND test_code='O'
                                        GROUP BY batch, test_code) o,
                                    (SELECT batch,test_code,count(test_code) test_code_count,max(start_date) max_date from summary_agilent
                                        WHERE start_date between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') AND to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS')
                                        AND test_code='P'
                                        GROUP BY batch, test_code) p
                                    WHERE o.batch=p.batch and o.max_date > p.max_date
                                    ORDER BY o.batch",
                                    DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString());
        DataTable dt = new DataTable();
        dt = ado.loadDataTable(str, null, "summary_AGILENT");
        GridView_O.DataSource = dt;
        GridView_O.DataBind();
        GridView_O.Visible = true;
        GVDisposal2.Visible = false;
        GridView_P.Visible = false;

        //========================================================
        //divTotal
        //========================================================
        divTotal.Style.Clear();
        divTotal.Style.Add("display", "inline");
        string totalStr = string.Format(@"SELECT batch, test_code,count(test_code) test_code_count, max(start_date) max_date 
                                          FROM summary_agilent 
                                          WHERE start_date between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') AND to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS')
                                          GROUP BY batch, test_code"
                                    , DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString());
        DataTable dtTotal = ado.loadDataTable(totalStr, null, "summary_agilent");
        int o_count = dt.Rows.Count, total = dtTotal.Rows.Count;
        lblTotal.Text = total.ToString();
        lblRows.Text = o_count.ToString();
        double Percentage = ((double)o_count / (double)total);
        lblPercentage.Text = ((Percentage * 100)).ToString("F2");
    }
    protected void P_Output_Click(object sender, EventArgs e)
    {
        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);

        string str = string.Format(@"SELECT batch, test_code,test_stage, count(test_code) as p_count,max(start_date) max_date from summary_agilent
                                    WHERE start_date between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') AND to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS')
                                          AND product != '0'
                                          AND test_stage like 'F%'
                                          AND length(batch) = 10
                                    GROUP BY batch,test_stage, test_code having test_code='P' and count(test_code) >= 2 
                                    ORDER BY batch,test_stage, count(test_code) desc",
                                    DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString());


        DataTable dt = new DataTable();
        dt = ado.loadDataTable(str, null, "summary_AGILENT");
        GridView_P.DataSource = dt;
        GridView_P.DataBind();
        GridView_P.Visible = true;
        GVDisposal2.Visible = false;
        GridView_O.Visible = false;

        //========================================================
        //divTotal
        //========================================================
        int p_count = 0;
        string compare = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (string.IsNullOrEmpty(compare) || !compare.Equals(dr["batch"].ToString()))
            {
                compare = dr["batch"].ToString();
                p_count += 1;
            }
        }


        divTotal.Style.Clear();
        divTotal.Style.Add("display", "inline");
        string totalStr = string.Format(@"SELECT batch
                                          FROM summary_agilent 
                                          WHERE start_date between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') AND to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS')
                                                AND product != '0'
                                                AND test_stage like 'F%'
                                                AND length(batch) = 10
                                          GROUP BY batch "
                                    , DateTime.Now.AddMonths(-1).ToShortDateString(), DateTime.Now.ToShortDateString());
        DataTable dtTotal = ado.loadDataTable(totalStr, null, "summary_agilent");
        //int p_count = dt.Rows.Count, total = dtTotal.Rows.Count;
        int total = dtTotal.Rows.Count;
        lblTotal.Text = total.ToString();
        lblRows.Text = p_count.ToString();
        double Percentage = ((double)p_count / (double)total);
        lblPercentage.Text = ((Percentage * 100)).ToString("F2");
    }
    private void RenderToXls(GridView gv)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=output.xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";

        this.EnableViewState = false;
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gv.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridView_P.Visible = false;
        GridView_O.Visible = false;
        GVDisposal2.Visible = true;
        divTotal.Style.Add("display", "none");


        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);
        string str = string.Format(@"SELECT BATCH,PASS,TOTAL-PASS FAIL, TOTAL,STATUS,DIFF,a.TESTER TESTER,to_char(a.COMMIT_DATE-40,'YYYY-MM-DD') COMMIT_DATE_40,
                                            to_char(a.COMMIT_DATE,'YYYY-MM-DD') COMMIT_DATE, to_char(a.COMMIT_DATE+1,'YYYY-MM-DD') COMMIT_DATE_1,
                                            to_char(a.COMMIT_DATE,'YYYY-MM-DD HH24:MI:SS') COMMIT_DATE_FULL, DEVICE,DEVICE_NAME,SUFFIX,a.PLATFORM PLATFORM,
                                            sumrowid ,spc_log,HOLDCODE,a.stage STAGE, a.sum_stage SUM_STAGE,typename, fab, a.qa_id QA_ID  
                                     FROM ACTION_LOG a ,DEVICE_POOL2 b 
                                     WHERE  b.id(+)=a.device 
                                            AND a.COMMIT_DATE BETWEEN TO_DATE('{0}','yyyy-mm-dd') 
                                            AND TO_DATE('{1}','yyyy-mm-dd')
                                            AND BATCH like '{2}%' AND DEVICE_NAME like '{3}%' AND a.PLATFORM LIKE '{4}%' AND STATUS like '{5}%' 
                                     ORDER BY a.COMMIT_DATE desc", txtDateFrom.Text, txtDateTo.Text, txtBatch.Text.Trim(),
                                                                  txtType.Text.Trim(),
                                                                  ddlPlatform.SelectedValue.Replace("*", ""),
                                                                  ddlOpAct.SelectedValue.Replace("*", ""));
        DataTable dt = ado.loadDataTable(str, null, "ACTION_LOG");
        GVDisposal2.DataSource = dt;
        GVDisposal2.DataBind();
        dtGv = dt;
        showPage();
    }

    protected void GVDisposal2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDisposal2.PageIndex = e.NewPageIndex;
        GVDisposal2.DataSource = dtGv;
        GVDisposal2.DataBind();
        showPage();
    }
    #region PagerTemplate
    protected void lbnFirst_Click(object sender, EventArgs e)
    {
        int num = 0;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        GVDisposal2_PageIndexChanging(null, ea);
    }
    protected void lbnPrev_Click(object sender, EventArgs e)
    {
        int num = GVDisposal2.PageIndex - 1;

        if (num >= 0)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            GVDisposal2_PageIndexChanging(null, ea);
        }
    }
    protected void lbnNext_Click(object sender, EventArgs e)
    {
        int num = GVDisposal2.PageIndex + 1;

        if (num < GVDisposal2.PageCount)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            GVDisposal2_PageIndexChanging(null, ea);
        }
    }
    protected void lbnLast_Click(object sender, EventArgs e)
    {
        int num = GVDisposal2.PageCount - 1;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        GVDisposal2_PageIndexChanging(null, ea);
    }
    private void showPage()
    {
        try
        {
            TextBox txtPage = (TextBox)GVDisposal2.BottomPagerRow.FindControl("txtSizePage");
            Label lblCount = (Label)GVDisposal2.BottomPagerRow.FindControl("lblTotalCount");
            Label lblPage = (Label)GVDisposal2.BottomPagerRow.FindControl("lblPage");
            Label lblbTotal = (Label)GVDisposal2.BottomPagerRow.FindControl("lblTotalPage");

            txtPage.Text = GVDisposal2.PageSize.ToString();
            lblCount.Text = dtGv.Rows.Count.ToString();
            lblPage.Text = (GVDisposal2.PageIndex + 1).ToString();
            lblbTotal.Text = GVDisposal2.PageCount.ToString();
        }
        catch (Exception e) { }
    }
    // page change
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            string numPage = ((TextBox)GVDisposal2.BottomPagerRow.FindControl("txtSizePage")).Text.ToString();
            if (!string.IsNullOrEmpty(numPage))
            {
                GVDisposal2.PageSize = Convert.ToInt32(numPage);
            }

            TextBox pageNum = ((TextBox)GVDisposal2.BottomPagerRow.FindControl("inPageNum"));
            string goPage = pageNum.Text.ToString();
            if (!string.IsNullOrEmpty(goPage))
            {
                int num = Convert.ToInt32(goPage) - 1;
                if (num >= 0)
                {
                    GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
                    GVDisposal2_PageIndexChanging(null, ea);
                    ((TextBox)GVDisposal2.BottomPagerRow.FindControl("inPageNum")).Text = null;
                }
            }

            GVDisposal2.DataSource = dtGv;
            GVDisposal2.DataBind();
            showPage();
        }
        catch (Exception ex) { }
    }
    #endregion
}

