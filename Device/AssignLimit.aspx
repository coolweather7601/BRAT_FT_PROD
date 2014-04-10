<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignLimit.aspx.cs" Inherits="Device_AssignLimit" Title="Apply Limit"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>未命名頁面</title>
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table style="border-right: fuchsia thin dashed; border-top: fuchsia thin dashed; margin: 1px; border-left: fuchsia thin dashed; border-bottom: fuchsia thin dashed" border="1">
        <tr>
            <td style="width: 120px">
                <asp:Label ID="LABDevice" runat="server" Text="Device"></asp:Label></td>
            <td style="width: 230px">
                <asp:TextBox ID="TXTDevice" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="LABPlatform" runat="server" Text="Platform"></asp:Label></td>
            <td style="width: 230px">
                <asp:DropDownList ID="DDLPlatform" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="LABStage" runat="server" Text="Stage"></asp:Label></td>
            <td style="width: 230px">
                <asp:DropDownList ID="DDLStage" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="LABBL" runat="server" Text="BL"></asp:Label></td>
            <td style="width: 230px">
                <asp:DropDownList ID="DDLBL" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:Label ID="LABOwner" runat="server" Text="Owner"></asp:Label></td>
            <td style="width: 230px">
                <asp:DropDownList ID="DDLOwner" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 26px">
                <asp:Button ID="BTNSearch" runat="server" Text="Search" OnClick="BTNSearch_Click" />
                <asp:CheckBox ID="cbSelAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbSelAll_CheckedChanged"
                    Text="Select All" />&nbsp;
                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
        </tr>
    </table>    

                <asp:GridView ID="GVDeviceLimit" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataSourceID="SqlDeviceLimit"
                    GridLines="Horizontal" Width="786px" PageSize="15" Font-Size="X-Small">
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="SELECT">
                            <ItemTemplate>
                                <asp:CheckBoxList ID="cblSelDevice" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                </asp:CheckBoxList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                        <asp:TemplateField HeaderText="DEVICE" SortExpression="DEVICE_NAME">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DEVICE_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DEVICE_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SUFFIX" HeaderText="SUFFIX" SortExpression="SUFFIX" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STAGE" HeaderText="STAGE" SortExpression="STAGE" />
                        <asp:BoundField DataField="PLATFORM" HeaderText="PLATFORM" SortExpression="PLATFORM" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEMP" HeaderText="TEMP" SortExpression="TEMP" />
                        <asp:BoundField DataField="BUDGET_YLD" HeaderText="YIELD" SortExpression="BUDGET_YLD" />
                        <asp:BoundField DataField="BL" HeaderText="BL" SortExpression="BL" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OWNER" HeaderText="OWNER" SortExpression="OWNER" />
                        <asp:BoundField DataField="NEW_EXPERTISE_ID" HeaderText="Limit ID" SortExpression="NEW_EXPERTISE_ID" />
                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="../user/ViewLimit.aspx?ID={0}" HeaderText="OPTION" Target="_blank" Text="View Limit" />
                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                </asp:GridView>

    <asp:SqlDataSource ID="SqlDeviceLimit" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>" ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" SelectCommand='SELECT "DEVICE_NAME", "SUFFIX", "STAGE", "PLATFORM", "BUDGET_YLD", "BL", "OWNER", "ID",  "TEMP",  "COMMIT_DATE","NEW_EXPERTISE_ID"  FROM "DEVICE_POOL2" ORDER BY "DEVICE_NAME"'></asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    &nbsp;<br />
    <br />

    <br />
    &nbsp;

    </form>
</body>
</html>