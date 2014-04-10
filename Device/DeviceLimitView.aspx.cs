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

public partial class Device_DeviceLimitView : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string ID = Request.QueryString["ID"];
            SqlDevicePool.SelectParameters["NEW_EXPERTISE_ID"].DefaultValue = ID;
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal) | (e.Row.RowState == DataControlRowState.Alternate))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label cmt = (Label)e.Row.FindControl("label3");
                cmt.Text = Server.UrlDecode(cmt.Text.ToString());
                cmt.Text = cmt.Text.ToString().Replace("\r\n", "<br/>");

                Label Ind = (Label)e.Row.FindControl("label1");
                Ind.Text = ucObj.IndicationRtn(Ind.Text.ToString());
                Label HCode = (Label)e.Row.FindControl("label2");
                HCode.Text = ucObj.HoldCodeRtn(HCode.Text.ToString());

            }
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].Attributes.Add("rowspan", "2");
                tcHeader[0].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[0].Text = "ID";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("rowspan", "2");
                tcHeader[1].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[1].Text = "優先";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("colspan", "4");
                tcHeader[2].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[2].Text = "條件1";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Attributes.Add("colspan", "4");
                tcHeader[3].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[3].Text = "條件2";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Attributes.Add("rowspan", "2");
                tcHeader[4].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[4].Text = "指示";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("rowspan", "2");
                tcHeader[5].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[5].Text = "Hold Code";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Attributes.Add("rowspan", "2");
                tcHeader[6].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[6].Text = "註解</th></tr><tr>";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[7].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[8].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[9].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[9].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[10].Text = "單位";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[11].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[12].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[13].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[13].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[14].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[14].Text = "單位";
                break;
        }
    }
}
