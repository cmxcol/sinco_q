<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Account/MP/LoginMaster.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication.Client.Web.Account.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Header" runat="server" ContentPlaceHolderID="HeadContent">
    <title></title>
    <link href="Style/SPublic.css" rel="stylesheet" />
    <style type="text/css">
        .style5
        {
            width: 100%;
        }
        .style6
        {
            width: 250px;
            height: 67px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="980px">
        <tr>
            <td style="width:500px" align="right">
                <img alt="" class="style6" src="Images/cemexLogo.jpg" />
            </td>
            <td class="pnlLogin">
                <asp:UpdatePanel ID="upPnlLogin" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlLogin" runat="server">
                            <table class="style5">
                                <tr>
                                    <td style="width:200px">
                                    </td>
                                    <td style="width:400px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:right;">
                                        <asp:Label ID="lblUsr" runat="server" AssociatedControlID="txtUsuario" CssClass="labelRq"
                                            Text="CemexID" Width="100px"></asp:Label>
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="txtLogin"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario"
                                            CssClass="ValidatorExpression" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                </tr>
                                <tr>
                                    <td style="text-align:right;">
                                        <asp:Label ID="lblPass" runat="server" AssociatedControlID="txtPass" CssClass="labelRq"
                                            Width="100px">Contraseña:</asp:Label>
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:TextBox ID="txtPass" runat="server" AUTOCOMPLETE="off" CssClass="txtLogin" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass"
                                            CssClass="ValidatorExpression" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <cc1:FilteredTextBoxExtender ID="Password_FiTxtExt" runat="server" Enabled="True"
                                            FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtPass"
                                            FilterMode="InvalidChars" InvalidChars="-._/+=">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:right;">
                                        <asp:Label ID="lblPa" runat="server" AssociatedControlID="txtPass" CssClass="labelRq"
                                            Width="100px" Text="Pais"></asp:Label>
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:DropDownList ID="ddlPa" runat="server" CssClass="ddlGVP">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:center;" colspan="2">
                                        <asp:Label ID="lblError" runat="server" CssClass="labelError"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btnRq" Height="20px" Text="Iniciar Sesión"
                                            Visible="true" Width="100px" OnClick="btnLogin_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
