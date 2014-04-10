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


public partial class SearchDeviceLimit : System.Web.UI.Page
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
        }

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

    protected void GVDeviceLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:

                HyperLink linkOpt = (HyperLink)e.Row.FindControl("linkOption");
                HiddenField hidID = (HiddenField)e.Row.FindControl("hiddenID");
                if (e.Row.Cells[8].Text == "&nbsp;")
                {
                    linkOpt.Text = "Add";
                    linkOpt.NavigateUrl = "~/Device/DeviceLimitManagement.aspx?ID=new&DeviceID=" + hidID.Value;
                }
                else
                {
                    linkOpt.Text = "Edit";
                    linkOpt.NavigateUrl = "~/Device/DeviceLimitManagement.aspx?ID=" + e.Row.Cells[8].Text.ToString() + "&DeviceID=" + hidID.Value;
                }
                break;
        }

    }
    protected void GVDeviceLimit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);
        string str;
        if (e.CommandName == "Dismiss")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            GridViewRow selectedRow = GVDeviceLimit.Rows[index];
            HiddenField hidID = (HiddenField)selectedRow.FindControl("hiddenID");

            str = string.Format(@"update DEVICE_POOL2 set NEW_EXPERTISE_ID = '' where ID = '{0}'", hidID.Value);
            ado.dbNonQuery(str, null);

            str = string.Format(@"insert into event_log (eventcode, description, owner) 
                                  values (9, 'Dismiss Expertise_ID {0} from Device_ID {1}' ,'{2}' )",
                                         this.GVDeviceLimit.Rows[index].Cells[8].Text,
                                         hidID.Value,
                                         Session["EID"]);
            ado.dbNonQuery(str, null);

        }

        if (e.CommandName == "SaveAs")
        {
            string newGroupID = NewGroupID();
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            GridViewRow selectedRow = GVDeviceLimit.Rows[index];
            Label device_name = (Label)selectedRow.FindControl("Label1");
            HiddenField hidID = (HiddenField)selectedRow.FindControl("hiddenID");

            str = string.Format(@"INSERT INTO EXP_GROUP2(NEW_EXPERTISE_ID,DEVICE_NAME,SUFFIX,PLATFORM,STAGE) 
                                  VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')"
                                             , newGroupID,
                                             device_name.Text,
                                             this.GVDeviceLimit.Rows[index].Cells[1].Text,
                                             this.GVDeviceLimit.Rows[index].Cells[3].Text,
                                             this.GVDeviceLimit.Rows[index].Cells[2].Text);
            ado.dbNonQuery(str, null);

            //==========================================================
            str = string.Format(@"update DEVICE_POOL2 
                                  set NEW_EXPERTISE_ID = '{0}' where ID = '{1}'",
                                                            newGroupID,
                                                            hidID.Value);
            ado.dbNonQuery(str, null);

            //==========================================================
            str = string.Format(@"INSERT INTO limit_expertise (ID,NEW_EXPERTISE_ID,PIORITY,COMMIT_DATE,CRITERION,EQUATION,UNIT,QTY,
                                                               CRITERION2,EQUATION2,UNIT2,QTY2,ACTION,COMMENTS,INDICATE_ID,ENGINEER,
                                                               PHONE,HOLD_CODE ) 
                                  SELECT sq_explimit.nextval,NEW_EXPERTISE_ID,PIORITY,COMMIT_DATE,CRITERION,EQUATION,UNIT,QTY,
                                         CRITERION2,EQUATION2,UNIT2,QTY2,ACTION,COMMENTS,INDICATE_ID,ENGINEER,PHONE,HOLD_CODE 
                                  FROM (SELECT '{0}' NEW_EXPERTISE_ID,PIORITY,COMMIT_DATE,CRITERION,EQUATION,UNIT,QTY,CRITERION2,
                                               EQUATION2,UNIT2,QTY2,ACTION,COMMENTS,INDICATE_ID,ENGINEER,PHONE,HOLD_CODE 
                                        FROM limit_expertise 
                                        WHERE NEW_EXPERTISE_ID = {1})",
                                                      newGroupID,
                                                      this.GVDeviceLimit.Rows[index].Cells[8].Text);
            ado.dbNonQuery(str, null);

            //==========================================================
            str = string.Format(@"insert into event_log (eventcode, description, owner) 
                                  values (0, 'Apply Expertise_ID {0} to Device_ID {1}' ,'{2}' )",
                                                                   newGroupID,
                                                                   hidID.Value,
                                                                   Session["EID"]);
            ado.dbNonQuery(str, null);

        }
    }

    protected string NewGroupID()
    {
        //Application.Clear();
        string newID = "";
        int draft_id = 0;
        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString))
        {
            connection.Open();
            OracleCommand command = connection.CreateCommand();
            command.CommandText = "select sq_expgrp2.nextval from dual";
            draft_id = Convert.ToInt32(command.ExecuteScalar());
        }
        newID = draft_id.ToString();
        return newID;
    }
    //==========================================================
    //2013/8/26 Alan
    //==========================================================
    protected void BTNSearch_Click(object sender, EventArgs e)
    {
        string conn = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString;
        Alan.db.AdoDbConn ado = new Alan.db.AdoDbConn(Alan.db.AdoDbConn.AdoDbType.Oracle, conn);
        string str = string.Format(@"SELECT DEVICE_NAME, SUFFIX, STAGE, PLATFORM, BUDGET_YLD, BL, OWNER, ID, NEW_EXPERTISE_ID,
                                            TEMP, COMMIT_DATE 
                                     FROM DEVICE_POOL2 
                                     WHERE device_name like '{0}%' 
                                           AND bl like '{1}%' 
                                           AND platform like '{2}%' 
                                           AND stage like '{3}%' 
                                           AND owner like '{4}%'
                                     ORDER BY DEVICE_NAME", TXTDevice.Text.Trim(),
                                                            DDLBL.SelectedValue.Replace("*", ""),
                                                            DDLPlatform.SelectedValue.Replace("*", ""),
                                                            DDLStage.SelectedValue.Replace("*", ""),
                                                            DDLOwner.SelectedValue.Replace("*", ""));
        DataTable dt = ado.loadDataTable(str, null, "DEVICE_POOL2");
        GVDeviceLimit.DataSource = dt;
        GVDeviceLimit.DataBind();
        dtGv = dt;
        showPage();
    }
    protected void GVDeviceLimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVDeviceLimit.PageIndex = e.NewPageIndex;
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
        try{
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
