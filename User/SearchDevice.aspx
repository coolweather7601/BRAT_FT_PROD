<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SearchDevice.aspx.cs" Inherits="SearchDevice" Title="BR@T" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FartherMain" Runat="Server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="LABDevice" runat="server" Text="Device"></asp:Label></td>
            <td style="width: 255px">
                <asp:TextBox ID="TXTDevice" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="LABPlatform" runat="server" Text="Platform"></asp:Label></td>
            <td style="width: 255px">
                <asp:DropDownList ID="DDLPlatform" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="LABStage" runat="server" Text="Stage"></asp:Label></td>
            <td style="width: 255px">
                <asp:DropDownList ID="DDLStage" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="LABBL" runat="server" Text="BL"></asp:Label></td>
            <td style="width: 255px">
                <asp:DropDownList ID="DDLBL" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 85px">
                <asp:Label ID="LABOwner" runat="server" Text="Owner"></asp:Label></td>
            <td style="width: 255px">
                <asp:DropDownList ID="DDLOwner" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 26px">
                <asp:Button ID="BTNSearch" runat="server" Text="Search" OnClick="BTNSearch_Click" /></td>
        </tr>
    </table>    

                <asp:GridView ID="GVDeviceLimit" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    GridLines="Horizontal" AllowPaging="True" Width="90%" PageSize="25" Font-Size="X-Small" AllowSorting="True"
                    OnPageIndexChanging="GVDeviceLimit_PageIndexChanging" OnSorting="GVDeviceLimit_Sorting">
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <Columns>
                        <asp:TemplateField HeaderText="DEVICE" SortExpression="DEVICE_NAME">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DEVICE_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("DEVICE_NAME") %>'></asp:Label>
                                <asp:Panel ID="PNL12ncInfo" runat="server" CssClass="PanelCSS" Wrap="False"> </asp:Panel>
                                <cc1:HoverMenuExtender ID="HME12ncInfo" runat="server" DynamicControlID="PNL12ncInfo" DynamicServiceMethod="Get12ncInfo" DynamicContextKey='<%# Eval("ID") %>' PopupControlID="PNL12ncInfo" PopupPosition="Top" TargetControlID="Label1" HoverCssClass="HoverCSS">
                                </cc1:HoverMenuExtender>
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
                        <asp:BoundField DataField="NEW_EXPERTISE_ID" HeaderText="LIMIT ID" SortExpression="NEW_EXPERTISE_ID" />
                        <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="../user/ViewLimit.aspx?ID={0}" HeaderText="OPTION" Target="_blank" Text="View Limit" />
                    </Columns>
                    <PagerTemplate>
                    <div style="text-align:center;" id="page">
                        共<asp:Label ID="lblTotalCount" runat="server" Text=""></asp:Label>筆 │ 
                        <asp:Label ID="lblPage" runat="server" ></asp:Label> / <asp:Label ID="lblTotalPage" runat="server" ></asp:Label>頁 │ 
                        <asp:LinkButton ID="lbnFirst" runat="Server" Text="第一頁" onclick="lbnFirst_Click" ></asp:LinkButton> │ 
                        <asp:LinkButton ID="lbnPrev" runat="server" Text="上一頁" onclick="lbnPrev_Click" ></asp:LinkButton> │ 
                        <asp:LinkButton ID="lbnNext" runat="Server" Text="下一頁" onclick="lbnNext_Click"></asp:LinkButton> │ 
                        <asp:LinkButton ID="lbnLast" runat="Server" Text="最後頁" onclick="lbnLast_Click" ></asp:LinkButton> │ 
                        到第<asp:TextBox ID="inPageNum" Width="20px" runat="server"></asp:TextBox>頁： 
                        每頁<asp:TextBox ID="txtSizePage" Width="25px" runat="server"></asp:TextBox>筆
                        <asp:Button ID="btnGo" runat="server" Text="Go" onclick="btnGo_Click"/>
                        <br />
                        </div>
                    </PagerTemplate>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                </asp:GridView>

    <asp:SqlDataSource ID="SqlDeviceLimit" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>" ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" SelectCommand='SELECT "DEVICE_NAME", "SUFFIX", "STAGE", "PLATFORM", "BUDGET_YLD", "BL", "OWNER", "ID",  "TEMP",  "COMMIT_DATE", "NEW_EXPERTISE_ID" FROM "DEVICE_POOL2" ORDER BY "DEVICE_NAME"'></asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    &nbsp;<br />
    <br />

    <br />
    &nbsp;
</asp:Content>

