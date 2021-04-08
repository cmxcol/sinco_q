<%@ Page Title="Proforma" Language="C#" MasterPageFile="~/CO/MP/UsrP.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Proforma.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Usr.Proforma" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SMaster.css" rel="stylesheet" />
    <link href="../../Styles/SPublic.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../Scripts/jquery-2.1.4.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 17px;
        }

        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: lighter;
            border: solid 1px #006699;
            line-height: 20px;
            width: 300px !important;
            display: block;
            background-color: White;
            padding-left: 2px;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        .AutoExtenderList {
            display: block;
            elevation: higher;
            position: relative;
            z-index: 9999;
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: #000000;
        }

        .OptionAutoDesc:hover {
            background-color: cornflowerblue;
            color: white;
        }

        #gridViewDiv {
            margin-left: 35px;
            text-align: center;
        }

        #TotalDiv table {
            margin-left: 0px;
        }

        #TextArea1 {
            position: fixed;
            margin-bottom: 40px;
        }

        #genFacDiv {
            margin-left: 425px;
        }
    </style>
    <script type="text/javascript">



        function assign() {
            var desc = $('#ctl00_MainContent_DescAuto li:hover').text();
            var value = $('#ctl00_MainContent_DescAuto li:hover').val();
            $('#ctl00_MainContent_txtDesc').val(desc);
            $('#ctl00_MainContent_txtCodMat').val(value);
            $('#ctl00_MainContent_txtUni').val("M3");
            $('#ctl00_MainContent_txtVol').val(1);
            $('#ctl00_MainContent_CalcBtn').click();
            $('#ctl00_MainContent_DescAuto').css('visibility', 'hidden')
        }
        function mostrar() {

            $('#ctl00_MainContent_DescAuto').css('visibility') == 'visible' ? $('#ctl00_MainContent_DescAuto').css('visibility', 'hidden') : $('#ctl00_MainContent_DescAuto').css('visibility', 'visible');
        }

    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="contentPage" runat="server" Visible="true">
        <asp:Panel ID="FacProforma" runat="server">
            <table style="margin: auto auto auto 0; width: 100%" border="0">

                <tr>
                    <td style="text-align: center; height: 30px" colspan="7" class="Fondo_de_Titulo">
                        <asp:Label ID="lblProforma" Text="FORMATO FACTURA PROFORMA" runat="server" CssClass="styleLabel1" ForeColor="white"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                    <td>
                        <br />
                    </td>
                    <td>
                        <br />
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNit" runat="server" CssClass="StyleLabel2" Text="Nit Proforma:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAnticipoChecked" runat="server" CssClass="StyleLabel2" Text="¿Es un Anticipo?"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblIvaChecked" runat="server" CssClass="StyleLabel2" Text="¿Cliente con IVA?"></asp:Label>
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DropDownListNit" runat="server" CssClass="styleDDL2" Enabled="true">
                            <asp:ListItem Text="Cemex Administraciones Ltda" Value="830116138-9"></asp:ListItem>
                            <asp:ListItem Text="CEMEX COLOMBIA S.A" Value="880.002.623-1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="anticipoCheck" />
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="ivaChecked" />
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: left; width: 20%;">
                        <asp:Label ID="lblObra" runat="server" CssClass="StyleLabel2" Text="Código de Obra"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 20%;">
                        <asp:Label ID="lblNObra" runat="server" CssClass="StyleLabel2" Text="Nombre de Obra"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 20%;">
                        <asp:Label ID="lblCliente" runat="server" CssClass="StyleLabel2" Text="Código de Cliente"></asp:Label>
                    </td>
                    <td style="text-align: left; width: 20%;">
                        <asp:Label ID="lblNCliente" runat="server" CssClass="StyleLabel2" Text="Nombre de Cliente"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCodigoObra" runat="server" CssClass="styletxt1" MaxLength="8" OnTextChanged="Obra_Changed" AutoPostBack="true" AutoCompleteType="None" Width="180"></asp:TextBox>
                        <%--johanProforma se agrega with--%>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtCodObra" runat="server" Enabled="True"
                            ValidChars="0123456789" TargetControlID="txtCodigoObra"></asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtNombreObra" runat="server" CssClass="styletxt1" Enabled="false" Width="280"></asp:TextBox><%--johanProforma se agrega with--%>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtNObra" runat="server" Enabled="True"
                            ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.- " TargetControlID="txtNombreObra"></asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCodigoCliente" runat="server" CssClass="styletxt1" MaxLength="8" Enabled="false" Width="180"></asp:TextBox><%--johanProforma se agrega with--%>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtCodCliente" runat="server" Enabled="True"
                            ValidChars="0123456789" TargetControlID="txtCodigoCliente"></asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="styletxt1" Enabled="false" Width="280"></asp:TextBox><%--johanProforma se agrega with--%>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtNCliente" runat="server" Enabled="True"
                            ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ.- " TargetControlID="txtNombreCliente"></asp:FilteredTextBoxExtender>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="onErrorObra" runat="server" CssClass="StyleLabel2" Text="" Style="font-size: 10px; color: red;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        <asp:Label ID="lblDir" runat="server" CssClass="StyleLabel2" Text="Dirección"></asp:Label>
                    </td>
                    <td style="text-align: left;" colspan="1">
                        <asp:Label ID="lnlNIT" runat="server" CssClass="StyleLabel2" Text="NIT"></asp:Label>
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <asp:Label ID="lblCom" runat="server" CssClass="StyleLabel2" Text="Comercial"></asp:Label>
                    </td>



                </tr>
                <tr>
                    <td style="text-align: left;" colspan="2">
                        <asp:TextBox ID="txtDir" runat="server" CssClass="styletxt1" Enabled="false" Width="440"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtDir" runat="server" Enabled="True"
                            ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz.-# " TargetControlID="txtDir"></asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtNIT" runat="server" CssClass="styletxt1" Enabled="false" Width="180"></asp:TextBox><%--johanProforma se agrega with--%>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                            ValidChars="0123456789" TargetControlID="txtNIT"></asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;" colspan="2">
                        <asp:DropDownList ID="DDCom" runat="server" CssClass="styleDDL2" DataTextField="NVen" DataValueField="IdCodVen" Enabled="true" Width="282"></asp:DropDownList>
                    </td>
                </tr>

            </table>
        </asp:Panel>

        <asp:Panel ID="PnlSol" runat="server">
            <br />
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center; height: 20px" class="Fondo_de_Titulo" colspan="5">
                        <asp:Label ID="Label29" Text="DATOS SOLICITANTE" runat="server" CssClass="styleLabel1"
                            ForeColor="white"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:Label ID="lblSolName" runat="server" CssClass="StyleLabel2" Text="Nombre"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblSolCargo" runat="server" CssClass="StyleLabel2" Text="Cargo"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblSolTel" runat="server" CssClass="StyleLabel2" Text="Teléfono"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblSolMail" runat="server" CssClass="StyleLabel2" Text="e-Mail"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblSolFecha" runat="server" CssClass="StyleLabel2" Text="Fecha"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSolName" runat="server" CssClass="styletxt1" Enabled="false" OnTextChanged="DatosSol_Changed" AutoPostBack="true"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtSolName" runat="server" Enabled="True"
                            ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz.-# " TargetControlID="txtSolName"></asp:FilteredTextBoxExtender>

                    </td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="DDCargo" runat="server" CssClass="styleDDL2"  DataTextField="NCargo" DataValueField="IdCargo" Enabled="false" OnSelectedIndexChanged="DDCargo_SelectedIndexChanged" AutoPostBack="true" >     </asp:DropDownList>
                        <%--<asp:SqlDataSource ID="CargosDir" runat="server" ConnectionString="<%$ ConnectionStrings:cnnCOS %>" SelectCommand="SELECT [IdCargo], [NCargo] FROM [hr].[catCargos]"></asp:SqlDataSource>--%>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSolTel" runat="server" CssClass="styletxt1" Enabled="false"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtSolTel" runat="server" Enabled="True"
                            ValidChars="0123456789" TargetControlID="txtSolTel"></asp:FilteredTextBoxExtender>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSolMail" runat="server" CssClass="styletxt1" Enabled="false" OnTextChanged="DatosSol_Changed" AutoPostBack="true"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredtxtSolMail" runat="server" Enabled="True"
                            ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz.-#@;" TargetControlID="txtSolMail"></asp:FilteredTextBoxExtender>

                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="styletxt1" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="onErrorSol" runat="server" CssClass="StyleLabel3" Text="" Style="font-size: 10px; color: red;"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <br />

        <asp:Panel ID="FilterProduc" runat="server">
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="text-align: center; height: 20px" class="Fondo_de_Titulo" colspan="10">
                        <asp:Label ID="lblConfMat" runat="server" Text="PRODUCTOS" CssClass="styleLabel1" ForeColor="White"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr id="FilterTitles" runat="server">
                    <%--johan proforma agregar TEXT--%>

                    <td style="text-align: left; background-color: #585858; height: 15px" class="Fondo_de_Titulo">
                        <asp:Label ID="lblCodProdc" runat="server" CssClass="StyleLabel3" Text="Codigo Producto:"></asp:Label>
                    </td>
                    <td style="text-align: left; background-color: #585858;" class="Fondo_de_Titulo">
                        <asp:Label ID="lblUniM" runat="server" CssClass="StyleLabel3" Text="Unidad M:"></asp:Label>
                    </td>
                    <td style="text-align: left; background-color: #585858;" class="Fondo_de_Titulo">
                        <asp:Label ID="lblCantidad" runat="server" CssClass="StyleLabel3" Text="Cantidad:"></asp:Label>
                    </td>
                    <td style="text-align: left; background-color: #585858;" class="Fondo_de_Titulo">
                        <asp:Label ID="lblValUnid" runat="server" CssClass="StyleLabel3" Text="Valor Unidad (Incluye IVA)"></asp:Label>
                    </td>
                    <td style="text-align: left; background-color: #585858;" class="Fondo_de_Titulo">
                        <asp:Label ID="lblValTotal" runat="server" CssClass="StyleLabel3" Text="Valor Total"></asp:Label>
                    </td>
                </tr>
                <tr id="FilterFields" runat="server">
                    <td>
                        <asp:TextBox ID="CodProducto" runat="server" CssClass="styletxt1" OnTextChanged="product_concat" AutoPostBack="true" autocomplete="off" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="DDUniM" DataTextField="UniM" DataValueField="UniM" CssClass="styleDDL2" Enabled="false" OnTextChanged="Prod_Changed" AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                          <asp:TextBox ID="txtUnidadMedi" runat="server" Enabled="false"  CssClass="styleDDL2" Visible="false" AutoPostBack="true"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="false"
                            ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz.-# " TargetControlID="txtSolName"></asp:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtCantidad" runat="server" CssClass="styletxt1" Enabled="false" Width="100px" OnTextChanged="Prod_Changed" AutoPostBack="true"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTxtCantidad" runat="server" Enabled="True" ValidChars="0123456789" TargetControlID="TxtCantidad"></asp:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <span><b>$</b></span><asp:TextBox ID="TxtValUnidad" runat="server" CssClass="styletxt1" Enabled="false" Width="100px" OnTextChanged="Prod_Changed" AutoPostBack="true"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTxtValUnidad" runat="server" Enabled="True" ValidChars="0123456789." TargetControlID="TxtValUnidad"></asp:FilteredTextBoxExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtValTotal" runat="server" CssClass="styletxt1" Enabled="false" Width="200px"></asp:TextBox>
                        <asp:FilteredTextBoxExtender ID="FilteredTxtValTotal" runat="server" Enabled="True" ValidChars="0123456789.$" TargetControlID="TxtValTotal"></asp:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="addNewMat2" runat="server" OnClick="add_mat_btn" Font-Underline="false">¿No encuentras el Material?</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="addUnidad" runat="server" OnClick="add_Unidad" Font-Underline="false">¿Otro cual?</asp:LinkButton>
                       
                    </td>
                    <td style="text-align: center;" colspan="1"><%--johanProforma--%>
                        <asp:Button ID="btn_Agregar_Prod" runat="server" CssClass="styleButton2" Text="AGREGAR PRODUCTO" OnClick="btn_Agregar_Prod_Click" Enabled="false" />
                    </td>
                    
                </tr>

             
            </table>

            <asp:Panel ID="pnlAddMat" runat="server" Visible="true">
            </asp:Panel>

            <br />
            <div id="gridViewDiv">
                <asp:GridView ID="GVProduct" runat="server" AllowPaging="false" CellPadding="1" PageSize="10" CssClass="styleGV1" Visible="true" AutoGenerateColumns="false" Width="900px" OnRowCommand="GVProduct_RowCommand" OnRowDeleting="GVProduct_RowDeleting">
                    <RowStyle BackColor="#D8D8D8" ForeColor="#000000" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="true" ForeColor="White" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="true" ForeColor="#333333" />
                    <HeaderStyle BackColor="#284775" Font-Bold="true" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Cod" HeaderText="Código Producto" ItemStyle-Width="20%" ItemStyle-Wrap="false"  htmlencode="false" />
                        <asp:BoundField DataField="Uni" HeaderText="Unidad M" ItemStyle-Width="20%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"   htmlencode="false"/>
                        <asp:BoundField DataField="Vol" HeaderText="Cantidad" ItemStyle-Width="10%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  htmlencode="false" />
                        <asp:BoundField DataField="VrUni" HeaderText="Vr/Unidad" ItemStyle-Width="15%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"   htmlencode="false"/>
                        <asp:BoundField DataField="VrIva" HeaderText="Iva" ItemStyle-Width="15%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  htmlencode="false" />
                        <asp:BoundField DataField="VrTotal" HeaderText="Vr/Total" ItemStyle-Width="15%" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"   htmlencode="false" />
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnBorrar" runat="server" CommandName="Borrar" Text="Borrar" CommandArgument="<%#Container.DisplayIndex %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Label ID="onErrorGrid" runat="server" CssClass="StyleLabel2" TFext="" Style="font-size: 10px; color: red;"></asp:Label>

        </asp:Panel>
        <br />
        <br />

        <asp:Panel ID="PnlTotal" runat="server">
            <div id="TotalDiv">
                <table border="0" style="width: 100%;">
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label runat="server" ID="lblOberser" Text="Observacion:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="3">
                            <asp:TextBox ID="TextAreaObserv" TextMode="multiline" style="resize:none" Columns="70" Rows="6" runat="server" enabled="false"  htmlencode="false" /></td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblSubtotal" Text="Subtotal"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" Enabled="false" ID="ValSubtotal" Style="text-align: right"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblIva" Text="IVA"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" Enabled="false" ID="ValIva" Style="text-align: right"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblTotal" Text="Neto a Pagar"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" Enabled="false" ID="ValTotal" Style="text-align: center"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
            <br />
            <div id="genFacDiv">
                <asp:Button runat="server" ID="btnGenFactura" Text="Generar Factura" CssClass="styleButton2" OnClick="btn_Gen_Factura_Click" Enabled="false" />
            </div>

            <script type="text/javascript">
                function recargar() {
                    location.reload();
                }
            </script>
        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="pnlMsgg2" runat="server" CssClass="modalpopup" ViewStateMode="Enabled"
        Style="display: none;" BorderColor="#0033cc" BackColor="White" BorderWidth="2px">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left;">
                    <asp:Panel ID="PnlMsg3" runat="server"
                        BackColor="White" DefaultButton="btnAceptarMsg">
                        <asp:Label ID="lblmsg12" runat="server"></asp:Label>
                        <asp:Label ID="lblmsg22" runat="server"></asp:Label>
                        <table style="width: 10%; margin: 10px;" border="0">
                            <tr>
                                <td colspan="4">
                                    <asp:Label runat="server" ID="OnErrorAddMat" CssClass="StyleLabel2" Style="font-size: 10px; color: red;"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label runat="server" ID="codMatAdd" Text="Producto: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProdAdd" runat="server" CssClass="styletxt1" Enabled="false" Width="60px" Height="16px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredtxtProdAdd" runat="server" Enabled="True" ValidChars="0123456789" TargetControlID="txtProdAdd"></asp:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label runat="server" ID="nomMatAdd" Text="-"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtnomMaterial" runat="server" CssClass="styletxt1" Enabled="false" Width="150px" Height="16px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" ValidChars="0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz " TargetControlID="txtnomMaterial"></asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="addIvaLvl" Text="Material con Iva: "></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxIvaAdd" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; margin-top: 5px;" colspan="2">
                                    <asp:Button ID="addMattBtn" runat="server" Text="GUARDAR" OnClick="addMattBtn_click" Width="100px" CssClass="styleButton2" />
                                </td>
                                <td style="text-align: center; margin-top: 5px;" colspan="2">
                                    <asp:Button ID="Button1" runat="server" Text="CANCELAR" OnClick="show_content" Width="100px" CssClass="styleButton2" />
                                </td>
                            </tr>
                        </table>

                    </asp:Panel>
                    <asp:ModalPopupExtender ID="MoPoUpMsg2" runat="server" Enabled="True"
                        TargetControlID="lblmsg12" CancelControlID="lblmsg22" PopupControlID="PnlMsgg2"
                        OkControlID="lblmsg12" BackgroundCssClass="modalBackground" Drag="True">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <%-----------------------------------------------------------------------------------%>


    <asp:Panel ID="pnlMsgCargo" runat="server" CssClass="modalpopup" ViewStateMode="Enabled"
        Style="display: none;" BorderColor="#0033cc" BackColor="White" BorderWidth="2px">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left;">
                    <asp:Panel ID="PnlMsg4" runat="server"
                        BackColor="White" DefaultButton="btnAceptarMsg">
                        <asp:Label ID="lblmsg13" runat="server"></asp:Label>
                        <asp:Label ID="lblmsg33" runat="server"></asp:Label>
                        <table style="width: 10%; margin: 10px;" border="0">
                            <tr>
                                <td colspan="4">
                                    <asp:Label runat="server" ID="OnErrorAddCargo" CssClass="StyleLabel2" Style="font-size: 10px; color: red;"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label runat="server" ID="Label6" Text="Cargo: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCargoAdd" runat="server" CssClass="styletxt1" Enabled="false" Width="60px" Height="16px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" ValidChars="0123456789" TargetControlID="txtProdAdd"></asp:FilteredTextBoxExtender>
                                </td>
                                
                            </tr>
                           
                            <tr>
                                <td style="text-align: center; margin-top: 5px;" colspan="2">
                                    <asp:Button ID="Button2" runat="server" Text="GUARDAR" OnClick="addCargoBtn_click" Width="100px" CssClass="styleButton2" />
                                </td>
                                <td style="text-align: center; margin-top: 5px;" colspan="2">
                                    <asp:Button ID="Button3" runat="server" Text="CANCELAR" OnClick="show_content" Width="100px" CssClass="styleButton2" />
                                </td>
                            </tr>
                        </table>

                    </asp:Panel>
                    <asp:ModalPopupExtender ID="MoPoUpCargo" runat="server" Enabled="True"
                        TargetControlID="lblmsg13" CancelControlID="lblmsg33" PopupControlID="pnlMsgCargo"
                        OkControlID="lblmsg13" BackgroundCssClass="modalBackground" Drag="True">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <%-----------------------------------------------------------------------------------%>

    



    <asp:Panel ID="pnlMsgg" runat="server" CssClass="modalpopup" ViewStateMode="Enabled"
        Visible="false" BorderColor="Black" BackColor="White">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left;">
                    <asp:Panel ID="PnlMsg" runat="server" BorderColor="#0033cc" BorderStyle="Solid" BorderWidth="2px"
                        BackColor="White" DefaultButton="btnAceptarMsg">
                        <table style="width: 100%; margin: 0 auto;" border="0">
                            <tr>
                                <td style="text-align: center;">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblMensaje" Font-Size="12px" runat="server" CssClass="StyleLabel2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblmsg1" runat="server"></asp:Label>
                                                <asp:Label ID="lblmsg2" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnAceptarMsg" runat="server" OnClick="Notificar_EmailBtn" Text="Enviar Email" Width="100px" CssClass="styleButton2" />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnCancelMsg" runat="server" Text="Cancelar Email" OnClick="Cancelar_EmailBtn" Width="100px" CssClass="styleButton2" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;" colspan="2">
                                                <asp:HyperLink ID="hyperlinkPreviewPDF" runat="server" Target="_blank">Vizualizar PDF </asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="MoPoUpMsg" runat="server" Enabled="True"
                        TargetControlID="lblmsg1" CancelControlID="lblmsg2" PopupControlID="PnlMsgg"
                        OkControlID="lblmsg1" BackgroundCssClass="modalBackground" Drag="True">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlMsgg3" runat="server" CssClass="modalpopup" ViewStateMode="Enabled"
        Visible="false" BorderColor="Black" BackColor="White">
        <table style="width: 100%">
            <tr>
                <td style="text-align: left;">
                    <asp:Panel ID="Panel2" runat="server" BorderColor="#0033cc" BorderStyle="Solid" BorderWidth="2px"
                        BackColor="White" DefaultButton="btnAceptarMsg">
                        <table style="width: 100%; margin: 0 auto;" border="0">
                            <tr>
                                <td style="text-align: center;">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblMensaje2" Font-Size="12px" runat="server" CssClass="StyleLabel2"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnAceptarMsg2" runat="server" Text="Entendido" Width="100px" CssClass="styleButton2" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="MoPoUpMsg3" runat="server" Enabled="True"
                        TargetControlID="Label2" CancelControlID="Label3" PopupControlID="pnlMsgg3"
                        OkControlID="lblmsg1" BackgroundCssClass="modalBackground" Drag="True">
                    </asp:ModalPopupExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>
