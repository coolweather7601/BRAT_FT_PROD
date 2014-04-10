<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewLimitVersion.aspx.cs" Inherits="ViewLimit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Limit</title>
    <link type="text/css" rel="stylesheet" href="../App_Themes/makeover/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <br />
            <table>
                <tr style="background-color: #333333">
                    <td colspan="5" style="height: 24px;">
                        <div style="text-align: center">
                            <b style="font-family: Verdana, Arial, Helvetica, sans-serif; color: #0000FF; font-size: 15pt;">
                                <i style="color: #FF00FF;">Limit File - Final Br@t System</i>
                            </b>
                        </div>
                    </td>
                </tr>
                <tr style="background-color: #FFFFFF">
                    <td style="width: 79px; height: 25px; background-color: #333333;">
                        <div style="text-align: center; color: #FF00FF;">
                            <b>Device Name</b></div>
                    </td>
                    <td align="center" style="width: 215px; height: 25px; background-color: #ffccff;">
                        <asp:Label ID="LABDeviceName" runat="server" Font-Size="Small"></asp:Label>
                    </td>
                    <td style="width: 65%; background-color: #FFFFCC; text-align: center; border-right: red 1px solid; border-top: red 1px solid; border-left: red 1px solid; border-bottom: red 1px solid;" colspan="3" rowspan="5">
                        <p>
                            <b style="color: #0000FF; font-size: 12pt;">Default Checking</b></p>
                        <p>
                            1.
                            Actual Yield &gt;= 100% --&gt; Hold</p>
                        <p>
                            2.
                            Open/Short &gt; 1% --&gt; Hold></p>
                        <p>
                            3.
                            Actual Yield &lt; Budget Yield--&gt; Hold&nbsp; (If&nbsp; BR@T limit is not defined)</p>
                    </td>
                </tr>
                <tr style="background-color: #FFFFFF">
                    <td style="width: 79px; height: 25px; background-color: #333333;">
                        <div style="text-align: center; color: #FF00FF;">
                            <b>Suffix</b></div>
                    </td>
                    <td style="width: 215px; height: 25px; background-color: #ffccff;">
                        <asp:Label ID="LABSuffix" runat="server" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFFF">
                    <td style="width: 79px; height: 25px; background-color: #333333;">
                        <div style="text-align: center; color: #FF00FF;">
                            <b>Platform</b></div>
                    </td>
                    <td style="width: 215px; height: 25px; background-color: #ffccff;">
                        <asp:Label ID="LABPlatform" runat="server" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFFF">
                    <td style="width: 79px; height: 25px; background-color: #333333;">
                        <div style="text-align: center; color: #FF00FF;">
                            <b>Stage</b></div>
                    </td>
                    <td style="width: 215px; height: 25px; background-color: #ffccff;">
                        <asp:Label ID="LABStage" runat="server" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFFF">
                    <td style="width: 79px; height: 25px; background-color: #333333;">
                        <div style="text-align: center; color: #FF00FF;">
                            <b>Budget Yield</b></div>
                    </td>
                    <td style="width: 215px; height: 25px; background-color: #ffccff;">
                        <asp:Label ID="LABBgtYld" runat="server" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #333333">
                    <td colspan="5" style="height: 25px;">
                        <div style="text-align: center; color: #0066FF; font-size: 5; font-family: Arial Black;">
                            Limit Information</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center" style="background-color: #ffffcc">
                        <asp:GridView ID="GVLimitItem" runat="server" DataSourceID="SqlLimitItem" OnRowDataBound="GVLimitItem_RowDataBound"
                            Font-Size="X-Small" CssClass="mGrid">
                            <PagerStyle BorderColor="#C0FFC0" />
                            <RowStyle BorderColor="Blue" BorderStyle="Double" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlLimitItem" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
                            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>"></asp:SqlDataSource>
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
    </form>
</body>
</html>
