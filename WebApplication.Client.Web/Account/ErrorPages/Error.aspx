<%@ Page Title = "Error" Language="C#" MasterPageFile="~/Account/MP/LoginMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WebApplication.Client.Web.Account.ErrorPages.Error" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Header" runat="server" ContentPlaceHolderID="HeadContent">
    <title></title>
        <style type="text/css">
        .estiloError
        {
            background-color: #023185;
            border-width: 1px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            position: center;
            font-family: Arial, Sans-Serif;
        }
        .style26
        {
            width: 100%;
            height: 100%;
        }
        .style30
        {
            height: 111px;
        }
        .style32
        {
            height: 111px;
            width: 149px;
        }
        .style31
        {
            width: 154px;
            height: 111px;
        }
        .style33
        {
            width: 149px;
        }
        .style29
        {
            width: 154px;
        }
        .style34
        {
            width: 35px;
        }
        .style24
        {
            height: 158px;
        }
        .style28
        {
            width: 164px;
        }
        .style27
        {
            width: 139px;
        }
        .style25
        {
            width: 35px;
            height: 158px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="text-align:center;">
                <asp:Panel ID="pnlErrorProceso" runat="server" CssClass="estiloError" Width="531px"
                    BackImageUrl="~/ErrorPages/Images/ErrorAsp.gif" Height="437px">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td class="style34">
                            </td>
                            <td>
                                <table class="style26">
                                    <tr>
                                        <td class="style30">
                                        </td>
                                        <td class="style32">
                                        </td>
                                        <td style="text-align:center;" class="style31" valign="top">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Red" Text="Se ha generado un error... Comuniquese con el Administrador de la Aplicación"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="style33">
                                            &nbsp;
                                        </td>
                                        <td class="style29">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="style33">
                                            &nbsp;
                                        </td>
                                        <td class="style29">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style34">
                            </td>
                        </tr>
                        <tr>
                            <td class="style24">
                            </td>
                            <td class="style24">
                                <table class="style26">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="style28">
                                            &nbsp;
                                        </td>
                                        <td class="style27">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="style28">
                                            &nbsp;
                                        </td>
                                        <td class="style27">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="style28">
                                            &nbsp;
                                        </td>
                                        <td class="style27">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style25">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
