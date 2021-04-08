<%@ Page Title="Programación" Language="C#" MasterPageFile="~/CO/MP/UsrP.Master"
    AutoEventWireup="true" CodeBehind="ProgPedidos.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Usr.ProgPedidos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SMaster.css" rel="stylesheet" />
    <link href="../../Styles/SPublic.css" rel="stylesheet" />
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
    <script type="text/javascript">
        var estado = false;

        function onShowing(ModalBName, TargetControlID, PositionControl) {
            var e = document.getElementById(TargetControlID);

            $find(ModalBName).set_X(e.offsetLeft);
            $find(ModalBName).set_Y(e.offsetTop + e.offsetHeight);
            AsignXY(PositionControl, TargetControlID);
            $find(ModalBName).show();
        }

        function AsignXY(PositionControl, TargetControlID) {
            var c = document.getElementById(PositionControl);
            var p_xy = txtXY(TargetControlID);
            c.value = p_xy;
        }

        function txtXY(TargetControlID) {
            var e = document.getElementById(TargetControlID);
            var x = e.offsetLeft;
            var y = e.offsetTop + e.offsetHeight;
            var ret = x + '|' + y;
            return ret;
        }

        function Show(ModalBName, TargetControlID) {
            estado = !estado;
            if (estado) {
                $find(ModalBName).show();
            }
            else {
                $find(ModalBName).hide();
            }

        }

    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td colspan="4" align="center" width="100%">
                <asp:GridView ID="gvAlSh" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    CssClass="gvAlSh" HorizontalAlign="Center" CellPadding="1" CellSpacing="0" PageSize="5"
                    OnPageIndexChanging="gvAlSh_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="Mensaje" DataField="MSGTEXT" ReadOnly="True" ItemStyle-ForeColor="#C2000B" />
                    </Columns>
                    <HeaderStyle Height="20px" CssClass="gvH" />
                </asp:GridView>
                <%-- BackColor="#D8D8D8" ForeColor="Black"--%>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" width="100%">
                <asp:GridView ID="gvBlCli" runat="server" AutoGenerateColumns="False" class="style1"
                    AllowPaging="true" CssClass="gvAlSh" HorizontalAlign="Center" CellPadding="1"
                    CellSpacing="0" PageSize="5" OnPageIndexChanging="gvBlCli_PageIndexChanging">
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
                    <HeaderStyle CssClass="gvH" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" width="100%">
                <asp:GridView ID="gv_ExCli" runat="server" AutoGenerateColumns="False" class="style1"
                    AllowPaging="true" CssClass="gvAlSh" HorizontalAlign="Center" CellPadding="1"
                    CellSpacing="0" PageSize="5" OnPageIndexChanging="gv_ExCli_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="ID" ReadOnly="True" ItemStyle-ForeColor="Black" />
                    </Columns>
                    <Columns>
                        <asp:BoundField HeaderText="TipoExcepción" DataField="NTEx" ReadOnly="True" ItemStyle-ForeColor="Black" />
                    </Columns>
                    <Columns>
                        <asp:BoundField HeaderText="FechaVigencia" DataField="dtVig" ReadOnly="True" ItemStyle-ForeColor="Black" />
                    </Columns>
                    <HeaderStyle CssClass="gvH" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%" align="right">
                <asp:Label ID="lblCodObra" runat="server" Text="Codigo de Obra" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%" align="left">
                            <asp:TextBox ID="txtCodObra" runat="server" CssClass="txtrq" MaxLength="10" Width="70px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtCodObra_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtCodObra"></asp:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align:left;" width="60%">
                            <asp:Label ID="lblErrorCod" runat="server" Text="Datos Incorrectos" CssClass="labelError"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td></td>
            <td style="text-align:right;">
                <asp:Label ID="lblSector" runat="server" Text="Sector" CssClass="labelRq"></asp:Label>
            </td>
            <td style="text-align:left;">
                <table class="style1">
                    <tr>
                        <td style="width:60%">
                            <asp:TextBox ID="txtSector" runat="server" CssClass="txtrq" MaxLength="2" Width="20px" Text ="03"
                                Enabled="true"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtSector_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtSector"></asp:FilteredTextBoxExtender>
                        </td>
                        <td style="width:40%"></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td style="text-align:center;" colspan="2">
                <asp:Button ID="btnValidar" runat="server" Text="Validar" CssClass="btnRq" OnClick="btnValidar_Click" />
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td></td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gv_DataCli" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" Visible="False" DataMember="DefaultView"
                    PageSize="5">
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
                    <HeaderStyle CssClass="gvH2" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lblASCLI" runat="server" Text="" CssClass="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnNVCli" runat="server" Height="23px" CssClass="btnRq" Text="Nueva validación"
                    Visible="False" Width="130px" OnClick="btnNVCli_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" width="100%">
                <asp:GridView ID="gv_EC_Cli" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" Visible="False" DataMember="DefaultView"
                    PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="CUPO" HeaderText="Cupo">
                        <ItemStyle Wrap="False" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SFAVOR" HeaderText="Estado Cta">
                        <ItemStyle Wrap="False" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CTOTAL" HeaderText="Cartera Total">
                        <ItemStyle Wrap="False" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CMPSAP" HeaderText="Comp. SAP">
                        <ItemStyle Wrap="False" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CMPGINCO" HeaderText="Comp. GINCO">
                        <ItemStyle Wrap="False" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TSCUPO" HeaderText="SALDO TOTAL">
                        <ItemStyle Wrap="False" Width="15%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="gvH2" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4"></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblSN" runat="server" Text="El cliente tiene un saldo negativo" CssClass="labelError"
                    Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblANP" runat="server" Text="" CssClass="labelError" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Button ID="btnAC" runat="server" Height="23px" CssClass="btnRq" Text="Registrar Ajuste"
                    Visible="False" Width="130px" OnClick="btnAC_Click" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4" align="center">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <table class="style1">
                    <tr>
                        <td style="width:245px"></td>
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
                                                            <asp:Label ID="lblVen" runat="server" CssClass="lblItem" Text="Comercial:" Visible="True"></asp:Label>
                                                        </td>
                                                        <td style="width:250px" align="left">
                                                            <asp:DropDownList ID="ddlVen" runat="server" CssClass="ddlGVei" Height="25px" Style="margin-left: 11px;"
                                                                Visible="True" Width="230px">
                                                                <asp:ListItem Selected="True" Value="0">Seleccione...</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblIdPedido" runat="server" CssClass="lblItem" Text="Pedido:" Visible="True"></asp:Label>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:TextBox ID="txtIdPedido" runat="server" CssClass="txtrq" Font-Bold="false" ForeColor="#000066"
                                                                Height="16px" MaxLength="10" Style="margin-left: 11px;" Visible="True" Width="85px">
                                                            </asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="txtIdPedido_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtIdPedido" FilterType="Numbers">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblDTComp" runat="server" CssClass="lblItem" Text="Fecha Compromiso:"
                                                                Visible="True">
                                                            </asp:Label>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:TextBox ID="txtDTComp" runat="server" CssClass="txtrq" Font-Bold="false" ForeColor="#000066"
                                                                Height="16px" MaxLength="10" Style="margin-left: 11px;" Visible="True" Width="85px">
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
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnGuardaAC" runat="server" Height="23px" CssClass="btnRq" Text="Guardar"
                                                                Visible="true" Width="79px" OnClick="btnGuardaAC_Click" />
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
                                            <asp:Button ID="btnNAC" runat="server" Height="23px" OnClick="btnNAC_Click" CssClass="btnRq"
                                                Text="Nueva validación" Visible="true" Width="130px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="width:245px"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <table class="style1">
                    <tr>
                        <td style="width:245px"></td>
                        <td style="width:510px">
                            <asp:Panel ID="pnlMP_OG" runat="server" Visible="false" CssClass="modalPopup" Width="510px">
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlA_OG" runat="server" Visible="true" CssClass="modalPopupInner"
                                                Width="500px">
                                                <table class="style1">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblAC_OG" runat="server" CssClass="labelError" Text="" Visible="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:GridView ID="gv_GPed_OG" runat="server" AllowPaging="True" CssClass="gvAlertas"
                                                                HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                                                                Visible="true" DataMember="DefaultView" PageSize="5" Width="100%" OnRowCommand="gv_GPed_OG_RowCommand"
                                                                OnPageIndexChanging="gv_GPed_OG_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:BoundField DataField="txt" HeaderText="" ReadOnly="True">
                                                                    <HeaderStyle Wrap="True" Width="25%" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Pedido">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtIdPed" CssClass="txtrqGCei" runat="server" Text='<%# Bind("IdPedido") %>'
                                                                                Width="90%"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtIdPed" runat="server"
                                                                                Enabled="True" FilterType="Numbers" TargetControlID="txtIdPed">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblValPed" runat="server" Text='<%# Bind("IdPedido") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Wrap="True" Width="25%" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Valor" HeaderText="Valor" ReadOnly="True">
                                                                    <HeaderStyle Wrap="True" Width="25%" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton ID="lbtnGuardar" runat="server" CommandName="Guardar" Text="Guardar"
                                                                                CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtnCan" runat="server" CommandName="Cancelar" Text="Cancelar"
                                                                                CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnEditar" runat="server" CommandName="Editar" Text="Editar"
                                                                                CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="25%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:GridView ID="gv_Ped" runat="server" AllowPaging="True" CssClass="gvAlertas"
                                                                HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                                                                Visible="true" DataMember="DefaultView" PageSize="5" Width="100%" OnPageIndexChanging="gv_Ped_PageIndexChanging"
                                                                OnRowCommand="gv_Ped_RowCommand">
                                                                <Columns>
                                                                    <asp:BoundField DataField="txt" HeaderText="" ReadOnly="True">
                                                                    <HeaderStyle Wrap="True" Width="30%" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Pedido">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtPed" CssClass="txtrqGCei" runat="server" Text='<%# Bind("IdPedido") %>'
                                                                                Width="90%"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtPed" runat="server" Enabled="True"
                                                                                FilterType="Numbers" TargetControlID="txtPed">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPed" runat="server" Text='<%# Bind("IdPedido") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Wrap="True" Width="30%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton ID="lbtnGuardar" runat="server" CommandName="Guardar" Text="Guardar"
                                                                                CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                            &nbsp;
                                                                            <asp:LinkButton ID="lbtnCan" runat="server" CommandName="Cancelar" Text="Cancelar"
                                                                                CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton ID="lbtnNew" runat="server" CommandName="Nuevo" Text="Nuevo" CommandArgument="1"
                                                                                ForeColor="Gray"></asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnEditar" runat="server" CommandName="Editar" Text="Editar"
                                                                                CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                            <asp:LinkButton ID="lbtnBorrar" runat="server" CommandName="Borrar" Text="Borrar"
                                                                                CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:250px" align="right">
                                                            <asp:Label ID="lblVenA_OG" runat="server" CssClass="lblItem" Text="Comercial:" Visible="True"></asp:Label>
                                                        </td>
                                                        <td style="width:250px" align="left">
                                                            <asp:DropDownList ID="ddlVenA_OG" runat="server" CssClass="ddlGVei" Height="25px"
                                                                Style="margin-left: 11px;" Visible="True" Width="230px">
                                                                <asp:ListItem Selected="True" Value="0">Seleccione...</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lbldt_OG" runat="server" CssClass="lblItem" Text="Fecha Compromiso:"
                                                                Visible="True">
                                                            </asp:Label>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:TextBox ID="txtdt_OG" runat="server" CssClass="txtrq" Font-Bold="false" ForeColor="#000066"
                                                                Height="16px" MaxLength="10" Style="margin-left: 11px;" Visible="True" Width="85px">
                                                            </asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtdt_OG" runat="server"
                                                                Enabled="True" TargetControlID="txtdt_OG" ValidChars="0123456789-/" FilterMode="ValidChars"
                                                                FilterType="Custom">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
                                                                TargetControlID="txtdt_OG" WatermarkText="yyyy-MM-dd">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" DaysModeTitleFormat="yyyy-MM-dd"
                                                                Enabled="True" TargetControlID="txtdt_OG" TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="lblGP_OG" runat="server" CssClass="labelInfo" Text="" Visible="false">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:Label ID="lblError_OG" runat="server" CssClass="labelError" Text="" Visible="false">
                                                            </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Button ID="btnGA_OG" runat="server" Height="23px" CssClass="btnRq" Text="Guardar"
                                                                Visible="true" Width="79px" OnClick="btnGA_OG_Click" />
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:Button ID="btnCan_OG" runat="server" Height="23px" CssClass="btnRq" Text="Cancelar"
                                                                Visible="true" Width="79px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblAcp_OG" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                                            <asp:ModalPopupExtender ID="MPUP_OG" runat="server" BackgroundCssClass="modalBackground"
                                                CancelControlID="lblCan_OG" DynamicServicePath="" Enabled="True" OkControlID="lblAcp_OG"
                                                PopupControlID="pnlMP_OG" TargetControlID="lblAcp_OG">
                                            </asp:ModalPopupExtender>
                                            <asp:Label ID="lblCan_OG" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnNAC_OG" runat="server" Height="23px" OnClick="btnNAC_Click" CssClass="btnRq"
                                                Text="Nueva validación" Visible="true" Width="130px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="width:245px"></td>
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
                                <asp:GridView ID="gv_ValPed" runat="server" AllowPaging="True" CssClass="gvAlertas"
                                    HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                                    Visible="true" DataMember="DefaultView" PageSize="5" Width="100%" OnRowCommand="gv_ValPed_RowCommand"
                                    OnPageIndexChanging="gv_ValPed_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="txt" HeaderText="" ReadOnly="True">
                                        <HeaderStyle Wrap="True" Width="30%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Valor">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtValPed" CssClass="txtrqGCei" runat="server" Text='<%# Bind("Valor") %>'
                                                    Width="90%"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtValPed" runat="server"
                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtValPed">
                                                </asp:FilteredTextBoxExtender>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblValPed" runat="server" Text='<%# Bind("Valor") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="True" Width="30%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbtnGuardar" runat="server" CommandName="Guardar" Text="Guardar"
                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton ID="lbtnCan" runat="server" CommandName="Cancelar" Text="Cancelar"
                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtnNew" runat="server" CommandName="Nuevo" Text="Nuevo" CommandArgument="1"
                                                    ForeColor="Gray"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEditar" runat="server" CommandName="Editar" Text="Editar"
                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnBorrar" runat="server" CommandName="Borrar" Text="Borrar"
                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <%--                        <tr>
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
                        </tr>--%>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Label ID="lbl_E_OG" runat="server" CssClass="labelError" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btnCalPed_OG" runat="server" Height="23px" CssClass="btnRq" Text="Calcular"
                                    Visible="true" Width="79px" OnClick="btnCalPed_OG_Click" />
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
                                <asp:Button ID="btnMod_OG" runat="server" Height="23px" CssClass="btnRq" Text="Modificar"
                                    Visible="true" Width="79px" OnClick="btnMod_OG_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;" colspan="2">
                                <asp:Button ID="btnRegP_OG" runat="server" Height="23px" OnClick="btnRegP_OG_Click"
                                    CssClass="btnRq" Text="Registrar Programación" Visible="false" Width="170px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="width:21%"></td>
            <td colspan="2" width="52%" align="center">
                <asp:Panel ID="pnlDatos" runat="server" Visible="false">
                    <asp:UpdatePanel ID="UpnlDatos" runat="server">
                        <ContentTemplate>
                            <fieldset>
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <table class="style1">
                                                <tr>
                                                    <td style="width:25%" align="right">
                                                        <asp:Label ID="lblCodMat" runat="server" CssClass="lblItem" Text="Códido Material:"
                                                            Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="width:20%" align="left">
                                                        <asp:TextBox ID="txtCodMat" runat="server" CssClass="txtrq" Font-Bold="True" ForeColor="#000066"
                                                            MaxLength="10" Style="margin-left: 11px;" Visible="True" Width="70px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="txtCodMat_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtCodMat">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>
                                                    <td style="width:15%" align="right">
                                                        <asp:Label ID="lblAD1" runat="server" CssClass="lblItem" Text="Aditivo 1:" Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="width:40%" align="left">
                                                        <asp:DropDownList ID="ddlAD1" runat="server" CssClass="ddlGVei" Height="15px" Visible="True"
                                                            Width="125px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;">
                                                        <asp:Label ID="lblVol" runat="server" CssClass="lblItem" Text="Cantidad:" Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align:left;">
                                                        <input id="txtVol" type="text" class="txtVol_css" style="width: 40px; margin-left: 11px; text-align: center; font-size: x-small; font-family: Verdana; color: Gray; font-weight: bold;"
                                                            maxlength="6" onclick="Show('ModalPopup_ID', 'ctl00_MainContent_txtVol');" readonly="readonly"
                                                            runat="server" />
                                                        <input id="p_xy" type="hidden" runat="server" />
                                                    </td>
                                                    <td style="text-align:right;">
                                                        <asp:Label ID="lblAD2" runat="server" CssClass="lblItem" Text="Aditivo 2:" Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align:left;">
                                                        <asp:DropDownList ID="ddlAD2" runat="server" CssClass="ddlGVei" Height="15px" Visible="True"
                                                            Width="125px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;">
                                                        <asp:Label ID="lblCentro" runat="server" CssClass="lblItem" Text="Centro:" Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align:left;">
                                                        <asp:TextBox ID="txtCentro" runat="server" CssClass="txtrq" Font-Bold="True" ForeColor="#000066"
                                                            MaxLength="8" Style="margin-left: 11px;" Visible="True" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align:right;">
                                                        <asp:Label ID="lblAD3" runat="server" CssClass="lblItem" Text="Aditivo 3:" Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align:left;">
                                                        <asp:DropDownList ID="ddlAD3" runat="server" CssClass="ddlGVei" Height="15px" Visible="True"
                                                            Width="125px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;">
                                                        <asp:Label ID="lblDtOrder" runat="server" CssClass="lblItem" Text="Fecha Entrega:"
                                                            Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align:left;">
                                                        <asp:TextBox ID="txtdt_Ord" runat="server" CssClass="txtrq" Font-Bold="false" ForeColor="#000066"
                                                            Height="16px" MaxLength="10" Style="margin-left: 11px;" Visible="True" Width="70px">
                                                        </asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="txtdt_Ord_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" FilterMode="ValidChars" FilterType="Custom" TargetControlID="txtdt_Ord"
                                                            ValidChars="0123456789-/">
                                                        </asp:FilteredTextBoxExtender>
                                                        <asp:TextBoxWatermarkExtender ID="txtdt_OG0_TextBoxWatermarkExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtdt_Ord" WatermarkText="yyyy-MM-dd">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="txtdt_OG0_CalendarExtender" runat="server" DaysModeTitleFormat="yyyy-MM-dd"
                                                            Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtdt_Ord" TodaysDateFormat="yyyy-MM-dd">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td style="text-align:right;">
                                                        <asp:Label ID="lblPump" runat="server" CssClass="lblItem" Text="Bomba" Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align:left;">
                                                        <asp:DropDownList ID="ddlPump" runat="server" CssClass="ddlGVei" Height="15px" Visible="True"
                                                            Width="125px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align:right;">
                                                        <asp:Label ID="lblAjuste" runat="server" CssClass="lblItem" Text="Ajuste:" Visible="True"></asp:Label>
                                                    </td>
                                                    <td style="text-align:left;">
                                                        <asp:CheckBox ID="chbAj" runat="server" CssClass="lblItem" Style="margin-left: 11px;"
                                                            Text="" Visible="True" />
                                                    </td>
                                                    <td style="text-align:right;"></td>
                                                    <td style="text-align:left;"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="p_xy" PopupControlID="pnltest"
                                    DropShadow="true" OkControlID="btnCancelar" CancelControlID="btnCancelar" PopupDragHandleControlID="pnltest"
                                    BehaviorID="ModalPopup_ID" Drag="true">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="pnlTest" runat="server" Visible="true" CssClass="modalPopup" Width="300px">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                                <table class="style1">
                                                    <tr>
                                                        <td style="width:50%"></td>
                                                        <td style="width:50%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:TextBox ID="txtVolAuAsg" runat="server" CssClass="txtrq" Font-Bold="True" ForeColor="#000066"
                                                                MaxLength="8" Visible="True" Width="50px"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="txtVolAuAsg_FTE" runat="server" Enabled="True" ValidChars="0123456789,."
                                                                TargetControlID="txtVolAuAsg">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:TextBoxWatermarkExtender ID="txtVolAuAsg_TWE" runat="server" Enabled="True"
                                                                TargetControlID="txtVolAuAsg" WatermarkText="Vol">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <%--  <asp:TextBox ID="txtCVh" runat="server" CssClass="txtrq" Font-Bold="True" ForeColor="#000066"
                                                                MaxLength="5" Visible="True" Width="30px"></asp:TextBox>--%>
                                                           <%-- <asp:FilteredTextBoxExtender ID="txtNumVh_FTE" runat="server" Enabled="True" ValidChars="0123456789,."
                                                                TargetControlID="txtCVh">
                                                            </asp:FilteredTextBoxExtender>--%>
                                                            <%--<asp:TextBoxWatermarkExtender ID="txtNumVh_TWE" runat="server" Enabled="True" TargetControlID="txtCVh"
                                                                WatermarkText="CVh">
                                                            </asp:TextBoxWatermarkExtender>--%>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:Button ID="btnAuAsg" runat="server" Visible="true" Text="Asignar" CssClass="btnRq"
                                                                OnClick="btnAuAsg_Click"></asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gv_AVh" runat="server" AllowPaging="True" CssClass="gvAlertas"
                                                    HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                                                    Visible="true" DataMember="DefaultView" PageSize="15" Width="250px" OnRowCommand="gv_AVh_RowCommand"
                                                    OnPageIndexChanging="gv_AVh_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="txt" HeaderText="" ReadOnly="True">
                                                        <HeaderStyle Wrap="True" Width="35%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Volumen">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValPed" runat="server" Text='<%# Bind("Valor") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtValPed" CssClass="txtrqGCei" runat="server" Text='<%# Bind("Valor") %>'
                                                                    Width="45%"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtValPed" runat="server"
                                                                    Enabled="True" ValidChars="0123456789," TargetControlID="txtValPed">
                                                                </asp:FilteredTextBoxExtender>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Wrap="True" Width="25%" />
                                                        </asp:TemplateField>
                                                      <%--  <asp:TemplateField ShowHeader="False">
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lbtnGuardar" runat="server" CommandName="Guardar" Text="Guardar"
                                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnCan" runat="server" CommandName="Cancelar" Text="Cancelar"
                                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                            </EditItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:LinkButton ID="lbtnNew" runat="server" CommandName="Nuevo" Text="Nuevo" CommandArgument="1"
                                                                    ForeColor="Gray"></asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEditar" runat="server" CommandName="Editar" Text="Editar"
                                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnBorrar" runat="server" CommandName="Borrar" Text="Borrar"
                                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40%" />
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center;">
                                                <asp:Label ID="lblAl_Vol" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center;">
                                                <asp:Button ID="btnGuardar" runat="server" Visible="true" Text="Guardar" CssClass="btnRq"
                                                    OnClick="btnGuardar_Click"></asp:Button>
                                                <asp:Button ID="btnCancelar" runat="server" Visible="true" CssClass="btnRq" Text="Cancelar"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
            <td style="width:27%"></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                <table class="style1">
                    <tr>
                        <td colspan="2" align="center"></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                <table class="style1">
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Button ID="btnCal" runat="server" Height="23px" CssClass="btnRq" Text="Calcular"
                    Visible="False" Width="79px" OnClick="btnCal_Click" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Button ID="btnMod" runat="server" Height="23px" Width="79px" CssClass="btnRq"
                    Visible="false" Text="Modificar" OnClick="btnMod_Click" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Button ID="btnNCal" runat="server" Height="23px" CssClass="btnRq" Text="Nueva validación"
                    Visible="False" Width="130px" OnClick="btnNCal_Click" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="2" align="right"></td>
            <td colspan="2" align="left">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Label ID="lbl_E_SO" runat="server" Text="" CssClass="labelError" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:GridView ID="gv_SOrder" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="1" Visible="False" DataMember="DefaultView"
                    PageSize="5" OnPageIndexChanging="gv_SOrder_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="IdMaterial" HeaderText="Material" />
                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="IdCentro" HeaderText="Centro" />
                        <asp:BoundField DataField="PBase" HeaderText="Precio Unitario" />
                        <asp:BoundField DataField="PNeto" HeaderText="Precio TOTAL" />
                    </Columns>
                    <HeaderStyle CssClass="gvH3" />
                </asp:GridView>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gv_Condition" runat="server" class="style1" CssClass="gvAlSh" HorizontalAlign="Center"
                    CellPadding="1" Visible="False" OnPageIndexChanging="gv_Condition_PageIndexChanging">
                    <Columns>
                    </Columns>
                    <HeaderStyle CssClass="gvH3" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">&nbsp;</td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblECT" runat="server" Text="" CssClass="labelInfo" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4" align="center">&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsgVSO" runat="server" Text="" CssClass="labelNorm" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsgSF" runat="server" Text="" CssClass="labelNorm" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblAPG" runat="server" Text="" CssClass="" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblAP" runat="server" Text="" CssClass="" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Button ID="btnASC" runat="server" Height="23px" CssClass="btnRq" Text="Registrar Ajuste"
                    Visible="False" Width="130px" OnClick="btnAC_Click" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <!--<td colspan="2" align="center">
                <asp:Button ID="btnRegP" runat="server" Height="23px" CssClass="btnRq" Text="Registrar Programación"
                    Visible="False" Width="170px" OnClick="btnRegP_Click" />
            </td>-->
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center"></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Panel ID="PnlPU" runat="server" Visible="false" CssClass="modalPopup" Width="510px">
                    <table class="style1">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Label ID="lblPUMsg" runat="server" CssClass="labelError"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:TextBox ID="SAPID" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAcpMsg" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                                <asp:ModalPopupExtender ID="MPUP_AMsg" runat="server" TargetControlID="lblAcpMsg"
                                    OkControlID="lblAcpMsg" CancelControlID="lblCanMsg" BackgroundCssClass="modalBackground"
                                    PopupControlID="PnlPU" DropShadow="true" PopupDragHandleControlID="PnlPU" Drag="true"
                                    Enabled="True">
                                </asp:ModalPopupExtender>
                                <asp:Label ID="lblCanMsg" runat="server" CssClass="" Text="" Visible="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                                <asp:Button ID="btnAcptMsg" runat="server" Visible="true" Text="Aceptar" CssClass="btnRq" OnClick="btnAcptMsg_Click"></asp:Button>
                                <%--                                <asp:ModalPopupExtender ID="MPUP_AMsg" runat="server" TargetControlID="btnAcptMsg"
                                    OkControlID="btnAcptMsg" CancelControlID="btnAcptMsg" BackgroundCssClass="modalBackground"
                                    PopupControlID="PnlPU" DropShadow="true" PopupDragHandleControlID="PnlPU" Drag="true"
                                    Enabled="True">
                                </asp:ModalPopupExtender>--%></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblEC" runat="server" Text="" CssClass="labelInfo" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="gv_ExClientes" runat="server" class="style1" AllowPaging="True"
                    CssClass="gvAEx" HorizontalAlign="Center" CellPadding="1" PageSize="5" Visible="False">
                    <Columns>
                    </Columns>
                    <HeaderStyle CssClass="gvH1" Height="20px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" align="center">
                <asp:Label ID="lblExMsg" runat="server" Text="" CssClass="" Visible="false"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
