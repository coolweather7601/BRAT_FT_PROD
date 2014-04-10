<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LimitCheck.aspx.cs"
    Inherits="Device_DeviceLimitManagement" Title="Br@T FT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDevicePool" Font-Size="Small">
            <RowStyle BackColor="White" ForeColor="#003399" />
            <Columns>
                <asp:BoundField DataField="DEVICE_NAME" HeaderText="DEVICE" SortExpression="DEVICE_NAME" />
                <asp:BoundField DataField="SUFFIX" HeaderText="SUFFIX" SortExpression="SUFFIX" />
                <asp:BoundField DataField="TESTER" HeaderText="PLATFORM" SortExpression="TESTER" />
                <asp:BoundField DataField="STAGE" HeaderText="STAGE" SortExpression="STAGE" />
                <asp:BoundField DataField="TO_CHAR(COMMIT_DATE,'DD-MM-YYYYHH24:MI:SS')" HeaderText="ISSUE_DATE"
                    SortExpression="TO_CHAR(COMMIT_DATE,'DD-MM-YYYYHH24:MI:SS')" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        </asp:GridView>
        &nbsp;&nbsp;
        <asp:SqlDataSource ID="SqlDevicePool" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" SelectCommand="SELECT DEVICE_NAME,SUFFIX,TESTER,STAGE,to_char(COMMIT_DATE,'DD-MM-YYYY HH24:MI:SS') FROM DEVICE_POOL2 WHERE NEW_EXPERTISE_ID = :NEW_EXPERTISE_ID">
            <SelectParameters>
                <asp:Parameter Name="NEW_EXPERTISE_ID" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
