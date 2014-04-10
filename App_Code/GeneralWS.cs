using System;
using System.Collections;
using System.Configuration;
using System.Data.OracleClient;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Services.Protocols;


/// <summary>
/// GeneralWS 的摘要描述
/// </summary>
[WebService(Namespace = "http://autow2ksvr05/BRAT_FT_Prod/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class GeneralWS : System.Web.Services.WebService {

    public GeneralWS () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public static string GetFailInfo2(string contextKey)
    {
        string RtnInfo = "";
        string Cmd_12nc = "SELECT bin1, bin12 FROM summary_agilent WHERE rowid ='00'";
        OracleConnection conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM"].ConnectionString);
        conn.Open();
        OracleCommand cmd = new OracleCommand(Cmd_12nc, conn);
        OracleDataReader dr1 = cmd.ExecuteReader();
        while (dr1.Read())
        {
            RtnInfo = RtnInfo + "<tr><td>" + dr1["bin1"].ToString() + "</td><td>" + dr1["bin12"].ToString() + "</td></tr>";
        }
        RtnInfo = "<table border=1 ><tr ><td><strong>TOP1</strong></td><td><strong>TOP2</strong></td></tr>" + RtnInfo + "</table>";
        dr1.Close();
        conn.Close();

        return RtnInfo;
    }
    
}

