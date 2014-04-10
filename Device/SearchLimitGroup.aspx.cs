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


public partial class SearchLimitGroup : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();
    public static DataTable dtGv;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Username"] == null)
            {
                //Response.Write("<script>alert('請先登錄後，才能流覽此頁面！');location='javascript:history.go(-1)';</script>");
                //Response.Write("<script>alert('請先登錄後，才能流覽此頁面！'); location.href= ('http://localhost:53969/BRAT_FT_Prod/User/Index.aspx'); </script> ");
                Response.Write("<script>alert('請先登錄後，才能流覽此頁面！'); location.href= ('http://autow2ksvr05/BRAT_FT_Prod/User/Index.aspx'); </script> ");

                return;
            }
            ucObj.DDLPlatformBind(DDLPlatform);
            BTNSearch_Click(null, null);
        }
    }

    protected void gvLimitGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int mem_cnt = 0;
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:

                SqlControl.SelectCommand = "select count(*) from device_pool2 where new_expertise_id = '" + e.Row.Cells[0].Text.ToString() + "'";
                SqlControl.DataSourceMode = SqlDataSourceMode.DataReader;
                OracleDataReader dr = (OracleDataReader)SqlControl.Select(DataSourceSelectArguments.Empty);
                dr.Read();
                mem_cnt = dr.GetInt32(0);
                dr.Close();

                HyperLink linkMem = (HyperLink)e.Row.FindControl("linkMember");
                HyperLink linkEdit = (HyperLink)e.Row.FindControl("linkEdit");
                HyperLink linkView = (HyperLink)e.Row.FindControl("linkView");
                HyperLink linkApply = (HyperLink)e.Row.FindControl("linkApply");
                linkMem.Text = mem_cnt.ToString();
                linkMem.NavigateUrl = "~/User/LimitCheck.aspx?ID=" + e.Row.Cells[0].Text.ToString();
                linkEdit.NavigateUrl = "~/Device/DeviceLimitManagement.aspx?ID=" + e.Row.Cells[0].Text.ToString();
                linkView.NavigateUrl = "~/Device/DeviceLimitView.aspx?ID=" + e.Row.Cells[0].Text.ToString();
                linkApply.NavigateUrl = "~/Device/AssignLimit.aspx?ID=" + e.Row.Cells[0].Text.ToString();
                break;
        }
    }
    protected void gvLimitGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            GridViewRow selectedRow = gvLimitGroup.Rows[index];
            TableCell new_expertise_id = selectedRow.Cells[0];

            SqlControl.UpdateCommand = "update DEVICE_POOL2 set NEW_EXPERTISE_ID=NULL where  new_expertise_id= '" + new_expertise_id.Text + "'";
            SqlControl.Update();

        }
    }
    /// <summary>
    /// Delete limit from Limit Group and update device_pool 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbtnDel_Click(object sender, EventArgs e)
    {

        string event_update = "";
        string DeviceIDlist = "";

        LinkButton DelBtn = sender as LinkButton;
        GridViewRow row = (GridViewRow)DelBtn.NamingContainer;

        TableCell new_expertise_id = row.Cells[0];
        SqlControl.DeleteCommand = "DELETE FROM LIMIT_EXPERTISE where  new_expertise_id= '" + new_expertise_id.Text + "'";
        SqlControl.Delete();
        SqlControl.UpdateCommand = "update DEVICE_POOL2 set NEW_EXPERTISE_ID=NULL where  new_expertise_id= '" + new_expertise_id.Text + "'";
        SqlControl.Update();
        SqlControl.DeleteCommand = "DELETE FROM EXP_GROUP2 where  new_expertise_id= '" + new_expertise_id.Text + "'";
        SqlControl.Delete();
        SqlControl.InsertCommand = "insert into event_log (eventcode, description, owner) values (1, 'Delete Expertise_ID " + new_expertise_id.Text + "' ,'" + Session["EID"] + "' )";
        SqlControl.Insert();

        SqlControl.SelectCommand = "select id from device_pool2 where new_expertise_id = '" + new_expertise_id.Text + "'";
        SqlControl.DataSourceMode = SqlDataSourceMode.DataReader;
        OracleDataReader dr = (OracleDataReader)SqlControl.Select(DataSourceSelectArguments.Empty);
        while (dr.Read())
        {
            DeviceIDlist = DeviceIDlist + dr["id"].ToString() + ",";
        }
        dr.Close();
        SqlControl.InsertCommand = "insert into event_log (eventcode, description, owner) values (9, 'Dismiss Expertise_ID " + new_expertise_id.Text + " from Device_ID " + DeviceIDlist + "' ,'" + Session["EID"] + "' )";
        SqlControl.Insert();

        gvLimitGroup.DataBind();

    }

    //==========================================================
    //2013/8/26 Alan
    //==========================================================
    protected void HoldCode_Click(object sender, EventArgs e)
    {
        string device = TXTDevice.Text.Trim();
        string Platform = DDLPlatform.SelectedValue;
        string whereStr = "";
        whereStr += (string.IsNullOrEmpty(device)) ? "" : string.Format(@" AND device_name like '{0}%'", device);
        whereStr += (string.IsNullOrEmpty(Platform) || Platform.Equals("*")) ? "" : string.Format(@" AND platform like '{0}%'", Platform);

        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);
        string str = string.Format(@"SELECT distinct EG.EXPERTISE_ID, EG.DEVICE_NAME, EG.SUFFIX, EG.STAGE, EG.PLATFORM, EG.COMMIT_DATE, EG.NEW_EXPERTISE_ID,LE.HOLD_CODE
                                     FROM EXP_GROUP2 EG
                                     inner join LIMIT_EXPERTISE LE on EG.NEW_EXPERTISE_ID = LE.NEW_EXPERTISE_ID
                                     WHERE LE.HOLD_CODE is null
                                           AND EG.COMMIT_DATE between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS')
                                           {2}
                                     ORDER BY EG.PLATFORM, EG.DEVICE_NAME, EG.COMMIT_DATE",
                                     DateTime.Now.AddMonths(-12).ToShortDateString(), DateTime.Now.ToShortDateString(), whereStr);


        DataTable dt = new DataTable();
        dt = ado.loadDataTable(str, null, "EXP_GROUP2");
        gvLimitGroup.DataSource = dt;
        gvLimitGroup.DataBind();
        dtGv = dt;
        showPage();

        //========================================================
        //divTotal
        //========================================================
        divTotal.Style.Clear();
        divTotal.Style.Add("display", "inline");
        string totalStr = string.Format(@"SELECT distinct EG.EXPERTISE_ID, EG.DEVICE_NAME, EG.SUFFIX, EG.STAGE, EG.PLATFORM, EG.COMMIT_DATE, EG.NEW_EXPERTISE_ID,LE.HOLD_CODE
                                     FROM EXP_GROUP2 EG
                                     inner join LIMIT_EXPERTISE LE on EG.NEW_EXPERTISE_ID = LE.NEW_EXPERTISE_ID
                                     WHERE EG.COMMIT_DATE between to_date('{0} 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('{1} 23:59:59','yyyy-mm-dd HH24:MI:SS')"
                                    , DateTime.Now.AddMonths(-12).ToShortDateString(), DateTime.Now.ToShortDateString());
        DataTable dtTotal = ado.loadDataTable(totalStr, null, "EXP_GROUP2");
        int NoneHold = dt.Rows.Count, total = dtTotal.Rows.Count;
        lblTotal.Text = total.ToString();
        lblRows.Text = NoneHold.ToString();
        double Percentage = ((double)NoneHold / (double)total);
        lblPercentage.Text = ((Percentage * 100)).ToString("F2");
    }
    protected void BTNSearch_Click(object sender, EventArgs e)
    {
        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);
        string str = string.Format(@"SELECT EXPERTISE_ID, DEVICE_NAME, SUFFIX, STAGE, PLATFORM, COMMIT_DATE, NEW_EXPERTISE_ID 
                                     FROM EXP_GROUP2 
                                     WHERE device_name like '{0}%' 
                                           AND platform like '{1}%' 
                                     ORDER BY PLATFORM, DEVICE_NAME, COMMIT_DATE", TXTDevice.Text.Trim(), DDLPlatform.SelectedValue.Replace("*", ""));
        DataTable dt = ado.loadDataTable(str, null, "EXP_GROUP2");
        gvLimitGroup.DataSource = dt;
        gvLimitGroup.DataBind();
        dtGv = dt;
        divTotal.Style.Add("display", "none");
        showPage();

        
    }
    protected void gvLimitGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLimitGroup.PageIndex = e.NewPageIndex;
        gvLimitGroup.DataSource = dtGv;
        gvLimitGroup.DataBind();
        showPage();
    }
    #region PagerTemplate
    protected void lbnFirst_Click(object sender, EventArgs e)
    {
        int num = 0;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        gvLimitGroup_PageIndexChanging(null, ea);
    }
    protected void lbnPrev_Click(object sender, EventArgs e)
    {
        int num = gvLimitGroup.PageIndex - 1;

        if (num >= 0)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            gvLimitGroup_PageIndexChanging(null, ea);
        }
    }
    protected void lbnNext_Click(object sender, EventArgs e)
    {
        int num = gvLimitGroup.PageIndex + 1;

        if (num < gvLimitGroup.PageCount)
        {
            GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
            gvLimitGroup_PageIndexChanging(null, ea);
        }
    }
    protected void lbnLast_Click(object sender, EventArgs e)
    {
        int num = gvLimitGroup.PageCount - 1;

        GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
        gvLimitGroup_PageIndexChanging(null, ea);
    }
    private void showPage()
    {
        try{
        TextBox txtPage = (TextBox)gvLimitGroup.BottomPagerRow.FindControl("txtSizePage");
        Label lblCount = (Label)gvLimitGroup.BottomPagerRow.FindControl("lblTotalCount");
        Label lblPage = (Label)gvLimitGroup.BottomPagerRow.FindControl("lblPage");
        Label lblbTotal = (Label)gvLimitGroup.BottomPagerRow.FindControl("lblTotalPage");

        txtPage.Text = gvLimitGroup.PageSize.ToString();
        lblCount.Text = dtGv.Rows.Count.ToString();
        lblPage.Text = (gvLimitGroup.PageIndex + 1).ToString();
        lblbTotal.Text = gvLimitGroup.PageCount.ToString();
        }
        catch (Exception e) { }
    }
    // page change
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            string numPage = ((TextBox)gvLimitGroup.BottomPagerRow.FindControl("txtSizePage")).Text.ToString();
            if (!string.IsNullOrEmpty(numPage))
            {
                gvLimitGroup.PageSize = Convert.ToInt32(numPage);
            }

            TextBox pageNum = ((TextBox)gvLimitGroup.BottomPagerRow.FindControl("inPageNum"));
            string goPage = pageNum.Text.ToString();
            if (!string.IsNullOrEmpty(goPage))
            {
                int num = Convert.ToInt32(goPage) - 1;
                if (num >= 0)
                {
                    GridViewPageEventArgs ea = new GridViewPageEventArgs(num);
                    gvLimitGroup_PageIndexChanging(null, ea);
                    ((TextBox)gvLimitGroup.BottomPagerRow.FindControl("inPageNum")).Text = null;
                }
            }

            gvLimitGroup.DataSource = dtGv;
            gvLimitGroup.DataBind();
            showPage();
        }
        catch (Exception ex) { }
    }
    #endregion
}
