<%@ Control Language="C#" AutoEventWireup="true" CodeFile="navigationControl.ascx.cs" Inherits="UserControl_navigationControl" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
 <%-- <%@ OutputCache Duration ="15" VaryByParam="None" %>--%><table  style="width: 220px; height: 549px; font-size: 9pt; vertical-align :top ;"  background ="../Images/LeftBackGround.jpg" border="0" cellpadding="0" cellspacing="0" ><TBODY><TR vAlign=top 
      align=center><TD><BR /><BR /><BR /><BR /><BR />
      <asp:DataList id="DLPlatform" oneditcommand="DLPlatform_EditCommand" runat="server" DataKeyField="platform">
      <ItemTemplate>
                    <table >
                        <tr>
                            <td align ="left" style ="width :80px; font-size: 9pt; " > 
                            <asp:LinkButton ID="lnkbtnClass" runat="server" commandname="Edit" CausesValidation="False" ><%# DataBinder.Eval(Container.DataItem, "platform")%></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                
</ItemTemplate>
</asp:DataList> 
</TD></TR></TBODY></table>

