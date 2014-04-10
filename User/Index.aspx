<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="User_Index" Title="Br@T Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FartherMain" runat="Server">
    <div style="text-align: center;">
        <table style="background-color: #ffffff">
            <tr align="left" valign="middle">
                <td style="height: 150px; vertical-align: top;">
                    <asp:ImageButton ID="Image2" runat="server" ImageUrl="~/images/BRAT.png" AlternateText="BR@T"
                        ImageAlign="left" />
                </td>
                <td valign="top" style="height: 150px">
                    <br />
                    <ol>
                        <li>An expert system<br />
                            - Engineer experience input & output </li>
                        <li>Release with better quality control</li>
                        <li>Cut hold rate<br />
                            - Dynamic yield control<br />
                            - Fail items control<br />
                            - Bin Values control </li>
                    </ol>
                </td>
            </tr>
            <tr align="left" valign="middle">
                <td colspan="2" style="height: 150px; vertical-align: top;">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/BRAT_Quality.png"
                        AlternateText="BR@T" ImageAlign="Middle" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
