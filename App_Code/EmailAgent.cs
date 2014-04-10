using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// EmailAgent 的摘要描述
/// </summary>
public class EmailAgent
{
	public EmailAgent()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}
    public static void sendEmailOut(string sender, string receiver, string subject, string body)
    {
        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["MAIL"].ConnectionString))
        {
            connection.Open();
            OracleCommand command = connection.CreateCommand();
            OracleTransaction transaction;
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;

            try
            {
                command.CommandText = "select seq_mail_id.nextval from dual";
                int mail_id = Convert.ToInt32(command.ExecuteScalar());
                command.CommandText = "insert into mail_pool (id,from_name,disable,datetime_in,send_period,mail_to,mail_cc,mail_subject,datetime_exp,exclusive_flag,check_sum,html_body) " +
                "values (" + mail_id + ",'" + sender + "',0,sysdate,0,'" + receiver + "',null,'" + subject + "',sysdate+1,0,null,0)";
                command.ExecuteNonQuery();

                command.CommandText = "insert into mail_body (id,sn,mail_cont) values (" + mail_id + ",1,'" + body + "')";
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //msg.Text = ex.ToString();
            }
        }
    }

    public static void sendHtmlEmailOut(string sender, string receiver, string subject, string body)
    {
        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["MAIL"].ConnectionString))
        {
            connection.Open();
            OracleCommand command = connection.CreateCommand();
            OracleTransaction transaction;
            transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;

            try
            {
                command.CommandText = "select seq_mail_id.nextval from dual";
                int mail_id = Convert.ToInt32(command.ExecuteScalar());
                command.CommandText = "insert into mail_pool (id,from_name,disable,datetime_in,send_period,mail_to,mail_cc,mail_subject,datetime_exp,exclusive_flag,check_sum,html_body) " +
                "values (" + mail_id + ",'" + sender + "',0,sysdate,0,'" + receiver + "',null,'" + subject + "',sysdate+1,0,null,1)";
                command.ExecuteNonQuery();

                command.CommandText = "insert into mail_body (id,sn,mail_cont) values (" + mail_id + ",1,'" + body + "')";
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //msg.Text = ex.ToString();
            }
        }
    }
}
