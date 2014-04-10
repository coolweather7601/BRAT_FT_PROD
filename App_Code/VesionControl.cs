using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Data.OracleClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// VesionControl 的摘要描述
/// </summary>
public class VesionControl
{
    DBClass dbObj = new DBClass();
    public VesionControl()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
    public void VersionUpdate(string exp_id)
    {
        string VersionID = "";
        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        string cmd = "select VERSION_CONTROL from exp_group2 where NEW_EXPERTISE_ID= '" + exp_id +"'";
        string cmd_update_exp_ver = "UPDATE EXP_GROUP2 set VERSION_CONTROL='1' where NEW_EXPERTISE_ID= '" + exp_id +"'";
        string cmd_select_device = "select id, bl, device_name, suffix, owner, stage, budget_yld, temp, platform from device_pool2 where new_expertise_id = '" + exp_id + "'";
        string cmd_select_limit = "select id, criterion, piority, equation, unit, qty, equation2, unit2, qty2, criterion2, action, comments, engineer, phone, hold_code from limit_expertise where  new_expertise_id = '" + exp_id + "'";

        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleCommand myCmd2 = new OracleCommand(cmd_update_exp_ver, myConn);
        OracleCommand myCmd3 = new OracleCommand(cmd_select_device, myConn);
        OracleCommand myCmd4 = new OracleCommand(cmd_select_limit, myConn);
        OracleCommand myCmd5 = new OracleCommand("insert into device_pool2_version(id, bl, device_name, suffix, owner, stage, budget_yld, temp, platform, new_expertise_id) values (:id, :bl, :device_name, :suffix, :owner, :stage, :budget_yld, :temp, :platform, :exp_id)", myConn);
        OracleCommand myCmd6 = new OracleCommand("insert into limit_expertise_version(id,piority,criterion, equation,unit,qty,criterion2,equation2,unit2,qty2,action,hold_code,comments,engineer,phone,new_expertise_id) values (:id,:piority,:criterion, :equation,:unit,:qty,:criterion2,:equation2,:unit2,:qty2,:action,:hold_code,:comments,:engineer,:phone,:exp_id)", myConn);

        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            if (Convert.ToInt32(dr[0].ToString()) == 0)
            {
                VersionID = LimitVersionID();
                OracleDataReader dr2 = myCmd3.ExecuteReader();
                while (dr2.Read())
                {
                    myCmd5.Parameters.Add(new OracleParameter("id", dr2["id"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("bl", dr2["bl"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("device_name", dr2["device_name"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("suffix", dr2["suffix"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("owner", dr2["owner"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("stage", dr2["stage"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("budget_yld", dr2["budget_yld"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("temp", dr2["temp"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("platform", dr2["platform"].ToString()));
                    myCmd5.Parameters.Add(new OracleParameter("exp_id", VersionID));
                    myCmd5.ExecuteNonQuery();
                }

                OracleDataReader dr3 = myCmd4.ExecuteReader();
                while (dr3.Read())
                {
                    myCmd6.Parameters.Add(new OracleParameter("id", dr3["id"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("piority", dr3["piority"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("criterion", dr3["criterion"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("equation", dr3["equation"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("unit", dr3["unit"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("qty", dr3["qty"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("criterion2", dr3["criterion2"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("equation2", dr3["equation2"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("unit2", dr3["unit2"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("qty2", dr3["qty2"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("action", dr3["action"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("hold_code", dr3["hold_code"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("comments", dr3["comments"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("engineer", dr3["engineer"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("phone", dr3["phone"].ToString()));
                    myCmd6.Parameters.Add(new OracleParameter("exp_id", VersionID));
                    myCmd6.ExecuteNonQuery();
                }
                myCmd2.ExecuteNonQuery();
            }
        }
        dr.Close();
        myCmd2.Dispose();
        myCmd.Dispose();
        myConn.Close();

    }

    protected string LimitVersionID()
    {
        //Application.Clear();
        string newID = "";
        int draft_id = 0;
        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["BRAT_TEST"].ConnectionString))
        {
            connection.Open();
            OracleCommand command = connection.CreateCommand();
            command.CommandText = "select sq_vercon2.nextval from dual";
            draft_id = Convert.ToInt32(command.ExecuteScalar());
        }
        newID = draft_id.ToString();
        return newID;
    }
}
