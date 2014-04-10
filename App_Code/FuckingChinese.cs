using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.OracleClient;


/// <summary>
/// FuckingChinese 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FuckingChinese : System.Web.Services.WebService {

    DBClass dbObj = new DBClass();

    public FuckingChinese () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {

        OracleConnection myConn1 = dbObj.GetLimitConnect();
        OracleConnection myConn2 = dbObj.GetLimitConnect2();
        myConn1.Open();
        myConn2.Open();

        string sql_expid = "select expertise_id, new_expertise_id from exp_group2 where expertise_id is not null";
        Hashtable expid_list = new Hashtable();

        OracleCommand myCmd1 = new OracleCommand(sql_expid, myConn1);
        OracleDataReader rd1 = myCmd1.ExecuteReader();
        while (rd1.Read())
        {
            if (!expid_list.ContainsKey(rd1["expertise_id"].ToString()))
            {
                expid_list[rd1["expertise_id"].ToString()] = rd1["new_expertise_id"].ToString();
            }
        }
        rd1.Close();

        string sqlstr = "select expertise_id, bin, yld,  UTL_RAW.cast_to_nvarchar2(UTL_RAW.CONVERT(UTL_RAW.cast_to_raw(COMMENTS),'TRADITIONAL CHINESE_TAIWAN.UTF8','TRADITIONAL CHINESE_TAIWAN.ZHT16MSWIN950')) as commen from bin_expertise ";
        string updated_cmt = "";
        string updated_exp_id = "";
        string updated_qty = "";
        string updated_bin = ""; 
        string updated_bin2 = "";

        OracleCommand myCmd2 = new OracleCommand(sqlstr, myConn2);
        OracleCommand myCmd3 = myConn1.CreateCommand();

        OracleDataReader rd2 = myCmd2.ExecuteReader();
        while (rd2.Read())
        {
            //updated_cmt = updated_cmt.Replace("'", " ");
            updated_cmt = Server.UrlDecode(rd2["commen"].ToString().Replace("'", "''"));
            updated_qty = rd2["yld"].ToString();
            updated_bin = rd2["bin"].ToString().Trim();
            updated_bin2 = "";
            string[] bin_array = new string[0];
            if (updated_bin.IndexOf(",") != -1)
            {
                bin_array = updated_bin.Split(',');
            }
            else
            {
                bin_array = updated_bin.Split('+');
            }
            
            foreach (string bin in bin_array)
            {
                updated_bin2 = updated_bin2 +  "B" + bin + "+";
            }
            updated_bin = updated_bin2.Remove(updated_bin2.Length - 1, 1);



            if (expid_list.ContainsKey(rd2["expertise_id"].ToString()))
            {

                updated_exp_id = expid_list[rd2["expertise_id"].ToString()].ToString();
                myCmd3.CommandText = "insert into LIMIT_EXPERTISE (ID, NEW_EXPERTISE_ID, CRITERION, EQUATION, QTY, UNIT, COMMENTS)VALUES (sq_explimit.NEXTVAL,'" + updated_exp_id + "','" + updated_bin + "','>','" + updated_qty + "','%','" +  Server.UrlEncode(updated_cmt) + "')";
                myCmd3.ExecuteNonQuery();
            }
        }
        rd2.Close();
        myConn1.Close();
        myConn2.Close();

        return "finish";
    }
    
}

