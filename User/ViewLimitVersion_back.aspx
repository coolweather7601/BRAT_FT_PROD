<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewLimitVersion_back.aspx.cs" Inherits="ViewLimitVersion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BR@T Final</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
        &nbsp;
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        &nbsp;&nbsp; &nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <br />
        <br />
        <table>
            <tr style="background-color:#333333"> 
                <td colspan="5" style="height:25px">
                    <div align="center"><b><font face="Verdana, Arial, Helvetica, sans-serif" color="#0000FF"><i><font size="4"><font color="#FF00FF">Limit File</font> - Final Br@t System</font></i></font></b></div>
	            </td>
            </tr>            
            <tr style="background-color:#FFFFFF">
                <td style="width: 79px; height:25px;  background-color:#333333"> 
                    <div align="center"><font color="#0099FF"><b><font size="1" color="#FF00FF">Device Name</font></b></font></div>
                </td>
                <td align="center" style="width: 215px; height:25px; background-color:#FFE6FF">
                    <asp:Label ID="LABDeviceName" runat="server"></asp:Label>
                </td>
                <td colspan="3" align="center" rowspan="5" style="width:65%; height:25px; background-color:#FFFFCC">
                    <p><b>Methodology of Decision Maker</b></p>
                    <p><b><font color="#0000FF"> <font size="+1"><u>Bottom Up</u>      </font></font></b></p>
                    <p><font color="#000000"><i>Check Sequence =</i></font><i><font color="#0000FF"> [ Fail item -&gt; Bin value -&gt; Dynamic yield ]</font></i></p><font color="#ff0000">Archive Date :&nbsp;
                        <asp:Label ID="LABCommitDate" runat="server"></asp:Label></font>
                </td>
            </tr>
            <tr style="background-color:#FFFFFF">
                <td style="width: 79px; height: 21px; background-color:#333333" > 
                    <div align="center"><font color="#0099FF"><b><font size="1" color="#FF00FF">Suffix</font></b></font></div>
                </td>
                <td align="center" style="width: 215px; background-color:#FFE6FF"> 
                    <asp:Label ID="LABSuffix" runat="server"></asp:Label>
                 </td>
            </tr>
            <tr style="background-color:#FFFFFF">
                <td style="width: 79px; background-color:#333333"> 
                    <div align="center"><font color="#0099FF"><b><font size="1" color="#FF00FF">Platform</font></b></font></div>
                </td>
                 <td align="center" style="width: 215px; height:25px; background-color:#FFE6FF">
                    <asp:Label ID="LABPlatform" runat="server"></asp:Label>
                 </td>
            </tr>
            <tr style="background-color:#FFFFFF">
                <td style="width: 79px; height:25px; background-color:#333333" align="center"> 
                    <div align="center"><font color="#0099FF"><b><font size="1" color="#FF00FF">Stage</font></b></font></div>
                </td>
                 <td align="center" style="width: 215px; height:25px; background-color:#FFE6FF">
                    <asp:Label ID="LABStage" runat="server"></asp:Label>
                  </td>
            </tr>
            <tr style="background-color:#FFFFFF">
                <td style="width: 79px; height:25px; background-color:#333333" > 
                    <div align="center"><font color="#0099FF"><b><font size="1" color="#FF00FF">Budget Yield</font></b></font></div>
                </td>
                 <td align="center" style="width: 215px; height:25px; background-color:#FFE6FF">
                    <asp:Label ID="LABBgtYld" runat="server"></asp:Label>
                  </td>
            </tr>
            <tr style="background-color:#333333"> 
                <td colspan="5" style="height:24px"> 
                    <div align="center"><font size="3" color="#FF00FF"><b>Dynamic<font size="3" color="#0066FF">Yield</font></b></font></div>
                </td>
            </tr>
            <tr  style="background-color:#333333" > 
            <td colspan = "5">
            <table width="100%">
            <tr  style="background-color:#999999"  >
                <td style="background-color:#CCCCCC; height:25px; width:10%"> 
                    <div align="center"><font size="1" face="Verdana, Arial, Helvetica, sans-serif"><i><font color="#000000"><b>Rank</b></font></i></font></div>
                </td>
                <td style="background-color:#CCCCCC; height:25px; width:10%"> 
                    <div align="center"><font size="1" face="Verdana, Arial, Helvetica, sans-serif"><i><font color="#000000"><b>Yield</b></font></i></font></div>
                </td>
                <td style="background-color:#CCCCCC; height:25px; width:10%"> 
                    <div align="center"><font size="1" face="Verdana, Arial, Helvetica, sans-serif"><i><font color="#000000"><b>Scrap</b></font></i></font></div>
                </td>
                <td style="background-color:#CCCCCC; height:25px; width:40%"> 
                    <div align="center"><font size="1" face="Verdana, Arial, Helvetica, sans-serif"><i><font color="#000000"><b>Action</b></font></i></font></div>
                </td>
                <td style="background-color:#CCCCCC; height:25px; width:30%"> 
                    <div align="center"><font size="1" face="Verdana, Arial, Helvetica, sans-serif"><i><font color="#000000"><b>Extra Condition</b></font></i><br /><font color="#FF0000">for Top Down only</font></font></div>
                </td>
            </tr>
            <tr style="background-color:#FFDDFF"> 
                <td style="background-color:#FFFFCC" > 
                    <div align="center"><i><font size="1" face="Geneva, Arial, Helvetica, san-serif">Level 1</font></i></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABYield1" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABScrap1" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                     <div align="center">
                         <asp:Label ID="LABAct1" runat="server" Text=""></asp:Label></div>
                </td>
	            <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABCondition1" runat="server" Text=""></asp:Label></div>
                </td>
            </tr>
            <tr style="background-color:#FFDDFF"> 
                <td style="background-color:#FFFFCC" >  
                    <div align="center"><i><font size="1" face="Geneva, Arial, Helvetica, san-serif">Level 2</font></i></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABYield2" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABScrap2" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                     <div align="center">
                         <asp:Label ID="LABAct2" runat="server" Text=""></asp:Label></div>
                </td>
	            <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABCondition2" runat="server" Text=""></asp:Label></div>
                </td>
            </tr>
            <tr style="background-color:#FFDDFF">
                <td style="background-color:#FFFFCC" > 
                    <div align="center"><i><font size="1" face="Geneva, Arial, Helvetica, san-serif">Level 3</font></i></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABYield3" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABScrap3" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                     <div align="center">
                         <asp:Label ID="LABAct3" runat="server" Text=""></asp:Label></div>
                </td>
	            <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABCondition3" runat="server" Text=""></asp:Label></div>
                </td>
            </tr>
           <tr style="background-color:#FFDDFF">
                <td style="background-color:#FFFFCC" > 
                    <div align="center"><i><font size="1" face="Geneva, Arial, Helvetica, san-serif">Level 4</font></i></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABYield4" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABScrap4" runat="server" Text=""></asp:Label></div>
                </td>
                <td style="background-color:#FFFFCC" > 
                     <div align="center">
                         <asp:Label ID="LABAct4" runat="server" Text=""></asp:Label></div>
                </td>
	            <td style="background-color:#FFFFCC" > 
                    <div align="center">
                        <asp:Label ID="LABCondition4" runat="server" Text=""></asp:Label></div>
                </td>
            </tr>
            </table>
            </td>
            </tr>

            <tr bgcolor="#333333"> 
                <td colspan="5" height="24"> 
                    <div align="center"><font color="#0066FF" size="3" face="Arial Black">Limit of Fail Test Items</font> </div>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:GridView ID="GVLimitItem" runat="server" CellPadding="3" DataSourceID="SqlLimitItem" OnRowDataBound="GVLimitItem_RowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                        <RowStyle ForeColor="#000066" />
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlLimitItem" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT %>" ProviderName="<%$ ConnectionStrings:BRAT.ProviderName %>"></asp:SqlDataSource>
                </td>
            </tr>
            <tr bgcolor="#333333"> 
                <td colspan="5" height="24"> 
                    <div align="center"><font color="#0066FF" size="3" face="Arial Black">Limit of Bin Value</font> </div>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:GridView ID="GVLimitBin" runat="server" CellPadding="3" DataSourceID="SqlLimitBin" GridLines="None" OnRowDataBound="GVLimitBin_RowDataBound" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1">
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlLimitBin" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT %>"
                        ProviderName="<%$ ConnectionStrings:BRAT.ProviderName %>" >
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
