<%@ Page Title="Vigencia - Clientes Nacionales" Language="C#" MasterPageFile="~/CO/MP/AdmCS.Master"
    AutoEventWireup="true" CodeBehind="Vigencia.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Adm.Vigencia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SPublic.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function prueba() {
          //var value = prompt("Ingrese le Motivo", "");
          
          //var option = document.createElement(value);
          //option.text = option.value = value;
          //document.getElementById("dropMotivo").add(option, 0);

            $('#dropMotivo').append(new Option("8756", "234"));
            
          
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td>
            </td>
            <td>
            </td>
                <%--<asp:LinkButton runat="server" id="SomeLinkButton" href="CBloMasivo.aspx" style="float:right" >Cargue masivo de Clientes</asp:LinkButton>--%>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="center">
                <asp:GridView ID="gvCliBlo" runat="server" AllowPaging="True" CssClass="gvAlertas"
                    HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                    Visible="False" DataMember="DefaultView" PageSize="15" OnPageIndexChanging="gvCliBlo_PageIndexChanging"
                    OnRowCommand="gvCliBlo_RowCommand" Width="70%">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True">
                            <HeaderStyle Wrap="True" Width="4%" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="IdCliente">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIdCli" CssClass="txtrqGCei" runat="server" Text='<%# Bind("IdCliente") %>'
                                    Width="30%"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtIdCli_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" ValidChars="0123456789" TargetControlID="txtIdCli"  >
                                </asp:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCli" runat="server" Text='<%# Bind("IdCliente") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="9%" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Deudor">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtVigencia" CssClass="txtrqGCei" runat="server" Text='<%# Bind("Vigencia") %>'
                                    Width="95%"></asp:TextBox>
                                
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblVigencia" runat="server" Text='<%# Bind("Vigencia") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="9%" />
                        </asp:TemplateField>
                        
                                              
                        
                      

                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnGuardar" runat="server" CommandName="Guardar" Text="Guardar"
                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCan" runat="server" CommandName="Cancelar" Text="Cancelar"
                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                <asp:LinkButton ID="lbtnNew" runat="server" CommandName="Nuevo" Text="Nuevo" CommandArgument="1"
                                    ForeColor="Gray"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnBq" runat="server" CommandName="Buscar" Text="Buscar" CommandArgument="2"
                                    ForeColor="Gray"></asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEditar" runat="server" CommandName="Editar" Text="Editar"
                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnBorrar" runat="server" CommandName="Borrar" Text="Borrar"
                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="4%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width="950px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align:center;">
                <asp:GridView ID="gv_MsgTEx" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="false" CellPadding="3" CellSpacing="1" 
                    CssClass="gvAlertas" DataMember="DefaultView" HorizontalAlign="Center" PageSize="5">
                    <Columns>
                        <asp:TemplateField ShowHeader="false">
                            <ItemTemplate>
                                    <asp:Label ID="lblMsgTEx" runat="server" CssClass="labelAlert" Visible="true" Text = '<%# Bind("Msg") %>' ></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="700px" Wrap="True" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="White" ForeColor="Black" Width="700px" />
                </asp:GridView>
            </td>
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
            <td style="text-align:center;">
                <asp:Label ID="lblEInsUp" runat="server" CssClass="labelError" Visible="false"></asp:Label>
            </td>
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
            <td colspan="3" align="center">
                <asp:Panel ID="pnlMpBA" runat="server" Visible="false" CssClass="modalPopup" Width="700px">
                    <table class="style1">
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                                <asp:GridView ID="gv_Alertas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="3" CellSpacing="1" CssClass="gvAlertas" DataMember="DefaultView"
                                    HorizontalAlign="Center" OnPageIndexChanging="gv_Alertas_PageIndexChanging" PageSize="5">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnId" runat="server" Text='<%# Bind("Id") %>' CommandArgument="<%# Container.DisplayIndex %>"
                                                    ForeColor="#000066" OnClick="lbtnId_Click"> </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cliente">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCli" runat="server" Text='<%# Bind("IdCliente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="250px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deudor">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbDeudor" runat="server" Text='<%# Bind("Vigencia") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="250px" Wrap="True" />
                                        </asp:TemplateField>
                              
                                        
                                        
                                        
                                    </Columns>
                                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width="600px" />
                                </asp:GridView>
                            </td>
                            <td>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                                <asp:Label ID="lblAcpMpBA" runat="server" Visible="true"></asp:Label>
                                <asp:ModalPopupExtender ID="MpUp_MA" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="lblCanlMpBA" DynamicServicePath="" Enabled="True" OkControlID="lblAcpMpBA"
                                    PopupControlID="pnlMpBA" TargetControlID="lblAcpMpBA">
                                </asp:ModalPopupExtender>
                                <asp:Label ID="lblCanlMpBA" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                                <asp:Panel ID="pnlBusq" runat="server" Visible="True" Width="600px">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="text-align:center;">
                                                <asp:Label ID="lblBusq" runat="server" CssClass="labelRq" Text="Búsqueda"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:100px">
                                            </td>
                                            <td style="text-align:center;" width="400px">
                                                <table class="style1">
                                                    <tr>
                                                        <td style="text-align:right;" width="200px">
                                                            <asp:Label ID="lblId" runat="server" CssClass="labelRq"></asp:Label>
                                                        </td>
                                                        <td style="text-align:left;" width="200px">
                                                            <asp:TextBox ID="txtIdBusq" runat="server" CssClass="txtrq2" MaxLength="10" Width="150px"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtIdBusq_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtIdBusq" WatermarkText="Id">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:FilteredTextBoxExtender ID="txtIdBusq_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterMode="InvalidChars" InvalidChars="'" TargetControlID="txtIdBusq">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr align="left">
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblTxtCod" runat="server" CssClass="labelRq"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMSGBusq" runat="server" CssClass="txtrq2" Width="150px">
                                                            </asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtMSGBusq_TextBoxWatermarkExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtMSGBusq">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:FilteredTextBoxExtender ID="txtMSGBusq_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterMode="InvalidChars" InvalidChars="'" TargetControlID="txtMSGBusq">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                   
                                                </table>
                                            </td>
                                            <td style="width:100px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="text-align:center;">
                                                <asp:Label ID="lblEBusq" runat="server" CssClass="labelError" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="text-align:center;">
                                                <table class="style1">
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Button ID="btnBusq" runat="server" CssClass="btnRq" OnClick="btnBusq_Click"
                                                                Text="Buscar" />
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:Button ID="btnCanlBusq" runat="server" CssClass="btnRq" Text="Cancelar" OnClick="btnCanlBusq_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>



            <%-----------------------------------------------------------%>
      
        </tr>

        

        <tr>
            <td>
            </td>
            <td colspan="3" align="center">
                <asp:Panel ID="pnlMpCEmpDoc" runat="server" CssClass="modalPopup" Visible="false"
                    Width="700px">
                    <table class="style1">
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="3">
                            </td>
                            <td>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="3">
                                <asp:GridView ID="gvCEmp" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                                    CellPadding="3" CellSpacing="1" CssClass="gvAlertas" DataMember="DefaultView"
                                    HorizontalAlign="Center" PageSize="5">
                                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width="600px" />
                                </asp:GridView>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                                <asp:Label ID="lblEDoc" runat="server" CssClass="labelError" Visible="false"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="3">
                                <asp:Label ID="lblAcpMpBA0" runat="server" Visible="true"></asp:Label>
                                <asp:ModalPopupExtender ID="MpUp_CEmpDoc" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="lblCanlMpBA0" DynamicServicePath="" Enabled="True" OkControlID="lblAcpMpBA0"
                                    PopupControlID="pnlMpCEmpDoc" TargetControlID="lblAcpMpBA0">
                                </asp:ModalPopupExtender>
                                <asp:Label ID="lblCanlMpBA0" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        
                    </table>
                </asp:Panel>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="text-align:center;" colspan="3">
                &nbsp;
            </td>
            
        </tr>
        <tr>
            <td >
               
            </td>
        </tr>
        
    </table>
    <div >
         <asp:Button id="btnCliBlo" Text="Carga Masiva de Vigencias" runat="server" OnClick="btnCMasiva_Click" style="float:right; background-color:#0E1725; color:#ffffff;padding:7px; cursor:pointer; border-radius: 10px; margin-right: 110px"></asp:Button >
    </div>

   
    
</asp:Content>
