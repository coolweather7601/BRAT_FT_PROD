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


public partial class SearchDevice : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();
    public static DataTable dtGv;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucObj.DDLBLBind(DDLBL);
            ucObj.DDLPlatformBind(DDLPlatform);
            ucObj.DDLStageBind(DDLStage);
            ucObj.DDLOwnerBind(DDLOwner);
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
        string str = string.Format(@"SELECT DEVICE_NAME, SUFFIX, STAGE, PLATFORM, BUDGET_YLD, BL, OWNER, ID,  TEMP,  COMMIT_DATE,
                                            NEW_EXPERTISE_ID 
                                     FROM DEVICE_POOL2 
                                     WHERE device_name like '{0}%' 
                                           AND bl like '{1}%' 
                                           AND platform like '{2}%' 
                                           AND stage like '{3}%' 
                                           AND owner like '{4}%'
                                     ORDER BY DEVICE_NAME", TXTDevice.Text.Trim(), DDLBL.SelectedValue.Replace("*", ""),
                                                            DDLPlatform.SelectedValue.Replace("*", ""), DDLStage.SelectedValue.Replace("*", ""),
                                                            DDLOwner.SelectedValue.Replace("*", ""));
        DataTable dt = ado.loadDataTable(str, null, "DEVICE_POOL2");
        GVDeviceLimit.DataSource = dt;
        GVDeviceLimit.DataBind();
        dtGv = dt;
        showPage();
    }
    protected void GVDeviceLimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DateTime startTime = DateTime.Now;
        GVDeviceLimit.PageIndex = e.NewPageIndex;
        GVDeviceLimit.DataSource = dtGv;
        GVDeviceLimit.DataBind();
        showPage();
    }
    protected void GVDeviceLimit_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression.ToString();
        string sortDirection = "ASC";

        if (sortExpression == this.GVDeviceLimit.Attributes["SortExpression"])
            sortDirection = (this.GVDeviceLimit.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");

        this.GVDeviceLimit.Attributes["SortExpression"] = sortExpression;
        this.GVDeviceLimit.Attributes["SortDirection"] = sortDirection;

        if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
        {
            dtGv.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
        }
        GVDeviceLimit.DataSource = dtGv;
        GVDeviceLimit.DataBind();
        showPage();
    }
    #region PagerTemplate
    protected void lbnFirst_Click(object sender, EventArgs e)
    {
        int num = 0;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        GVDeviceLimit_PageIndexChanging(null, ea);
    }
    protected void lbnPrev_Click(object sender, EventArgs e)
    {
        int num = GVDeviceLimit.PageIndex - 1;

        if (num >= 0)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            GVDeviceLimit_PageIndexChanging(null, ea);
        }
    }
    protected void lbnNext_Click(object sender, EventArgs e)
    {
        int num = GVDeviceLimit.PageIndex + 1;

        if (num < GVDeviceLimit.PageCount)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            GVDeviceLimit_PageIndexChanging(null, ea);
        }
    }
    protected void lbnLast_Click(object sender, EventArgs e)
    {
        int num = GVDeviceLimit.PageCount - 1;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        GVDeviceLimit_PageIndexChanging(null, ea);
    }
    private void showPage()
    {
        try
        {
            TextBox txtPage = (TextBox)GVDeviceLimit.BottomPagerRow.FindControl("txtSizePage");
            Label lblCount = (Label)GVDeviceLimit.BottomPagerRow.FindControl("lblTotalCount");
            Label lblPage = (Label)GVDeviceLimit.BottomPagerRow.FindControl("lblPage");
            Label lblbTotal = (Label)GVDeviceLimit.BottomPagerRow.FindControl("lblTotalPage");

            txtPage.Text = GVDeviceLimit.PageSize.ToString();
            lblCount.Text = dtGv.Rows.Count.ToString();
            lblPage.Text = (GVDeviceLimit.PageIndex + 1).ToString();
            lblbTotal.Text = GVDeviceLimit.PageCount.ToString();
        }
        catch (Exception e) { }
    }
    // page change
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            string numPage = ((TextBox)GVDeviceLimit.BottomPagerRow.FindControl("txtSizePage")).Text.ToString();
            if (!string.IsNullOrEmpty(numPage))
            {
                GVDeviceLimit.PageSize = Convert.ToInt32(numPage);
            }

            TextBox pageNum = ((TextBox)GVDeviceLimit.BottomPagerRow.FindControl("inPageNum"));
            string goPage = pageNum.Text.ToString();
            if (!string.IsNullOrEmpty(goPage))
            {
                int num = Convert.ToInt32(goPage) - 1;
                if (num >= 0)
                {
                    GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
                    GVDeviceLimit_PageIndexChanging(null, ea);
                    ((TextBox)GVDeviceLimit.BottomPagerRow.FindControl("inPageNum")).Text = null;
                }
            }

            GVDeviceLimit.DataSource = dtGv;
            GVDeviceLimit.DataBind();
            showPage();
        }
        catch (Exception ex) { }
    }
    #endregion
}
