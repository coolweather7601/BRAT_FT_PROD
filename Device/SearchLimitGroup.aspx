<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SearchLimitGroup.aspx.cs" Inherits="SearchLimitGroup" Title="BR@T" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FartherMain" Runat="Server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table>
        <tr>
            <td style="width: 78px">
                <asp:Label ID="LABDevice" runat="server" Text="Device"></asp:Label></td>
            <td style="width: 32px">
                <asp:TextBox ID="TXTDevice" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 78px">
                <asp:Label ID="LABPlatfrom" runat="server" Text="Platform"></asp:Label></td>
            <td style="width: 32px">
                <asp:DropDownList ID="DDLPlatform" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 26px">
                <asp:Button ID="BTNSearch" runat="server" Text="Search" OnClick="BTNSearch_Click" />
                <asp:Button ID="HoldCode" runat="server" OnClick="HoldCode_Click" Text="None HoldCode" />
            </td>
        </tr>
    </table>    &nbsp;
            <div id="divTotal" style="display:none;" runat="server">
            <table border="1" style="border-color: #E7E7FF;border-width: 1px;border-style: None;font-size: small;font-weight: bold;">
                <tr>
                    <td style="background-color:Yellow;">Total</td>
                    <td style="background-color: #4A3C8C;border-spacing: 2px;color:White;">共<asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>筆</td>
                    <td style="background-color:Silver">Rows</td>
                    <td style="background-color: #4A3C8C ;border-spacing: 2px;color:White;"><asp:Label ID="lblRows" runat="server" Text=""></asp:Label>筆</td>
                    <td style="background-color:Lime">Percentage</td>
                    <td style="background-color: #4A3C8C ;border-spacing: 2px;color:White;"><asp:Label ID="lblPercentage" runat="server" Text=""></asp:Label>%</td>
                </tr>
            </table>
            </div>
            <asp:GridView ID="gvLimitGroup" runat="server" AutoGenerateColumns="False" AllowPaging="True" BackColor="White"
                          BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" 
                          OnRowDataBound="gvLimitGroup_RowDataBound" PageSize="25" Font-Size="X-Small"
                          OnPageIndexChanging="gvLimitGroup_PageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="NEW_EXPERTISE_ID" HeaderText="LIMIT ID" SortExpression="NEW_EXPERTISE_ID" />
                    <asp:BoundField DataField="DEVICE_NAME" HeaderText="DEVICE" SortExpression="DEVICE_NAME" />
                    <asp:BoundField DataField="SUFFIX" HeaderText="SUFFIX" SortExpression="SUFFIX" />
                    <asp:BoundField DataField="PLATFORM" HeaderText="PLATFORM" SortExpression="PLATFORM" />
                    <asp:BoundField DataField="STAGE" HeaderText="STAGE" SortExpression="STAGE" />
                    <asp:BoundField DataField="COMMIT_DATE" HeaderText="ISSUE DATE" SortExpression="COMMIT_DATE" />
                    <asp:TemplateField HeaderText="MEMBER">
                        <ItemTemplate>
                            <asp:HyperLink ID="linkMember" runat="server" Target="_blank">[linkMember]</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OPTION">
                        <ItemTemplate>
                            <asp:HyperLink ID="linkView" runat="server" Target="_blank">View</asp:HyperLink>&nbsp;
                            <asp:HyperLink ID="linkEdit" runat="server" NavigateUrl="~/Device/DeviceLimitManagement.aspx?ID={0}"
                                Target="_blank">Edit</asp:HyperLink>&nbsp;
                            &nbsp;<asp:LinkButton
                                ID="linkbtnDel" runat="server" OnClientClick="return confirm('Are you sure you want to delete this data？');" OnClick="linkbtnDel_Click">Delete</asp:LinkButton>
                            <asp:HyperLink ID="linkApply" runat="server" Target="_blank">ApplyTo</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <div style="text-align:center;" id="page">
                    共<asp:Label runat="server" ID="lblTotalCount" Text=""></asp:Label>筆 │ 
                    <asp:Label runat="server" ID="lblPage"></asp:Label> / <asp:Label ID="lblTotalPage" runat="server" ></asp:Label>頁 │ 
                    <asp:LinkButton ID="lbnFirst" runat="server" Text="第一頁" onclick="lbnFirst_Click"></asp:LinkButton>│
                    <asp:LinkButton ID="lbnPrev" runat="server" Text="上一頁" onclick="lbnPrev_Click" ></asp:LinkButton>│
                    <asp:LinkButton ID="lbnNext" runat="server" Text="下一頁" onclick="lbnNext_Click"></asp:LinkButton>│
                    <asp:LinkButton ID="lbnLast" runat="server" Text="最後頁" onclick="lbnLast_Click"></asp:LinkButton>│
                    到第<asp:TextBox ID="inPageNum" Width="20px" runat="server"></asp:TextBox>頁：
                    每頁<asp:TextBox ID="txtSizePage" Width="25px" runat="server"></asp:TextBox>筆
                    <asp:Button ID="btnGo" onclick="btnGo_Click" runat="server" Text="Go"></asp:Button>
                    <br />
                    </div>
                </PagerTemplate>
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" />
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="#F7F7F7" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlControl" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>" ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" >
            </asp:SqlDataSource>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    &nbsp;<br />
    <br />

    <br />
    &nbsp;
</asp:Content>

