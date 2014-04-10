using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

    /// <summary>
    /// ProductQueryService 的摘要描述
    /// </summary>
    /// 
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProductQueryService : System.Web.Services.WebService
    {
        
         [WebMethod]
        public string GetProductQuantity(string batch)
        {
            OracleConnection conn1 = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM"].ConnectionString);
            string YieldQry = "select batch from summary_agilent where rowid = '" + batch + "'";
            OracleCommand cmd1 = new OracleCommand(YieldQry, conn1);
            string unitsInStock = "";
            conn1.Open();
            using (OracleDataReader dr1 = cmd1.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr1.Read())
                    unitsInStock = dr1[0].ToString();
            }

            System.Threading.Thread.Sleep(3000);
            return unitsInStock;
        }

    }

