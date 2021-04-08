<%@ Page Title="Validación" Language="C#" MasterPageFile="~/CO/MP/UsrP.Master" AutoEventWireup="true" CodeBehind="DescVal.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Usr.DescVal" EnableEventValidation="false" %>


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
            <td style="width:10%"></td>
            <td style="width:30%;text-align:right">
                <asp:Label ID="lblCodObra" runat="server" Text="Codigo de Obra" CssClass="labelRq"></asp:Label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:20%;text-align:left">
                            <asp:TextBox ID="txtCodObra" runat="server" CssClass="txtrq"  MaxLength="8" Width="70px" AutoCOmpleteType="None"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="txtCodObra_FilteredTextBoxExtender" runat="server"
                                Enabled="True" FilterType="Numbers" TargetControlID="txtCodObra">
                            </asp:FilteredTextBoxExtender>
                            </td>
                        <td style="text-align:left;width:40%">


                                <asp:DropDownList ID="CHKPlanta" runat="server" Style="font-family: Arial; font-size: 9pt">
                                    <asp:ListItem Enabled="true" Text="No Aplica Planta" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Planta Rionegro" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Planta Anapoima" Value="1"></asp:ListItem>
                                </asp:DropDownList>


                            </td>
                        
                        <%--</td>

                         <td style="width:40%;text-align:left">
                           <asp:DropDownList ID="DDLTipoAjuste" runat="server" CssClass="styleDDL2" AutoPostBack="true"
                                                Visible="true" OnSelectedIndexChanged="DDLTipoAjuste_SelectedIndexChanged">
                           </asp:DropDownList>
                          
                        </td>--%>


                        <td style="text-align:left;width:60%">
                            <asp:Label ID="lblErrorCod" runat="server" Text="Datos Incorrectos" CssClass="labelError"
                                Visible="false"></asp:Label>
                             <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtCodObra" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{8,8}$" runat="server" ErrorMessage="Inserte número de 8 dígitos."></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
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
            <td colspan="4" style="text-align:center">
                <asp:GridView ID="gv_DataDesc" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" Visible="False" DataMember="DefaultView"
                    PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="IdCliente" HeaderText="Cod Cliente">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdObra" HeaderText="Cod Obra">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdSector" HeaderText="Sector">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Segmentacion" HeaderText="Tamaño Cliente"  />
                        <asp:BoundField DataField="SegmentacionCli" HeaderText="Segmentacion Cliente"  />
                        <asp:BoundField DataField="SegmentacionReg" HeaderText="Segmentacion Regional"  />
                        <asp:BoundField DataField="Descuento_planta" HeaderText="Descuento planta"  />
                        <asp:BoundField DataField="TipoObra" HeaderText="Tipo de Obra"  />
                        <asp:BoundField DataField="Logistica" HeaderText="Logística"  />
                        <asp:BoundField DataField="Max" HeaderText="Máximo Descuento"  />
                      

                    </Columns>
                    <HeaderStyle CssClass="gvH2" />
                </asp:GridView>
            </td>
        </tr>

         <tr>
             <td></td>
            <td style="text-align:center;" colspan="2">
            <asp:Label  runat="server" id="lbl_Error" style="font-size: 20px; color: #ff0000;display:none;text-align:center"> </asp:Label>
                 </td>
            <td>&nbsp;
            </td>
        </tr>

        <tr>
             <td></td>
            <td style="text-align:center;" colspan="2">
            <asp:Label  runat="server" id="Desc" style="font-size: 20px; color: #ff0000;display:none;text-align:center"> </asp:Label>
                 </td>
            <td>&nbsp;
            </td>
        </tr>
           </table>
     <table id="formulario" class="style1" style="display:none">
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%;text-align:right">
                <asp:Label ID="lblSeg" runat="server" Text="Tamaño Cliente" CssClass="labelRq"></asp:Label><label style="color:#ff0000">*</label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%;text-align:left">
                            <select id="Seg">
                            </select>
                        </td>
                        <td style="text-align:left;width:60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
   

        <tr>
            <td style="width:20%"></td>
            <td style="width:30%;text-align:right">
                <asp:Label ID="lblLogis" runat="server" Text="Logística" CssClass="labelRq"></asp:Label><label style="color:#ff0000">*</label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%;text-align:left">
                            <select id="Logis">
                            </select>
                        </td>
                        <td style="text-align:left;width:60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%;text-align:right">
                <asp:Label ID="lblSegCli" runat="server" Text="Segmentación Cliente" CssClass="labelRq"></asp:Label><label style="color:#ff0000">*</label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%;text-align:left">
                            <select id="SegCli">
                            </select>
                        </td>
                        <td style="text-align:left;width:60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%;text-align:right">
                <asp:Label ID="lblSegReg" runat="server" Text="Segmentación Region" CssClass="labelRq"></asp:Label><label style="color:#ff0000">*</label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%;text-align:left">
                            <select id="SegReg">
                            </select>
                        </td>
                        <td style="text-align:left;width:60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>
        <tr>
            <td style="width:20%"></td>
            <td style="width:30%;text-align:right">
                <asp:Label ID="Label1" runat="server" Text="Planta" CssClass="labelRq"></asp:Label><label style="color:#ff0000">*</label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%;text-align:left">
                            <select id="planta">
                            </select>
                        </td>
                        <td style="text-align:left;width:60%"></td>
                    </tr>
                </table>
            </td>
            <td style="width:20%"></td>
        </tr>



        <tr>
            <td style="width:20%"></td>
            <td style="width:30%;text-align:right">
                <asp:Label ID="lblTObra" runat="server" Text="Tipo Obra" CssClass="labelRq"></asp:Label><label style="color:#ff0000">*</label>
            </td>
            <td style="width:30%">
                <table class="style1">
                    <tr>
                        <td style="width:40%;text-align:left">
                            <select id="TObra">
                            </select>
                        </td>
                        <td style="text-align:left;width:60%"></td>
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
            <label  id="Desc2" style="font-size: 20px; color: #ff0000;display:none;text-align:center"> </label>
                 </td>
            <td>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
