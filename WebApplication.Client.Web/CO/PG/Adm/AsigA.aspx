<%@ Page Title="Alertas" Language="C#" MasterPageFile="~/Account/MP/LoginMaster.Master"
    AutoEventWireup="true" CodeBehind="AsigA.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Adm.AsigA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 97%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class = "style1">
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td width = "25px"></td>
            <td colspan = "3" align = "center">
                <asp:GridView  ID = "gvRelAlert" runat ="server" AllowPaging = "True" 
                    CssClass="gvAlertas" HorizontalAlign="Center" CellPadding="3" CellSpacing="1" 
                    AutoGenerateColumns="False" Visible="False" DataMember="DefaultView" 
                    PageSize="5" onpageindexchanging="gvRelAlert_PageIndexChanging" 
                    onrowcommand="gvRelAlert_RowCommand">
                    <Columns>
                        <asp:BoundField  DataField = "IDALERT" HeaderText = "ID" ReadOnly="True">
                            <HeaderStyle Wrap = "True"  Width="100px" />                            
                        </asp:BoundField>
                        <asp:TemplateField HeaderText = "TIPO ALERTA">
                            <ItemTemplate>
                                <asp:Label  ID = "lblTAlert" runat = "server" Text='<%# Bind("TALERT") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList  ID = "ddlTAlert" runat = "server" CssClass="ddlGV" 
                                    Height="25px" style="font-family: Arial; font-size: 9pt" Width="125px" 
                                    AutoPostBack="True" onselectedindexchanged="ddlTAlert_SelectedIndexChanged"></asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="True" Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IDMSG">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnIdMsgRel" runat="server" CommandName = "IDMSG" 
                                    Text='<%# Bind("IDMSGA") %>' CommandArgument = "<%# Container.DisplayIndex %>" 
                                    ForeColor = "#000066"  Width="50px" onclick="lbtnIdMsgRel_Click" ></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "CRITERIO" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCri"  CssClass = "txtrqGVlow" runat="server" Text='<%# Bind("IDCRI") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCri" runat="server" Text='<%# Bind("IDCRI") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="200px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "ESTADO" >
                            <ItemTemplate>
                                <asp:Label ID="lblSta" runat="server" Text='<%# Bind("ESTADO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>                                
                                <asp:DropDownList  ID = "ddlSta" runat = "server" CssClass="ddlGV" Height="25px" style="font-family: Arial; font-size: 9pt" Width="125px"></asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Wrap="True" Width="200px" />
                        </asp:TemplateField>                                             
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lbtnGuardar" runat="server" CommandName="Guardar" Text="Guardar"  CommandArgument ="<%# Container.DisplayIndex %>" ></asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="lbtnCan" runat="server" CommandName="Cancelar" Text="Cancelar"  CommandArgument ="<%# Container.DisplayIndex %>"></asp:LinkButton>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                <asp:LinkButton ID="lbtnNew" runat="server" CommandName = "Nuevo" Text = "Nuevo" CommandArgument = "1" ForeColor = "Gray"></asp:LinkButton>                                
                            </HeaderTemplate>                            
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEditar" runat="server" 
                                    CommandName="Editar" Text="Editar"  CommandArgument ="<%# Container.DisplayIndex %>"  ></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width = "150px"/>
                        </asp:TemplateField>                                                                             
                    </Columns>
                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width = "950px" />                       
                </asp:GridView>
            
            </td>
            <td width = "25px"></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td align = "center">
                 <asp:Label ID="lblEInsUp" runat="server" CssClass="labelError" Visible="false"></asp:Label>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td  colspan = "3" align ="center">
                <asp:Panel  ID = "pnlMpBA" runat = "server" Visible = "false" CssClass = "modalPopup" Width = "700px">
                    <table class = "style1">
                        <tr>
                            <td></td>
                            <td colspan = "3" align = "center"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan = "3" align = "center">
                                <asp:GridView ID="gv_Alertas" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="3" CellSpacing="1" CssClass="gvAlertas" DataMember="DefaultView" 
                                    HorizontalAlign="Center" onpageindexchanging="gv_Alertas_PageIndexChanging" onrowcommand="gv_Alertas_RowCommand" PageSize="5" Visible="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:LinkButton ID = "lbtnIdMSGA" runat ="server" 
                                                    Text = '<%# Bind("IDMSGA") %>' CommandArgument = "<%# Container.DisplayIndex %>" 
                                                    ForeColor = "#000066" onclick="lbtnIdMSGA_Click"> </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px"  Wrap="True"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MENSAJE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMsg" runat="server" Text='<%# Bind("MSGTEXT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="500px" Wrap="True" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width="600px" />
                                </asp:GridView>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan = "3" align = "center">
                                <asp:Label ID="lblAcpMpBA" runat="server" Visible="true"></asp:Label>
                                <asp:ModalPopupExtender ID="MpUp_MA" runat="server" 
                                    BackgroundCssClass="modalBackground" CancelControlID="lblCanlMpBA" 
                                    DynamicServicePath="" Enabled="True" OkControlID="lblAcpMpBA" 
                                    PopupControlID="pnlMpBA" TargetControlID="lblAcpMpBA">
                                </asp:ModalPopupExtender>
                                <asp:Label ID="lblCanlMpBA" runat="server" Visible="true"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan = "3" align = "center">
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
                                                        <td style="text-align:center;" width="100px">
                                                            <asp:TextBox ID="txtIdBusq" runat="server" CssClass="txtrq2" MaxLength="10" 
                                                                Width="80px"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtIdBusq_TextBoxWatermarkExtender" 
                                                                runat="server" Enabled="True" TargetControlID="txtIdBusq" WatermarkText="ID">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:FilteredTextBoxExtender ID="txtIdBusq_FilteredTextBoxExtender" 
                                                                runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtIdBusq">
                                                            </asp:FilteredTextBoxExtender>
                                                        </td>
                                                        <td style="text-align:center;" width="300px">
                                                            <asp:TextBox ID="txtMSGBusq" runat="server" CssClass="txtrq2" Width="300px">
                                                            </asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="txtMSGBusq_TextBoxWatermarkExtender" 
                                                                runat="server" Enabled="True" TargetControlID="txtMSGBusq" 
                                                                WatermarkText="Texto">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:FilteredTextBoxExtender ID="txtMSGBusq_FilteredTextBoxExtender" 
                                                                runat="server" Enabled="True" FilterMode="InvalidChars" InvalidChars="'" 
                                                                TargetControlID="txtMSGBusq">
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
                                            <td></td>
                                            <td style="text-align:center;">
                                                <table  class ="style1">
                                                    <tr>
                                                        <td style="text-align:center;">
                                                            <asp:Button ID="btnBusq" runat="server" CssClass="btnRq" onclick="btnBusq_Click" Text="Buscar" />
                                                        </td>

                                                         <td style="text-align:center;">
                                                            <asp:Button ID="btnCanlBusq" runat="server" CssClass="btnRq" Text="Cancelar" 
                                                                 onclick="btnCanlBusq_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                             <td></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan = "3" align = "center">
                                &nbsp;</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan = "3" align = "center">
                                
                                <asp:Panel ID="pnlMPB" runat="server" CssClass="modalPopup" Visible="false" 
                                    Width="510px">
                                    <table class="style1">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBC" runat="server" CssClass="labelRq" Text="Coincidencias"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:GridView ID="gv_ABusq" runat="server" AllowPaging="True" 
                                                    AutoGenerateColumns="False" CellPadding="3" CellSpacing="1" 
                                                    CssClass="gvAlertas" HorizontalAlign="Center" 
                                                    onpageindexchanging="gv_ABusq_PageIndexChanging" PageSize="5" Visible="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnId" runat="server" 
                                                                    CommandArgument="<%# Container.DisplayIndex %>" CommandName="ID" 
                                                                    ForeColor="#000066" Text='<%# Bind("IdMSGA") %>' Width="100px" 
                                                                    onclick="lbtnId_Click" ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MENSAJE">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtMsg" runat="server" CssClass="txtrq2" Text='<%# Bind("MSGTEXT") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMsg" runat="server" Text='<%# Bind("MSGTEXT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="500px" Wrap="True" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#D8D8D8" ForeColor="Black" Width="150px" />
                                                </asp:GridView>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAcp" runat="server" Visible="true"></asp:Label>
                                                <asp:ModalPopupExtender ID="MpUp_AB" runat="server" 
                                                    BackgroundCssClass="modalBackground" CancelControlID="lblCanl" 
                                                    DynamicServicePath="" Enabled="True" OkControlID="lblAcp" 
                                                    PopupControlID="pnlMPB" TargetControlID="lblAcp">
                                                </asp:ModalPopupExtender>
                                                &nbsp;
                                                <asp:Label ID="lblCanl" runat="server" Visible="true"></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCanMP" runat="server" CssClass="btnRq" 
                                                     Text="Cancelar" onclick="btnCanMP_Click" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan = "3" align = "center"></td>
                            <td></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td colspan = "3" align ="center"></td>
            <td></td>
        </tr>
    </table>
        <div class="Bottom">
    </div>
</asp:Content>
