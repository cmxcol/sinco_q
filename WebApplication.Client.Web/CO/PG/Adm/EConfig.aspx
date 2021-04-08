<%@ Page Title="Excepciones" Language="C#" MasterPageFile="~/CO/MP/AdmCS.Master"
    AutoEventWireup="true" CodeBehind="EConfig.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Adm.EConfig" %>

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
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="center">
                <asp:GridView ID="gvExCli" runat="server" AllowPaging="True" CssClass="gvAlertas"
                    HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                    Visible="False" DataMember="DefaultView" PageSize="5" OnPageIndexChanging="gvExCli_PageIndexChanging"
                    OnRowCommand="gvExCli_RowCommand" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="IdEx" HeaderText="ID" ReadOnly="True">
                            <HeaderStyle Wrap="True" Width="4%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="IdCliente">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIdCli" CssClass="txtrqGCei" runat="server" Text='<%# Bind("IdCliente") %>' AutoPostBack="true" OnTextChanged ="txtIdCli_TextChanged"
                                    Width="95%"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="txtIdCli_FilteredTextBoxExtender" runat="server"
                                    Enabled="True" FilterMode="ValidChars" ValidChars="0123456789" TargetControlID="txtIdCli"  >
                                </asp:FilteredTextBoxExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCli" runat="server" Text='<%# Bind("IdCliente") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="9%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Segmento">
                            <ItemTemplate>
                                <asp:Label ID="lblSegmento" runat="server" Text='<%# Bind("Segmento") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:DropDownList ID="ddlTSegmento" runat="server" CssClass="ddlGVei" Width="98%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlTSeg_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="True" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo_Ex">
                            <ItemTemplate>
                                <asp:Label ID="lblTEx" runat="server" Text='<%# Bind("NTEx") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlTEx" runat="server" CssClass="ddlGVei" Width="98%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlTEx_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="True" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Vigencia">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtdtVg" CssClass="txtrqGCei" runat="server" Text='<%# Bind("dtVig") %>'
                                    Width="95%"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txtdtVg_TextBoxWatermarkExtender" runat="server"
                                    Enabled="true" TargetControlID="txtdtVg" WatermarkText="yyyy-MM-dd">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="txtdtVg_CalendarExtender" runat="server" Enabled="true"
                                    Format="yyyy-MM-dd" TargetControlID="txtdtVg">
                                </asp:CalendarExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbldtVg" runat="server" Text='<%# Bind("dtVig") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="9%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MsgEx">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMsgEx" CssClass="txtrqGCei" runat="server" Text='<%# Bind("MsgEx") %>'
                                    Width="95%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMsgEx" runat="server" Text='<%# Bind("MsgEx") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Autorizante">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnIdEmpAuthC" runat="server" CommandName="EmpAuthC" Text='<%# Bind("IdEmp") %>'
                                    CommandArgument="<%# Container.DisplayIndex %>" ForeColor="#000066" Width="50px"
                                    OnClick="lbtnIdEmpAuthC_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="9%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Doc Adjuntos">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDocAdjV" runat="server" CommandName="DocAdj" Text='<%# Bind("CountDoc") %>'
                                    CommandArgument="<%# Container.DisplayIndex %>" ForeColor="#000066" Width="50px"></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnAddDocAdj" runat="server" CommandName="AddDoc" Text="Agregar"
                                    ForeColor="#000066" CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                <%--                                &nbsp;<asp:LinkButton ID="lbtnModDocAdj" runat="server" CommandName="ModDoc" Text="Editar"
                                    ForeColor="#000066" CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>--%>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="True" Width="9%" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Comercial">
                            <ItemTemplate>
                                <asp:Label ID="lblComercial" runat="server" Text='<%# Bind("Comercial") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="9%" />
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCome" runat="server" CssClass="ddlGVei" Width="98%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCome_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <%-------------------------------%>

                        <asp:TemplateField HeaderText="Motivo">
                         
                                

                        <EditItemTemplate>                                                         
                               <asp:DropDownList ID="dropMotivo" runat="server" Width="95%" CssClass="ddlGVei"  AutoPostBack="true" OnSelectedIndexChanged="dropMotivo_SelectedIndexChanged"  >

                                </asp:DropDownList>

                               </EditItemTemplate>
                              
                           
                            <ItemTemplate>
                                <asp:Label ID="lblMotivo" runat="server" Text='<%# Bind("Motivo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="95%" />
                        </asp:TemplateField>

                        <%-------------------------------%>

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
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width="950px" />
                </asp:GridView>
            </td>
        </tr>
        <tr >
         
         </tr>
        
        <tr >
           
            <td colspan="5" align="center">
                <asp:Button id="btnCliBlo" Text="Clientes Bloqueados" runat="server" OnClick="btnCliBlo_Click" style="float:right; background-color:#0E1725; color:#ffffff;padding:7px; cursor:pointer; border-radius: 10px;"></asp:Button >
                <%--<asp:LinkButton runat="server" id="SomeLinkButton" href="CBloqueados.aspx" style="float:right" >Clientes Bloqueados</asp:LinkButton>--%>
                
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
                 
                <asp:Label ID="lblEInsUp" runat="server" CssClass="labelError" Visible="false"></asp:Label>
           
                <asp:GridView ID="gv_MsgTEx" runat="server" AllowPaging="False" 
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
                                                <asp:LinkButton ID="lbtnIdEx" runat="server" Text='<%# Bind("Id") %>' CommandArgument="<%# Container.DisplayIndex %>"
                                                    ForeColor="#000066" OnClick="lbtnIdEx_Click"> </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cliente">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCli" runat="server" Text='<%# Bind("IdCliente") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="250px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo_Ex">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNTEx" runat="server" Text='<%# Bind("NTEx") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="500px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Vig">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldtVg" runat="server" Text='<%# Bind("dtVig") %>'></asp:Label>
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
                                
                                <asp:GridView ID="gvEmpA" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="3" CellSpacing="1" CssClass="gvAlertas" DataMember="DefaultView"
                                    HorizontalAlign="Center" PageSize="5" OnPageIndexChanging="gvEmpA_PageIndexChanging">
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnId" runat="server" Text='<%# Bind("Id") %>' CommandArgument="<%# Container.DisplayIndex %>"
                                                    ForeColor="#000066" OnClick="lbtnId_Click"> </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NEmp">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNEmp" runat="server" Text='<%# Bind("NEmp") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="250px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cargo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCargo" runat="server" Text='<%# Bind("Cargo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width="600px" />
                                </asp:GridView>
                            </td>

                            <%-----------------------------------------------------------%>

         
                            <%-----------------------------------------------------------%>

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
                                                <asp:Label ID="lblBusq" runat="server" CssClass="labelRq" Text="Busqueda"></asp:Label>
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
                                                            <asp:TextBox ID="txtIdBusq" runat="server" CssClass="txtrq2" MaxLength="10" Width="100px"></asp:TextBox>
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
                                                    <tr>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblCargo" runat="server" CssClass="labelRq" Text="Cargo:"></asp:Label>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            <asp:DropDownList ID="ddlCargo" runat="server" CssClass="ddlGVei" Width="50%" AutoPostBack="false"
                                                                OnSelectedIndexChanged="ddlCargo_SelectedIndexChanged">
                                                            </asp:DropDownList>
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
             <td td colspan="3" align="center">
                <asp:Panel ID="pnlMotivo" runat="server" Visible="false" CssClass="modalPopup" Width="700px">
                    <table class="style1">
                    
                         
                      
                        <tr>
                            <td>
                            </td>
                            <td colspan="3" align="center">
                                <asp:Panel ID="Panel2" runat="server" Visible="True" Width="600px">
                                    <table class="style1">
                                       
                                        <tr  style="text-align:center;">                                            
                                                                                            
                                            <td style="text-align:center;">
                                                <asp:Label ID="Label6" runat="server" CssClass="labelRq" Text="Nuevo Motivo:"></asp:Label>
                                            </td>

                                             <td style="text-align:center;">
                                                <asp:TextBox ID="txtNMotivo" runat="server" CssClass="txtrq2" Width="150px">
                                                </asp:TextBox>                                                   
                                            </td>
                                            
                                            <td style="text-align:center;">
                                                <asp:Button ID="btnMotivo" runat="server" CssClass="btnRq" OnClick="btnMotivo_Click"
                                                    Text="Agregar Motivo" />
                                            </td>
                                        
                                        </tr>
                                        
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                            </td>
                        </tr>
                       
                        
                       
                    </table>
                </asp:Panel>

         
            </td>

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
                                <asp:GridView ID="gvCDoc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="3" CellSpacing="1" CssClass="gvAlertas" DataMember="DefaultView"
                                    HorizontalAlign="Center" PageSize="5" OnRowCommand="gvCDoc_RowCommand" 
                                    OnPageIndexChanging="gvCDoc_PageIndexChanging" onprerender="gvCDoc_PreRender">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdDoc" runat="server" Text='<%# Bind("IdDocument") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNDoc" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="250px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FileExt">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFExt" runat="server" Text='<%# Bind("FileExt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDL" runat="server" CommandName="dlDocCon" Text="Descargar"
                                                    CommandArgument="<%# Container.DisplayIndex %>"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnBDoc" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                    CommandName="Borrar" Text="Borrar"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" Wrap="True" />
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
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="3">
                                <asp:Button ID="btnCCon" runat="server" CssClass="btnRq" Text="Regresar" OnClick="btnCCon_Click" />
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
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3" align="center">
                <asp:Panel ID="pnlMpAdj" runat="server" CssClass="modalPopup" Visible="false" Width="700px">
                    <table class="style1">
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="3">
                                <asp:GridView ID="gvNDocOld" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="3" CellSpacing="1" CssClass="gvAlertas" DataMember="DefaultView"
                                    HorizontalAlign="Center" PageSize="5" Visible="true" Width="100%" 
                                    OnRowCommand="gvNDocOld_RowCommand" onprerender="gvNDocOld_PreRender">
                                    <Columns>
                                        <asp:BoundField DataField="IdDocument" HeaderText="ID" ReadOnly="True">
                                            <HeaderStyle Width="10%" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNom" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ext">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFE" runat="server" Text='<%# Bind("FileExt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtDDocO" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                    CommandName="DesDocO" Text="Ver" Enabled="true"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnBD" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                    CommandName="Borrar" Text="Borrar"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" />
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
                            <td style="text-align:center;" colspan="3">
                                <asp:GridView ID="gvNDoc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="3" CellSpacing="1" CssClass="gvAlertas" DataMember="DefaultView"
                                    HorizontalAlign="Center" OnRowCommand="gvNDoc_RowCommand" PageSize="5" Visible="true"
                                    Width="620px" OnPageIndexChanging="gvNDoc_PageIndexChanging" 
                                    onprerender="gvNDoc_PreRender">
                                    <Columns>
                                        <asp:BoundField DataField="IdDocument" HeaderText="ID" ReadOnly="True">
                                            <HeaderStyle Width="10%" Wrap="True" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ext">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFExt" runat="server" Text='<%# Bind("FileExt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjunto">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocAdj" runat="server" Text='<%# Bind("Path") %>'>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:FileUpload ID="fp_DocExt" runat="server" CssClass="fUp1" Width="200px" Height="18px" />
                                            </EditItemTemplate>
                                            <HeaderStyle Width="40%" Wrap="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbtnGuardarDoc" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                    CommandName="Guardar" Text="Guardar"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnCanDoc" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                    CommandName="Cancelar" Text="Cancelar"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtnNewDoc" runat="server" CommandArgument="1" CommandName="Nuevo"
                                                    ForeColor="Gray" Text="Nuevo"></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtDesDoc" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                    CommandName="DesDoc" Text="Ver" Enabled="true"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnBorrarDoc" runat="server" CommandArgument="<%# Container.DisplayIndex %>"
                                                    CommandName="Borrar" Text="Borrar"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" />
                                        </asp:TemplateField>
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
                            <td style="text-align:center;" colspan="3">
                                &nbsp;
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="3">
                                <asp:Label ID="lblAcpMpBA1" runat="server" Visible="true"></asp:Label>
                                <asp:ModalPopupExtender ID="MpUp_Adj" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="lblCanlMpBA1" DynamicServicePath="" Enabled="True" OkControlID="lblAcpMpBA1"
                                    PopupControlID="pnlMpAdj" TargetControlID="lblAcpMpBA1">
                                </asp:ModalPopupExtender>
                                <asp:Label ID="lblCanlMpBA1" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;" colspan="3">
                                <asp:Button ID="btnBackAdj" runat="server" CssClass="btnRq" Text="Regresar" OnClick="btnBackAdj_Click" />
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
    </table>
    <div class="Bottom">
    </div>

  

    
</asp:Content>
