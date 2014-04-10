using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// DBClass 的摘要描述
/// </summary>
public class DBClass
{
    public DBClass()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }
    public OracleConnection GetSumConnect()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM"].ConnectionString);
        return myConn;
    }
    public OracleConnection GetLimitConnect()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString);
        return myConn;
    }

    public OracleConnection GetLimitConnect2()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BRAT"].ConnectionString);
        return myConn;
    }

    public OracleConnection GetIntrackConnect()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INTRACK"].ConnectionString);
        return myConn;
    }
    public OracleConnection GetEmployeeConnect()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Employee"].ConnectionString);
        return myConn;
    }
    public OracleConnection GetSumTestConnect()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SUM_TEST"].ConnectionString);
        return myConn;
    }
    public OracleConnection GetTENinfoConnect()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TEN_INFO"].ConnectionString);
        return myConn;
    }
    public OracleConnection GetHandlerCkConnect1()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PARAMDB"].ConnectionString);
        return myConn;
    }
    public OracleConnection GetHandlerCkConnect2()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["MMAPS"].ConnectionString);
        return myConn;
    }
    public OracleConnection GetEncInfoConnect()
    {
        OracleConnection myConn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ENC_INFO"].ConnectionString);
        return myConn;
    }
}
