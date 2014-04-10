<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="DisposalList.aspx.cs" Inherits="DisposalList" Title="BR@T" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="FartherMain" runat="server">

    <table style="width: 600px; height: 50px">
        <tr>
            <td colspan="2" style="height: 34px">
               <div class="demoarea">
                <asp:Panel ID="Panel2" runat="server" CssClass="collapsePanelHeader" Height="30px"> 
                   <div style="padding:5px; cursor: pointer; vertical-align: middle;">
                     <div style="float: left;" >
                         Tip</div>
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
                             <p>Disposal list is the result of batch disposal from production floor base on real-time basese. </p>
                            <p>Engineers can easily utilize list and quick response to those hold batch.</p>
                            <div align="left"></div>
                        </td>
                        <td>
                            <asp:ImageButton ID="Image2" runat="server" ImageUrl="~/images/BRAT.png" AlternateText="ASP.NET AJAX" ImageAlign="right" />
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
                    
    <table style="border-left-color: blue; border-bottom-color: blue; border-top-style: solid; border-top-color: blue; border-right-style: solid; border-left-style: solid; border-right-color: blue; border-bottom-style: solid" border="1">
        <tr>
            <td style="width: 196px">
                <asp:Label ID="LABBatch" runat="server" Text="Batch Number"></asp:Label></td>
            <td style="width: 396px">
                <asp:TextBox ID="txtBatch" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 196px">
                <asp:Label ID="LABType" runat="server" Text="Type Name"></asp:Label></td>
            <td style="width: 396px">
                <asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 196px">
                <asp:Label ID="LABPlatform" runat="server" Text="Tester Platform"></asp:Label></td>
            <td style="width: 396px">
                <asp:DropDownList ID="ddlPlatform" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 196px">
                <asp:Label ID="LABLookFor" runat="server" Text="Look for"></asp:Label></td>
            <td style="width: 396px">
                <asp:DropDownList ID="ddlOpAct" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 196px">
            <asp:Label ID="LABPeriod" runat="server" Text="Period ......(yyyy-mm-dd)"></asp:Label></td>
            <td style="width: 396px">
                    <asp:TextBox runat="server" ID="txtDateFrom" /><asp:ImageButton runat="Server" ID="IMGFrom" ImageUrl="~/images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" /><asp:TextBox runat="server" ID="txtDateTo" /><asp:ImageButton runat="Server" ID="IMGTo" ImageUrl="~/images/Calendar_scheduleHS.png" AlternateText="Click to show calendar" /></td>
        </tr>
        <tr>
            <td colspan="2" style="height: 26px">
                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                <asp:Button ID="O_Output" runat="server" OnClick="O_Output_Click" Text="O_Output" />
                <asp:Button ID="P_Output" runat="server" OnClick="P_Output_Click" Text="P_Output" />
            </td>
        </tr>
    </table>    
        <cc1:CalendarExtender ID="calendarButtonExtender1" runat="server" TargetControlID="TXTDateFrom" Format="yyyy-MM-dd" PopupButtonID="IMGFrom">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="calendarButtonExtender2" runat="server" TargetControlID="TXTDateTo" Format="yyyy-MM-dd" PopupButtonID="IMGTo">
        </cc1:CalendarExtender>
            &nbsp;&nbsp;<br />
            &nbsp;
            
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
            
            <asp:GridView id="GridView_O" AutoGenerateColumns="False" runat="server"
                 BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                 BorderStyle="None" Font-Size="XX-Small">
            <Columns>
                <asp:BoundField HeaderText="batch" datafield="batch"/>
                <asp:BoundField HeaderText="test_code" datafield="test_code" />
                <asp:BoundField HeaderText="p_count" datafield="p_count" />
                <asp:BoundField HeaderText="o_count" datafield="o_count" />
                <asp:BoundField HeaderText="max_Date" datafield="max_date" />
            </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingRowStyle BackColor="Gainsboro" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
            
            <asp:GridView id="GridView_P" AutoGenerateColumns="False" runat="server"
                 BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                 BorderStyle="None" Font-Size="XX-Small">
            <Columns>
                <asp:BoundField HeaderText="batch" datafield="batch"/>
                <asp:BoundField HeaderText="test_code" datafield="test_code" />
                <asp:BoundField HeaderText="test_stage" datafield="test_stage" />
                <asp:BoundField HeaderText="p_count" datafield="p_count" />
                <asp:BoundField HeaderText="max_Date" datafield="max_date" />
            </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingRowStyle BackColor="Gainsboro" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
                
            </asp:GridView>
            <asp:GridView ID="GVDisposal2" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                OnRowDataBound="GVDisposal2_RowDataBound" BorderStyle="None" Font-Size="XX-Small" 
                AllowPaging="True" OnPageIndexChanging="GVDisposal2_PageIndexChanging" PageSize="15">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="BATCH,PASS,TOTAL,SUMROWID,DEVICE,DIFF,COMMIT_DATE_40,COMMIT_DATE,PLATFORM,STAGE,TYPENAME,FAB,SUM_STAGE,QA_ID"
                        DataNavigateUrlFormatString="GetActionLog{8}.aspx?Batch={0}&amp;PassQty={1}&amp;TotalQty={2}&amp;RowID={3}&amp;DeviceID={4}&amp;DIFF={5}&amp;FromDate={6}&amp;EndDate={7}&amp;Platform={8}&amp;Stage={9}&amp;TypeName={10}&amp;Fab={11}&amp;SumStage={12}&amp;QaId={13}"
                        DataTextField="BATCH" HeaderText="BATCH" Target="_blank">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="PASS" HeaderText="PASS">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FAIL" HeaderText="FAIL">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TOTAL" HeaderText="TOTAL">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STATUS" HeaderText="STATUS">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STAGE" HeaderText="STAGE" />
                    <asp:BoundField DataField="DEVICE_NAME" HeaderText="TYPE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUFFIX" HeaderText="SUFFIX">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DIFF" HeaderText="DIFF">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PLATFORM" HeaderText="PLATFORM">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TESTER" HeaderText="TESTER" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="COMMIT_DATE_FULL" HeaderText="DATE">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="SPCMessage">
                        <ItemTemplate>
                            <asp:Label ID="labSPCurl" runat="server"></asp:Label>
                            <asp:HiddenField ID="hiddSpcLog" runat="server" Value='<%# Bind("spc_log") %>' />
                            <asp:HiddenField ID="hiddMisc" runat="server" Value='<%# String.Format("{0}|{1}|{2}|{3}", Eval("typename"), Eval("fab"), Eval("batch"), Eval("commit_date_1")) %>' />
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="FA Num" />
                    <asp:BoundField HeaderText="FA In" />
                    <asp:BoundField HeaderText="FA Out" />
                    <asp:BoundField HeaderText="FA Status" />
                    <asp:BoundField HeaderText="FA Engineer" />
                    <asp:BoundField HeaderText="FA Result" />
                    <asp:TemplateField HeaderText="eNC">
                        <ItemTemplate>
                            <asp:Label ID="labEncUrl" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resp.Dept">
                        <ItemTemplate>
                            <asp:Label ID="labRespDept" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="eNC Status">
                        <ItemTemplate>
                            <asp:Label ID="labEncStatus" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status Date">
                        <ItemTemplate>
                            <asp:Label ID="labStatusDate" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Defect Code">
                        <ItemTemplate>
                            <asp:Label ID="labDefectCode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <AlternatingRowStyle BackColor="Gainsboro" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
