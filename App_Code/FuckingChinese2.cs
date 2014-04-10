using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.OracleClient;


/// <summary>
/// FuckingChinese2 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FuckingChinese2 : System.Web.Services.WebService {

    DBClass dbObj = new DBClass();

    public FuckingChinese2 () {

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

        string sqlstr = "select expertise_id, item, qty,yld,  UTL_RAW.cast_to_nvarchar2(UTL_RAW.CONVERT(UTL_RAW.cast_to_raw(COMMENTS),'TRADITIONAL CHINESE_TAIWAN.UTF8','TRADITIONAL CHINESE_TAIWAN.ZHT16MSWIN950')) as commen from fail_expertise  ";
        string updated_cmt = "";
        string updated_exp_id = "";
        string updated_yld = "";
        string updated_qty = "";
        string updated_item = "";
        string updated_item2 = "";

        OracleCommand myCmd2 = new OracleCommand(sqlstr, myConn2);
        OracleCommand myCmd3 = myConn1.CreateCommand();

        OracleDataReader rd2 = myCmd2.ExecuteReader();
        while (rd2.Read())
        {
            //updated_cmt = updated_cmt.Replace("'", " ");
            updated_cmt = Server.UrlDecode(rd2["commen"].ToString().Replace("'", "''"));
            updated_qty = rd2["qty"].ToString();
            updated_yld = rd2["yld"].ToString();
            updated_item = rd2["item"].ToString().Trim();
            updated_item2 = "";

            string[] item_array = new string[0];
            if (updated_item.IndexOf(",") != -1)
            {
                item_array = updated_item.Split(',');
            }
            else
            {
                item_array = updated_item.Split('+');
            }
            foreach (string item in item_array)
            {
                updated_item2 = updated_item2 + "F" + item + "+";
            }
            updated_item = updated_item2.Remove(updated_item2.Length - 1, 1);

            if (expid_list.ContainsKey(rd2["expertise_id"].ToString()))
            {

                updated_exp_id = expid_list[rd2["expertise_id"].ToString()].ToString();

                if ((string.IsNullOrEmpty(updated_qty) == false) && (string.IsNullOrEmpty(updated_yld) == false))
                {
                    myCmd3.CommandText = "insert into LIMIT_EXPERTISE (ID, NEW_EXPERTISE_ID, CRITERION, EQUATION, QTY, UNIT, CRITERION2, EQUATION2, QTY2, UNIT2, COMMENTS)VALUES (sq_explimit.NEXTVAL,'" + updated_exp_id + "','" + updated_item + "','>','" + updated_qty + "','pcs','"  + updated_item + "','>','" + updated_yld + "','%','"  + Server.UrlEncode(updated_cmt) + "')";
                    myCmd3.ExecuteNonQuery();
                }
                else
                {
                    if (string.IsNullOrEmpty(updated_qty) == false)
                    {
                        myCmd3.CommandText = "insert into LIMIT_EXPERTISE (ID, NEW_EXPERTISE_ID, CRITERION, EQUATION, QTY, UNIT, COMMENTS)VALUES (sq_explimit.NEXTVAL,'" + updated_exp_id + "','" + updated_item + "','>','" + updated_qty + "','pcs','" + Server.UrlEncode(updated_cmt) + "')";
                        myCmd3.ExecuteNonQuery();
                    }

                    if (string.IsNullOrEmpty(updated_yld) == false)
                    {
                        myCmd3.CommandText = "insert into LIMIT_EXPERTISE (ID, NEW_EXPERTISE_ID, CRITERION, EQUATION, QTY, UNIT, COMMENTS)VALUES (sq_explimit.NEXTVAL,'" + updated_exp_id + "','" + updated_item + "','>','" + updated_yld + "','%','" + Server.UrlEncode(updated_cmt) + "')";
                        myCmd3.ExecuteNonQuery();
                    }
                }

            }
        }
        rd2.Close();
        myConn1.Close();
        myConn2.Close();

        return "finish";
    }
    
}

