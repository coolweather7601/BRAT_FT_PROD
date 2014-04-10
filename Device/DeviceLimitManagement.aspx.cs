using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Web.Configuration;
using System.Text;

public partial class Device_DeviceLimitManagement : System.Web.UI.Page
{
    OracleConnection conn;
    string myCmd;
    OracleDataAdapter da;
    DataSet ds;
    UserInfoClass ucObj = new UserInfoClass();
    VesionControl vcObj = new VesionControl();
    string DeviceID;
    string GroupID;

    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {

            if (Session["Username"] == null)
            {
                //Response.Write("<script>alert('請先登錄後，才能流覽此頁面！');location='javascript:history.go(-1)';</script>");
                //Response.Write("<script> location.href= ('http://localhost:53969/BRAT_FT_Prod/User/Index.aspx'); </script> ");
                //Response.Write("<script>alert('請先登錄後，才能流覽此頁面！'); location.href= ('http://localhost:53969/BRAT_FT_Prod/User/Index.aspx'); </script> ");
                Response.Write("<script>alert('請先登錄後，才能流覽此頁面！'); location.href= ('http://autow2ksvr05/BRAT_FT_Prod/User/Index.aspx'); </script> ");
                return;
            }
        }
        //Get Limit ID
        GroupID = Request.QueryString["ID"];
        hiddGroupID.Value = GroupID;
        //Get Device ID (only when Adding new limit)
        DeviceID = Request.QueryString["DeviceID"];
        hiddDeviceID.Value = DeviceID;


        SqlIndication.FilterParameters.Clear();
        SessionParameter EID = new SessionParameter();
        EID.SessionField = "EID";
        EID.DefaultValue = "TEST";
        EID.Name = "EID";
        SqlIndication.FilterParameters.Add(EID);
        SqlIndication.FilterExpression = "Owner = '{0}' ";

        SqlDevicePool2.FilterParameters.Clear();
        Parameter LLID = new Parameter();
        LLID.Name = "LLID";
        if (GroupID.CompareTo("new") == 0)
        {
            LLID.DefaultValue = "0";
        }
        else
        {
            LLID.DefaultValue = GroupID;
        }
        SqlDevicePool2.FilterParameters.Add(LLID);
        SqlDevicePool2.FilterExpression = "NEW_EXPERTISE_ID = '{0}' ";

        //TabContainer1.Width = gvLimit.Width;

    }

    protected void gvTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal) | (e.Row.RowState == DataControlRowState.Alternate))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // To Transfer Code to Detail description
                Label Ind = (Label)e.Row.FindControl("labTempAction");
                Ind.Text = ucObj.IndicationRtn(Ind.Text.ToString());
                Label HCode = (Label)e.Row.FindControl("labTempHoldCode");
                HCode.Text = ucObj.HoldCodeRtn(HCode.Text.ToString());
                Label cmt = (Label)e.Row.FindControl("labTempCmt");
                e.Row.Cells[4].Text = Server.UrlDecode(cmt.Text.ToString());
                e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString().Replace("\r\n", "<br/>");

            }
        }
    }
    protected void gvTemplate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DropDownList ddlIndicate = (DropDownList)(gvTemplate.Rows[index].FindControl("DDLIndication1"));
        DropDownList ddlHodeCode = (DropDownList)(gvTemplate.Rows[index].FindControl("DDLHoldCode1"));
        TextBox txtComment = (TextBox)(gvTemplate.Rows[index].FindControl("txtTempCmt1"));

        SqlIndication.UpdateParameters["INDICATION"].DefaultValue = ddlIndicate.SelectedValue;
        SqlIndication.UpdateParameters["HOLD_CODE"].DefaultValue = ddlHodeCode.SelectedValue;
        SqlIndication.UpdateParameters["COMMENTS"].DefaultValue = txtComment.Text;


    }
    protected void gvTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string newIndicateID = "";
        if ((string)Page.Session["EID"] == "")
        {
            string js = "";
            js += "<script>";
            js += "alert('Please log-in first !!');";
            js += "</script>";
            this.RegisterStartupScript("alarm", js);
        }
        else
        {

            if (e.CommandName.Equals("Insert"))
            {
                newIndicateID = NewIndicateID();

                DropDownList ddlIndicate2 = (DropDownList)(gvTemplate.FooterRow.FindControl("DDLIndication2"));
                DropDownList ddlHodeCode2 = (DropDownList)(gvTemplate.FooterRow.FindControl("DDLHoldCode2"));
                TextBox txtComment2 = (TextBox)(gvTemplate.FooterRow.FindControl("txtTempCmt2"));

                SqlIndication.InsertCommand = "insert into INDICATION_EXPERTISE (INDICATE_ID, INDICATION, HOLD_CODE, COMMENTS, OWNER) values ('" +
                  newIndicateID + "','" + ddlIndicate2.SelectedValue + "','" + ddlHodeCode2.SelectedValue + "','" + Server.UrlEncode(txtComment2.Text) + "','" + Session["EID"] + "')";
                SqlIndication.Insert();

                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (8, 'Add Indication_ID " + newIndicateID + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();

                gvLimit.DataBind();

            }

            if (e.CommandName.Equals("Delete"))
            {

                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int selIndex = row.RowIndex;

                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (7, 'Delete Indication_ID " + this.gvTemplate.Rows[selIndex].Cells[1].Text + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();
            }


            if (e.CommandName.Equals("Update"))
            {

                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int selIndex = row.RowIndex;

                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (6, 'Update Indication_ID " + this.gvTemplate.Rows[selIndex].Cells[1].Text + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();
            }


        }
    }

    protected void txtTempCmt1_DataBinding(object sender, EventArgs e)
    {
        TextBox cmt = (TextBox)sender;
        cmt.Text = Server.UrlDecode(cmt.Text.ToString());
    }
    protected void SqlIndication_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {
        if (e.Command.Parameters["COMMENTS"].Value != null)
        {
            string test = e.Command.Parameters["COMMENTS"].Value.ToString();
            e.Command.Parameters["COMMENTS"].Value = Server.UrlEncode(test);
        }
    }

    protected void gvLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((e.Row.RowState == DataControlRowState.Normal) | (e.Row.RowState == DataControlRowState.Alternate))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // To Transfer Code to Detail description
                Label cmt = (Label)e.Row.FindControl("label4");
                cmt.Text = Server.UrlDecode(cmt.Text.ToString());
                cmt.Text = cmt.Text.ToString().Replace("\r\n", "<br/>");

                Label Ind = (Label)e.Row.FindControl("label1");
                Ind.Text = ucObj.IndicationRtn(Ind.Text.ToString());
                Label HCode = (Label)e.Row.FindControl("label3");
                HCode.Text = ucObj.HoldCodeRtn1(HCode.Text.ToString());
            }
        }

        if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate))
        {
            //Get template Indication list
            DropDownList ddlCreateid = (DropDownList)e.Row.FindControl("ddlCreateId");
            conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString);
            myCmd = "select indicate_id from INDICATION_EXPERTISE where owner = '" + Session["EID"] + "' order by indicate_id";
            conn.Open();
            ds = new DataSet();
            da = new OracleDataAdapter(myCmd, conn);
            da.Fill(ds);
            conn.Close();

            ddlCreateid.DataValueField = "indicate_id";
            ddlCreateid.DataTextField = "indicate_id";
            ddlCreateid.DataSource = ds;
            ddlCreateid.DataBind();
            ddlCreateid.Items.Insert(0, new ListItem("Select Indication", "Select Indication"));

            Label labIndicationEdit = (Label)e.Row.FindControl("labIndicationEdit");
            labIndicationEdit.Text = ucObj.IndicationRtn(labIndicationEdit.Text.ToString());
            Label labHoldCodeEdit = (Label)e.Row.FindControl("labHoldCodeEdit");
            HiddenField HCodeHidn = (HiddenField)e.Row.FindControl("HidnHoldCodeEdit");
            labHoldCodeEdit.Text = ucObj.HoldCodeRtn1(labHoldCodeEdit.Text.ToString());
            TextBox txtCmtEdit = (TextBox)e.Row.FindControl("txtCmtEdit");
            txtCmtEdit.Text = Server.UrlDecode(txtCmtEdit.Text.ToString());

        }

        //Get template Indication list
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlIndicationInst1 = (DropDownList)e.Row.FindControl("ddlIndicationInst");
            conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString);
            myCmd = "select indicate_id from INDICATION_EXPERTISE where owner = '" + Session["EID"] + "' order by indicate_id";
            conn.Open();
            ds = new DataSet();
            da = new OracleDataAdapter(myCmd, conn);
            da.Fill(ds);
            conn.Close();

            ddlIndicationInst1.DataValueField = "indicate_id";
            ddlIndicationInst1.DataTextField = "indicate_id";
            ddlIndicationInst1.DataSource = ds;
            ddlIndicationInst1.DataBind();
            ddlIndicationInst1.Items.Insert(0, new ListItem("Select Indication", "Select Indication"));

            Button OScheck = (Button)e.Row.FindControl("btnOScheck");
            Button UpdateAll = (Button)e.Row.FindControl("btnUpdateAll");
            HiddenField OSflag = (HiddenField)e.Row.FindControl("hiddOScheck");
            Label OSwarning = (Label)e.Row.FindControl("labOSwarning");
            int os_check = 1;
            if (hiddGroupID.Value.ToString().CompareTo("new") == 0)
            {
                OScheck.Enabled = false;
                UpdateAll.Enabled = false;
            }
            else
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString))
                {
                    connection.Open();
                    OracleCommand command = connection.CreateCommand();
                    command.CommandText = "select OS_CHECK from exp_group2 where new_expertise_id = '" + hiddGroupID.Value + "'";
                    os_check = Convert.ToInt32(command.ExecuteScalar());
                    OSflag.Value = os_check.ToString(); ;
                }
            }

            if (os_check == 0)
            {
                OScheck.Text = "Enable O/S check";
                OSwarning.Text = "Default O/S check is disable!!";
            }
            else
            {
                OScheck.Text = "Disable O/S check";
                OSwarning.Text = "";
            }


        }

    }
    protected void ddlCreateId_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddl1 = sender as DropDownList;
        GridViewRow row = (GridViewRow)ddl1.NamingContainer;
        if (!string.IsNullOrEmpty(ddl1.SelectedValue) && (ddl1.SelectedValue.ToString() != "Select Indication"))
        {

            Label LabIndication = row.FindControl("labIndicationEdit") as Label;
            Label LabHoldCode = row.FindControl("labHoldCodeEdit") as Label;
            TextBox TxtCmt = row.FindControl("txtCmtEdit") as TextBox;
            DropDownList ddlHoldCode1 = row.FindControl("DDLHoldCode1") as DropDownList;
            DropDownList ddlIndication1 = row.FindControl("DDLIndication1") as DropDownList;

            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["BRAT_TEST"];
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = setting.ConnectionString;
            conn.Open();
            string sqlstr = "select indication, hold_code, comments from indication_expertise where INDICATE_ID =  '" + ddl1.SelectedValue.ToString() + "'";
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            try
            {
                OracleDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    TxtCmt.Text = Server.UrlDecode(rd["comments"].ToString());
                    ddlHoldCode1.SelectedValue = rd["hold_code"].ToString();
                    ddlIndication1.SelectedValue = rd["indication"].ToString();
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

    }
    protected void gvLimit_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);

        TextBox txtPty1 = (TextBox)(gvLimit.Rows[index].FindControl("txtPiorityEdit"));
        TextBox txtCri1 = (TextBox)(gvLimit.Rows[index].FindControl("txtCriterionEdit"));
        DropDownList ddlEqu1 = (DropDownList)(gvLimit.Rows[index].FindControl("ddlEquListEdit"));
        TextBox txtqty1 = (TextBox)(gvLimit.Rows[index].FindControl("txtQtyEdit"));
        DropDownList ddlUnit1 = (DropDownList)(gvLimit.Rows[index].FindControl("ddlUnitEdit"));
        TextBox txtCri2 = (TextBox)(gvLimit.Rows[index].FindControl("txtCriterionEdit2"));
        DropDownList ddlEqu2 = (DropDownList)(gvLimit.Rows[index].FindControl("ddlEquListEdit2"));
        TextBox txtqty2 = (TextBox)(gvLimit.Rows[index].FindControl("txtQtyEdit2"));
        DropDownList ddlUnit2 = (DropDownList)(gvLimit.Rows[index].FindControl("ddlUnitEdit2"));
        //DropDownList ddlIndicate = (DropDownList)(gvLimit.Rows[index].FindControl("ddlCreateId"));
        DropDownList ddlAction = (DropDownList)(gvLimit.Rows[index].FindControl("DDLIndication1"));
        DropDownList ddlHoldCode = (DropDownList)(gvLimit.Rows[index].FindControl("DDLHoldCode1"));
        TextBox txtComment = (TextBox)(gvLimit.Rows[index].FindControl("txtCmtEdit"));

        SqlDevicePool2.UpdateParameters["PIORITY"].DefaultValue = txtPty1.Text;
        SqlDevicePool2.UpdateParameters["CRITERION"].DefaultValue = txtCri1.Text;
        SqlDevicePool2.UpdateParameters["EQUATION"].DefaultValue = ddlEqu1.SelectedValue;
        SqlDevicePool2.UpdateParameters["QTY"].DefaultValue = txtqty1.Text;
        SqlDevicePool2.UpdateParameters["UNIT"].DefaultValue = ddlUnit1.SelectedValue;
        SqlDevicePool2.UpdateParameters["CRITERION2"].DefaultValue = txtCri2.Text;
        SqlDevicePool2.UpdateParameters["EQUATION2"].DefaultValue = ddlEqu2.SelectedValue;
        SqlDevicePool2.UpdateParameters["QTY2"].DefaultValue = txtqty2.Text;
        SqlDevicePool2.UpdateParameters["UNIT2"].DefaultValue = ddlUnit2.SelectedValue;
        //SqlDevicePool2.UpdateParameters["INDICATE_ID"].DefaultValue = ddlIndicate.SelectedValue;
        SqlDevicePool2.UpdateParameters["HOLD_CODE"].DefaultValue = ddlHoldCode.SelectedValue;
        SqlDevicePool2.UpdateParameters["COMMENTS"].DefaultValue = Server.UrlEncode(txtComment.Text.ToString());

    }
    protected void gvLimit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string newGroupID = "";
        string newLimitID = "";
        StringBuilder update_parameter = new StringBuilder();
        string str_update_parameter = "";
        string event_update = "";
        string indicatIDlist = "";

        TextBox txtPty3 = (TextBox)(gvLimit.FooterRow.FindControl("txtPiorityInst"));
        TextBox txtCri3 = (TextBox)(gvLimit.FooterRow.FindControl("txtCriterionInst"));
        DropDownList ddlEqu3 = (DropDownList)(gvLimit.FooterRow.FindControl("ddlEquListInst"));
        TextBox txtqty3 = (TextBox)(gvLimit.FooterRow.FindControl("txtQtyInst"));
        DropDownList ddlUnit3 = (DropDownList)(gvLimit.FooterRow.FindControl("ddlUnitInst"));
        DropDownList ddlEqu4 = (DropDownList)(gvLimit.FooterRow.FindControl("ddlEquListInst2"));
        TextBox txtqty4 = (TextBox)(gvLimit.FooterRow.FindControl("txtQtyInst2"));
        DropDownList ddlUnit4 = (DropDownList)(gvLimit.FooterRow.FindControl("ddlUnitInst2"));
        //DropDownList ddlIndication3 = (DropDownList)(gvLimit.FooterRow.FindControl("ddlIndicationInst"));
        DropDownList ddlHoldCode2 = (DropDownList)(gvLimit.FooterRow.FindControl("DDLHoldCode2"));
        DropDownList ddlIndication2 = (DropDownList)(gvLimit.FooterRow.FindControl("DDLIndication2"));
        TextBox txtCmt = (TextBox)(gvLimit.FooterRow.FindControl("txtCmtInst"));
        HiddenField OSflag = (HiddenField)(gvLimit.FooterRow.FindControl("hiddOScheck"));



        if (string.IsNullOrEmpty(ddlIndication2.SelectedValue) == false)
        {
            update_parameter.Append("action = '" + Server.UrlEncode(ddlIndication2.SelectedValue) + "' ,");
        }
        if (string.IsNullOrEmpty(ddlHoldCode2.SelectedValue) == false)
        {
            update_parameter.Append("hold_code = '" + Server.UrlEncode(ddlHoldCode2.SelectedValue) + "' ,");
        }

        if (string.IsNullOrEmpty(txtCmt.Text) == false)
        {
            update_parameter.Append("comments = '" + Server.UrlEncode(txtCmt.Text) + "' ,");
        }

        if (e.CommandName.Equals("Delete"))
        {

            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int selIndex = row.RowIndex;

            sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (4, 'Delete Criterion_ID " + this.gvLimit.Rows[selIndex].Cells[1].Text + " of Expertise_ID " + hiddGroupID.Value + "' ,'" + Session["EID"] + "' )";
            sqlEventLog.Insert();
        }


        if (e.CommandName.Equals("Update"))
        {

            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int selIndex = row.RowIndex;

            sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (3, 'Update Criterion_ID " + this.gvLimit.Rows[selIndex].Cells[1].Text + " of Expertise_ID " + hiddGroupID.Value + "' ,'" + Session["EID"] + "' )";
            sqlEventLog.Insert();
        }

        if (e.CommandName.Equals("Insert"))
        {
            newLimitID = NewLimitID();
            if (hiddGroupID.Value.ToString().CompareTo("new") == 0)
            {
                newGroupID = NewGroupID();
                DataSet ds = ucObj.ReturnDeviceInfo("DeviceInfo", hiddDeviceID.Value.Trim());
                SqlControl.InsertCommand = "INSERT INTO EXP_GROUP2(NEW_EXPERTISE_ID,DEVICE_NAME,SUFFIX,PLATFORM,STAGE) VALUES ('" + newGroupID + "', '" + ds.Tables["DeviceInfo"].Rows[0][0].ToString() + "', '" + ds.Tables["DeviceInfo"].Rows[0][1].ToString() + "', '" + ds.Tables["DeviceInfo"].Rows[0][2].ToString() + "', '" + ds.Tables["DeviceInfo"].Rows[0][3].ToString() + "')";
                SqlControl.Insert();

                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (2, 'Add Expertise_ID " + newGroupID + " of Device " + ds.Tables["DeviceInfo"].Rows[0][0].ToString() + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();



                SqlControl.UpdateCommand = "update DEVICE_POOL2 set NEW_EXPERTISE_ID = '" + newGroupID + "' where ID = '" + hiddDeviceID.Value + "'";
                SqlControl.Update();

                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (0, 'Apply Expertise_ID " + newGroupID + " to Device_ID " + hiddDeviceID.Value + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();


                SqlDevicePool2.InsertCommand = "insert into LIMIT_EXPERTISE (ID, NEW_EXPERTISE_ID, PIORITY, CRITERION, EQUATION, QTY, UNIT, EQUATION2, QTY2, UNIT2, ACTION, HOLD_CODE, COMMENTS) values ('" +
                newLimitID + "','" + newGroupID + "','" + txtPty3.Text + "','" + txtCri3.Text + "','" + ddlEqu3.SelectedValue + "','" + txtqty3.Text + "','" + ddlUnit3.SelectedValue + "','" + ddlEqu4.SelectedValue + "','" + txtqty4.Text + "','" + ddlUnit4.SelectedValue + "','" + ddlIndication2.SelectedValue + "','" + ddlHoldCode2.SelectedValue + "','" + Server.UrlEncode(txtCmt.Text) + "')";
                SqlDevicePool2.Insert();

                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (5, 'Add Criterion_ID " + newLimitID + " of Expertise_ID " + newGroupID + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();


                Page.Response.Redirect("/BRAT_FT_Prod/Device/DeviceLimitManagement.aspx?ID=" + newGroupID + "&DeviceID=" + hiddDeviceID.Value, true);
            }
            else
            {

                vcObj.VersionUpdate(hiddGroupID.Value);

                SqlDevicePool2.InsertCommand = "insert into LIMIT_EXPERTISE (ID, NEW_EXPERTISE_ID, PIORITY, CRITERION, EQUATION, QTY, UNIT, EQUATION2, QTY2, UNIT2, ACTION, HOLD_CODE, COMMENTS) values ('" +
                  newLimitID + "','" + hiddGroupID.Value + "','" + txtPty3.Text + "','" + txtCri3.Text + "','" + ddlEqu3.SelectedValue + "','" + txtqty3.Text + "','" + ddlUnit3.SelectedValue + "','" + ddlEqu4.SelectedValue + "','" + txtqty4.Text + "','" + ddlUnit4.SelectedValue + "','" + ddlIndication2.SelectedValue + "','" + ddlHoldCode2.SelectedValue + "','" + Server.UrlEncode(txtCmt.Text) + "')";
                SqlDevicePool2.Insert();

                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (5, 'Add Criterion_ID " + newLimitID + " of Expertise_ID " + hiddGroupID.Value + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();

            }


            gvLimit.DataBind();
            //gvLimit.DataSource = null;
            //gvLimit.DataSourceID = "SqlDevicePool2";
            //Page_Load();
        }
        if (e.CommandName.Equals("UpdateAll"))
        {
            CheckBoxList SelLimit = new CheckBoxList();
            string IndicatList = "";
            string IndicatListLog = "";
            for (int i = 0; i < this.gvLimit.Rows.Count; i++)
            {
                SelLimit = ((CheckBoxList)this.gvLimit.Rows[i].FindControl("cblSelLimit"));
                if (SelLimit.Items[0].Selected)
                {
                    indicatIDlist = indicatIDlist + "'" + this.gvLimit.Rows[i].Cells[1].Text + "',";
                }
            }

            if ((string.IsNullOrEmpty(indicatIDlist) == false) && ((string.IsNullOrEmpty(update_parameter.ToString()) == false)))
            {
                vcObj.VersionUpdate(hiddGroupID.Value);
                IndicatList = indicatIDlist.TrimEnd(',');
                str_update_parameter = update_parameter.ToString().Remove(update_parameter.ToString().Length - 1, 1);
                SqlControl.UpdateCommand = "update LIMIT_EXPERTISE set " + str_update_parameter + " where ID in (" + IndicatList + ")";
                SqlControl.Update();

                IndicatListLog = IndicatList.Replace("'", "");
                event_update = "Update Criterion_ID " + IndicatListLog + " of Expertise_ID " + hiddGroupID.Value;
                sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (3, '" + event_update + "' ,'" + Session["EID"] + "' )";
                sqlEventLog.Insert();
                gvLimit.DataBind();

            }
        }

        if (e.CommandName.Equals("OS_Check"))
        {
            if (OSflag.Value.CompareTo("0") == 0)
            {
                SqlControl.UpdateCommand = "update exp_group2 set os_check = 1 where new_expertise_id = '" + hiddGroupID.Value + "'";
                SqlControl.Update();
                event_update = "Enable default Open/Short check of Expertise_ID " + hiddGroupID.Value;
            }
            else
            {
                SqlControl.UpdateCommand = "update exp_group2 set os_check = 0 where new_expertise_id = '" + hiddGroupID.Value + "'";
                SqlControl.Update();
                event_update = "Disable default Open/Short check of Expertise_ID " + hiddGroupID.Value;
            }

            sqlEventLog.InsertCommand = "insert into event_log (eventcode, description, owner) values (10, '" + event_update + "' ,'" + Session["EID"] + "' )";
            sqlEventLog.Insert();
            gvLimit.DataBind();

        }
    }
    protected void ddlIndicationInst_Init(object sender, EventArgs e)
    {

        DropDownList ddl1 = sender as DropDownList;
        conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString);
        myCmd = "select indicate_id from INDICATION_EXPERTISE where owner = '" + Session["EID"] + "' order by indicate_id";
        conn.Open();
        ds = new DataSet();
        da = new OracleDataAdapter(myCmd, conn);
        da.Fill(ds);
        conn.Close();

        /*
        ddl1.DataValueField = "indicate_id";
        ddl1.DataTextField = "indicate_id";
        ddl1.DataSource = ds;
        ddl1.DataBind();
        ddl1.Items.Insert(0, new ListItem("Select Indication", "Select Indication"));*/

    }
    protected void ddlIndicationInst_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl1 = sender as DropDownList;
        GridViewRow row = (GridViewRow)ddl1.NamingContainer;
        if (!string.IsNullOrEmpty(ddl1.SelectedValue) && (ddl1.SelectedValue.ToString() != "Select Indication"))
        {

            Label LabIndication = row.FindControl("labIndicationInst") as Label;
            Label LabHoldCode = row.FindControl("labHoldCodeInst") as Label;
            TextBox TxtCmt = row.FindControl("txtCmtInst") as TextBox;
            DropDownList ddlHoldCode2 = row.FindControl("DDLHoldCode2") as DropDownList;
            DropDownList ddlIndication2 = row.FindControl("DDLIndication2") as DropDownList;


            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["BRAT_TEST"];
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = setting.ConnectionString;
            conn.Open();
            string sqlstr = "select indication, hold_code, comments from indication_expertise where INDICATE_ID =  '" + ddl1.SelectedValue.ToString() + "'";
            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            try
            {
                OracleDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    TxtCmt.Text = Server.UrlDecode(rd["comments"].ToString());
                    ddlHoldCode2.SelectedValue = rd["hold_code"].ToString();
                    ddlIndication2.SelectedValue = rd["indication"].ToString();
                }
                rd.Close();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

    }
    public static void renderEmptyGridView(GridView EmptyGridView, string FieldNames)
    {
        //將GridView變成只有Header和Footer列，以及被隱藏的空白資料列    
        DataTable dTable = new DataTable();
        char[] delimiterChars = { ',' };
        string[] colName = FieldNames.Split(delimiterChars);
        foreach (string myCol in colName)
        {
            DataColumn dColumn = new DataColumn(myCol.Trim());
            dTable.Columns.Add(dColumn);
        }
        DataRow dRow = dTable.NewRow();
        foreach (string myCol in colName)
        {
            dRow[myCol.Trim()] = DBNull.Value;
        }
        dTable.Rows.Add(dRow);
        EmptyGridView.DataSourceID = null;
        EmptyGridView.DataSource = dTable;
        EmptyGridView.DataBind();
        EmptyGridView.Rows[0].Visible = false;
    }


    protected void gvLimit_PreRender(object sender, EventArgs e)
    {
        if (gvLimit.Rows.Count == 0)
        {
            renderEmptyGridView(gvLimit, "ID,PIORITY,CRITERION,EQUATION,UNIT,QTY,CRITERION2,EQUATION2,UNIT2,QTY2,ACTION,HOLD_CODE,COMMENTS");
        }
    }


    protected void gvTemplate_PreRender(object sender, EventArgs e)
    {
        if (gvTemplate.Rows.Count == 0)
        {
            renderEmptyGridView(gvTemplate, "INDICATE_ID,INDICATION,HOLD_CODE,COMMENTS");
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

    protected string NewLimitID()
    {
        //Application.Clear();
        string newID = "";
        int draft_id = 0;
        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString))
        {
            connection.Open();
            OracleCommand command = connection.CreateCommand();
            command.CommandText = "select sq_explimit.nextval from dual";
            draft_id = Convert.ToInt32(command.ExecuteScalar());
        }
        newID = draft_id.ToString();
        return newID;
    }

    protected string NewIndicateID()
    {
        //Application.Clear();
        string newID = "";
        int draft_id = 0;
        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString))
        {
            connection.Open();
            OracleCommand command = connection.CreateCommand();
            command.CommandText = "select sq_expindicate.nextval from dual";
            draft_id = Convert.ToInt32(command.ExecuteScalar());
        }
        newID = draft_id.ToString();
        return newID;
    }

    protected void gvTemplate_Load(object sender, EventArgs e)
    {
        gvTemplate.DataSource = null;
        gvTemplate.DataSourceID = "SqlIndication";
    }
    protected void SqlDevicePool2_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {

        vcObj.VersionUpdate(hiddGroupID.Value);
        if (e.Command.Parameters["COMMENTS"].Value != null)
        {
            string test = e.Command.Parameters["COMMENTS"].Value.ToString();
            e.Command.Parameters["COMMENTS"].Value = Server.UrlEncode(test);
        }
    }

    // Add blank item to Engineer list
    protected void ddlEngInst_DataBound(object sender, EventArgs e)
    {
        DropDownList ddleng = sender as DropDownList;
        ddleng.Items.Insert(0, new ListItem(String.Empty, String.Empty));
    }

    // Add blank item to Engineer list
    protected void ddlEngEdit_DataBound(object sender, EventArgs e)
    {
        DropDownList ddleng = sender as DropDownList;
        ddleng.Items.Insert(0, new ListItem(String.Empty, String.Empty));
    }

    protected void gvLimit_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].Attributes.Add("rowspan", "2");
                tcHeader[0].Text = "變更";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("rowspan", "2");
                tcHeader[1].Text = "ID";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("rowspan", "2");
                tcHeader[2].Text = "優先";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Attributes.Add("colspan", "4");
                tcHeader[3].Text = "條件1";
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
                tcHeader[7].Text = "註解</th></tr><tr>";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[9].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Text = "單位";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[13].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[14].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[15].Text = "單位";
                break;
        }

    }
    protected void cbSelAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxList SelLimit = new CheckBoxList();
        CheckBox SelAll = (CheckBox)sender;
        if (SelAll.Checked)
        {
            for (int i = 0; i < this.gvLimit.Rows.Count; i++)
            {
                SelLimit = ((CheckBoxList)this.gvLimit.Rows[i].FindControl("cblSelLimit"));
                SelLimit.Items[0].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.gvLimit.Rows.Count; i++)
            {
                SelLimit = ((CheckBoxList)this.gvLimit.Rows[i].FindControl("cblSelLimit"));
                SelLimit.Items[0].Selected = false;
            }
        }
    }

    protected void SqlDevicePool2_Deleting(object sender, SqlDataSourceCommandEventArgs e)
    {
        vcObj.VersionUpdate(hiddGroupID.Value);
    }
}
