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

public partial class Device_DeviceLimitManagement : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string ID = Request.QueryString["ID"];
            SqlDevicePool.SelectParameters["NEW_EXPERTISE_ID"].DefaultValue = ID;
        }
    }


}
