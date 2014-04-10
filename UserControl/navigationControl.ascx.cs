using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Mobile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class UserControl_navigationControl : System.Web.UI.MobileControls.MobileUserControl
{
    UserInfoClass ucObj = new UserInfoClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucObj.DLPlatformBind(DLPlatform);
        }
    }
    protected void DLPlatform_EditCommand(object source, DataListCommandEventArgs e)
    {
        string platform = "";
        if (DLPlatform.DataKeys[e.Item.ItemIndex].ToString().CompareTo("ADVANTEST") == 0)
        {
            platform = "AGILENT";
        }
        else
        {
            platform = DLPlatform.DataKeys[e.Item.ItemIndex].ToString();
        }
        Response.Redirect("~/User/GetSummary" + platform + ".aspx?Platform=" + platform);
    }
}
