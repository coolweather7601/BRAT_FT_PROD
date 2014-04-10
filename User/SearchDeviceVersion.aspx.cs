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


public partial class SearchDeviceVersion : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();
    public static DataTable dtGv;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucObj.DDLPlatformBind(DDLPlatform);
            BTNSearch_Click(null, null);
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string Get12ncInfo(string contextKey)
    {


        string RtnInfo = "";
        string Cmd_12nc = "SELECT PROD_12NC, COMMER_NAME FROM PROD12NC_POOL2 WHERE ID ='" + contextKey + "'";
        OracleConnection conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(Cmd_12nc, conn);
        OracleDataReader dr1 = cmd.ExecuteReader();
        while (dr1.Read())
        {
            RtnInfo = RtnInfo + "<tr><td>" + dr1["PROD_12NC"].ToString() + "</td><td>" + dr1["COMMER_NAME"].ToString() + "</td></tr>";
        }
        RtnInfo = "<table border=1 ><tr ><td><strong>12NC</strong></td><td><strong>Device</strong></td></tr>" + RtnInfo + "</table>";
        dr1.Close();
        conn.Close();

        return RtnInfo;
    }

    //==========================================================
    //2013/8/26 Alan
    //==========================================================
    protected void BTNSearch_Click(object sender, EventArgs e)
    {
        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);
        string str = string.Format(@"SELECT  DEVICE_NAME ,  SUFFIX ,  STAGE ,  PLATFORM ,  BUDGET_YLD ,  BL ,  OWNER ,  ID ,
                                             NEW_EXPERTISE_ID ,  TEMP ,   COMMIT_DATE  
                                     FROM  DEVICE_POOL2_VERSION 
                                     WHERE device_name like '{0}%' and platform like '{1}%'
                                     ORDER BY  DEVICE_NAME ,  NEW_EXPERTISE_ID ", TXTDevice.Text,
                                                                  DDLPlatform.SelectedValue.Replace("*", ""));
        DataTable dt = ado.loadDataTable(str, null, "DEVICE_POOL2_VERSION");
        GVDeviceVersion.DataSource = dt;
        GVDeviceVersion.DataBind();
        dtGv = dt;
        showPage();
    }
    protected void GVDeviceVersion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDeviceVersion.PageIndex = e.NewPageIndex;
        GVDeviceVersion.DataSource = dtGv;
        GVDeviceVersion.DataBind();
        showPage();
    }
    #region PagerTemplate
    protected void lbnFirst_Click(object sender, EventArgs e)
    {
        int num = 0;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        GVDeviceVersion_PageIndexChanging(null, ea);
    }
    protected void lbnPrev_Click(object sender, EventArgs e)
    {
        int num = GVDeviceVersion.PageIndex - 1;

        if (num >= 0)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            GVDeviceVersion_PageIndexChanging(null, ea);
        }
    }
    protected void lbnNext_Click(object sender, EventArgs e)
    {
        int num = GVDeviceVersion.PageIndex + 1;

        if (num < GVDeviceVersion.PageCount)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            GVDeviceVersion_PageIndexChanging(null, ea);
        }
    }
    protected void lbnLast_Click(object sender, EventArgs e)
    {
        int num = GVDeviceVersion.PageCount - 1;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        GVDeviceVersion_PageIndexChanging(null, ea);
    }
    private void showPage()
    {
        try
        {
            TextBox txtPage = (TextBox)GVDeviceVersion.BottomPagerRow.FindControl("txtSizePage");
            Label lblCount = (Label)GVDeviceVersion.BottomPagerRow.FindControl("lblTotalCount");
            Label lblPage = (Label)GVDeviceVersion.BottomPagerRow.FindControl("lblPage");
            Label lblbTotal = (Label)GVDeviceVersion.BottomPagerRow.FindControl("lblTotalPage");

            txtPage.Text = GVDeviceVersion.PageSize.ToString();
            lblCount.Text = dtGv.Rows.Count.ToString();
            lblPage.Text = (GVDeviceVersion.PageIndex + 1).ToString();
            lblbTotal.Text = GVDeviceVersion.PageCount.ToString();
        }
        catch (Exception e) { }
    }
    // page change
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            string numPage = ((TextBox)GVDeviceVersion.BottomPagerRow.FindControl("txtSizePage")).Text.ToString();
            if (!string.IsNullOrEmpty(numPage))
            {
                GVDeviceVersion.PageSize = Convert.ToInt32(numPage);
            }

            TextBox pageNum = ((TextBox)GVDeviceVersion.BottomPagerRow.FindControl("inPageNum"));
            string goPage = pageNum.Text.ToString();
            if (!string.IsNullOrEmpty(goPage))
            {
                int num = Convert.ToInt32(goPage) - 1;
                if (num >= 0)
                {
                    GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
                    GVDeviceVersion_PageIndexChanging(null, ea);
                    ((TextBox)GVDeviceVersion.BottomPagerRow.FindControl("inPageNum")).Text = null;
                }
            }

            GVDeviceVersion.DataSource = dtGv;
            GVDeviceVersion.DataBind();
            showPage();
        }
        catch (Exception ex) { }
    }
    #endregion

}
