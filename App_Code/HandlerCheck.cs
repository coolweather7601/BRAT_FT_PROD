using System;
using System.Data;
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
/// HandlerCheck 的摘要描述
/// </summary>
public class HandlerCheck
{
    DBClass dbObj = new DBClass();
    public HandlerCheck()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }
    public string CheckHandlerStatus(string Batch, string Status, string StartDate)
    {
        string select_valid = "select COUNT(*) AS cnt from HANDLER_EVT_DETAILS where BATCH_NR='" + Batch + "' and STAGE_F='" + Status + "' and STARTTIME_LOG between TO_DATE('" + StartDate + "','YYYYMMDD.HH24MISS')  -2.5  and  TO_DATE('" + StartDate + "','YYYYMMDD.HH24MISS')  +1.5";
        string select = "select distinct a.batch_nr as batch,a.stage as stage,a.hth_err_code as err_code,a.hth_err_cnt as err_cnt,b.model as model,b.limit as limit,b.mail as mail from handler_evt_hth a, handler_hold_rule b,handler_evt_details c " +
                        "where a.batch_nr='" + Batch + "' and a.stage='" + Status + "' and a.hth_err_code like concat(b.err_code,'%') and a.batch_nr=c.batch_nr and a.stage=c.stage_f and c.model=b.model and a.hth_err_cnt >= b.limit";
        string select_valid2 = "select COUNT(*) AS cnt from  HANDLER_EVT WHERE BATCH_NR='" + Batch + "' and stage_f='" + Status + "' and START_TIME between TO_DATE('" + StartDate + "','YYYYMMDD.HH24MISS')  -2.5  and  TO_DATE('" + StartDate + "','YYYYMMDD.HH24MISS')  +1.5";
        string select_bin3 = "select * from (select SUM(ng_die) AS resort  from  HANDLER_EVT WHERE BATCH_NR='" + Batch + "' and stage_f='" + Status + "' ) ";
        string select_totalDie = "select * from (select SUM(TOTAL_die) AS TOTALDIE  from  HANDLER_EVT WHERE BATCH_NR='" + Batch + "' and stage_f='" + Status + "' ) ";

        string cnt1 = "";
        string cnt2 = "";
        string total_die = "";
        string err_list = "";
        string err_list_resort = "";
        string mail = "";
        OracleConnection myConn1 = dbObj.GetHandlerCkConnect1();
        myConn1.Open();

        OracleCommand myCmd = new OracleCommand(select_valid, myConn1);
        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            cnt1 = dr["cnt"].ToString();
        }
        dr.Close();

        if (System.Convert.ToInt32(cnt1) > 0)
        {
            myCmd = new OracleCommand(select, myConn1);
            dr = myCmd.ExecuteReader();
            while (dr.Read())
            {
                mail = dr["mail"].ToString();
                if (err_list.CompareTo("") == 0)
                {
                    err_list = dr["err_code"].ToString() + ":" + dr["err_cnt"].ToString() + ">=" + dr["limit"].ToString();
                }
                else
                {
                    err_list = err_list + "|" + dr["err_code"].ToString() + ":" + dr["err_cnt"].ToString() + ">=" + dr["limit"].ToString();
                }
            }
            dr.Close();
            if (err_list.CompareTo("") != 0)
            {
                err_list = err_list + "|" + mail + "|APK";
            }

        }
        myConn1.Close();

        OracleConnection myConn2 = dbObj.GetHandlerCkConnect2();
        myConn2.Open();

        myCmd = new OracleCommand(select_valid2, myConn2);
        dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            cnt2 = dr["cnt"].ToString();
        }
        dr.Close();

        myCmd = new OracleCommand(select_totalDie, myConn2);
        dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            total_die = dr["TOTALDIE"].ToString();
        }
        dr.Close();

        if (System.Convert.ToInt32(cnt2) > 0)
        {
            myCmd = new OracleCommand(select_bin3, myConn2);
            dr = myCmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["resort"].ToString().CompareTo("") == 0)
                {
                    continue;
                }

                if (err_list_resort.CompareTo("") == 0)
                {
                    err_list_resort = "Resort_Bin:" + dr["resort"].ToString() + ">0.1% per lot";
                }
                else
                {
                    err_list_resort = err_list_resort + "|Resort_Bin:" + dr["resort"].ToString() + ">0.1% per lot";
                }
            }
            dr.Close();
            if (err_list_resort.CompareTo("") != 0)
            {
                err_list_resort = err_list_resort + "|type Eng|AQK";
            }
        }

        myConn2.Close();

        if ((err_list.CompareTo("") != 0) &&  (err_list_resort.CompareTo("") != 0))
        {
            err_list = "1;" + err_list+";"+err_list_resort;
        }
        if ((err_list.CompareTo("") != 0) &&  (err_list_resort.CompareTo("") == 0))
        {
            err_list = "1;" + err_list;
        }
        if ((err_list.CompareTo("") == 0) &&  (err_list_resort.CompareTo("") != 0))
        {
            err_list = "1;" + err_list_resort;
        }
        if ((err_list.CompareTo("") == 0) &&  (err_list_resort.CompareTo("") == 0))
        {
            err_list = "0";
        }

        return err_list;

    }
}
