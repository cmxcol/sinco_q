<%@ Page Title="Modificación" Language="C#" MasterPageFile="~/CO/MP/UsrP.Master"
    AutoEventWireup="true" CodeBehind="ModPedidos.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Usr.ModPedidos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SPublic.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 97%;
        }
        .style2
        {
            width: 96px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gvAlSh" runat="server" AutoGenerateColumns="False" class="style1"
                    AllowPaging="true" CssClass="gvAlSh" HorizontalAlign="Center" CellPadding="1"
                    CellSpacing="0" PageSize="5">
                    <Columns>
                        <asp:BoundField HeaderText="Mensaje" DataField="MSGTEXT" ReadOnly="True" ItemStyle-ForeColor="#C2000B" />
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gvBlCli" runat="server" AutoGenerateColumns="False" class="style1"
                    AllowPaging="true" CssClass="gvAlSh" HorizontalAlign="Center" CellPadding="1"
                    CellSpacing="0" PageSize="5">
                    <Columns>
                        <asp:BoundField HeaderText="Cliente" DataField="IdRsObr" ReadOnly="True" ItemStyle-ForeColor="Black" />
                    </Columns>
                    <Columns>
                        <asp:BoundField HeaderText="Bloqueo Pedidos" DataField="BlCenPed" ReadOnly="True"
                            ItemStyle-ForeColor="Black" />
                    </Columns>
                    <Columns>
                        <asp:BoundField HeaderText="Bloqueo Entrega" DataField="BlCenEnt" ReadOnly="True"
                            ItemStyle-ForeColor="Black" />
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gv_ExCli" runat="server" AutoGenerateColumns="False" class="style1"
                    AllowPaging="true" CssClass="gvAlSh" HorizontalAlign="Center" CellPadding="1"
                    CellSpacing="0" PageSize="5">
                    <Columns>
                        <asp:BoundField HeaderText="TipoExcepción" DataField="NTEx" ReadOnly="True" ItemStyle-ForeColor="Black" />
                    </Columns>
                    <Columns>
                        <asp:BoundField HeaderText="FechaVigencia" DataField="dtVig" ReadOnly="True" ItemStyle-ForeColor="Black" />
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width:230px">
            </td>
            <td style="width:250px" align="right">
                <asp:Label ID="Label2" runat="server" Text="No Pedido" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:270px">
                <table>
                    <tr>
                        <td style="width:145px" align="left">
                            <asp:TextBox ID="txtPed" runat="server" CssClass="txtrq" MaxLength="15"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                FilterType="Numbers" TargetControlID="txtPed">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align:center;" width="125px">
                            <asp:Label ID="lblErrorPed" runat="server" Text="Datos Incorrectos" CssClass="labelError"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:250px">
            </td>
        </tr>
        <tr>
            <td style="width:230px">
                &nbsp;</td>
            <td style="width:250px" align="right">
                <asp:Label ID="lblSector" runat="server" Text="Sector" CssClass="labelRq" Visible="true"></asp:Label>
            </td>
            <td style="width:270px">
                <table>
                    <tr>
                        <td style="width:145px" align="left">
                            <asp:TextBox ID="txtSector" runat="server" CssClass="txtrq" MaxLength="2" Width="20px"
                                Enabled="true" Visible="true"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtSector_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtSector">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align:center;" width="125px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:250px">
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align:right;">
                <asp:Label ID="lblCodObra" runat="server" Text="" CssClass="labelRq" Visible="false"></asp:Label>
            </td>
            <td style="text-align:left;">
                <table class="style1">
                    <tr>
                        <td style="width:140px">
                            <asp:TextBox ID="txtCodObra" runat="server" CssClass="txtrq" MaxLength="8" Visible="false"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td style="width:125px">
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align:center;" colspan="2">
                <asp:Button ID="btnValidar" runat="server" Text="Validar" CssClass="btnRq" OnClick="btnValidar_Click" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" style="margin-left: 40px">
                <asp:GridView ID="gv_DataCli" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" Visible="False" DataMember="DefaultView"
                    PageSize="100">
                    <Columns>
                        <asp:BoundField DataField="NOMOBRA" HeaderText="Nombre Obra">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CODCLI" HeaderText="Razón Social">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOMCLI" HeaderText="Nombre Razón Social">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FPAGO" HeaderText="Mod. de Pago Obra">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NSEG" HeaderText="Segmentación" Visible="False" />
                        <asp:BoundField DataField="VEN" HeaderText="Comercial" Visible="False" />
                        <asp:BoundField DataField="TELVEN" HeaderText="Telefono" Visible="False" />
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lblASCLI" runat="server" Text="" CssClass="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnNVCli" runat="server" Height="23px" Style="text-align: center;
                    color: #000066; font-family: Arial; font-size: small;" Text="Nueva validación"
                    Visible="False" Width="130px" OnClick="btnNVCli_Click" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center" width="500px">
                <asp:GridView ID="gv_EC_Cli" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" Visible="False" DataMember="DefaultView"
                    PageSize="100">
                    <Columns>
                        <asp:BoundField DataField="CUPO" HeaderText="Cupo">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SFAVOR" HeaderText="Estado Cta">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CTOTAL" HeaderText="Cartera Total">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CMPSAP" HeaderText="Comp. SAP" />
                        <asp:BoundField DataField="CMPGINCO" HeaderText="Comp. GINCO">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TSCUPO" HeaderText="SALDO TOTAL">
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                </asp:GridView>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:GridView ID="gv_SOrderPrev" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="1" Visible="False" DataMember="DefaultView"
                    PageIndex="5" PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="IdMaterial" HeaderText="Material" />
                        <asp:BoundField DataField="Volumen" HeaderText="Cantidad" />
                        <asp:BoundField DataField="IdCentro" HeaderText="Centro" />
                        <asp:BoundField DataField="Ajuste" HeaderText="Ajuste" />
                        <asp:BoundField DataField="PBase" HeaderText="Precio Unitario" />
                        <asp:BoundField DataField="PNeto" HeaderText="Precio TOTAL" />
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                </asp:GridView>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblSN" runat="server" Text="El cliente tiene un saldo negativo" CssClass="labelError"
                    Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblANP" runat="server" Text="" CssClass="labelError" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnAC" runat="server" Height="23px" Style="text-align: center; color: #000066;
                    font-family: Arial; font-size: small;" Text="Registrar Ajuste" Visible="False"
                    Width="130px" OnClick="btnAC_Click" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <table class="style1">
                    <tr>
                        <td style="width:245px">
                        </td>
                        <td style="width:510px">
                            <asp:Panel ID="pnlMpUp" runat="server" Visible="false" CssClass="modalPopup" Width="510px">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlAC" runat="server" Visible="true" CssClass="modalPopupInner" Width="500px">
                                                <table class="style1">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblAC" runat="server" CssClass="labelError" Text="" Visible="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:250px" align="right">
                                                            <asp:Label ID="lblVen" runat="server" CssClass="style32" Style="font-family: Arial;
                                                                font-size: 9pt" Text="Comercial:" Visible="True"></asp:Label>
                                                        </td>
                                                        <td style="width:250px" align="left">
                                                            <asp:DropDownList ID="ddlVen" runat="server" CssClass="style32" Height="25px" Style="font-family: Arial;
                                                                margin-left: 11px; font-size: 9pt" Visible="True" Width="230px">
                                                                <asp:ListItem Selected="True" Value="0">Seleccione...</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblIdPedido" runat="server" CssClass="style32" Style="font-family: Arial;
                                                                font-size: 9pt" Text="Pedido:" Visible="True"></asp:Label>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:TextBox ID="txtIdPedido" runat="server" CssClass="style36" Font-Bold="false"
                                                                ForeColor="#000066" Height="16px" MaxLength="10" Style="font-size: small; margin-left: 11px;"
                                                                Visible="True" Width="85px">
                                                            </asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="txtIdPedido_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtIdPedido" FilterType="Numbers">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblDTComp" runat="server" CssClass="style32" Style="font-family: Arial;
                                                                font-size: 9pt" Text="Fecha Compromiso:" Visible="True">
                                                            </asp:Label>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:TextBox ID="txtDTComp" runat="server" CssClass="style36" Font-Bold="false" ForeColor="#000066"
                                                                Height="16px" MaxLength="10" Style="font-size: small; margin-left: 11px;" Visible="True"
                                                                Width="85px">
                                                            </asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="txtDTComp_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtDTComp" ValidChars="0123456789-/" FilterMode="ValidChars"
                                                                FilterType="Custom">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:TextBoxWatermarkExtender ID="txtDTComp_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtDTComp" WatermarkText="yyyy-MM-dd">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="txtDTComp_CalendarExtender" runat="server" DaysModeTitleFormat="yyyy-MM-dd"
                                                                Enabled="True" TargetControlID="txtDTComp" TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="lblGP" runat="server" CssClass="labelInfo" Text="" Visible="false">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="lblErrorDC" runat="server" CssClass="labelError" Text="" Visible="false">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnGuardaAC" runat="server" Height="23px" Style="text-align: center;
                                                                color: #000066; font-family: Arial; font-size: small;" Text="Guardar" Visible="true"
                                                                Width="79px" OnClick="btnGuardaAC_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAcp" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                                            <asp:ModalPopupExtender ID="MpUp_AC" runat="server" BackgroundCssClass="modalBackground"
                                                CancelControlID="lblCan" DynamicServicePath="" Enabled="True" OkControlID="lblAcp"
                                                PopupControlID="pnlMpUp" TargetControlID="lblAcp">
                                            </asp:ModalPopupExtender>
                                            <asp:Label ID="lblCan" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnNAC" runat="server" Height="23px" OnClick="btnNAC_Click" Style="text-align: center;
                                                color: #000066; font-family: Arial; font-size: small;" Text="Nueva validación"
                                                Visible="true" Width="130px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="width:245px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Panel ID="pnlPedidos" runat="server" Visible="false" Width="30%">
                    <table class="style1">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblPed_OG" runat="server" CssClass="labelInfo" Text="" Visible="true">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;" width="40%">
                                <asp:Label ID="lblPed1" runat="server" CssClass="lblItem" Text="Pedido 1:" Visible="True"></asp:Label>
                            </td>
                            <td style="text-align:left;" width="60%">
                                <asp:TextBox ID="txtPed1" runat="server" CssClass="txtrq" MaxLength="20" Width="150px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtPed" runat="server" Enabled="True"
                                    FilterType="Numbers" TargetControlID="txtPed1">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label ID="lblPed2" runat="server" CssClass="lblItem" Text="Pedido 2:" Visible="True"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtPed2" runat="server" CssClass="txtrq" MaxLength="20" Width="150px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtPed2" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtPed2">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label ID="lblPed3" runat="server" CssClass="lblItem" Text="Pedido 3:" Visible="True"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtPed3" runat="server" CssClass="txtrq" MaxLength="20" Width="150px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtPed3" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtPed3">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label ID="lblPed4" runat="server" CssClass="lblItem" Text="Pedido 4:" Visible="True"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtPed4" runat="server" CssClass="txtrq" MaxLength="20" Width="150px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtPed4" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtPed4">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                <asp:Label ID="lblPed5" runat="server" CssClass="lblItem" Text="Pedido 5:" Visible="True"></asp:Label>
                            </td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtPed5" runat="server" CssClass="txtrq" MaxLength="20" Width="150px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtPed5" runat="server"
                                    Enabled="True" FilterType="Numbers" TargetControlID="txtPed5">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Label ID="lbl_E_OG" runat="server" CssClass="labelError" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btnCalPed_OG" runat="server" Height="23px" CssClass="btnRq" Text="Calcular" Visible="true"
                                    Width="79px" OnClick="btnCalPed_OG_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Panel ID="pnlPedReg" runat="server" Visible="false" Width="30%">
                    <table class="style1">
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btnMod_OG" runat="server" Height="23px" CssClass="btnRq" Text="Modificar" Visible="true"
                                    Width="79px" onclick="btnMod_OG_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btnRegP_OG" runat="server" Height="23px" OnClick="btnRegP_OG_Click"
                                    CssClass="btnRq"
                                    Text="Registrar Programación" Visible="false" Width="170px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <table class="style1">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlDatos" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td style="width:125px" align="left">
                                            <asp:Label ID="lblCodMat" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Códido Material:" Visible="True"></asp:Label>
                                        </td>
                                        <td style="width:125px" align="left">
                                            <asp:TextBox ID="txtCodMat" runat="server" CssClass="style36" Font-Bold="True" ForeColor="#000066"
                                                Height="16px" MaxLength="10" Style="font-size: small; margin-left: 11px;" Visible="True"
                                                Width="70px"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txtCodMat_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtCodMat">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" align="left">
                                            <asp:Label ID="lblVol" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Cantidad:" Visible="True"></asp:Label>
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:TextBox ID="txtVol" runat="server" CssClass="style36" Font-Bold="True" ForeColor="#000066"
                                                Height="16px" MaxLength="6" Style="font-size: small; margin-left: 11px; text-align: left;"
                                                Visible="True" Width="40px"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="txtVol_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                TargetControlID="txtVol" ValidChars="0123456789.,">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2" align="left">
                                            <asp:Label ID="lblCentro" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Centro:" Visible="True"></asp:Label>
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:TextBox ID="txtCentro" runat="server" CssClass="style36" Font-Bold="True" ForeColor="#000066"
                                                Height="16px" MaxLength="8" Style="font-size: small; margin-left: 11px;" Visible="True"
                                                Width="70px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="pnlAditivos" runat="server" Visible="false">
                                <table class="style1">
                                    <tr>
                                        <td style="width:80px" align="right">
                                            <asp:Label ID="lblAD1" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Aditivo 1:" Visible="True"></asp:Label>
                                        </td>
                                        <td style="width:170px" align="right">
                                            <asp:DropDownList ID="ddlAD1" runat="server" CssClass="style32" Height="25px" Style="font-family: Arial;
                                                font-size: 9pt" Visible="True" Width="125px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;">
                                            <asp:Label ID="lblAD2" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Aditivo 2:" Visible="True"></asp:Label>
                                        </td>
                                        <td style="text-align:right;">
                                            <asp:DropDownList ID="ddlAD2" runat="server" CssClass="style32" Height="25px" Style="font-family: Arial;
                                                font-size: 9pt" Visible="True" Width="125px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;">
                                            <asp:Label ID="lblAD3" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Aditivo 3:" Visible="True"></asp:Label>
                                        </td>
                                        <td style="text-align:right;">
                                            <asp:DropDownList ID="ddlAD3" runat="server" CssClass="style32" Height="25px" Style="font-family: Arial;
                                                font-size: 9pt" Visible="True" Width="125px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlAB" runat="server" Visible="false">
                                <table class="style1">
                                    <tr>
                                        <td style="text-align:left;">
                                            <asp:CheckBox ID="chbAj" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Ajuste" Visible="True" />
                                        </td>
                                        <td style="text-align:left;">
                                            <asp:CheckBox ID="chbBom" runat="server" CssClass="style32" Style="font-family: Arial;
                                                font-size: 9pt" Text="Bomba" Visible="True" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnCal" runat="server" Height="23px" Style="text-align: center; color: #000066;
                    font-family: Arial; font-size: small;" Text="Calcular" Visible="False" Width="79px"
                    OnClick="btnCal_Click" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnMod" runat="server" Height="23px" Width="79px" Style="text-align: center;
                    color: #000066; font-family: Arial; font-size: small;" Visible="false" Text="Modificar"
                    OnClick="btnMod_Click" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnNCal" runat="server" Height="23px" Style="text-align: center;
                    color: #000066; font-family: Arial; font-size: small;" Text="Nueva validación"
                    Visible="False" Width="130px" OnClick="btnNCal_Click" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
            </td>
            <td colspan="2" align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lbl_E_SO" runat="server" Text="" CssClass="labelError" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:GridView ID="gv_SOrder" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="1" Visible="False" DataMember="DefaultView"
                    PageIndex="5" PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="IdMaterial" HeaderText="Material" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="IdCentro" HeaderText="Centro" />
                        <asp:BoundField DataField="PBase" HeaderText="Precio Unitario" />
                        <asp:BoundField DataField="PNeto" HeaderText="Precio TOTAL" />
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                </asp:GridView>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblEC" runat="server" Text="" CssClass="labelInfo" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblECT" runat="server" Text="" CssClass="labelInfo" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gv_ExClientes" runat="server" class="style1" AllowPaging="True"
                    CssClass="gvAlSh" HorizontalAlign="Center" CellPadding="1" PageSize="5" Visible="False">
                    <Columns>
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsgVSO" runat="server" Text="" CssClass="labelNorm" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsgSF" runat="server" Text="" CssClass="labelNorm" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblAPG" runat="server" Text="" CssClass="" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblAP" runat="server" Text="" CssClass="" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnASC" runat="server" Height="23px" Style="text-align: center; color: #000066;
                    font-family: Arial; font-size: small;" Text="Registrar Ajuste" Visible="False"
                    Width="130px" OnClick="btnAC_Click" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnRegP" runat="server" Height="23px" Style="text-align: center;
                    color: #000066; font-family: Arial; font-size: small;" Text="Registrar Programación"
                    Visible="False" Width="170px" OnClick="btnRegP_Click" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <%--        <tr>
            <td colspan="4" align="center">
                <table class="style1">
                    <tr>
                        <td style="width:245px">
                        </td>
                        <td style="width:510px">
                            <asp:Panel ID="pnlMpUA" runat="server" Visible="false" CssClass="modalPopup" Width="510px">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <table class="style1">
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:GridView ID="gvAlShMpU" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                                            CellPadding="1" CellSpacing="0" class="style1" CssClass="gvAlSh" HorizontalAlign="Center"
                                                            PageSize="5">
                                                            <Columns>
                                                                <asp:BoundField DataField="MSGTEXT" HeaderText="Mensaje" ItemStyle-ForeColor="#C2000B"
                                                                    ReadOnly="True" />
                                                            </Columns>
                                                            <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Height="20px" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblAcpA" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                                                        <asp:ModalPopupExtender ID="MPUAl" runat="server" BackgroundCssClass="modalBackground"
                                                            CancelControlID="lblCan" DynamicServicePath="" Enabled="True" OkControlID="lblAcpA"
                                                            PopupControlID="pnlMpUA" TargetControlID="btnASA0">
                                                        </asp:ModalPopupExtender>
                                                        <asp:Label ID="lblCanA" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnASA" runat="server" Height="23px" Style="text-align: center; color: #000066;
                                                            font-family: Arial; font-size: small;" Text="Aceptar" Visible="true" Width="79px"
                                                            OnClick="btnASA_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="width:245px">
                            <asp:Button ID="btnASA0" runat="server" Height="23px" Style="text-align: center;
                                color: #000066; font-family: Arial; font-size: small;" Text="Aceptar" Visible="true"
                                Width="79px" OnClick="btnASA_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
    </table>
</asp:Content>
