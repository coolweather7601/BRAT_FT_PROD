<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="GetSummaryA5XX.aspx.cs" Inherits="GetSummaryA5XX" Title="BR@T" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FartherMain" runat="Server">
    <div style="background-color: #ffffcc;">
        <table style="width: 600px; height: 50px; background-color: #FFFFCC;">
            <tr>
                <td colspan="2" style="width: 596px;">
                    <div class="demoarea" style="background-color: #FFFFCC">
                        <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" Height="30px">
                            <div style="padding: 5px; cursor: pointer; vertical-align: middle;">
                                <div style="float: left;">
                                    Br@t System Instruction</div>
                                <div style="float: left; margin-left: 20px;">
                                    <asp:Label ID="Label1" runat="server">(Show Details...)</asp:Label>
                                </div>
                                <div style="float: right; vertical-align: middle;">
                                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="Panel1" runat="server" CssClass="collapsePanel" BackColor="#FFFFCC">
                            <table style="background-color: #ffffcc">
                                <tr align="left" valign="middle">
                                    <td valign="top" style="height: 150px">
                                        <br />
                                        <ol>
                                            <li>Please choose platform</li>
                                            <li>Please input batch no.</li>
                                            <li>Pick period, that batch was tested</li>
                                            <li>Click &quot;Search&quot; button.</li>
                                        </ol>
                                        <div align="left">
                                            <div align="left">
                                                <ol>
                                                    <li>選擇測試平台</li>
                                                    <li>輸內批號</li>
                                                    <li>選擇此批的大致時間點</li>
                                                    <li>按 &quot;Search&quot; 按鈕</li>
                                                </ol>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="height: 150px; vertical-align: top;">
                                        <asp:ImageButton ID="Image2" runat="server" ImageUrl="~/images/BRAT.png" AlternateText="ASP.NET AJAX"
                                            ImageAlign="right" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <cc1:CollapsiblePanelExtender ID="cpeDemo" runat="Server" TargetControlID="Panel1"
                        ExpandControlID="Panel2" CollapseControlID="Panel2" Collapsed="True" TextLabelID="Label1"
                        ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
                        ExpandedImage="~/images/collapse_blue.jpg" CollapsedImage="~/images/expand_blue.jpg"
                        SuppressPostBack="true">
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
                        <td style="width: 265px">
                            <asp:Label ID="LABBatch" runat="server" Text="Batch Number"></asp:Label></td>
                        <td style="width: 367px">
                            <asp:TextBox ID="txtBatch" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 265px">
                            <asp:Label ID="LABPeriod" runat="server" Text="Period ......(yyyy-mm-dd)"></asp:Label></td>
                        <td style="width: 367px">
                            &nbsp;<asp:TextBox runat="server" ID="txtDateFrom" /><asp:ImageButton runat="Server" ID="IMGFrom" ImageUrl="~/images/Calendar_scheduleHS.png"
                                AlternateText="Click to show calendar" /><asp:TextBox runat="server" ID="txtDateTo" /><asp:ImageButton runat="Server" ID="IMGTo" ImageUrl="~/images/Calendar_scheduleHS.png"
                                AlternateText="Click to show calendar" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 26px">
                            <asp:Button ID="BTNSearch" runat="server" Text="Search" OnClick="BTNSearch_Click" /></td>
                    </tr>
                </table>
                <cc1:CalendarExtender ID="calendarButtonExtender1" runat="server" TargetControlID="TXTDateFrom"
                    Format="yyyy-MM-dd" PopupButtonID="IMGFrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="calendarButtonExtender2" runat="server" TargetControlID="TXTDateTo"
                    Format="yyyy-MM-dd" PopupButtonID="IMGTo">
                </cc1:CalendarExtender>
                <asp:Label ID="labLogging" runat="server" ForeColor="Red"></asp:Label><br />
                <asp:GridView ID="gvBatchInfo" runat="server" BackColor="#CCFF99" BorderColor="DodgerBlue"
                    BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" AutoGenerateColumns="False"
                    Font-Size="X-Small" OnRowDataBound="gvBatchInfo_RowDataBound" Width="100%" OnSelectedIndexChanging="gvBatchInfo_SelectedIndexChanging">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True">
                            <ItemStyle Width="5%" Wrap="False" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="OP">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DF_LOTID" HeaderText="DIFF">
                            <ItemStyle Width="10%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT" HeaderText="PRODUCT">
                            <ItemStyle Width="14%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="START_DATE" HeaderText="DATE">
                            <ItemStyle Width="18%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="FAIL">
                            <ItemStyle Width="6%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="labFail" runat="server" Text="View"></asp:Label>
                                <asp:Panel ID="pnlFailList" runat="server" CssClass="PanelCSS" Wrap="False"> </asp:Panel>
                                <cc1:HoverMenuExtender ID="HoverMenuFail" runat="server" DynamicContextKey='<%# Eval("rowid") + "$" + Eval("tested_qty") %>'
                                    DynamicControlID="pnlFailList" DynamicServiceMethod="GetFailInfo" HoverCssClass="HoverCSS"
                                    PopupControlID="pnlFailList" TargetControlID="labFail" DynamicServicePath="" PopupPosition="Bottom">
                                </cc1:HoverMenuExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BIN">
                            <ItemStyle Width="6%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="labBin" runat="server" Text="View"></asp:Label>
                                <asp:Panel ID="pnlBinList" runat="server" CssClass="PanelCSS" Wrap="False"> </asp:Panel>
                                <cc1:HoverMenuExtender ID="HoverMenuBin" runat="server" DynamicContextKey= '<%# Eval("rowid") + "$" + Eval("tested_qty") %>' DynamicControlID="pnlBinList"
                                    DynamicServiceMethod="GetDetails" HoverCssClass="HoverCSS" PopupControlID="pnlBinList"
                                    TargetControlID="labBin" PopupPosition="Bottom">
                                </cc1:HoverMenuExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PASS_QTY" HeaderText="PASS">
                            <ItemStyle Width="6%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TESTED_QTY" HeaderText="TOTAL">
                            <ItemStyle Width="8%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEST_CODE" HeaderText="CODE">
                            <ItemStyle Width="8%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEST_STAGE" HeaderText="STAGE">
                            <ItemStyle Width="6%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="FILE_LOCATION,FILE_NAME" DataNavigateUrlFormatString="http://klx1001vm.tw-khh01.nxp.com/cgi-bin/Paperless/A5XX_readRawData.pl/{0}/{1}"
                            DataTextField="TESTER" HeaderText="TESTER" Target="_blank">
                            <ItemStyle Width="15%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="rowid" HeaderText="RowID" />
                    </Columns>
                    <FooterStyle BackColor="Tan" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    <HeaderStyle BackColor="#6633CC" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#CCFF99" />
                </asp:GridView>
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                <table>
                    <tr>
                        <td style="width: 50px">
                            Pass:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtPassQty" runat="server"></asp:TextBox></td>
                        <td style="width: 50px">
                            Total:</td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtTotalQty" runat="server"></asp:TextBox></td>
                        <td style="width: 100px">
                            <asp:Button ID="btnBrat" runat="server" OnClick="btnBrat_Click" Text="BR@T" Enabled="False" />
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="panelResult" runat="server" Visible="False">
                    <div style="text-align: center"  >

                    <table width="650" style="border-right: silver thin solid; border-top: silver thin solid; border-left: silver thin solid; border-bottom: silver thin solid">
                        <tr>
                            <td align="center" colspan="1" rowspan="3" style="vertical-align: middle; text-align: center; border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; background-color: #ccff99; width: 30px;"
                                valign="bottom">
                                結<br />
                                果</td>
                            <td align="center" rowspan="3" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                vertical-align: middle; border-left: gray 1px solid; width: 420px; border-bottom: gray 1px solid;
                                text-align: center" valign="bottom">
                                <div id="div1" runat="server" style="height: 100px; vertical-align: text-bottom; text-align: center;" >
                                    <br />
                                    <br />
                                <asp:Label ID="labBratResult" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label></div>
                            </td>
                            <td align="center" colspan="2" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; width: 200px; background-color: #99cccc;">
                                &nbsp;<asp:Label ID="labDetailInfo" runat="server" ForeColor="Fuchsia"></asp:Label><br />
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle; width: 50px; background-color: #ccff99; text-align: center">Hold Code</td>
                            <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; width: 150px; background-color: yellow;">
                                <asp:Label ID="labHoldCode" runat="server" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: middle; width: 50px; background-color: #ccff99; text-align: center">
                                Contact Engineer</td>
                            <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                border-left: gray 1px solid; width: 150px; border-bottom: gray 1px solid; background-color: yellow">
                                <asp:Label ID="labEng" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="1" style="border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid; border-bottom: gray 1px solid; background-color: #ccff99; width: 30px;"
                                valign="bottom">
                                交<br />
                                代<br />
                                事<br />
                                項</td>
                            <td align="center" colspan="3" valign="bottom" style="border-right: gray 1px solid; border-top: gray 1px solid; vertical-align: middle; border-left: gray 1px solid; border-bottom: gray 1px solid; text-align: center; width: 620px; background-color: #cccccc;">
                                <div id="div2" runat="server" style="height: 50px; vertical-align: text-bottom; text-align: center; background-color: #cccccc;" >
                                    <br />
                                <asp:Label ID="labComment" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></div></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" style="background-color: #ccff99">
                                <table style="width: 100%; border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid;
                                    border-bottom: gray thin solid">
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Device</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="labDevice" runat="server" ForeColor="Fuchsia"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            Batch No.</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="labBatchNr" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Suffix</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="labSuffix" runat="server"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            Diffusion Lot</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="labDifussion" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            12NC</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="lab12Nc" runat="server"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            Platform</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="labPlatform" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Temperture</td>
                                        <td align="center" style="width: 110px; height: 21px; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid;
                                            color: gray; border-bottom: gray 1px solid; text-align: center;">
                                            <asp:Label ID="labTemp" runat="server"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            Stage</td>
                                        <td align="center" style="width: 110px; height: 21px; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid;
                                            color: gray; border-bottom: gray 1px solid; text-align: center;">
                                            <asp:Label ID="labStage" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px;">
                                            Budget Yield</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="labBudgetYield" runat="server" ForeColor="#C000C0"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            Actual Yield</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: center;">
                                            <asp:Label ID="labActualYield" runat="server" ForeColor="#C000C0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 110px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            Yield 100%</td>
                                        <td colspan="3" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 540px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99; text-align: left">
                                            <asp:Label ID="labAYHCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 110px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            Open/Short</td>
                                        <td colspan="3" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 540px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99; text-align: left">
                                            <asp:Label ID="labOpenShortCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Handler Check</td>
                                        <td colspan="3" style="width: 540px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: left;">
                                            <asp:Label ID="labHandlerCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Bin SPC Check</td>
                                        <td colspan="3" style="width: 540px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; text-align: left;">
                                            <asp:Label ID="labBinSpcCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 110px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            QA Sampling</td>
                                        <td colspan="3" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 540px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99; text-align: left">
                                            <asp:Label ID="labQACheck" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </div>
                </asp:Panel>
                <asp:SqlDataSource ID="sqlBatchInfo" runat="server" ConnectionString="<%$ ConnectionStrings:SUM %>"
                    ProviderName="<%$ ConnectionStrings:SUM.ProviderName %>"></asp:SqlDataSource>
                <asp:SqlDataSource ID="sqlLimit" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
                    ProviderName="<%$ ConnectionStrings:BRAT.ProviderName %>"></asp:SqlDataSource>
                <asp:HiddenField ID="hiddStage" runat="server" /><asp:HiddenField ID="hiddProduct" runat="server" /><asp:HiddenField ID="hiddSpecStage" runat="server" /><asp:HiddenField ID="hiddDevice" runat="server" />
                <asp:HiddenField ID="hiddSelectRow" runat="server" />
                <asp:HiddenField ID="hiddIntrackTypeName" runat="server" />
                <asp:HiddenField ID="hiddIntrack12NC" runat="server" />
                <asp:HiddenField ID="hiddFab" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        &nbsp;<br />
        <br />
        <br />
        &nbsp;
    </div>
</asp:Content>
