<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeviceLimitView2.aspx.cs"
    Inherits="Device_DeviceLimitView2" Title="Br@T FT" %>

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
            BorderColor="#CC9966" BorderWidth="1px" CellPadding="4" DataSourceID="SqlDevicePool" OnRowDataBound="GridView1_RowDataBound" OnRowCreated="GridView1_RowCreated" BorderStyle="None" Font-Size="X-Small">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="PIORITY" HeaderText="優先" SortExpression="PIORITY" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CRITERION" HeaderText="條件" SortExpression="CRITERION" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EQUATION" HeaderText="比較" SortExpression="EQUATION" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="QTY" HeaderText="值" SortExpression="QTY" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="UNIT" HeaderText="單位" SortExpression="UNIT" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="CRITERION2" HeaderText="條件2" SortExpression="CRITERION2" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="EQUATION2" HeaderText="比較2" SortExpression="EQUATION2" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="QTY2" HeaderText="值2" SortExpression="QTY2" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="UNIT2" HeaderText="單位2" SortExpression="UNIT2" >
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="指示" SortExpression="ACTION">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ACTION") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACTION") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HOLD_CODE" SortExpression="HOLD_CODE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("HOLD_CODE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("HOLD_CODE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="註解" SortExpression="COMMENTS">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("COMMENTS") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("COMMENTS") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" BorderStyle="Outset" BorderWidth="2px" ForeColor="#FFFFCC" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
        </asp:GridView>
        &nbsp;&nbsp;
        <asp:SqlDataSource ID="SqlDevicePool" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" SelectCommand="SELECT ID, CRITERION, PIORITY, EQUATION, UNIT, QTY, CRITERION2, EQUATION2, UNIT2, QTY2,INDICATE_ID, ACTION, HOLD_CODE,  COMMENTS, ENGINEER, PHONE FROM  LIMIT_EXPERTISE WHERE NEW_EXPERTISE_ID =  :NEW_EXPERTISE_ID  order by PIORITY&#13;&#10;">
            <SelectParameters>
                <asp:Parameter Name="NEW_EXPERTISE_ID" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
