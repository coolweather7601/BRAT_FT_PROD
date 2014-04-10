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
using System.Data.SqlClient;

public partial class UserControl_LoadingControl : System.Web.UI.MobileControls.MobileUserControl
{
    DBClass dbObj = new DBClass();
    UserInfoClass ucObj = new UserInfoClass();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["Username"] != null)
            {

                tabLoad.Visible = true;
                tabLoading.Visible = false;
            }
        }

    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        Session["Username"] = null;
        Session["Dept"] = null;
        Session["EMail"] = null;
        if (txtName.Text.Trim() == "" || txtPassword.Text.Trim() == "")
        {
            Response.Write("<script>alert('登錄名和密碼不能為空！');location='javascript:history.go(-1)';</script>");
        }
        else
        {

            int P_Int_IsExists = ucObj.UserExists(txtName.Text.Trim(), txtPassword.Text.Trim());

            if (P_Int_IsExists == 1)
            {
                DataSet ds = ucObj.ReturnUIDs(txtName.Text.Trim(), txtPassword.Text.Trim(), "UserInfo");

                Session["Username"] = ds.Tables["UserInfo"].Rows[0][0].ToString();
                Session["Dept"] = ds.Tables["UserInfo"].Rows[0][1].ToString();
                Session["EMail"] = ds.Tables["UserInfo"].Rows[0][2].ToString();
                Session["EID"] = ds.Tables["UserInfo"].Rows[0][3].ToString();

                Response.Redirect("~/User/index.aspx");

            }
            else
            {
                //Page.RegisterStartupScript("0", "<script>alert('您的登錄有誤，請核對後再重新登錄！');location='javascript:history.go(-1)';</script>");
                Response.Write("<script>alert('您沒有權限登入或是您的登錄有誤，請核對後再重新登錄！');location='javascript:history.go(-1)';</script>");

            }



        }
    }


    protected void LinkBtnLougout_Click(object sender, EventArgs e)
    {
        Session["Username"] = null;
        Session["Dept"] = null;
        Session["EMail"] = null;
        Session["EID"] = null;
        Response.Redirect("~/User/index.aspx");
    }
}

