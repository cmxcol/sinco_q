<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/CO/MP/UsrSp.Master" AutoEventWireup="true"
    CodeBehind="Reportes.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Usr.Reportes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SPublic.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 97%;">
        <tr>
            <td style="text-align:center;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="width:15%">
                        </td>
                        <td style="width:25%" align="right">
                            <asp:Label ID="lblRep" runat="server" Font-Bold="True" Text="Consulta:" CssClass="labelRq"></asp:Label>
                        </td>
                        <td style="width:25%" align="left">
                            <asp:DropDownList ID="ddlRep" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRep_SelectedIndexChanged"
                                CssClass="ddlGV">
                                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                <asp:ListItem Value="1">Programaciones</asp:ListItem>
                                <asp:ListItem Value="2">Ajustes</asp:ListItem>
                                <asp:ListItem Value="3">SincovsGinco</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:25%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <table style="width:100%;">
                    <tr>
                        <td style="text-align:center;" width="15%">
                        </td>
                        <td style="text-align:right;" width="25%">
                            <asp:RadioButton ID="rbFecha" runat="server" Text="Fechas" AutoPostBack="True" OnCheckedChanged="rbFecha_CheckedChanged"
                                Visible="false" />
                        </td>
                        <td style="text-align:left;width:20%;">
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                TargetControlID="txtIdFac" ValidChars="1234567890">
                            </cc1:FilteredTextBoxExtender>
                            <asp:RadioButton ID="rbPed" runat="server" Text="Pedido" AutoPostBack="True" OnCheckedChanged="rbPed_CheckedChanged"
                                Visible="false" />
                        </td>
                        <td style="text-align:center;" width="25%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlRFac" runat="server" Visible="false">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align:center;" width="15%">
                            </td>
                            <td style="text-align:right;" width="25%">
                                <asp:Label ID="lblIdFac" runat="server" CssClass="labelRq" Text="No Pedido:" Visible="true"></asp:Label>
                            </td>
                            <td style="text-align:left;" width="25%">
                                <asp:TextBox ID="txtIdFac" runat="server" Visible="true" Wrap="False" CssClass="txtrq"
                                    Enabled="true" MaxLength="10" Font-Size="X-Small" Height="15px"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="txtIdFac_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" TargetControlID="txtIdFac" ValidChars="1234567890">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td style="text-align:center;" width="25%">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlDREnv" runat="server" Visible="False">
                    <table style="width:100%;">
                        <tr>
                            <td style="text-align:center;" width="15%">
                            </td>
                            <td style="text-align:right;" width="25%">
                                <asp:Label ID="lblDIniRFE" runat="server" CssClass="labelRq" Text="Fecha Inicio:"></asp:Label>
                            </td>
                            <td style="text-align:left;" width="25%">
                                <asp:TextBox ID="txtDIniRFE" runat="server" CssClass="txtDt" Enabled="true" Width="100px"
                                    Font-Size="X-Small" Height="15px"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtDIniRFE_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtDIniRFE" WatermarkText="yyyy-MM-dd">
                                </cc1:TextBoxWatermarkExtender>
                                <cc1:CalendarExtender ID="txtDIniRFE_CalendarExtender" runat="server" CssClass="cExt"
                                    Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtDIniRFE">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="text-align:center;" width="25%">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" width="15%">
                            </td>
                            <td style="text-align:right;" width="25%">
                                <asp:Label ID="lblDFinRFE" runat="server" CssClass="labelRq" Enabled="false" Text="Fecha Fin:"></asp:Label>
                            </td>
                            <td style="text-align:left;" width="25%">
                                <asp:TextBox ID="txtDFinRFE" runat="server" CssClass="txtDt" Enabled="true" Width="100px"
                                    Font-Size="X-Small" Height="15px"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtDFinRFE_TextBoxWatermarkExtender" runat="server"
                                    Enabled="True" TargetControlID="txtDFinRFE" WatermarkText="yyyy-MM-dd">
                                </cc1:TextBoxWatermarkExtender>
                                <cc1:CalendarExtender ID="txtDFinRFE_CalendarExtender" runat="server" CssClass="cExt"
                                    Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtDFinRFE">
                                </cc1:CalendarExtender>
                            </td>
                            <td style="text-align:center;" width="25%">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <asp:Label ID="lblError" runat="server" CssClass="labelError" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                &nbsp;&nbsp;
                <asp:Button ID="btnConsultar" runat="server" Height="20px" Text="Consultar" Width="100px"
                    OnClick="btnConsultar_Click" CssClass="btnRq" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <asp:Label ID="lblESin" runat="server" CssClass="labelError" Text="Programaciones en SINCO no encontradas en GINCO"
                    Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <asp:GridView ID="gv_Rep1" runat="server" CssClass="gvAlertas" Font-Size="X-Small"
                    HeaderStyle-Font-Size="Small" HeaderStyle-CssClass="gvHead1" Visible="false"
                    AllowPaging="True" PageSize="10" 
                    onpageindexchanging="gv_Rep1_PageIndexChanging">
                    <HeaderStyle CssClass="gvHead1" Font-Size="Small"></HeaderStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:center;">
                &nbsp;
                <asp:GridView ID="gv_Rep2" runat="server" CssClass="gvAlertas" Font-Size="X-Small"
                    HeaderStyle-Font-Size="Small" HeaderStyle-CssClass="gvHead1" Visible="false"
                    AllowPaging="True" PageSize="10" 
                    onpageindexchanging="gv_Rep2_PageIndexChanging">
                    <HeaderStyle CssClass="gvHead1" Font-Size="Small"></HeaderStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <table style="width:100%;">
                    <tr>
                        <td colspan = "2" align ="center">
                <asp:Label ID="lblEGinIn" runat="server" CssClass="labelError" Text="Inconsistencias"
                    Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;" width="50%">
                            <asp:Label ID="lblPSin" runat="server" CssClass="labelInfo" Text="SINCO" 
                                Visible="false"></asp:Label>
                        </td>
                        <td style="text-align:center;"  width="50%">
                            <asp:Label ID="lblPGin" runat="server" CssClass="labelInfo" Text="GINCO" 
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;" width="50%" valign="top" >
                            <asp:GridView ID="gv_RepSINCO" runat="server" CssClass="gvAlertas" Font-Size="X-Small"
                                HeaderStyle-Font-Size="Small" HeaderStyle-CssClass="gvHead1" Visible="false"
                                AllowPaging="True" PageSize="10" 
                                onpageindexchanging="gv_RepSINCO_PageIndexChanging">
                                <HeaderStyle CssClass="gvHead1" Font-Size="Small"></HeaderStyle>
                            </asp:GridView>
                        </td>
                        <td style="text-align:center;" width="50%" valign="top">
                            <asp:GridView ID="gv_RepGINCO" runat="server" CssClass="gvAlertas" Font-Size="X-Small"
                                HeaderStyle-Font-Size="Small" HeaderStyle-CssClass="gvHead1" Visible="false"
                                AllowPaging="True" PageSize="10" 
                                onpageindexchanging="gv_RepGINCO_PageIndexChanging">
                                <HeaderStyle CssClass="gvHead1" Font-Size="Small"></HeaderStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                        </td>
                        <td style="text-align:left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;">
                        </td>
                        <td style="text-align:left;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
