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

public partial class ViewLimitVersion : System.Web.UI.Page
{
    UserInfoClass ucObj = new UserInfoClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucObj.DDLTest(DropDownList1);
        string ID = Request.QueryString["ID"];
        string DevVer = Request.QueryString["DevVer"];
        Label1.Text = ID;
        Label2.Text = DevVer;

        SqlLimitBin.SelectCommand = "select BIN, YLD, UTL_RAW.cast_to_nvarchar2(UTL_RAW.CONVERT(UTL_RAW.cast_to_raw(COMMENTS),'TRADITIONAL CHINESE_TAIWAN.UTF8','TRADITIONAL CHINESE_TAIWAN.ZHT16MSWIN950')) as dd , YLD_LEVEL from BIN_EXPERTISE_VERSION where EXPERTISE_ID= '" + DevVer + "' order by YLD_LEVEL,ID DESC";
        SqlLimitItem.SelectCommand = "SELECT ITEM,DSC,QTY,YLD,UTL_RAW.cast_to_nvarchar2(UTL_RAW.CONVERT(UTL_RAW.cast_to_raw(COMMENTS),'TRADITIONAL CHINESE_TAIWAN.UTF8','TRADITIONAL CHINESE_TAIWAN.ZHT16MSWIN950')) as dd,top_check,range_check,tester_check,diff_check FROM FAIL_EXPERTISE_VERSION WHERE EXPERTISE_ID= '" + DevVer + "' order by ID";
        ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["BRAT"];
        OracleConnection conn = new OracleConnection();
        conn.ConnectionString = setting.ConnectionString;
        conn.Open();
        string sqlstr = "SELECT DEVICE_NAME,SUFFIX,TESTER,STAGE,BUDGET_YLD,YIELD1,AMOUNT1,COMMENT1,YIELD2,AMOUNT2,COMMENT2,YIELD3,AMOUNT3,COMMENT3,YIELD4,AMOUNT4,COMMENT4," +
                        "EXPERTISE_ID,FURTHER1,FURTHER2,FURTHER3,FURTHER4,methodology,to_char(COMMIT_DATE,'YYYY-MM-DD') as CMT_DATE FROM DEVICE_POOL_VERSION WHERE ID ='" + ID + "' and EXPERTISE_ID='"+DevVer+"'";
        OracleCommand cmd = new OracleCommand(sqlstr, conn);
        OracleDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            LABDeviceName.Text = rd["DEVICE_NAME"].ToString();
            LABSuffix.Text = rd["SUFFIX"].ToString();
            LABPlatform.Text = rd["TESTER"].ToString();
            LABStage.Text = rd["STAGE"].ToString();
            LABBgtYld.Text = rd["BUDGET_YLD"].ToString();
            LABCommitDate.Text = rd["CMT_DATE"].ToString();
            LABYield1.Text = rd["YIELD1"].ToString();
            LABYield2.Text = rd["YIELD2"].ToString();
            LABYield3.Text = rd["YIELD3"].ToString();
            LABYield4.Text = rd["YIELD4"].ToString();
            LABScrap1.Text = rd["AMOUNT1"].ToString();
            LABScrap2.Text = rd["AMOUNT2"].ToString();
            LABScrap3.Text = rd["AMOUNT3"].ToString();
            LABScrap4.Text = rd["AMOUNT4"].ToString();
            LABAct1.Text = rd["COMMENT1"].ToString();
            LABAct2.Text = rd["COMMENT2"].ToString();
            LABAct3.Text = rd["COMMENT3"].ToString();
            LABAct4.Text = rd["COMMENT4"].ToString();
            LABCondition1.Text = rd["FURTHER1"].ToString();
            LABCondition2.Text = rd["FURTHER2"].ToString();
            LABCondition3.Text = rd["FURTHER3"].ToString();
            LABCondition4.Text = rd["FURTHER4"].ToString();
        }
        rd.Close();
        conn.Close();

    }

    }


    public static string ConvertStrToBig5(string str)
    {

        return Encoding.GetEncoding("utf-8").GetString(Encoding.GetEncoding("us-ascii").GetBytes(str));
    }



    protected void GVLimitItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                //e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString().Replace("//n", "</br>");
                e.Row.Cells[4].Text.Replace("\r", "</b>");
                break;

            case DataControlRowType.Header:
                e.Row.Cells[0].Text = "Fail No.";
                e.Row.Cells[1].Text = "Desc.";
                e.Row.Cells[2].Text = "Oty.";
                e.Row.Cells[3].Text = "Yield";
                e.Row.Cells[4].Text = "Comment & Action";
                e.Row.Cells[5].Text = "Top?";
                e.Row.Cells[6].Text = "Level";
                e.Row.Cells[7].Text = "Tester";
                e.Row.Cells[8].Text = "Diffusion";

                break;
        }

    }
    protected void GVLimitBin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Cells[2].Text.Replace("\r", "</b>");
                break;

            case DataControlRowType.Header:
                e.Row.Cells[0].Text = "Bin No.";
                e.Row.Cells[1].Text = "Yield";
                e.Row.Cells[2].Text = "Comment & Action";
                e.Row.Cells[3].Text = "Dedicated Level</br><font color=\"#FF0000\">for Top Down only</font></font>";
                break;
        }

    }
}
