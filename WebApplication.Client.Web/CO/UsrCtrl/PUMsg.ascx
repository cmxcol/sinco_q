<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PUMsg.ascx.cs" Inherits="WebApplication.Client.Web.CO.UsrCtrl.PUMsg" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" %>
<asp:Panel ID="PnlPU" runat="server" Visible="true" CssClass="modalPopup" Width="510px">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <table class = "style1">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="lblPUMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
