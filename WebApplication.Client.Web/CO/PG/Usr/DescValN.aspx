<%@ Page Title="Validación" Language="C#" MasterPageFile="~/CO/MP/UsrP.Master" AutoEventWireup="true" CodeBehind="DescValN.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Usr.DescValN" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SMaster.css" rel="stylesheet" />
    <link href="../../Styles/SPublic.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-2.1.4.js"></script>
    <script src="DescVal.js"></script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .overlay {
            position: fixed;
            z-index: 98;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: white;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .overlayContent {
            z-index: 99;
            margin: 250px auto;
            width: 45px;
            height: 45px;
        }

            .overlayContent h2 {
                font-size: 18px;
                font-weight: bold;
                color: #000;
            }

            .overlayContent img {
                width: 45px;
                height: 45px;
            }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%" align="right">
                <asp:Label ID="lblSeg" runat="server" Text="Segmentación" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%" align="left">
                            <select id="Seg">
                            </select>
                        </td>
                        <td style="text-align:left;" width="60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%" align="right">
                <asp:Label ID="lblShare" runat="server" Text="Share Of Wallet" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%" align="left">
                            <select id="Share">
                            </select>
                        </td>
                        <td style="text-align:left;" width="60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%" align="right">
                <asp:Label ID="lblLogis" runat="server" Text="Logística" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%" align="left">
                            <select id="Logis">
                            </select>
                        </td>
                        <td style="text-align:left;" width="60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%" align="right">
                <asp:Label ID="lblTObra" runat="server" Text="Tipo Obra" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%" align="left">
                            <select id="TObra">
                            </select>
                        </td>
                        <td style="text-align:left;" width="60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td></td>
            <td style="text-align:center;" colspan="2">
                <button id="Validar" type="button">Validar</button>
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
             <td></td>
            <td style="text-align:center;" colspan="2">
            <label  id="Desc" align="center" style="font-size: 20px; color: #ff0000;display:none"> </label>
                 </td>
            <td>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
