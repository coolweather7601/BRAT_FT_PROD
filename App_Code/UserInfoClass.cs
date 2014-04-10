using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// UserInfoClass 的摘要描述
/// </summary>
public class UserInfoClass
{
    DBClass dbObj = new DBClass();
    public UserInfoClass()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public void DLTeamRoomBind(DataList dlName)
    {
        string P_Str_SqlStr = "select name from follow_teamroom";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "follow_teamroom");
        dlName.DataSource = ds.Tables["follow_teamroom"].DefaultView;
        dlName.DataBind();
    }

    public void DLPlatformBind(DataList dlName)
    {
        string P_Str_SqlStr = "select distinct platform from TESTER_group where platform is not null and platform != 'AGILENT' order by platform";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "platfrom");
        DataTable dt = new DataTable();
        dt = ds.Tables["platfrom"];
        DataRow drNewRow = dt.NewRow();
        drNewRow["platform"] = "ADVANTEST";
        dt.Rows.InsertAt(drNewRow, 1);
        dlName.DataSource = dt.DefaultView;
        //dlName.DataSource = ds.Tables["platfrom"].DefaultView;
        dlName.DataBind();



    }

    public void DDLPlatformBind(DropDownList ddlName)
    {
        string P_Str_SqlStr = "select distinct PLATFORM from TESTER_GROUP where platform is not null and platform != 'AGILENT' order by platform";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "platform");
        ddlName.DataValueField = "platform";        //在此輸入的是資料表的欄位名稱
        ddlName.DataTextField = "platform";      //在此輸入的是資料表的欄位名稱

        ddlName.DataSource = ds.Tables["platform"].DefaultView;
        ddlName.DataBind();
        ddlName.Items.Insert(0, new ListItem("All", "*"));    //Add * as additional item
        ddlName.Items.Insert(2, new ListItem("ADVANTEST(AGILENT)", "AGILENT"));    //Add * as additional item
    }

    public void DDLSpecificPlatformBind(string ID, DropDownList ddlName)
    {
        string P_Str_SqlStr = "SELECT distinct PLATFORM FROM TESTER_GROUP WHERE PLATFORM IN (select PLATFORM from EXP_GROUP2 where new_expertise_id='" + ID + "')";

        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "PLATFORM");
        ddlName.DataValueField = "PLATFORM";        //在此輸入的是資料表的欄位名稱
        ddlName.DataTextField = "PLATFORM";      //在此輸入的是資料表的欄位名稱

        ddlName.DataSource = ds.Tables["PLATFORM"].DefaultView;
        ddlName.DataBind();

    }


    public void DDLBLBind(DropDownList ddlName)
    {
        string P_Str_SqlStr = "SELECT BL FROM BL_GROUP2";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "BL");
        ddlName.DataValueField = "BL";
        ddlName.DataTextField = "BL";
        ddlName.DataSource = ds.Tables["BL"].DefaultView;
        ddlName.DataBind();
        ddlName.Items.Insert(0, new ListItem("*", "*"));
    }

    public void DDLStageBind(DropDownList ddlName)
    {
        string P_Str_SqlStr = "select distinct stage from DEVICE_POOL2";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Stage");
        ddlName.DataValueField = "Stage";
        ddlName.DataTextField = "Stage";
        ddlName.DataSource = ds.Tables["stage"].DefaultView;
        ddlName.DataBind();
        ddlName.Items.Insert(0, new ListItem("*", "*"));
    }

    public void DDLOwnerBind(DropDownList ddlName)
    {
        string P_Str_SqlStr = "select distinct owner from DEVICE_POOL2";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Owner");
        ddlName.DataValueField = "Owner";
        ddlName.DataTextField = "Owner";
        ddlName.DataSource = ds.Tables["owner"].DefaultView;
        ddlName.DataBind();
        ddlName.Items.Insert(0, new ListItem("*", "*"));
    }
    /*
    public void DDLPlatformBind(DropDownList ddlName)
    {
        string P_Str_SqlStr = "SELECT unique platform FROM TESTER_GROUP WHERE PLATFORM is not NULL order by platform";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "platform");
        ddlName.DataValueField = "platform";
        ddlName.DataTextField = "platform";
        ddlName.DataSource = ds.Tables["platform"].DefaultView;
        ddlName.DataBind();
    }
    */
    public void DDLTest(DropDownList ddlName)
    {
        string P_Str_SqlStr = "select UTL_RAW.cast_to_nvarchar2(UTL_RAW.CONVERT(UTL_RAW.cast_to_raw(COMMENTS),'TRADITIONAL CHINESE_TAIWAN.UTF8','TRADITIONAL CHINESE_TAIWAN.ZHT16MSWIN950')) as dd from BIN_EXPERTISE_VERSION where EXPERTISE_ID= '422'";
        OracleConnection myConn = dbObj.GetLimitConnect();
        OracleDataAdapter da = new OracleDataAdapter(P_Str_SqlStr, myConn);
        DataSet ds = new DataSet();
        da.Fill(ds, "test");
        ddlName.DataValueField = "dd";
        ddlName.DataTextField = "dd";
        ddlName.DataSource = ds.Tables["test"].DefaultView;
        ddlName.DataBind();
        da.Dispose();

    }

    public static DataTable GetDataTableFromArray(object[] array)
    {

        DataTable dataTable = new DataTable();
        dataTable.LoadDataRow(array, true);//Pass array object to LoadDataRow method
        return dataTable;
    }

    public String Batchback(string inBatch)
    {
        string itemstr = "";
        ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["INTRACK"];
        OracleConnection conn = new OracleConnection();
        conn.ConnectionString = setting.ConnectionString;
        conn.Open();
        string sqlstr = "Select ed_desc2,diffusion_center from intrack.final_external where batch_nr = '" + inBatch + "'";
        OracleCommand cmd = new OracleCommand(sqlstr, conn);
        try
        {
            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                itemstr = rd["ed_desc2"].ToString() + "|" + rd["diffusion_center"].ToString();
            }
            rd.Close();

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            conn.Close();
        }
        return itemstr;

    }

    public String RtnEngName(string email)
    {
        string itemstr = "";
        OracleConnection myConn = dbObj.GetEmployeeConnect();
        myConn.Open();
        OracleCommand myCmd = new OracleCommand("select emp_name from EMP_NAME_LIST2 where upper(email) = upper('" + email + "')", myConn);

        try
        {
            OracleDataReader rd = myCmd.ExecuteReader();
            while (rd.Read())
            {
                itemstr = rd["emp_name"].ToString();
            }
            rd.Close();

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            myCmd.Dispose();
            myConn.Close();
        }
        return itemstr;

    }

    public int UserExists(string P_Str_Name, string P_Str_Password)
    {
        int P_Int_returnValue = 0;
        OracleConnection myConn = dbObj.GetEmployeeConnect();
        myConn.Open();
        //OracleCommand myCmd = new OracleCommand("select count(emp_no) from emp_name_list2 where emp_no = '" + P_Str_Name + "' and id_no like '%" + P_Str_Password + "'", myConn);
        OracleCommand myCmd = new OracleCommand("select count(emp_no) from emp_name_list2 where " +
                                      "dept_name2 in ('FTF', 'FTF-PACKING', 'WTF', 'IT-EQUIP/ENG', 'IT-EQUIP/SYS','AIR-CENTRAL','AIR-IE','AIR-MANPOWER','AIR-PC','AIR-PE','AIR-PROJECT','AIR-TQM') and emp_no = '" + P_Str_Name + "' and id_no like '%" + P_Str_Password + "'", myConn);


        /*OracleCommand myCmd = new OracleCommand("select count(emp_no) from emp_name_list2 where title_name2 in " +
                              "('ACCOUNTANT A','ACCOUNTANT B','ADV. ENGR.','ASSOC. ENGR.','BUYER A','CHIEF ACCT. A','CHIEF BUYER','CHIEF PLANNER'," +
                               "'CHIEF SPEC.','CONTROLLER','CONTROLLER B','DIRECTOR','ENGINEER','MANAGER','MANAGER A','MGR. B','PLANNER A'," +
                                "'PRINCIPAL ENGINEER','PROJECT MGR. B','PROJECT MGR. A','SR. BUYER','SR. DIRECTOR','SR. ENGINEER B','SR. MANAGER'," +
                                "'SR. PLANNER','SR. PRINCIPAL','SR. PROJECT MGR','SR. SECRETARY','SR. SPEC.','SR.ACCOUNTANT','SR. ENGINEER A','TECH. SUPER.','SR. IMPEX OFFICER') " +
                                "and dept_name2 in ('FTF', 'FTF-PACKING', 'WTF', 'IT-EQUIP/ENG', 'IT-EQUIP/SYS','AIR-CENTRAL','AIR-IE','AIR-MANPOWER','AIR-PC','AIR-PE','AIR-PROJECT','AIR-TQM') and emp_no = '" + P_Str_Name + "' and id_no like '%" + P_Str_Password + "'", myConn);
        */
        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            P_Int_returnValue = dr.GetInt32(0);
        }
        dr.Close();
        myCmd.Dispose();
        myConn.Close();
        return P_Int_returnValue;

    }

    public int RtnEmptyActionCnt(string exp_id)
    {
        int P_Int_returnValue = 0;
        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        OracleCommand myCmd = new OracleCommand("select count(*) from limit_expertise where new_expertise_id = '" + exp_id + "' and action is null", myConn);
        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            P_Int_returnValue = dr.GetInt32(0);
        }
        dr.Close();
        myCmd.Dispose();
        myConn.Close();
        return P_Int_returnValue;
    }


    /// <summary>
    /// 獲取會員資訊
    /// </summary>
    /// <param name="P_Str_Name">會員登錄名</param>
    /// <param name="P_Str_Password">會員登錄密碼</param>
    /// <param name="P_Str_srcTable">查詢表資訊</param>
    /// <returns></returns>
    public DataSet ReturnUIDs(string P_Str_Name, string P_Str_Password, string P_Str_srcTable)
    {
        OracleConnection myConn = dbObj.GetEmployeeConnect();
        myConn.Open();
        // OracleCommand myCmd = new OracleCommand("select EMP_NAME, DEPT_NAME2, EMAIL, EMP_NO from emp_name_list2 where emp_no = '" + P_Str_Name + "' and id_no like '%" + P_Str_Password + "'", myConn);
        OracleCommand myCmd = new OracleCommand("select EMP_NAME, DEPT_NAME2, EMAIL, EMP_NO from emp_name_list2 where " +
                                                "dept_name2 in ('FTF', 'FTF-PACKING', 'WTF', 'IT-EQUIP/ENG', 'IT-EQUIP/SYS','AIR-CENTRAL','AIR-IE','AIR-MANPOWER','AIR-PC','AIR-PE','AIR-PROJECT','AIR-TQM') and emp_no = '" + P_Str_Name + "' and id_no like '%" + P_Str_Password + "'", myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        da.Fill(ds, P_Str_srcTable);
        myConn.Close();
        da.Dispose();
        return ds;

    }

    public String RtnLastStage(string batch_nr, string start_date, string end_date, string platform)
    {
        string stage = "";
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        string cmd = "select test_stage from (select test_stage from summary_" + platform + " where test_code in ('P', 'O') " +
                     " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and data_type='F' and test_stage not like 'AR%' and TESTED_QTY != 0 order by start_date desc) where rownum = 1";

        OracleCommand myCmd = new OracleCommand(cmd, myConn);

        try
        {
            OracleDataReader rd = myCmd.ExecuteReader();
            while (rd.Read())
            {
                stage = rd["test_stage"].ToString();
            }
            rd.Close();

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            myCmd.Dispose();
            myConn.Close();
        }
        return stage;
    }

    public String RtnLastTemp(string batch_nr, string start_date, string end_date, string platform)
    {
        string temp = "";
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        string cmd = "select temperature from (select temperature from summary_" + platform + " where " +
                     " batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and data_type='F' and TESTED_QTY != 0 order by start_date desc) where rownum = 1";

        OracleCommand myCmd = new OracleCommand(cmd, myConn);

        try
        {
            OracleDataReader rd = myCmd.ExecuteReader();
            while (rd.Read())
            {
                temp = rd["temperature"].ToString();
            }
            rd.Close();

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            myCmd.Dispose();
            myConn.Close();
        }
        return temp;
    }

    public String RtnTestStage(string product_12nc, string temp, string platform)
    {
        string stage = "";
        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        string cmd = "select Stage from DEVICE_POOL2,PROD12NC_POOL2 where DEVICE_POOL2.id=PROD12NC_POOL2.id and PROD12NC_POOL2.prod_12nc='" + product_12nc + "' and DEVICE_POOL2.platform = '" + platform + "' and temp = '" + temp + "'";

        OracleCommand myCmd = new OracleCommand(cmd, myConn);

        try
        {
            OracleDataReader rd = myCmd.ExecuteReader();
            while (rd.Read())
            {
                stage = rd["Stage"].ToString();
            }
            rd.Close();

        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            myCmd.Dispose();
            myConn.Close();
        }
        return stage;
    }

    public DataSet RtnSumFail(string P_Str_Name, string strRowid, string platform)
    {
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        OracleCommand myCmd = new OracleCommand("select top1,top2,top3,top4,top5,top6,top7,top8,top9,top10,top11,top12,top13,top14,top15,top16,top17,top18,top19,top20 from  summary_" + platform + " where ROWID = '" + strRowid + "'", myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds, P_Str_Name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
            da.Dispose();
        }
        return ds;
    }

    public DataSet RtnSumBin(string P_Str_Name, string strRowid, string platform)
    {
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        string cmd = "select bin1,bin2,bin3,bin4,bin5,bin6,bin7,bin8,bin9,bin10,bin11,bin12,bin13,bin14,bin15,bin16,bin17,bin18,bin19,bin20," +
  "bin21,bin22,bin23,bin24,bin25,bin26,bin27,bin28,bin29,bin30,bin31,bin32,bin33,bin34,bin35,bin36,bin37,bin38,bin39,bin40," +
"bin41,bin42,bin43,bin44,bin45,bin46,bin47,bin48,bin49,bin50,bin51,bin52,bin53,bin54,bin55,bin56,bin57,bin58,bin59,bin60," +
"bin61,bin62,bin63,bin64,bin65,bin66,bin67,bin68,bin69,bin70,bin71,bin72,bin73,bin74,bin75,bin76,bin77,bin78,bin79,bin80," +
"bin81,bin82,bin83,bin84,bin85,bin86,bin87,bin88,bin89,bin90,bin91,bin92,bin93,bin94,bin95,bin96,bin97,bin98,bin99,bin100," +
"bin101,bin102,bin103,bin104,bin105,bin106,bin107,bin108,bin109,bin110,bin111,bin112,bin113,bin114,bin115,bin116,bin117,bin118,bin119,bin120," +
"bin121,bin122,bin123,bin124,bin125,bin126,bin127,bin128,bin129,bin130,bin131,bin132,bin133,bin134,bin135,bin136,bin137,bin138,bin139,bin140," +
"bin141,bin142,bin143,bin144,bin145,bin146,bin147,bin148,bin149,bin150,bin151,bin152,bin153,bin154,bin155,bin156,bin157,bin158,bin159,bin160," +
"bin161,bin162,bin163,bin164,bin165,bin166,bin167,bin168,bin169,bin170,bin171,bin172,bin173,bin174,bin175,bin176,bin177,bin178,bin179,bin180," +
"bin181,bin182,bin183,bin184,bin185,bin186,bin187,bin188,bin189,bin190,bin191,bin192,bin193,bin194,bin195,bin196,bin197,bin198,bin199,bin200," +
"bin201,bin202,bin203,bin204,bin205,bin206,bin207,bin208,bin209,bin210,bin211,bin212,bin213,bin214,bin215,bin216,bin217,bin218,bin219,bin220," +
"bin221,bin222,bin223,bin224,bin225,bin226,bin227,bin228,bin229,bin230,bin231,bin232,bin233,bin234,bin235,bin236,bin237,bin238,bin239,bin240," +
"bin241,bin242,bin243,bin244,bin245,bin246,bin247,bin248,bin249,bin250,bin251,bin252,bin253,bin254,bin255,bin256 from summary_" + platform + " where ROWID = '" + strRowid + "'";
        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds, P_Str_Name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
            da.Dispose();
        }
        return ds;
    }

    public DataSet RtnAllSumInfo(string P_Str_Name, string batch_nr, string start_date, string end_date, string stage, string platform, int type)
    {
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();

        string cmd = "select top1,top2,top3,top4,top5,top6,top7,top8,top9,top10,top11,top12,top13,top14,top15,top16,top17,top18,top19,top20," +
        "bin1,bin2,bin3,bin4,bin5,bin6,bin7,bin8,bin9,bin10,bin11,bin12,bin13,bin14,bin15,bin16,bin17,bin18,bin19,bin20," +
        "bin21,bin22,bin23,bin24,bin25,bin26,bin27,bin28,bin29,bin30,bin31,bin32,bin33,bin34,bin35,bin36,bin37,bin38,bin39,bin40," +
        "bin41,bin42,bin43,bin44,bin45,bin46,bin47,bin48,bin49,bin50,bin51,bin52,bin53,bin54,bin55,bin56,bin57,bin58,bin59,bin60," +
        "bin61,bin62,bin63,bin64,bin65,bin66,bin67,bin68,bin69,bin70,bin71,bin72,bin73,bin74,bin75,bin76,bin77,bin78,bin79,bin80," +
        "bin81,bin82,bin83,bin84,bin85,bin86,bin87,bin88,bin89,bin90,bin91,bin92,bin93,bin94,bin95,bin96,bin97,bin98,bin99,bin100," +
        "bin101,bin102,bin103,bin104,bin105,bin106,bin107,bin108,bin109,bin110,bin111,bin112,bin113,bin114,bin115,bin116,bin117,bin118,bin119,bin120," +
        "bin121,bin122,bin123,bin124,bin125,bin126,bin127,bin128,bin129,bin130,bin131,bin132,bin133,bin134,bin135,bin136,bin137,bin138,bin139,bin140," +
        "bin141,bin142,bin143,bin144,bin145,bin146,bin147,bin148,bin149,bin150,bin151,bin152,bin153,bin154,bin155,bin156,bin157,bin158,bin159,bin160," +
        "bin161,bin162,bin163,bin164,bin165,bin166,bin167,bin168,bin169,bin170,bin171,bin172,bin173,bin174,bin175,bin176,bin177,bin178,bin179,bin180," +
        "bin181,bin182,bin183,bin184,bin185,bin186,bin187,bin188,bin189,bin190,bin191,bin192,bin193,bin194,bin195,bin196,bin197,bin198,bin199,bin200," +
        "bin201,bin202,bin203,bin204,bin205,bin206,bin207,bin208,bin209,bin210,bin211,bin212,bin213,bin214,bin215,bin216,bin217,bin218,bin219,bin220," +
        "bin221,bin222,bin223,bin224,bin225,bin226,bin227,bin228,bin229,bin230,bin231,bin232,bin233,bin234,bin235,bin236,bin237,bin238,bin239,bin240," +
        "bin241,bin242,bin243,bin244,bin245,bin246,bin247,bin248,bin249,bin250,bin251,bin252,bin253,bin254,bin255,bin256," +
        "rowid, file_location,file_name,df_lotid,product,to_char(start_date,'YYYYmmdd.HH24MISS') start_date,pass_qty,tested_qty,test_code,tester,test_stage,non_retest,pscver ";

        string sub_cmd = "";

        switch (type)
        {
            case 1:
                sub_cmd = "from summary_" + platform + " where substr(test_stage, 3,1) in (select max (substr(test_stage, 3,1)) FROM summary_" + platform + " where batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS')  and test_stage like 'F%' and test_code in ('P', 'O') )" +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage like 'F%' and TESTED_QTY != 0 and test_code in ('P', 'O') order by start_date";

                break;

            case 2:
                sub_cmd = "from summary_" + platform + " where test_code in ('P', 'O') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage like '" + stage[0] + "%" + stage[1] + "' and TESTED_QTY != 0 order by start_date";

                break;

            case 3:
                sub_cmd = "from summary_" + platform + " where test_code in ('P', 'O') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage like '" + stage[0] + "%' and TESTED_QTY != 0 order by start_date";

                break;

            case 4:
                sub_cmd = "from summary_" + platform + " where test_code in ('P', 'O') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage = '" + stage + "' and TESTED_QTY != 0 order by start_date";
                break;

            case 5:
                sub_cmd = "from summary_" + platform + " where test_code in ('P', 'O') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage  not like 'AR%' and TESTED_QTY != 0 order by start_date";
                break;

            case 6:
                sub_cmd = "from summary_" + platform + " where test_code in ('P', 'O') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage  not like 'Q%' and TESTED_QTY != 0 order by start_date";
                break;

            case 7:
                sub_cmd = "from summary_" + platform + " where test_code in ('00', '60', '30') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and temperature = '" + stage + "' and TESTED_QTY != 0 order by start_date";
                break;

        }

        cmd = cmd + sub_cmd;

        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds, P_Str_Name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
            da.Dispose();
        }
        return ds;
    }

    public DataSet RtnAllSumInfo2(string P_Str_Name, string batch_nr, string start_date, string end_date, string stage, string platform)
    {
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();

        string cmd = "select top1,top2,top3,top4,top5,top6,top7,top8,top9,top10,top11,top12,top13,top14,top15,top16,top17,top18,top19,top20," +
        "bin1,bin2,bin3,bin4,bin5,bin6,bin7,bin8,bin9,bin10,bin11,bin12,bin13,bin14,bin15,bin16,bin17,bin18,bin19,bin20," +
        "bin21,bin22,bin23,bin24,bin25,bin26,bin27,bin28,bin29,bin30,bin31,bin32,bin33,bin34,bin35,bin36,bin37,bin38,bin39,bin40," +
        "bin41,bin42,bin43,bin44,bin45,bin46,bin47,bin48,bin49,bin50,bin51,bin52,bin53,bin54,bin55,bin56,bin57,bin58,bin59,bin60," +
        "bin61,bin62,bin63,bin64,bin65,bin66,bin67,bin68,bin69,bin70,bin71,bin72,bin73,bin74,bin75,bin76,bin77,bin78,bin79,bin80," +
        "bin81,bin82,bin83,bin84,bin85,bin86,bin87,bin88,bin89,bin90,bin91,bin92,bin93,bin94,bin95,bin96,bin97,bin98,bin99,bin100," +
        "bin101,bin102,bin103,bin104,bin105,bin106,bin107,bin108,bin109,bin110,bin111,bin112,bin113,bin114,bin115,bin116,bin117,bin118,bin119,bin120," +
        "bin121,bin122,bin123,bin124,bin125,bin126,bin127,bin128,bin129,bin130,bin131,bin132,bin133,bin134,bin135,bin136,bin137,bin138,bin139,bin140," +
        "bin141,bin142,bin143,bin144,bin145,bin146,bin147,bin148,bin149,bin150,bin151,bin152,bin153,bin154,bin155,bin156,bin157,bin158,bin159,bin160," +
        "bin161,bin162,bin163,bin164,bin165,bin166,bin167,bin168,bin169,bin170,bin171,bin172,bin173,bin174,bin175,bin176,bin177,bin178,bin179,bin180," +
        "bin181,bin182,bin183,bin184,bin185,bin186,bin187,bin188,bin189,bin190,bin191,bin192,bin193,bin194,bin195,bin196,bin197,bin198,bin199,bin200," +
        "bin201,bin202,bin203,bin204,bin205,bin206,bin207,bin208,bin209,bin210,bin211,bin212,bin213,bin214,bin215,bin216,bin217,bin218,bin219,bin220," +
        "bin221,bin222,bin223,bin224,bin225,bin226,bin227,bin228,bin229,bin230,bin231,bin232,bin233,bin234,bin235,bin236,bin237,bin238,bin239,bin240," +
        "bin241,bin242,bin243,bin244,bin245,bin246,bin247,bin248,bin249,bin250,bin251,bin252,bin253,bin254,bin255,bin256," +
        "rowid, file_location,file_name,df_lotid,product,to_char(start_date,'YYYYmmdd.HH24MISS') start_date,pass_qty,tested_qty,test_code,tester,test_stage,non_retest,pscver " +
        "from summary_" + platform + " where test_code in ('P', 'O') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage like '" + stage[0] + "%" + stage[1] + "' and TESTED_QTY != 0 order by start_date";


        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds, P_Str_Name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
            da.Dispose();
        }
        return ds;
    }

    public DataSet RtnAllSumInfo3(string P_Str_Name, string batch_nr, string start_date, string end_date, string stage, string platform)
    {
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();

        string cmd = "select top1,top2,top3,top4,top5,top6,top7,top8,top9,top10,top11,top12,top13,top14,top15,top16,top17,top18,top19,top20," +
        "bin1,bin2,bin3,bin4,bin5,bin6,bin7,bin8,bin9,bin10,bin11,bin12,bin13,bin14,bin15,bin16,bin17,bin18,bin19,bin20," +
        "bin21,bin22,bin23,bin24,bin25,bin26,bin27,bin28,bin29,bin30,bin31,bin32,bin33,bin34,bin35,bin36,bin37,bin38,bin39,bin40," +
        "bin41,bin42,bin43,bin44,bin45,bin46,bin47,bin48,bin49,bin50,bin51,bin52,bin53,bin54,bin55,bin56,bin57,bin58,bin59,bin60," +
        "bin61,bin62,bin63,bin64,bin65,bin66,bin67,bin68,bin69,bin70,bin71,bin72,bin73,bin74,bin75,bin76,bin77,bin78,bin79,bin80," +
        "bin81,bin82,bin83,bin84,bin85,bin86,bin87,bin88,bin89,bin90,bin91,bin92,bin93,bin94,bin95,bin96,bin97,bin98,bin99,bin100," +
        "bin101,bin102,bin103,bin104,bin105,bin106,bin107,bin108,bin109,bin110,bin111,bin112,bin113,bin114,bin115,bin116,bin117,bin118,bin119,bin120," +
        "bin121,bin122,bin123,bin124,bin125,bin126,bin127,bin128,bin129,bin130,bin131,bin132,bin133,bin134,bin135,bin136,bin137,bin138,bin139,bin140," +
        "bin141,bin142,bin143,bin144,bin145,bin146,bin147,bin148,bin149,bin150,bin151,bin152,bin153,bin154,bin155,bin156,bin157,bin158,bin159,bin160," +
        "bin161,bin162,bin163,bin164,bin165,bin166,bin167,bin168,bin169,bin170,bin171,bin172,bin173,bin174,bin175,bin176,bin177,bin178,bin179,bin180," +
        "bin181,bin182,bin183,bin184,bin185,bin186,bin187,bin188,bin189,bin190,bin191,bin192,bin193,bin194,bin195,bin196,bin197,bin198,bin199,bin200," +
        "bin201,bin202,bin203,bin204,bin205,bin206,bin207,bin208,bin209,bin210,bin211,bin212,bin213,bin214,bin215,bin216,bin217,bin218,bin219,bin220," +
        "bin221,bin222,bin223,bin224,bin225,bin226,bin227,bin228,bin229,bin230,bin231,bin232,bin233,bin234,bin235,bin236,bin237,bin238,bin239,bin240," +
        "bin241,bin242,bin243,bin244,bin245,bin246,bin247,bin248,bin249,bin250,bin251,bin252,bin253,bin254,bin255,bin256," +
        "rowid, file_location,file_name,df_lotid,product,to_char(start_date,'YYYYmmdd.HH24MISS') start_date,pass_qty,tested_qty,test_code,tester,test_stage,non_retest,pscver " +
        "from summary_" + platform + " where test_code in ('P', 'O') " +
        " and batch = upper('" + batch_nr + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') and test_stage like '" + stage[0] + "%' and TESTED_QTY != 0 order by start_date";


        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds, P_Str_Name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
            da.Dispose();
        }
        return ds;
    }

    public short RtnQAResult(string batch_nr, string start_date, string end_date, string tester)
    {
        short QA_QTY = 0;
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        string cmd = "select sum(pass_qty) from summary_agilent where batch = '" + batch_nr + "' and TEST_STAGE like 'A%' and tester = '" + tester + "' " +
            "and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') ";
        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            QA_QTY = dr.GetInt16(0);
        }
        dr.Close();
        myCmd.Dispose();
        myConn.Close();
        return QA_QTY;

    }


    public string RtnQAResult2(string batch_nr, string start_date, string end_date, string platform, int QA_qty, string tester, string test_stage, int type)
    {

        string platform_condition = "";
        string hold_code = "None";
        switch (platform)
        {
            case "UFLEX":
                platform_condition = "and (TEST_STAGE like 'A%" + test_stage[1] + "' or TEST_STAGE = 'Q22') ";
                break;
            case "AGILENT":
                platform_condition = "and TEST_STAGE like 'A%" + test_stage[1] + "' ";
                break;
            case "SPEA":
                break;
            case "A5XX":
                platform_condition = "and TEST_STAGE like 'A%' ";
                break;
            case "VISTA":
                platform_condition = "and TEST_STAGE like 'A%" + test_stage[1] + "' ";
                break;
            case "TURBO":
                break;

        }

        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        string cmd = "select tested_qty, pass_qty from summary_" + platform + " where batch = '" + batch_nr + "' and tester = '" + tester + "' " +
            "and tested_qty != 0 and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " + platform_condition;
        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataReader dr = myCmd.ExecuteReader();
        int idx = 1;
        int QA_size = 0;
        int last_pass = 0;
        int last_fail = 0;
        int total_retest = 0;
        int first_fail = 0;
        string contact = "david.tsai@nxp.com";
        string mail_body = "";
        while (dr.Read())
        {
            if (idx == 1)
            {
                QA_size = Convert.ToInt32(dr[0].ToString());
                last_pass = Convert.ToInt32(dr[1].ToString());
                last_fail = QA_size - last_pass;
                first_fail = last_fail;
            }
            else
            {
                total_retest += Convert.ToInt32(dr[0].ToString());
                last_pass += Convert.ToInt32(dr[1].ToString());
                last_fail -= Convert.ToInt32(dr[1].ToString());
            }
            idx++;
        }
        dr.Close();
        myCmd.Dispose();
        myConn.Close();

        if (last_pass > (QA_qty + 100))
        {
            hold_code = "AYT";
            return hold_code;
        }
        
        if ((last_fail > 0) || (last_pass < QA_size))
        {
            hold_code = "AQE";
        }

        if (QA_size < QA_qty)
        {
            if (hold_code.CompareTo("None") == 0)
            {
                hold_code = "AQN";
            }
            else
            {
                hold_code = hold_code + ";AQN";
            }
            
        }
        if ((total_retest > 3 * first_fail) && (type == 0))
        {
            mail_body += "<p>Dear QA team </p><br/>";
            mail_body += "<p>You are receiving this email notification because of high retest QA rate.</p>";
            mail_body += "<p>Batch : " + batch_nr + "</p>";
            mail_body += "<p>First QA fail : " + first_fail + "<====> Total QA retest : " + total_retest + "</p>";

            EmailAgent.sendHtmlEmailOut("BRC mail agent", contact, "QA retest rate too hight", mail_body);
        }
        return hold_code;
    }

    public string RtnQAResultTNP(string batch_nr, string start_date, string end_date, string platform, int QA_qty, string tester, string test_stage, int type)
    {

        string platform_condition = "";
        string hold_code = "None";
        switch (platform)
        {
            case "UFLEX":
                platform_condition = "and TEST_STAGE like 'A%" + test_stage[1] + "' ";
                break;
            case "AGILENT":
                platform_condition = "and TEST_STAGE like 'A%" + test_stage[1] + "' ";
                break;
            case "SPEA":
                break;
            case "A5XX":
                platform_condition = "and TEST_STAGE like 'A%" + test_stage[1] + "' ";
                break;
            case "VISTA":
                platform_condition = "and TEST_STAGE like 'A%" + test_stage[1] + "' ";
                break;
            case "TURBO":
                break;

        }

        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        string cmd = "select tested_qty, pass_qty from summary_" + platform + " where batch = '" + batch_nr + "' and tester = '" + tester + "' " +
            "and tested_qty != 0 and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " + platform_condition;
        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataReader dr = myCmd.ExecuteReader();
        int idx = 1;
        int QA_size = 0;
        int last_pass = 0;
        int last_fail = 0;
        int total_retest = 0;
        int first_fail = 0;
        string contact = "david.tsai@nxp.com";
        string mail_body = "";
        while (dr.Read())
        {
            if (idx == 1)
            {
                QA_size = Convert.ToInt32(dr[0].ToString());
                last_pass = Convert.ToInt32(dr[1].ToString());
                last_fail = QA_size - last_pass;
                first_fail = last_fail;
            }
            else
            {
                total_retest += Convert.ToInt32(dr[0].ToString());
                last_pass += Convert.ToInt32(dr[1].ToString());
                last_fail -= Convert.ToInt32(dr[1].ToString());
            }
            idx++;
        }
        dr.Close();
        myCmd.Dispose();
        myConn.Close();

        if ((last_fail > 0) || (last_pass < QA_size))
        {
            hold_code = "AQE";
        }

        if (QA_size < QA_qty)
        {
            if (hold_code.CompareTo("None") == 0)
            {
                hold_code = "AQN";
            }
            else
            {
                hold_code = hold_code + ";AQN";
            }

        }
        if ((total_retest > 3 * first_fail) && (type == 0))
        {
            mail_body += "<p>Dear QA team </p><br/>";
            mail_body += "<p>You are receiving this email notification because of high retest QA rate.</p>";
            mail_body += "<p>Batch : " + batch_nr + "</p>";
            mail_body += "<p>First QA fail : " + first_fail + "<====> Total QA retest : " + total_retest + "</p>";

            EmailAgent.sendHtmlEmailOut("BRC mail agent", contact, "QA retest rate too hight", mail_body);
        }
        return hold_code;
    }



    public DataSet RtnCriterion(string P_Str_Name, string exp_id)
    {
        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        OracleCommand myCmd = new OracleCommand("select PIORITY, CRITERION, EQUATION, QTY, UNIT, INDICATE_ID from limit_expertise where NEW_EXPERTISE_ID = '" + exp_id + "' order by PIORITY", myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds, P_Str_Name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
            da.Dispose();
        }
        return ds;
    }

    public string HoldCodeRtn1(string code)
    {
        string rtnStr = "";
        switch (code)
        {
            case "BDE":
                rtnStr = "BDE";
                break;
            case "BBH":
                rtnStr = "BBH";
                break;
            case "BYM":
                rtnStr = "BYM";
                break;
            case "BDM":
                rtnStr = "BDM";
                break;
            case "BAM":
                rtnStr = "BAM";
                break;
            case "AYA":
                rtnStr = "AYA";
                break;
            case "AYT":
                rtnStr = "AYT";
                break;
            case "AQA":
                rtnStr = "AQA";
                break;
            case "AQE":
                rtnStr = "AQE";
                break;
            case "AFE":
                rtnStr = "AFE";
                break;
            case "AXL":
                rtnStr = "AXL";
                break;
            case "AEF":
                rtnStr = "AEF";
                break;
        }
        return rtnStr;
    }

    public string HoldCodeRtn(string code)
    {
        string rtnStr = "";
        switch (code)
        {
            case "BDE":
                rtnStr = "(BDE)BL Delegate Hold";
                break;
            case "BBH":
                rtnStr = "(BBH)BL Request Hold";
                break;
            case "BYM":
                rtnStr = "(BYM)Maverick Low Yield";
                break;
            case "BDM":
                rtnStr = "(BDM)Maverick Delegation";
                break;
            case "BAM":
                rtnStr = "(BAM)Maverick Assembly";
                break;
            case "AYA":
                rtnStr = "(AYA)Open Short Fails";
                break;
            case "AYT":
                rtnStr = "(AYT)General Low Yield";
                break;
            case "AQA":
                rtnStr = "(AQA)Subcon Low Yield";
                break;
            case "AQE":
                rtnStr = "(AQE)QA Fails";
                break;
            case "AFE":
                rtnStr = "(AFE)Future Hold";
                break;
            case "AXL":
                rtnStr = "(AXL)修腳彎";
                break;
            case "AEF":
                rtnStr = "(AEF)過期或待實驗分析均須報廢";
                break;
        }
        return rtnStr;
    }

    public string IndicationRtn(string indicate)
    {
        string rtnStr = "";
        switch (indicate)
        {
            case "0":
                rtnStr = "扣住";
                break;
            case "1":
                rtnStr = "良出廢留";
                break;
            case "2":
                rtnStr = "良出廢丟";
                break;
            case "3":
                rtnStr = "廢品重測";
                break;
            case "4":
                rtnStr = "通知工程師";
                break;
        }
        return rtnStr;
    }

    public DataSet ReturnDeviceInfo(string P_Str_srcTable, string Str_DeviceID)
    {
        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        OracleCommand myCmd = new OracleCommand("select DEVICE_NAME, SUFFIX, PLATFORM, STAGE from DEVICE_POOL2 where ID = '" + Str_DeviceID + "'", myConn);
        OracleDataAdapter da = new OracleDataAdapter(myCmd);
        DataSet ds = new DataSet();
        da.Fill(ds, P_Str_srcTable);
        myConn.Close();
        da.Dispose();
        return ds;
    }

    public EncResult ReturnEncInfo(string BatchNr)
    {
        EncResult Result = new EncResult();
        OracleConnection myConn = dbObj.GetEncInfoConnect();
        myConn.Open();

        string queryeng = "select ref_no,responsible_dept,b.defect_code||' '||defect_mode2 defect_code,c.status, " +
        "decode(c.status,'Release',rel_date,'Return',return_date,c.last_date) status_date,a.doc_no " +
        "from encuser.ea_request_bc a, encuser.ea_notice b, encuser.ea_request c, encuser.ea_defect d " +
        "where a.doc_no=b.doc_no and a.doc_no=c.doc_no and b.defect_code=d.defect_code and inf_bc_no='" + BatchNr + "'";
        OracleCommand myCmd = new OracleCommand(queryeng, myConn);
        OracleDataReader rd = myCmd.ExecuteReader();
        while (rd.Read())
        {
            if ((string.IsNullOrEmpty(Result.RefNo) == false) && (Result.RefNo.IndexOf(rd["ref_no"].ToString()) == null))
            {
                Result.RefNo = Result.RefNo + ";" + rd["ref_no"].ToString();
            }
            else
            {
                Result.RefNo = rd["ref_no"].ToString();
            }

            if ((string.IsNullOrEmpty(Result.RespDept) == false) && (Result.RespDept.IndexOf(rd["responsible_dept"].ToString()) == null))
            {
                Result.RespDept = Result.RespDept + ";" + rd["responsible_dept"].ToString();
            }
            else
            {
                Result.RespDept = rd["responsible_dept"].ToString();
            }

            if ((string.IsNullOrEmpty(Result.Defect) == false) && (Result.Defect.IndexOf(rd["defect_code"].ToString()) == null))
            {
                Result.Defect = Result.RespDept + ";" + rd["defect_code"].ToString();
            }
            else
            {
                Result.Defect = rd["defect_code"].ToString();
            }

            if ((string.IsNullOrEmpty(Result.DocNo) == false) && (Result.DocNo.IndexOf(rd["doc_no"].ToString()) == null))
            {
                Result.DocNo = Result.RespDept + ";" + rd["doc_no"].ToString();
            }
            else
            {
                Result.DocNo = rd["doc_no"].ToString();
            }

            Result.Status = rd["status"].ToString();
            Result.StatusDate = rd["status_date"].ToString();
        }
        rd.Close();
        myConn.Close();
        return Result;

    }

    public string ReturnTenInfo(string product12nc)
    {
        using (TenInfo.ProductDBService service = new TenInfo.ProductDBService())
        {
            TenInfo.ProductDBProxy proxy = service.getRouting(product12nc, "", "", "");
            if (proxy != null)
            {
                if (proxy.ErrorNumber == 0)
                {
                    foreach (TenInfo.ProcessFlowProxy PFP in proxy.ProcessFlowList)
                    {
                        //if ((PFP.Process_Value != string.Empty) && (PFP.Process_Value.CompareTo("QA1") == 0))
                        //{
                        //    return "F1";
                        //}
                        if ((PFP.Process_Value != string.Empty) && (Regex.IsMatch(PFP.Process_Value, @"^QA\d{1}$")))
                        {
                            return "F1";
                        }
                    }
                }
            }
            return "F2";
        }

    }

    public int CheckSpecialQA(string product12nc)
    {
        using (TenInfo.ProductDBService service = new TenInfo.ProductDBService())
        {
            TenInfo.ProductDBProxy proxy = service.getRouting(product12nc, "", "", "");
            if (proxy != null)
            {
                if (proxy.ErrorNumber == 0)
                {
                    foreach (TenInfo.ProcessFlowProxy PFP in proxy.ProcessFlowList)
                    {
                        if ((PFP.Process_Value != string.Empty) && (Regex.IsMatch(PFP.Process_Value, @"^QA\d{1}$")))
                        {
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }

    }

    public int CheckSpecialQA2(string product12nc, string stage)
    {
        int tmp1 = 0;
        int tmp2 = 0;
        using (TenInfo.ProductDBService service = new TenInfo.ProductDBService())
        {
            TenInfo.ProductDBProxy proxy = service.getRouting(product12nc, "", "", "");
            if (proxy != null)
            {
                if (proxy.ErrorNumber == 0)
                {
                    foreach (TenInfo.ProcessFlowProxy PFP in proxy.ProcessFlowList)
                    {
                        if ((PFP.Process_Value != string.Empty) && (Regex.IsMatch(PFP.Process_Value, @"^QA\d{1}$")))
                        {
                            tmp2 = PFP.SID;
                        }
                        if ((PFP.Process_Value != string.Empty) && (PFP.Process_Value.CompareTo(stage) == 0))
                        {
                            tmp1 = PFP.SID;
                        }
                    }
                }
            }
            if ((tmp2-tmp1)/10 >1)
            {
                return 0;

            }else{
                return 1;
            }
        }

    }

    public int ReturnQAsize(int lot_size)
    {
        int QA_size = 0;
        if (lot_size <= 200)
        {
            QA_size = lot_size;
            return QA_size;
        }

        if (lot_size >= 150000)
        {
            QA_size = 500;
        }
        else
        {
            if (lot_size >= 35000)
            {
                QA_size = 315;
            }
            else
            {
                QA_size = 200;
            }
        }
        return QA_size;
    }


    public int CheckHoldStatus(string Batch, string Stage)
    {
        int P_Int_returnValue = 0;
        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        OracleCommand myCmd = new OracleCommand("select count(status) from action_log where  batch = '" + Batch + "' and stage = '" + Stage + "' and status = '0'", myConn);

        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            P_Int_returnValue = dr.GetInt32(0);
        }
        dr.Close();
        myCmd.Dispose();
        myConn.Close();
        return P_Int_returnValue;

    }

    public string FindActionLog(string Batch, string Stage)
    {
        string Str_returnValue = "";
        OracleConnection myConn = dbObj.GetLimitConnect();
        myConn.Open();
        OracleCommand myCmd = new OracleCommand("select status from (select status from action_log where batch = '" + Batch + "' and  stage = '" + Stage + "' order by commit_date desc) where rownum = 1", myConn);

        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            Str_returnValue = dr["status"].ToString();
        }
        dr.Close();
        myCmd.Dispose();
        myConn.Close();

        if (string.IsNullOrEmpty(Str_returnValue) == true)
        {
            Str_returnValue = "NoLog";
        }
        return Str_returnValue;

    }


}
