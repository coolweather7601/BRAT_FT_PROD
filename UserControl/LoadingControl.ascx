<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadingControl.ascx.cs"  Inherits="UserControl_LoadingControl" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<table style="background-image: url(../Images/Login.jpg); width: 220px; height: 117px" border="0" cellpadding="0" cellspacing="0" runat="server" id="tabLoading"><TBODY><TR><TD 
    style="WIDTH: 220px; HEIGHT: 117px" vAlign=top align=center><TABLE 
      style="FONT-SIZE: 9pt; WIDTH: 178px; HEIGHT: 90px"><TBODY><TR 
        style="FONT-SIZE: 9pt; WIDTH: 152px; vAlign: center"><TD>UserName：</TD><TD 
          style="WIDTH: 170px"><asp:TextBox id="txtName" runat="server" height="12px" width="79px"></asp:TextBox></TD></TR><TR 
        style="FONT-SIZE: 9pt; WIDTH: 152px; vAlign: center"><TD 
          style="WIDTH: 78px; vAlign: center">PassWord：</TD><TD 
          style="WIDTH: 170px"><asp:TextBox id="txtPassword" runat="server" height="12px" width="79px" textmode="Password"></asp:TextBox></TD></TR><TR><TD 
          style="HEIGHT: 23px" colSpan=2><CENTER><asp:Button id="btnLoad" onclick="btnLoad_Click" runat="server" CausesValidation="False" text="Login"></asp:Button> 
            </CENTER></TD></TR></TBODY></TABLE></TD></TR></TBODY></table><table style="background-image: url(../Images/Login.jpg); width: 220px; height: 117px; font-size: 9pt;" runat="server" id="tabLoad" visible="false" border="0" cellpadding="0" cellspacing="0"><TBODY><TR><TD 
    style="WIDTH: 220px; HEIGHT: 117px" vAlign=top align=center><BR /><BR 
      /><TABLE 
          style="FONT-SIZE: 9pt; WIDTH: 178px; HEIGHT: 50px"><TBODY><TR><TD 
          style="WIDTH: 174px" colSpan=2>&nbsp; Welcome<U><%=Session["UserName"]%></U></TD></TR><TR><TD 
          style="WIDTH: 174px" colSpan=2><CENTER>&nbsp;<asp:LinkButton id="LinkBtnLougout" onclick="LinkBtnLougout_Click" runat="server" __designer:wfdid="w170">Logout</asp:LinkButton></CENTER></TD></TR></TBODY></TABLE></TD></TR></TBODY></table>
