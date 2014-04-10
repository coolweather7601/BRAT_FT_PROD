<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Limit_Match_Case_HB.aspx.cs" Inherits="User_Limit_Match_Case_HB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <style type="text/css">
        div
        {
            padding:10px;
        }
        .labelStyle
        {
            color:red;
            background-color:yellow;
            border:Solid 2px Red;
            vertical-align: middle; 
            text-align: center;
vertical-align:middle;
        }
    </style>

<title>Limit Case Check</title>
<link type="text/css" rel="stylesheet" href="../App_Themes/makeover/style.css" />
</head>
<body>
    <form id="form2" runat="server">
    <div style="background-color: #ffffcc; text-align: center">
        <br />
        <table style="border-left-color: black; border-bottom-color: black; border-top-style: solid; border-top-color: black; border-right-style: solid; border-left-style: solid; border-right-color: black; border-bottom-style: solid">
            <tr style="background-color: #333333">
                <td colspan="3" height="25">
                    <div align="center">
                        <b><font color="#0000ff" face="Verdana, Arial, Helvetica, sans-serif"><i><font size="4">
                            <font color="#ff00ff">Limit File</font> - Final Br@t System</font></i></font></b></div>
                </td>
            </tr>
            <tr style="background-color: #ffffff">
                <td bgcolor="#333333" height="25" style="width: 79px; vertical-align: middle; text-align: center;">
                    <div align="center">
                        <font color="#0099ff"><b><font color="#ff00ff" size="1">Device Name</font></b></font></div>
                </td>
                <td align="center" bgcolor="#ffe6ff" height="25" style="width: 215px; font-size: 8pt; background-color: #ffff99;">
                    <asp:Label ID="LABDeviceName" runat="server"></asp:Label><em><span style="font-size: 7pt;
                        color: #ff00ff"> </span></em>
                </td>
                <td align="center"  height="175" rowspan="5" style="background-color: #ffff99;" width="65%">
                    <p><b style="color: #0000FF; font-size: 14pt;">Default Checking</b></p>
                    1.
                    Actual Yield &gt;= 100% --&gt; Hold<p>
                        2.
                        Open/Short &gt; 1% --&gt; Hold</p>
                    <p>
                        3.
                        Actual Yield &lt; Budget Yield--&gt; Hold&nbsp; (If&nbsp; BR@T limit is not defined)</p>
                </td>
            </tr>
            <tr style="background-color: #ffffff">
                <td align="center" bgcolor="#333333" height="25" style="width: 79px; vertical-align: middle; text-align: center;">
                    <div align="center">
                        <font color="#0099ff"><b><font color="#ff00ff" size="1">Suffix</font></b></font></div>
                </td>
                <td align="center" bgcolor="#ffe6ff" height="25" style="width: 215px; font-size: 8pt; background-color: #ffff99;">
                    <asp:Label ID="LABSuffix" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="background-color: #ffffff">
                <td align="center" bgcolor="#333333" height="25" style="width: 79px; vertical-align: middle; text-align: center;">
                    <div align="center">
                        <font color="#0099ff"><b><font color="#ff00ff" size="1">Platform</font></b></font></div>
                </td>
                <td align="center" bgcolor="#ffe6ff" height="25" style="width: 215px; font-size: 8pt; background-color: #ffff99;">
                    <asp:Label ID="LABPlatform" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="background-color: #ffffff">
                <td align="center" bgcolor="#333333" height="25" style="width: 79px; vertical-align: middle; text-align: center;">
                    <div align="center">
                        <font color="#0099ff"><b><font color="#ff00ff" size="1">Stage</font></b></font></div>
                </td>
                <td align="center" bgcolor="#ffe6ff" height="25" style="width: 215px; font-size: 8pt; background-color: #ffff99;">
                    <asp:Label ID="LABStage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="background-color: #ffffff">
                <td bgcolor="#333333" height="25" style="width: 79px; vertical-align: middle; text-align: center;">
                    <div align="center">
                        <font color="#0099ff"><b><font color="#ff00ff" size="1">Budget Yield</font></b></font></div>
                </td>
                <td align="center" bgcolor="#ffe6ff" height="25" style="width: 215px; font-size: 8pt; background-color: #ffff99;">
                    <asp:Label ID="LABBgtYld" runat="server"></asp:Label></td>
            </tr>
            <tr style="background-color: #333333">
                <td colspan="3" height="24">
                    <div align="center">
                        <font color="#0066ff" face="Arial Black" size="3">Limit Information</font></div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
        <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="False" CellPadding="3" OnRowDataBound="gvResult_RowDataBound" Font-Size="X-Small" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" OnRowCreated="gvResult_RowCreated" CssClass="mGrid">
            <RowStyle ForeColor="#000066" />
            <Columns>
                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                <asp:BoundField DataField="VALUE" HeaderText="VALUE" />
                <asp:BoundField DataField="CRITERION" HeaderText="條件" SortExpression="CRITERION" />
                <asp:BoundField DataField="EQUATION" HeaderText="比較" SortExpression="EQUATION" />
                <asp:BoundField DataField="QTY" HeaderText="值" SortExpression="QTY" />
                <asp:BoundField DataField="UNIT" HeaderText="單位" SortExpression="UNIT" />
                <asp:TemplateField HeaderText="AND"></asp:TemplateField>
                <asp:BoundField DataField="CRITERION2" HeaderText="條件2" SortExpression="CRITERION2" />
                <asp:BoundField DataField="EQUATION2" HeaderText="比較2" SortExpression="EQUATION2" />
                <asp:BoundField DataField="QTY2" HeaderText="值2" SortExpression="QTY2" />
                <asp:BoundField DataField="UNIT2" HeaderText="單位2" SortExpression="UNIT2" />
                <asp:TemplateField HeaderText="指示" SortExpression="ACTION">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACTION") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACTION") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HOLD_CODE" SortExpression="HOLD_CODE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("HOLD_CODE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("HOLD_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="註解" SortExpression="COMMENTS">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("COMMENTS") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("COMMENTS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
                    &nbsp;
                    &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
        <asp:SqlDataSource ID="SqlDevicePool" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" SelectCommand="SELECT CRITERION, PIORITY, EQUATION, UNIT, QTY, CRITERION2, EQUATION2, UNIT2, QTY2,INDICATE_ID, ACTION, HOLD_CODE, COMMENTS, ENGINEER, PHONE FROM  LIMIT_EXPERTISE WHERE NEW_EXPERTISE_ID =  :NEW_EXPERTISE_ID  order by PIORITY">
            <SelectParameters>
                <asp:Parameter Name="NEW_EXPERTISE_ID" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
