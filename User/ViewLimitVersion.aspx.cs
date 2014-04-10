using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

public partial class ViewLimit : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        GVLimitItem.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        if (!IsPostBack)
        {
            string ID = Request.QueryString["ID"];
            string LimitIdx = Request.QueryString["DevVer"];

            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["BRAT_TEST"];
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = setting.ConnectionString;
            conn.Open();
            string sqlstr = "SELECT DEVICE_NAME,SUFFIX,PLATFORM,STAGE,BUDGET_YLD,NEW_EXPERTISE_ID FROM DEVICE_POOL2_VERSION WHERE ID = '" + ID + "'";

            OracleCommand cmd = new OracleCommand(sqlstr, conn);
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                LABDeviceName.Text = rd["DEVICE_NAME"].ToString();
                LABSuffix.Text = rd["SUFFIX"].ToString();
                LABPlatform.Text = rd["PLATFORM"].ToString();
                LABStage.Text = rd["STAGE"].ToString();
                LABBgtYld.Text = rd["BUDGET_YLD"].ToString();
            }
            rd.Close();
            conn.Close();
            SqlLimitItem.SelectCommand = "SELECT PIORITY,CRITERION, EQUATION, QTY, UNIT, CRITERION2, EQUATION2, QTY2, UNIT2, ACTION, HOLD_CODE, COMMENTS, ENGINEER, PHONE FROM  LIMIT_EXPERTISE_VERSION WHERE NEW_EXPERTISE_ID = '" + LimitIdx + "' order by PIORITY";
        }

    }

    protected void GVLimitItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:

                e.Row.Cells[11].Text = Server.UrlDecode(e.Row.Cells[11].Text.ToString());
                e.Row.Cells[11].Text = e.Row.Cells[11].Text.ToString().Replace("\r\n", "<br/>");
                e.Row.Cells[12].Text = Server.UrlDecode(e.Row.Cells[12].Text.ToString());
                e.Row.Cells[9].Text = ucObj.IndicationRtn(e.Row.Cells[9].Text.ToString());
                e.Row.Cells[10].Text = ucObj.HoldCodeRtn(e.Row.Cells[10].Text.ToString());

                break;

            case DataControlRowType.Header:
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].Attributes.Add("rowspan", "2");
                tcHeader[0].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[0].Text = "優先";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("colspan", "4");
                tcHeader[1].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[1].Text = "條件1";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("colspan", "4");
                tcHeader[2].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[2].Text = "條件2";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Attributes.Add("rowspan", "2");
                tcHeader[3].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[3].Text = "指示";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Attributes.Add("rowspan", "2");
                tcHeader[4].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[4].Text = "Hold Code";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("rowspan", "2");
                tcHeader[5].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[5].Text = "註解";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Attributes.Add("rowspan", "2");
                tcHeader[6].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[6].Text = "連絡人";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Attributes.Add("rowspan", "2");
                tcHeader[7].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[7].Text = "電話</th></tr><tr>";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[8].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[9].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[9].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[10].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[11].Text = "單位";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[12].Text = "條件";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[13].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[13].Text = "比較";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[14].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[14].Text = "值";
                tcHeader.Add(new TableHeaderCell());
                tcHeader[15].Attributes.Add("bgcolor", "Fuchsia");
                tcHeader[15].Text = "單位";
                break;
        }

    }
}
