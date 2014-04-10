<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SearchDeviceVersion.aspx.cs" Inherits="SearchDeviceVersion" Title="BR@T" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FartherMain" Runat="Server">
    <table style="width: 600px; height: 50px">
        <tr>
            <td style="height: 34px">
               <div class="demoarea">
                <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" Height="30px"> 
                   <div style="padding:5px; cursor: pointer; vertical-align: middle;">
                     <div style="float: left;" >
                         Purpose of Limit file Version Control</div>
                     <div style="float: left; margin-left: 20px;">
                       <asp:Label ID="Label1" runat="server" >(Show Details...)</asp:Label>
                     </div>
                     <div style="float: right; vertical-align: middle;">
                       <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                     </div>
                   </div>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanel" Height="0">
                    <table>
                      <tr align="left" valign="middle"> 
                        <td width="385" valign="top" > <br/>
                            This web function is for Limit file <b>Version Control</b> purpose. Whenever Brat users update limit, the previous limit version will be kept in 
                            DB for further reference. <br/><br/>  The search function is in principle list the very first page.  You can image there will be lot of limit version items. 
                            So, If you the specific type that you are looking for, I suggest that you better input the type name and click "search".  Web will list all the version
                            history. That can save you lot of time for finding your limit verion log.
                        </td>
                        <td>
                            <asp:ImageButton ID="Image2" runat="server" ImageUrl="~/images/BRAT.png" AlternateText="ASP.NET AJAX" ImageAlign="right" />
                        </td>
                      </tr>
                    </table>
                </asp:Panel>
               </div>
               <div class="demoarea">
                <asp:Panel ID="Panel3" runat="server" CssClass="collapsePanelHeader" Height="30px"> 
                   <div style="padding:5px; cursor: pointer; vertical-align: middle;">
                     <div style="float: left;" >
                         How to fine the device information</div>
                     <div style="float: left; margin-left: 20px;">
                       <asp:Label ID="Label2" runat="server" >(Show Details...)</asp:Label>
                     </div>
                     <div style="float: right; vertical-align: middle;">
                       <asp:ImageButton ID="Image3" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                     </div>
                   </div>
                </asp:Panel>
                <asp:Panel ID="Panel4" runat="server" CssClass="collapsePanel" Height="0">
                   <br />
                    <table>
                     <tr align="left" valign="middle"> 
                       <td height="217"> 
                        <div align="left"> <font color="#000000">Please input your device 
                        name to view or define dynamic yield and create 
                        limit file. </font> 
                        <p><font color="#000000">Wildcard Characters Usage:</font></p>
                        <p><font color="#000000" size="2">The star sign(*) matches zero or more characters. <br/>
                        The underscore(_) matches any single character. <br/>
                        Example:</font></p>
                        <p><font color="#000000" size="2">Put &quot;TDA*&quot; in Program name field. You will get the result like following: <br/>
                        =&gt; TDA4856/V03 .... .... ....</font></p>
                        <p><font color="#000000" size="2">=&gt; TDA4856/V2 ... .... ....</font></p>
                        <p><font color="#000000" size="2">Put &quot;CB_05*&quot; in Diffusion ID field. You will get the result like these: <br/>
                        =&gt; CBX052343</font></p>
                        <p><font color="#000000" size="2">=&gt; CBW055655</font><br/></p>
                        </div>
                      </td>
                    </tr>
                  </table>
                </asp:Panel>
               </div>
 
    <cc1:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="True"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
         >
        
        </cc1:CollapsiblePanelExtender>
                <cc1:CollapsiblePanelExtender ID="cpeDemo2" runat="Server"
        TargetControlID="Panel4"
        ExpandControlID="Panel3"
        CollapseControlID="Panel3" 
        Collapsed="True"
        TextLabelID="Label2"
        ImageControlID="Image3"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
         >
                </cc1:CollapsiblePanelExtender>
                &nbsp;&nbsp;
            </td>
        </tr>

    </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table>
        <tr>
            <td style="width: 90px">
                <asp:Label ID="LABDevice" runat="server" Text="Device"></asp:Label></td>
            <td style="width: 251px">
                <asp:TextBox ID="TXTDevice" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 90px">
                <asp:Label ID="LABPlatform" runat="server" Text="Platform"></asp:Label></td>
            <td style="width: 251px">
                <asp:DropDownList ID="DDLPlatform" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 26px">
                <asp:Button ID="BTNSearch" runat="server" Text="Search" OnClick="BTNSearch_Click" /></td>
        </tr>
    </table>    

                <asp:GridView ID="GVDeviceVersion" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    GridLines="Horizontal" AllowPaging="True" Width="90%" PageSize="15"
                    OnPageIndexChanging="GVDeviceVersion_PageIndexChanging">
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
                        <asp:BoundField DataField="PLATFORM" HeaderText="PLATFORM" SortExpression="TESTER" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEMP" HeaderText="TEMP" SortExpression="TEMP" />
                        <asp:BoundField DataField="BUDGET_YLD" HeaderText="YIELD" SortExpression="BUDGET_YLD" />
                        <asp:BoundField DataField="BL" HeaderText="BL" SortExpression="BL" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OWNER" HeaderText="OWNER" SortExpression="OWNER" />
                        <asp:HyperLinkField DataNavigateUrlFields="ID,NEW_EXPERTISE_ID" DataNavigateUrlFormatString="../user/ViewLimitVersion.aspx?ID={0}&amp;DevVer={1}"
                            DataTextField="NEW_EXPERTISE_ID" HeaderText="LIMIT(ID)" Target="_blank" />
                        <asp:BoundField DataField="COMMIT_DATE" HeaderText="Archive Date" SortExpression="COMMIT_DATE" DataFormatString="{0:MM-dd-yyyy}" />
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
    
        </ContentTemplate>
    </asp:UpdatePanel>
    &nbsp;<br />
    <br />

    <br />
    &nbsp;
</asp:Content>

