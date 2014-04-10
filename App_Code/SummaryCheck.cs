using System;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/// <summary>
/// SummaryCheck 的摘要描述
/// </summary>
public class SummaryCheck
{
    DBClass dbObj = new DBClass();
    public SummaryCheck()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }


    /// <summary>
    /// Collect Non-retest bin base on softbin (for Agilent)
    /// </summary>
    /// <param name="batch_no"></param>
    /// <param name="rowid"></param>
    /// <param name="start_date"></param>
    /// <param name="end_date"></param>
    /// <param name="stage"></param>
    /// <param name="platform"></param>
    /// <returns></returns>
    public Dictionary<string, ArrayList> NonRetestBinCollect(string batch_no, string rowid, string start_date, string end_date, string stage, string platform)
    {
        int RetestIndex = 0;
        int NR_reset = 0;
        int bin11 = 0;
        string NR_bin_list = "";
        Hashtable NonRtBinHash = new Hashtable();
        // Dictionary<string, List<string>> NonRtBinDict = new Dictionary<string, List<string>>();
        Dictionary<string, ArrayList> NonRtBinDict = new Dictionary<string, ArrayList>();
        OracleConnection myConn = dbObj.GetSumConnect();
        myConn.Open();
        String query_str = "select top1,top2,top3,top4,top5,top6,top7,top8,top9,top10,top11,top12,top13,top14,top15,top16,top17,top18,top19,top20,non_retest, bin11 " +
                    "FROM summary_" + platform + " where batch = upper('" + batch_no + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " +
                    " and test_stage = '" + stage + "' and rowid != '" + rowid + "' order by start_date";
        OracleCommand myCmd = new OracleCommand(query_str, myConn);
        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {
            //Collecting Bin11 information
            if (dr[21].ToString().CompareTo("") != 0)
            {
                bin11 = Convert.ToInt32(dr[21].ToString());

                if (!NonRtBinDict.ContainsKey("bin11"))
                {
                    NonRtBinDict.Add("bin11", new ArrayList());
                    NonRtBinDict["bin11"].Add(bin11);
                    NonRtBinDict["bin11"].Add("");

                }
                else
                {
                    NonRtBinDict["bin11"][0] = Convert.ToInt32(NonRtBinDict["bin11"][0]) + bin11;
                }
            }

            NR_bin_list = dr[20].ToString();

            if ((NR_bin_list.CompareTo("") == 0) && (platform.CompareTo("AGILENT") == 0))
            {
                NR_bin_list = "R:all";
            }

            if (NR_bin_list[0] == 'R')
            {
                RetestIndex = 1;
            }

            if (NR_bin_list.CompareTo("") != 0)
            {
                if (NR_reset == 0)
                {
                    NRtBinParse(NR_bin_list, NonRtBinHash);
                    NR_reset = 1;
                }

                for (int i = 0; i <= 19; i++)
                {
                    if (dr[i].ToString().CompareTo("") != 0)
                    {
                        string[] binarray = dr[i].ToString().Split(';');

                        if (RetestIndex == 1)
                        {
                            if (dr[20].ToString().IndexOf("all") == -1)
                            {
                                if (!NonRtBinHash.ContainsKey(binarray[0]))
                                {
                                    if (!NonRtBinDict.ContainsKey(binarray[0]))
                                    {
                                        NonRtBinDict.Add(binarray[0], new ArrayList());
                                        NonRtBinDict[binarray[0]].Add(binarray[1]);
                                        NonRtBinDict[binarray[0]].Add(binarray[2]);
                                    }
                                    else
                                    {
                                        NonRtBinDict[binarray[0]][0] = Convert.ToInt32(NonRtBinDict[binarray[0]][0]) + Convert.ToInt32(binarray[1]);
                                    }
                                }
                            }
                            else
                            {
                                return NonRtBinDict;
                            }
                        }
                        else
                        {
                            if (dr[20].ToString().IndexOf("all") != -1)
                            {
                                if (!NonRtBinDict.ContainsKey(binarray[0]))
                                {
                                    NonRtBinDict.Add(binarray[0], new ArrayList());
                                    NonRtBinDict[binarray[0]].Add(binarray[1]);
                                    NonRtBinDict[binarray[0]].Add(binarray[2]);

                                }
                                else
                                {
                                    NonRtBinDict[binarray[0]][0] = Convert.ToInt32(NonRtBinDict[binarray[0]][0]) + Convert.ToInt32(binarray[1]);
                                }

                            }
                            else
                            {
                                if (NonRtBinHash.ContainsKey(binarray[0]))
                                {
                                    if (!NonRtBinDict.ContainsKey(binarray[0]))
                                    {
                                        NonRtBinDict.Add(binarray[0], new ArrayList());
                                        NonRtBinDict[binarray[0]].Add(binarray[1]);
                                        NonRtBinDict[binarray[0]].Add(binarray[2]);
                                    }
                                    else
                                    {
                                        NonRtBinDict[binarray[0]][0] = Convert.ToInt32(NonRtBinDict[binarray[0]][0]) + Convert.ToInt32(binarray[1]);
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        dr.Close();
        myConn.Close();

        return NonRtBinDict;
    }
    
    /// <summary>
    /// Collect Non-retest bin base on hardbin (for A5XX)
    /// </summary>
    /// <param name="batch_no"></param>
    /// <param name="rowid"></param>
    /// <param name="start_date"></param>
    /// <param name="end_date"></param>
    /// <param name="stage"></param>
    /// <param name="platform"></param>
    /// <returns></returns>
    public Dictionary<string, ArrayList> NonRetestHBinCollect(string batch_no, string rowid, string start_date, string end_date, string stage, string platform)
    {
        int NR_reset = 0;
        string NR_bin_list = "";
        int bin11 = 0;
        Hashtable NonRtBinHash = new Hashtable();
        // Dictionary<string, List<string>> NonRtBinDict = new Dictionary<string, List<string>>();
        Dictionary<string, ArrayList> NonRtBinDict = new Dictionary<string, ArrayList>();
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
                           "bin241,bin242,bin243,bin244,bin245,bin246,bin247,bin248,bin249,bin250,bin251,bin252,bin253,bin254,bin255,bin256 ,non_retest ";        
        string sub_cmd = "";
        switch (platform)
        {

            case "A5XX":
                sub_cmd = "FROM summary_" + platform + " where batch = upper('" + batch_no + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " +
                " and test_stage like '" + stage[0] + "%' and rowid != '" + rowid + "' and test_code != 'PRODUCTION' order by start_date";

                break;

            case "VISTA":
                sub_cmd = "FROM summary_" + platform + " where batch = upper('" + batch_no + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " +
                " and test_stage like '" + stage[0] + "%' and rowid != '" + rowid + "' and test_code != 'PRODUCTION' order by start_date";

                break;

            case "UFLEX":
                sub_cmd = "FROM summary_" + platform + " where batch = upper('" + batch_no + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " +
                " and test_stage = '" + stage + "' and rowid != '" + rowid + "' and test_code != 'PRODUCTION' order by start_date";

                break;

            case "SPEA":
                sub_cmd = "FROM summary_" + platform + " where batch = upper('" + batch_no + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " +
                " and test_stage like '" + stage[0] + "%' and rowid != '" + rowid + "' and test_code != 'PRODUCTION' order by start_date";

                break;

            case "TURBO":
                sub_cmd = "from summary_" + platform + " where batch = upper('" + batch_no + "') and start_date between to_date('" + start_date + " 00:00:00','yyyy-mm-dd HH24:MI:SS') and to_date('" + end_date + " 23:59:59','yyyy-mm-dd HH24:MI:SS') " +
                " and test_code in ('00', '60', '30')  and rowid != '" + rowid + "' and temperature = '" + stage + "' and TESTED_QTY != 0 order by start_date";
                break;

        }

        cmd = cmd + sub_cmd;

        OracleCommand myCmd = new OracleCommand(cmd, myConn);
        OracleDataReader dr = myCmd.ExecuteReader();
        while (dr.Read())
        {

            NR_bin_list = dr[256].ToString();

            if (NR_bin_list.CompareTo("") == 0)
            {
                NR_bin_list = "11";
            }

            //Collecting Bin11 information
            if (dr[10].ToString().CompareTo("") != 0)
            {
                bin11 = Convert.ToInt32(dr[10].ToString());

                if (!NonRtBinDict.ContainsKey("bin11"))
                {
                    NonRtBinDict.Add("bin11", new ArrayList());
                    NonRtBinDict["bin11"].Add(bin11);
                    NonRtBinDict["bin11"].Add("");
                }
                else
                {
                    NonRtBinDict["bin11"][0] = Convert.ToInt32(NonRtBinDict["bin11"][0]) + bin11;
                }
            }


            if (NR_bin_list.CompareTo("") != 0)
            {
                if (NR_reset == 0)
                {
                    NRtBinParse(NR_bin_list, NonRtBinHash);
                    NR_reset = 1;
                }

                for (int i = 0; i <= 255; i++)
                {
                    if (dr[i].ToString().CompareTo("") != 0)
                    {
                        if (NonRtBinHash.ContainsKey(i+1))
                        {
                            if (!NonRtBinDict.ContainsKey(Convert.ToString(i + 1)))
                            {
                                NonRtBinDict.Add(Convert.ToString(i + 1), new ArrayList());
                                NonRtBinDict[Convert.ToString(i + 1)].Add(dr[i].ToString());
                            }
                            else
                            {
                                NonRtBinDict[Convert.ToString(i + 1)][0] = Convert.ToInt32(NonRtBinDict[Convert.ToString(i + 1)][0]) + Convert.ToInt32(dr[i].ToString());
                            }
                        }
                    }
                }

                //To prevent Bin 11 is not defined as non-retest bin
                if ((NonRtBinDict.ContainsKey("bin11")) && (!NonRtBinDict.ContainsKey("11")))
                {
                    NonRtBinDict.Add("11", new ArrayList());
                    NonRtBinDict["11"].Add(NonRtBinDict["bin11"][0]);
                }
            }
        }
        dr.Close();
        myConn.Close();

        return NonRtBinDict;
    }




    public void NRtBinParse(string NRtBinList, Hashtable NonRtBin)
    {
        if (NRtBinList[0] == 'R')
        {
            NRtBinList = NRtBinList.Replace("R:", "");
        }
        string zero_factor = "";
        string[] conArray = NRtBinList.Split(',');
        foreach (string cond1 in conArray)
        {
            if (cond1.IndexOf("-") != -1)
            {
                string[] ary2 = cond1.Split('-');
                string start = ary2[0];
                string end = ary2[1];
                if (Regex.IsMatch(start, "^0"))
                {
                    zero_factor = "0";
                    if (Regex.IsMatch(start, "^00"))
                    {
                        zero_factor = "00";
                    }
                    start = Regex.Replace(start, "^0+", "");
                    end = Regex.Replace(end, "^0+", "");
                }
                for (int i = Convert.ToInt32(start); i <= Convert.ToInt32(end); i++)
                {
                    string finalstring = zero_factor + i.ToString();
                    if (!NonRtBin.ContainsKey(finalstring))
                    {
                        NonRtBin[finalstring] = 0;
                    }
                }
            }
            else
            {
                if (!NonRtBin.ContainsKey(cond1))
                {
                    NonRtBin[cond1] = 0;
                }
            }
        }
    }



}
