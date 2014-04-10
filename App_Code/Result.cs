using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

/// <summary>
/// Result 的摘要描述
/// </summary>
/// 

// Base Result of each specific check
public class SpecificCheck
{
    private string type;
    private string holdcode;
    private string comment;

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    public string HoldCode
    {
        get { return holdcode; }
        set { holdcode = value; }
    }

    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }
}

public class EncResult
{
    private string ref_no;
    private string resp_dept;
    private string defect;
    private string status;
    private string status_date;
    private string doc_no;

    public string RefNo
    {
        get { return ref_no; }
        set { ref_no = value; }
    }

    public string RespDept
    {
        get { return resp_dept; }
        set { resp_dept = value; }
    }

    public string Defect
    {
        get { return defect; }
        set { defect = value; }
    }
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    public string StatusDate
    {
        get { return status_date; }
        set { status_date = value; }
    }

    public string DocNo
    {
        get { return doc_no; }
        set { doc_no = value; }
    }
}

public class WholeResult
{
    private int statusidx;
    private string stage;
    private string temperature;
    private float budgetyield;
    private float yield;
    private string status;
    private string holdcode;
    private string comment;
    private string engineer;
    private string phone;
    private long errornum;
    private string errordesc;
    private bool result;
    private ArrayList specificresult = new ArrayList();

    public int StatusIdx
    {
        get { return statusidx; }
        set { statusidx = value; }
    }
    public string Stage
    {
        get { return stage; }
        set { stage = value; }
    }
    public string Temperature
    {
        get { return temperature; }
        set { temperature = value; }
    }
    public float BudgetYield
    {
        get { return budgetyield; }
        set { budgetyield = value; }
    }
    public float Yield
    {
        get { return yield; }
        set { yield = value; }
    }
    public string Status
    {
        get { return status; }
        set { status = value; }
    }
    public string HoldCode
    {
        get { return holdcode; }
        set { holdcode = value; }
    }
    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }
    public string Engineer
    {
        get { return engineer; }
        set { engineer = value; }
    }
    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }
    public long ErrorNumber
    {
        get { return errornum; }
        set { errornum = value; }
    }
    public string ErrorDescription
    {
        get { return errordesc; }
        set { errordesc = value; }
    }
    public bool Result
    {
        get { return result; }
        set { result = value; }
    }
    public ArrayList SpecificResult
    {
        get { return specificresult; }
        set { specificresult = value; }
    }


}

