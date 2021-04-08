<%@ Page Title="Programación" Language="C#" MasterPageFile="~/ME/MP/AdmME.Master"
    AutoEventWireup="true" CodeBehind="ProgPedidos.aspx.cs" Inherits="WebApplication.Client.Web.ME.PG.Usr.ProgPedidos" EnableEventValidation="false" %>

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
            <td colspan="4" align="center">&nbsp;
            </td>
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
            <td colspan="2" align="center"></td>
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
