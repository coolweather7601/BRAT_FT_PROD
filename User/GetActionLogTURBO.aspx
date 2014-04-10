<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetActionLogTURBO.aspx.cs" Inherits="GetActionLogTURBO" Title="BR@T" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Limit Case Check</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
        </div>
           <div align="center" style="background-color: #ffffcc">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                &nbsp;
                <asp:GridView ID="gvBatchInfo" runat="server" BackColor="#CCFF99" BorderColor="DodgerBlue"
                    BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" AutoGenerateColumns="False"
                    Font-Size="X-Small" OnRowDataBound="gvBatchInfo_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="OP">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DF_LOTID" HeaderText="DIFF" >
                            <ItemStyle Width="10%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT" HeaderText="PRODUCT" >
                            <ItemStyle Width="14%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="START_DATE" HeaderText="DATE" >
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
                        <asp:BoundField DataField="PASS_QTY" HeaderText="PASS" >
                            <ItemStyle Width="6%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TESTED_QTY" HeaderText="TOTAL" >
                            <ItemStyle Width="8%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEST_CODE" HeaderText="CODE" >
                            <ItemStyle Width="8%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEST_STAGE" HeaderText="STAGE" >
                            <ItemStyle Width="6%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="FILE_LOCATION,FILE_NAME" DataNavigateUrlFormatString="http://atoweb1.tw-khh01.nxp.com/cgi-bin/Paperless/TURBO_readRawData.pl/{0}/{1}"
                            DataTextField="TESTER" HeaderText="TESTER" Target="_blank" >
                            <ItemStyle Width="15%" HorizontalAlign="Center" Wrap="False" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="rowid" HeaderText="RowID" />
                    </Columns>
                    <FooterStyle BackColor="Tan" />
                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="Gold" ForeColor="GhostWhite" />
                    <HeaderStyle BackColor="#6633CC" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#CCFF99" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
                <asp:SqlDataSource ID="sqlBatchInfo" runat="server" ConnectionString="<%$ ConnectionStrings:SUM %>"
                    ProviderName="<%$ ConnectionStrings:SUM.ProviderName %>"></asp:SqlDataSource>
                &nbsp; &nbsp;&nbsp;<br />
                <asp:SqlDataSource ID="sqlLimit" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
                    ProviderName="<%$ ConnectionStrings:BRAT.ProviderName %>"></asp:SqlDataSource>
                &nbsp;<br />
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
                                <div id="div1" runat="server" style="height: 120px; vertical-align: text-bottom; text-align: center;" >
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
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labDevice" runat="server" ForeColor="Fuchsia"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            Batch No.</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labBatchNr" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Suffix</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labSuffix" runat="server"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            Diffusion Lot</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labDifussion" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            12NC</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="lab12Nc" runat="server"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            Platform</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labPlatform" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Temperture</td>
                                        <td align="center" style="width: 110px; height: 21px; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid;
                                            color: gray; border-bottom: gray 1px solid; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labTemp" runat="server"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            Stage</td>
                                        <td align="center" style="width: 110px; height: 21px; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid;
                                            color: gray; border-bottom: gray 1px solid; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labStage" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 110px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            Total Qty</td>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; vertical-align: middle; border-left: gray 1px solid; width: 110px;
                                            color: gray; border-bottom: gray 1px solid; height: 21px; background-color: #ccff99;
                                            text-align: center">
                                            <asp:Label ID="labTotal" runat="server"></asp:Label></td>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; vertical-align: middle; border-left: gray 1px solid; width: 110px;
                                            color: gray; border-bottom: gray 1px solid; height: 21px; background-color: #ccff99;
                                            text-align: center">
                                            Pass Qty</td>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; vertical-align: middle; border-left: gray 1px solid; width: 110px;
                                            color: gray; border-bottom: gray 1px solid; height: 21px; background-color: #ccff99;
                                            text-align: center">
                                            <asp:Label ID="labPass" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px;">
                                            Budget Yield</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labBudgetYield" runat="server" ForeColor="#C000C0"></asp:Label></td>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; background-color: #ccff99;
                                            border-right: gray 1px solid; border-top: gray 1px solid; border-left: gray 1px solid;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            Actual Yield</td>
                                        <td align="center" style="width: 110px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px; vertical-align: middle; text-align: center;">
                                            <asp:Label ID="labActualYield" runat="server" ForeColor="#C000C0"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 110px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            Yield 100%</td>
                                        <td colspan="3" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 540px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            <asp:Label ID="labAYHCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 110px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            Open/Short</td>
                                        <td colspan="3" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 540px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            <asp:Label ID="labOpenShortCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Handler Check</td>
                                        <td colspan="3" style="width: 540px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px;">
                                            <asp:Label ID="labHandlerCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-size: 8pt; width: 110px; color: gray; height: 21px;
                                            background-color: #ccff99; border-right: gray 1px solid; border-top: gray 1px solid;
                                            border-left: gray 1px solid; border-bottom: gray 1px solid;">
                                            Bin SPC Check</td>
                                        <td colspan="3" style="width: 540px; background-color: #ccff99; border-right: gray 1px solid;
                                            border-top: gray 1px solid; font-size: 8pt; border-left: gray 1px solid; color: gray;
                                            border-bottom: gray 1px solid; height: 21px;">
                                            <asp:Label ID="labBinSpcCheck" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 110px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            QA Sampling</td>
                                        <td colspan="3" style="border-right: gray 1px solid; border-top: gray 1px solid;
                                            font-size: 8pt; border-left: gray 1px solid; width: 540px; color: gray; border-bottom: gray 1px solid;
                                            height: 21px; background-color: #ccff99">
                                            <asp:Label ID="labQACheck" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </div>                <br />
                <asp:HiddenField ID="hiddDevice" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
        <br />
    </form>
</body>
</html>
