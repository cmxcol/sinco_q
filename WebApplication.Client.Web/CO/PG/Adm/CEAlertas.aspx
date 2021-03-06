<%@ Page Title="Alertas" Language="C#" MasterPageFile="~/Account/MP/LoginMaster.Master"
    AutoEventWireup="true" CodeBehind="CEAlertas.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Adm.CEAlertas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/SPublic.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 97%;
        }
    </style>
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
            <td>
            </td>
            <td colspan="3" align="center">
                <asp:GridView ID="gv_Alertas" runat="server" AllowPaging="True" CssClass="gvAlertas"
                    HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                    Visible="False" DataMember="DefaultView" PageSize="5" OnPageIndexChanging="gv_Alertas_PageIndexChanging"
                    OnRowCancelingEdit="gv_Alertas_RowCancelingEdit" OnRowEditing="gv_Alertas_RowEditing"
                    OnRowUpdating="gv_Alertas_RowUpdating" OnRowCommand="gv_Alertas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdMSGA" HeaderText="ID" ReadOnly="True">
                            <HeaderStyle Wrap="True" Width="100px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="MENSAJE">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMsg" Aling="center" Width="400px" CssClass="txtrq2" runat="server"
                                    Text='<%# Bind("MSGTEXT") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMsg" runat="server" Text='<%# Bind("MSGTEXT") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="True" Width="500px" />
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
                            </ItemTemplate>
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
                <asp:Panel ID="pnlBusq" runat="server" Visible="True" Width="600px">
                    <table class="style1">
                        <tr>
                            <td>
                            </td>
                            <td style="text-align:center;">
                                <asp:Label ID="lblBusq" runat="server" Text="Busqueda" CssClass="labelRq"></asp:Label>
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
                                            <asp:TextBox ID="txtIdBusq" runat="server" CssClass="txtrq2" MaxLength="10" Width="80px"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="txtIdBusq_TextBoxWatermarkExtender" runat="server"
                                                Enabled="True" TargetControlID="txtIdBusq" WatermarkText="ID">
                                            </asp:TextBoxWatermarkExtender>
                                            <asp:FilteredTextBoxExtender ID="txtIdBusq_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtIdBusq">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td style="text-align:center;" width="300px">
                                            <asp:TextBox ID="txtMSGBusq" runat="server" CssClass="txtrq2" Width="300px">
                                            </asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="txtMSGBusq_TextBoxWatermarkExtender" runat="server"
                                                Enabled="True" TargetControlID="txtMSGBusq" WatermarkText="Texto">
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
                                <asp:Button ID="btnBusq" runat="server" Text="Buscar" CssClass="btnRq" OnClick="btnBusq_Click" />
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
                <asp:Panel ID="pnlMPB" runat="server" Visible="false" CssClass="modalPopup" Width="510px">
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
                                <asp:GridView ID="gv_ABusq" runat="server" AllowPaging="True" CssClass="gvAlertas"
                                    HorizontalAlign="Center" CellPadding="3" CellSpacing="1" AutoGenerateColumns="False"
                                    Visible="true" PageSize="5" OnPageIndexChanging="gv_ABusq_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnId" runat="server" CommandName="ID" Text='<%# Bind("IdMSGA") %>'
                                                    CommandArgument="<%# Container.DisplayIndex %>" ForeColor="#000066" Width="100px"
                                                    OnClick="lbtnId_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MENSAJE">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMsg" CssClass="txtrq2" runat="server" Text='<%# Bind("MSGTEXT") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblMsg" runat="server" Text='<%# Bind("MSGTEXT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="True" Width="500px" />
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
                                <asp:ModalPopupExtender ID="MpUp_AB" runat="server" DynamicServicePath="" Enabled="True"
                                    TargetControlID="lblAcp" PopupControlID="pnlMPB" OkControlID="lblAcp" CancelControlID="lblCanl"
                                    BackgroundCssClass="modalBackground">
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
                                <asp:Button ID="btnCanMP" runat="server" CssClass="btnRq" OnClick="btnCanMP_Click"
                                    Text="Cancelar" />
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
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
