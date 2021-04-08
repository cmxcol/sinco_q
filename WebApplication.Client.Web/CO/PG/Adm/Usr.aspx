<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Account/MP/LoginMaster.Master"
    AutoEventWireup="true" CodeBehind="Usr.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Adm.Usr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SPublic.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td style="width:15%">
            </td>
            <td style="width:25%" align="right">
                <asp:Label ID="lblRep" runat="server" Font-Bold="True" Text="Acción:" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:25%" align="left">
                <asp:DropDownList ID="ddlRep" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRep_SelectedIndexChanged"
                    CssClass="ddlGV">
                    <asp:ListItem Value="0">Seleccione</asp:ListItem>
                    <asp:ListItem Value="1">Crear</asp:ListItem>
                    <asp:ListItem Value="2">Actualizar</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:25%">
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gv_Usr" runat="server" CssClass="gvAlertas" HeaderStyle-CssClass="gvHead1"
                    Visible="false" AllowPaging="True" PageSize="10" OnPageIndexChanging="gv_Usr_PageIndexChanging">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Panel ID="pnlRegAct" runat="server" Visible="false">
                    <table style="width:100%;">
                        <tr>
                            <td style="width:25%" class="style1">
                            </td>
                            <td colspan="2" align="center" class="style1">
                                <asp:Label ID="lblRegG" runat="server" CssClass="labelInfo" Text="Ingrese los Datos Requeridos"
                                    Visible="false"></asp:Label>
                            </td>
                            <td style="width:25%" class="style1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width:15%" align="right">
                                <asp:Label ID="lblCemexID" runat="server" Font-Bold="True" Text="CemexID:" CssClass="labelRq"></asp:Label>
                            </td>
                            <td style="width:25%" align="left">
                                <asp:TextBox ID="txtCemexID" runat="server" Visible="true" Wrap="False" AutoPostBack="True"
                                    CssClass="txtrq" Enabled="true" OnTextChanged="txtCemexID_TextChanged"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtIdFac_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" TargetControlID="txtCemexID" InvalidChars="1234567890" FilterMode="InvalidChars">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    CssClass="ValidatorExpression" ControlToValidate="txtCemexID" ValidationGroup="User"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblNom" runat="server" Font-Bold="True" Text="Nombre:" CssClass="labelRq"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtNom" runat="server" Visible="true" Wrap="False" AutoPostBack="False"
                                    CssClass="txtrq3" Width="240px"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtNom_FilteredTextBoxExtender" runat="server" Enabled="True"
                                    TargetControlID="txtNom" InvalidChars="1234567890" FilterMode="InvalidChars">
                                </cc1:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNom"
                                    CssClass="ValidatorExpression" ErrorMessage="*" ValidationGroup="User"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblEmail" runat="server" Font-Bold="True" Text="Email:" CssClass="labelRq"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtEmail" runat="server" Visible="true" Wrap="False" AutoPostBack="False"
                                    CssClass="txtrq3" Width="240px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail"
                                    CssClass="ValidatorExpression" ErrorMessage="*" ValidationGroup="User"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblRol" runat="server" Font-Bold="True" Text="Rol:" CssClass="labelRq"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:DropDownList ID="ddlRol" runat="server" Visible="true" Width="150px" CssClass="ddlGV">
                                    <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:right;">
                                <asp:Label ID="lblEstado" runat="server" Font-Bold="True" Text="Estado:" CssClass="labelRq"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:DropDownList ID="ddlStaRs" runat="server" Visible="true" Width="150px" CssClass="ddlGV">
                                    <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="btnRq" Height="20px" Text="Guardar"
                                    Visible="true" Width="100px" OnClick="btnGuardar_Click" ValidationGroup="User" />
                                <asp:Button ID="btnNuevo" runat="server" CssClass="btnRq" Height="20px" Text="Nuevo"
                                    Visible="true" Width="100px" OnClick="btnNuevo_Click" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblResp" runat="server" CssClass="labelInfo" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblError" runat="server" CssClass="labelError" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;
            </td>
        </tr>
    </table>
        <div class="Bottom">
    </div>
</asp:Content>
