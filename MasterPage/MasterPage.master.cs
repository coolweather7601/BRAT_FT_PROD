using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //labDate.Text = DateTime.Now.ToLongDateString();
    }
    protected void lnkbtnShopCart_Click(object sender, EventArgs e)
    {
        if (Session["UID"] == null)
        {
            Response.Write("<script>alert('您還沒有登錄，請先登錄！')</script>");

        }
        else
        {
            Response.Redirect("CommitGoods.aspx");
        }
    }
    protected void lbtnALogin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Write("<script language=javascript>window.open('../BatchSearch.aspx','','width=455,height=255')</script>");
    }
}
