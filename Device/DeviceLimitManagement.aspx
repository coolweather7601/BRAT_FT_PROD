<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeviceLimitManagement.aspx.cs"
    Inherits="Device_DeviceLimitManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Limit Management</title>
    <link type="text/css" rel="stylesheet" href="../App_Themes/makeover/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="1400px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Limit">
                <ContentTemplate>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div>
                                <asp:HiddenField ID="hiddGroupID" runat="server" />
                                <asp:HiddenField ID="hiddDeviceID" runat="server" />
                                <br />
                                <asp:GridView ID="gvLimit" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDevicePool2"
                                    OnRowDataBound="gvLimit_RowDataBound" BackColor="White" BorderColor="White" BorderStyle="Ridge"
                                    BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" DataKeyNames="ID"
                                    OnRowUpdating="gvLimit_RowUpdating" OnRowCommand="gvLimit_RowCommand" ShowFooter="True"
                                    OnPreRender="gvLimit_PreRender" Font-Size="X-Small" OnRowCreated="gvLimit_RowCreated"
                                    AllowSorting="True" CssClass="mGrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="變更" ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Font-Size="X-Small" Text="Update" />
                                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Font-Size="X-Small" Text="Cancel" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Font-Size="X-Small" Text="Edit" />
                                                <asp:Button ID="btnDel" runat="server" CommandName="Delete" Font-Size="X-Small" Text="Del" OnClientClick="return confirm('Are you sure you want to delete this data？');" />
                                                <asp:CheckBoxList ID="cblSelLimit" runat="server" Font-Size="XX-Small">
                                                     <asp:ListItem></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="Insert" Font-Size="X-Small" /><br />
                                                <asp:Button ID="btnUpdateAll" runat="server" CommandName="UpdateAll" Text="Update" Font-Size="X-Small" /><asp:CheckBox ID="cbSelAll" runat="server" AutoPostBack="True" OnCheckedChanged="cbSelAll_CheckedChanged" Text="Select All" />
                                                <br />
                                                <asp:Button ID="btnOScheck" runat="server" CommandName="OS_Check" /><br />
                                                <asp:Label ID="labOSwarning" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                                <asp:HiddenField ID="hiddOScheck" runat="server" />
                                            </FooterTemplate>
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="優先" SortExpression="PIORITY">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPiorityEdit" runat="server" Text='<%# Bind("PIORITY") %>' Width="20px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("PIORITY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPiorityInst" runat="server" Text='<%# Bind("PIORITY") %>' Width="20px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="條件" SortExpression="CRITERION">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCriterionEdit" runat="server" Text='<%# Bind("CRITERION") %>'
                                                    Width="90px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers, UppercaseLetters"
                                                    TargetControlID="txtCriterionEdit" ValidChars="+,">
                                                </cc1:FilteredTextBoxExtender>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("CRITERION") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCriterionInst" runat="server" Text='<%# Bind("CRITERION") %>'
                                                    Width="90px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers, UppercaseLetters"
                                                    TargetControlID="txtCriterionInst" ValidChars="+,">
                                                </cc1:FilteredTextBoxExtender>
                                            </FooterTemplate>
                                            <ItemStyle Width="90px" />
                                            <HeaderStyle Width="90px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="比較" SortExpression="EQUATION">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEquListEdit" runat="server" SelectedValue='<%# Bind("EQUATION") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                    <asp:ListItem>=</asp:ListItem>
                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("EQUATION") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlEquListInst" runat="server" SelectedValue='<%# Bind("EQUATION") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                    <asp:ListItem>=</asp:ListItem>
                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="值" SortExpression="QTY">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQtyEdit" runat="server" Text='<%# Bind("QTY") %>' Width="30px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtQtyInst" runat="server" Text='<%# Bind("QTY") %>' Width="30px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="單位" SortExpression="UNIT">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlUnitEdit" runat="server" SelectedValue='<%# Bind("UNIT") %>'>
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Selected="True">%</asp:ListItem>
                                                    <asp:ListItem>pcs</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("UNIT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUnitInst" runat="server" SelectedValue='<%# Bind("UNIT") %>'>
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Selected="True">%</asp:ListItem>
                                                    <asp:ListItem>pcs</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="條件2" SortExpression="CRITERION2">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCriterionEdit2" runat="server" Text='<%# Bind("CRITERION2") %>'
                                                    Width="90px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers, UppercaseLetters"
                                                    TargetControlID="txtCriterionEdit2" ValidChars="+,">
                                                </cc1:FilteredTextBoxExtender>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCriterionInst2" runat="server" Text='<%# Bind("CRITERION2") %>'
                                                    Width="90px"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers, UppercaseLetters"
                                                    TargetControlID="txtCriterionInst2" ValidChars="+,">
                                                </cc1:FilteredTextBoxExtender>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("CRITERION2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="比較2" SortExpression="EQUATION2">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEquListEdit2" runat="server" SelectedValue='<%# Bind("EQUATION2") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                    <asp:ListItem>=</asp:ListItem>
                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label102" runat="server" Text='<%# Bind("EQUATION2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlEquListInst2" runat="server" SelectedValue='<%# Bind("EQUATION2") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                    <asp:ListItem>=</asp:ListItem>
                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="值2" SortExpression="QTY2">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQtyEdit2" runat="server" Text='<%# Bind("QTY2") %>' Width="30px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label112" runat="server" Text='<%# Bind("QTY2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtQtyInst2" runat="server" Text='<%# Bind("QTY2") %>' Width="30px"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="單位2" SortExpression="UNIT2">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlUnitEdit2" runat="server" SelectedValue='<%# Bind("UNIT2") %>'>
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Selected="True">%</asp:ListItem>
                                                    <asp:ListItem>pcs</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label122" runat="server" Text='<%# Bind("UNIT2") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlUnitInst2" runat="server" SelectedValue='<%# Bind("UNIT2") %>'>
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Selected="True">%</asp:ListItem>
                                                    <asp:ListItem>pcs</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="指示" SortExpression="INDICATE_ID">
                                            <EditItemTemplate>
                                                &nbsp;<asp:DropDownList ID="ddlCreateId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCreateId_SelectedIndexChanged">
                                                </asp:DropDownList><br />
                                                <asp:Label ID="labIndicationEdit" runat="server" Text='<%# Eval("ACTION") %>'></asp:Label>
                                                <asp:DropDownList ID="DDLIndication1" runat="server" SelectedValue='<%# Bind("ACTION") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="0">扣住</asp:ListItem>
                                                    <asp:ListItem Value="1">良出廢留</asp:ListItem>
                                                    <asp:ListItem Value="2">良出廢丟</asp:ListItem>
                                                    <asp:ListItem Value="3">重測廢品</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ACTION") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlIndicationInst" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndicationInst_SelectedIndexChanged"
                                                    OnInit="ddlIndicationInst_Init">
                                                </asp:DropDownList><br />
                                                <asp:Label ID="labIndicationInst" runat="server" Text='<%# Eval("ACTION") %>'></asp:Label>
                                                <asp:DropDownList ID="DDLIndication2" runat="server" SelectedValue='<%# Bind("ACTION") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="0">扣住</asp:ListItem>
                                                    <asp:ListItem Value="1">良出廢留</asp:ListItem>
                                                    <asp:ListItem Value="2">良出廢丟</asp:ListItem>
                                                    <asp:ListItem Value="3">重測廢品</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HOLD_CODE" SortExpression="HOLD_CODE">
                                            <EditItemTemplate>
                                                <asp:Label ID="labHoldCodeEdit" runat="server" Text='<%# Eval("HOLD_CODE") %>'></asp:Label>
                                                <asp:HiddenField ID="HidnHoldCodeEdit" runat="server" Value='<%# Bind("HOLD_CODE") %>' />
                                                <asp:DropDownList ID="DDLHoldCode1" runat="server" SelectedValue='<%# Bind("HOLD_CODE") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="BDE">BDE</asp:ListItem>
                                                    <asp:ListItem Value="BBH">BBH</asp:ListItem>
                                                    <asp:ListItem Value="BYM">BYM</asp:ListItem>
                                                    <asp:ListItem Value="BDM">BDM</asp:ListItem>
                                                    <asp:ListItem Value="BAM">BAM</asp:ListItem>
                                                    <asp:ListItem Value="AYA">AYA</asp:ListItem>
                                                    <asp:ListItem Value="AYT">AYT</asp:ListItem>
                                                    <asp:ListItem Value="AQA">AQA</asp:ListItem>
                                                    <asp:ListItem Value="AQE">AQE</asp:ListItem>
                                                    <asp:ListItem Value="AFE">AFE</asp:ListItem>
                                                    <asp:ListItem Value="AXL">AXL</asp:ListItem>
                                                    <asp:ListItem Value="AEF">AEF</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("HOLD_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="labHoldCodeInst" runat="server" Text='<%# Eval("HOLD_CODE") %>'></asp:Label>
                                                <asp:DropDownList ID="DDLHoldCode2" runat="server" SelectedValue='<%# Bind("HOLD_CODE") %>'>
                                                    <asp:ListItem Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="BDE">BDE</asp:ListItem>
                                                    <asp:ListItem Value="BBH">BBH</asp:ListItem>
                                                    <asp:ListItem Value="BYM">BYM</asp:ListItem>
                                                    <asp:ListItem Value="BDM">BDM</asp:ListItem>
                                                    <asp:ListItem Value="BAM">BAM</asp:ListItem>
                                                    <asp:ListItem Value="AYA">AYA</asp:ListItem>
                                                    <asp:ListItem Value="AYT">AYT</asp:ListItem>
                                                    <asp:ListItem Value="AQA">AQA</asp:ListItem>
                                                    <asp:ListItem Value="AQE">AQE</asp:ListItem>
                                                    <asp:ListItem Value="AFE">AFE</asp:ListItem>
                                                    <asp:ListItem Value="AXL">AXL</asp:ListItem>
                                                    <asp:ListItem Value="AEF">AEF</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="註解" SortExpression="COMMENTS">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCmtEdit" runat="server" BackColor="WhiteSmoke" Height="50px"
                                                    Text='<%# Bind("COMMENTS") %>' TextMode="MultiLine"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("COMMENTS") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCmtInst" runat="server" BackColor="WhiteSmoke" Height="50px"
                                                    Text='<%# Eval("COMMENTS") %>' TextMode="MultiLine"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Indication">
                <ContentTemplate>
                    &nbsp;<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvTemplate" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2"
                                DataSourceID="SqlIndication" Font-Size="X-Small" AllowSorting="True" Height="84px"
                                ShowFooter="True" DataKeyNames="INDICATE_ID" OnRowDataBound="gvTemplate_RowDataBound"
                                OnRowUpdating="gvTemplate_RowUpdating" OnRowCommand="gvTemplate_RowCommand" OnPreRender="gvTemplate_PreRender"
                                OnLoad="gvTemplate_Load">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Command" ShowHeader="False">
                                        <EditItemTemplate>
                                            &nbsp;
                                            <asp:Button ID="BtnUpdate" runat="server" CommandName="Update" Font-Size="X-Small"
                                                Text="Update" />
                                            <asp:Button ID="BtnCancel" runat="server" CommandName="Cancel" Font-Size="X-Small"
                                                Text="Cancel" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="BtnInsert" runat="server" CommandName="Insert" Text="Insert" />
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            &nbsp;<asp:Button ID="BtnEdit" runat="server" CommandName="Edit" Font-Size="X-Small"
                                                Text="Edit" />
                                            <asp:Button ID="BtnDel" runat="server" CommandName="Delete" Font-Size="X-Small" OnClientClick="return confirm('Are you sure you want to delete this data？');"
                                                Text="Del" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="INDICATE_ID" HeaderText="ID" SortExpression="INDICATE_ID"
                                        ReadOnly="True" />
                                    <asp:TemplateField HeaderText="INDICATION" SortExpression="INDICATION">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DDLIndication1" runat="server" SelectedValue='<%# Bind("INDICATION") %>'>
                                                <asp:ListItem Value="0" Selected="True">扣住</asp:ListItem>
                                                <asp:ListItem Value="1">良出廢留</asp:ListItem>
                                                <asp:ListItem Value="2">良出廢丟</asp:ListItem>
                                                <asp:ListItem Value="3">重測廢品</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="labTempAction" runat="server" Text='<%# Bind("INDICATION") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="DDLIndication2" runat="server" SelectedValue='<%# Bind("INDICATION") %>'>
                                                <asp:ListItem Value="0" Selected="True">扣住</asp:ListItem>
                                                <asp:ListItem Value="1">良出廢留</asp:ListItem>
                                                <asp:ListItem Value="2">良出廢丟</asp:ListItem>
                                                <asp:ListItem Value="3">重測廢品</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HOLD_CODE" SortExpression="HOLD_CODE">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DDLHoldCode1" runat="server" SelectedValue='<%# Bind("HOLD_CODE") %>'>
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="BDE">(BDE)BL Delegate Hold</asp:ListItem>
                                                <asp:ListItem Value="BBH">(BBH)BL Request Hold</asp:ListItem>
                                                <asp:ListItem Value="BYM">(BYM)Maverick Low Yield</asp:ListItem>
                                                <asp:ListItem Value="BDM">(BDM)Maverick Delegation</asp:ListItem>
                                                <asp:ListItem Value="BAM">(BAM)Maverick Assembly</asp:ListItem>
                                                <asp:ListItem Value="AYA">(AYA)Open Short Fails</asp:ListItem>
                                                <asp:ListItem Value="AYT">(AYT)General Low Yield</asp:ListItem>
                                                <asp:ListItem Value="AQA">(AQA)Subcon Low Yield</asp:ListItem>
                                                <asp:ListItem Value="AQE">(AQE)QA Fails</asp:ListItem>
                                                <asp:ListItem Value="AFE">(AFE)Future Hold</asp:ListItem>
                                                <asp:ListItem Value="AXL">(AXL)修腳彎</asp:ListItem>
                                                <asp:ListItem Value="AEF">(AEF)過期或待實驗分析均須報廢</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="labTempHoldCode" runat="server" Text='<%# Bind("HOLD_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="DDLHoldCode2" runat="server" SelectedValue='<%# Bind("HOLD_CODE") %>'>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="BDE">(BDE)BL Delegate Hold</asp:ListItem>
                                                <asp:ListItem Value="BBH">(BBH)BL Request Hold</asp:ListItem>
                                                <asp:ListItem Value="BYM">(BYM)Maverick Low Yield</asp:ListItem>
                                                <asp:ListItem Value="BDM">(BDM)Maverick Delegation</asp:ListItem>
                                                <asp:ListItem Value="BAM">(BAM)Maverick Assembly</asp:ListItem>
                                                <asp:ListItem Value="AYA">(AYA)Open Short Fails</asp:ListItem>
                                                <asp:ListItem Value="AYT">(AYT)General Low Yield</asp:ListItem>
                                                <asp:ListItem Value="AQA">(AQA)Subcon Low Yield</asp:ListItem>
                                                <asp:ListItem Value="AQE">(AQE)QA Fails</asp:ListItem>
                                                <asp:ListItem Value="AFE">(AFE)Future Hold</asp:ListItem>
                                                <asp:ListItem Value="AXL">(AXL)修腳彎</asp:ListItem>
                                                <asp:ListItem Value="AEF">(AEF)過期或待實驗分析均須報廢</asp:ListItem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="COMMENTS" SortExpression="COMMENTS">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTempCmt1" runat="server" Text='<%# Bind("COMMENTS") %>' Width="208px"
                                                TextMode="MultiLine" OnDataBinding="txtTempCmt1_DataBinding" Height="56px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="labTempCmt" runat="server" Text='<%# Bind("COMMENTS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTempCmt2" runat="server" TextMode="MultiLine" Width="208px" Height="54px"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" BackColor="#C6C3C6" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" Font-Size="X-Small" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;
                </ContentTemplate>
                <HeaderTemplate>
                    Template action
                </HeaderTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>&nbsp;<br />
        <asp:SqlDataSource ID="SqlIndication" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" SelectCommand='SELECT "INDICATE_ID", "INDICATION", "HOLD_CODE", "COMMENTS", "OWNER" FROM "INDICATION_EXPERTISE" order by "INDICATE_ID" '
            UpdateCommand="UPDATE INDICATION_EXPERTISE SET INDICATION =:INDICATION, HOLD_CODE =:HOLD_CODE, COMMENTS =:COMMENTS where INDICATE_ID = :INDICATE_ID"
            DeleteCommand="DELETE FROM INDICATION_EXPERTISE WHERE INDICATE_ID = :INDICATE_ID"
            OnUpdating="SqlIndication_Updating">
            <UpdateParameters>
                <asp:Parameter Name="INDICATION" />
                <asp:Parameter Name="HOLD_CODE" />
                <asp:Parameter Name="COMMENTS" />
                <asp:Parameter Name="INDICATE_ID" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="INDICATE_ID" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlControl" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDevicePool2" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>" SelectCommand='SELECT ID, CRITERION, PIORITY, EQUATION, UNIT, QTY,CRITERION2, EQUATION2, UNIT2, QTY2,INDICATE_ID, ACTION, HOLD_CODE, COMMENTS, ENGINEER, PHONE, NEW_EXPERTISE_ID FROM  LIMIT_EXPERTISE order by PIORITY'
            UpdateCommand="UPDATE LIMIT_EXPERTISE SET CRITERION =:CRITERION, PIORITY =:PIORITY, EQUATION =:EQUATION, UNIT =:UNIT, QTY =:QTY, CRITERION2 =:CRITERION2, EQUATION2 =:EQUATION2, UNIT2 =:UNIT2, QTY2 =:QTY2,  ACTION=:ACTION, HOLD_CODE=:HOLD_CODE, COMMENTS=:COMMENTS where ID =:ID"
            DeleteCommand="DELETE FROM LIMIT_EXPERTISE WHERE ID=:ID" OnUpdating="SqlDevicePool2_Updating" OnDeleting="SqlDevicePool2_Deleting">
            <UpdateParameters>
                <asp:Parameter Name="CRITERION" />
                <asp:Parameter Name="PIORITY" />
                <asp:Parameter Name="EQUATION" />
                <asp:Parameter Name="UNIT" />
                <asp:Parameter Name="QTY" />
                <asp:Parameter Name="CRITERION2" />
                <asp:Parameter Name="EQUATION2" />
                <asp:Parameter Name="UNIT2" />
                <asp:Parameter Name="QTY2" />
                <asp:Parameter Name="ACTION" />
                <asp:Parameter Name="HOLD_CODE" />
                <asp:Parameter Name="COMMENTS" />
                <asp:Parameter Name="ID" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="ID" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlEngSrc" runat="server" ConnectionString="<%$ ConnectionStrings:Employee %>"
            ProviderName="<%$ ConnectionStrings:Employee.ProviderName %>" SelectCommand="SELECT EMP_NAME FROM EMP_NAME_LIST2 WHERE (DEPT_NAME2 LIKE 'WTF%' OR DEPT_NAME2 LIKE 'FTF%') AND (TITLE_NAME2 IN ('ACCOUNTANT A', 'ACCOUNTANT B', 'ADV. ENGR.', 'ASSOC. ENGR.', 'BUYER A', 'CHIEF ACCT. A', 'CHIEF BUYER', 'CHIEF PLANNER', 'CHIEF SPEC.', 'CONTROLLER', 'CONTROLLER B', 'DIRECTOR', 'ENGINEER', 'MANAGER', 'MANAGER A', 'MGR. B', 'PLANNER A', 'PRINCIPAL ENGINEER', 'PROJECT MGR. B', 'PROJECT MGR. A', 'SR. BUYER', 'SR. DIRECTOR', 'SR. ENGINEER B', 'SR. MANAGER', 'SR. PLANNER', 'SR. PRINCIPAL', 'SR. PROJECT MGR', 'SR. SECRETARY', 'SR. SPEC.', 'SR.ACCOUNTANT', 'SR. ENGINEER A', 'TECH. SUPER.', 'SR. IMPEX OFFICER')) order by EMP_NAME ">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlEventLog" runat="server" ConnectionString="<%$ ConnectionStrings:BRAT_TEST %>"
            ProviderName="<%$ ConnectionStrings:BRAT_TEST.ProviderName %>"></asp:SqlDataSource>
    </form>
</body>
</html>
