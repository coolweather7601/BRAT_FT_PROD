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


public partial class Device_AssignLimit : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();
    GeneralWS gwsObj = new GeneralWS();
    string ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ID = Request.QueryString["ID"];
            HiddenField1.Value = ID;
            ucObj.DDLBLBind(DDLBL);
            ucObj.DDLSpecificPlatformBind(ID, DDLPlatform);
            ucObj.DDLStageBind(DDLStage);
            ucObj.DDLOwnerBind(DDLOwner);


            SqlDeviceLimit.FilterParameters.Clear();


            ControlParameter DeviceName = new ControlParameter();
            DeviceName.ControlID = "TXTDevice";
            DeviceName.DefaultValue = "%";
            DeviceName.Name = "device_name";
            DeviceName.PropertyName = "Text";
            SqlDeviceLimit.FilterParameters.Add(DeviceName);

            ControlParameter BL = new ControlParameter();
            BL.ControlID = "DDLBL";
            BL.DefaultValue = "%";
            BL.Name = "bl";
            SqlDeviceLimit.FilterParameters.Add(BL);

            ControlParameter Platform = new ControlParameter();
            Platform.ControlID = "DDLPlatform";
            Platform.DefaultValue = "%";
            Platform.Name = "platform";
            SqlDeviceLimit.FilterParameters.Add(Platform);

            ControlParameter Stage = new ControlParameter();
            Stage.ControlID = "DDLStage";
            Stage.DefaultValue = "%";
            Stage.Name = "stage";
            SqlDeviceLimit.FilterParameters.Add(Stage);

            ControlParameter Owner = new ControlParameter();
            Owner.ControlID = "DDLOwner";
            Owner.DefaultValue = "%";
            Owner.Name = "owner";
            SqlDeviceLimit.FilterParameters.Add(Owner);

            SqlDeviceLimit.FilterExpression = "device_name like '{0}%' and bl like '{1}%' and platform = '{2}%' and stage like '{3}%' and owner like '{4}%' ";

        }
        
        /*
        else
        {
            SqlDeviceLimit.FilterParameters.Clear();
            ControlParameter DeviceName = new ControlParameter();
            DeviceName.ControlID = "TXTDevice";
            DeviceName.DefaultValue = "%";
            DeviceName.Name = "device_name";
            DeviceName.PropertyName = "Text";
            SqlDeviceLimit.FilterParameters.Add(DeviceName);
            ControlParameter BL = new ControlParameter();
            BL.ControlID = "DDLBL";
            BL.DefaultValue = "%";
            BL.Name = "bl";
            SqlDeviceLimit.FilterParameters.Add(BL);

            ControlParameter Tester = new ControlParameter();
            Tester.ControlID = "DDLTester";
            Tester.DefaultValue = "%";
            Tester.Name = "tester";
            SqlDeviceLimit.FilterParameters.Add(Tester);

            ControlParameter Stage = new ControlParameter();
            Stage.ControlID = "DDLStage";
            Stage.DefaultValue = "%";
            Stage.Name = "stage";
            SqlDeviceLimit.FilterParameters.Add(Stage);

            ControlParameter Owner = new ControlParameter();
            Owner.ControlID = "DDLOwner";
            Owner.DefaultValue = "%";
            Owner.Name = "owner";
            SqlDeviceLimit.FilterParameters.Add(Owner);

            // SqlDeviceLimit.FilterExpression = "device_name like '{0}%' and bl like '{1}%' and tester like '{2}%'";
            SqlDeviceLimit.FilterExpression = "device_name like '{0}%' and bl like '{1}%' and tester like '{2}%' and stage like '{3}%' and owner like '{4}%' ";
        }*/
    }
    protected void BTNSearch_Click(object sender, EventArgs e)
    {
        SqlDeviceLimit.FilterParameters.Clear();
        ControlParameter DeviceName = new ControlParameter();
        DeviceName.ControlID = "TXTDevice";
        DeviceName.DefaultValue = "%";
        DeviceName.Name = "device_name";
        DeviceName.PropertyName = "Text";
        SqlDeviceLimit.FilterParameters.Add(DeviceName);
        ControlParameter BL = new ControlParameter();
        BL.ControlID = "DDLBL";
        BL.DefaultValue = "%";
        BL.Name = "bl";
        SqlDeviceLimit.FilterParameters.Add(BL);

        ControlParameter Platform = new ControlParameter();
        Platform.ControlID = "DDLPlatform";
        Platform.DefaultValue = "%";
        Platform.Name = "platform";
        SqlDeviceLimit.FilterParameters.Add(Platform);

        ControlParameter Stage = new ControlParameter();
        Stage.ControlID = "DDLStage";
        Stage.DefaultValue = "%";
        Stage.Name = "stage";
        SqlDeviceLimit.FilterParameters.Add(Stage);

        ControlParameter Owner = new ControlParameter();
        Owner.ControlID = "DDLOwner";
        Owner.DefaultValue = "%";
        Owner.Name = "owner";
        SqlDeviceLimit.FilterParameters.Add(Owner);

        SqlDeviceLimit.FilterExpression = "device_name like '{0}%' and bl like '{1}%' and platform like '{2}%' and stage like '{3}%' and owner like '{4}%' ";
    }

    protected void cbSelAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxList SelDevice = new CheckBoxList();
        if (cbSelAll.Checked)
        {
            for (int i = 0; i < this.GVDeviceLimit.Rows.Count; i++)
            {
                SelDevice = ((CheckBoxList)this.GVDeviceLimit.Rows[i].FindControl("cblSelDevice"));
                SelDevice.Items[0].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < this.GVDeviceLimit.Rows.Count; i++)
            {
                SelDevice = ((CheckBoxList)this.GVDeviceLimit.Rows[i].FindControl("cblSelDevice"));
                SelDevice.Items[0].Selected = false;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string event_update = "";
        string DeviceIDlist = "";
        string DeviceList = "";
        string DeviceListLog = "";

        CheckBoxList SelDevice = new CheckBoxList();
        for (int i = 0; i < this.GVDeviceLimit.Rows.Count; i++)
        {
            SelDevice = ((CheckBoxList)this.GVDeviceLimit.Rows[i].FindControl("cblSelDevice"));
            if (SelDevice.Items[0].Selected)
            {
                DeviceIDlist = DeviceIDlist + "'" + this.GVDeviceLimit.Rows[i].Cells[1].Text + "',";
            }
        }
        if (string.IsNullOrEmpty(DeviceIDlist) == false)
        {
            DeviceList = DeviceIDlist.TrimEnd(',');
            SqlDeviceLimit.UpdateCommand = "update device_pool2 set new_expertise_id = '" + HiddenField1.Value + "' where ID in ( " + DeviceList + " )";
            SqlDeviceLimit.Update();

            DeviceListLog = DeviceList.Replace("'", "");
            event_update = "Apply Expertise_ID " + HiddenField1.Value + " to Device_ID " + DeviceListLog;
            SqlDeviceLimit.InsertCommand = "insert into event_log (eventcode, description, owner) values (0, '" + event_update + "' ,'" + Session["EID"] + "' )";
            SqlDeviceLimit.Insert();
        }

    }
}
