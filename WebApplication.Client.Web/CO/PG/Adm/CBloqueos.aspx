<%@ Page Title="Bloqueos" Language="C#" MasterPageFile="~/CO/MP/AdmC.Master" AutoEventWireup="true"
    CodeBehind="CBloqueos.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Adm.CBloqueos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td colspan="4" align="center">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:FileUpload ID="fp_Doc_xls" runat="server" CssClass="fUp" Width="250px" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:UpdatePanel ID="Upnl_FileUpload" runat="server" >
                    <ContentTemplate>
                        <fieldset style="border: none">
                            <asp:Button ID="btnUDoc" runat="server" Text="Subir Archivo" Style="margin-bottom: 0px"
                                CssClass="btnRq" OnClick="btnUDoc_Click" />
                        </fieldset>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID ="btnUDoc" />
                    </Triggers>
                </asp:UpdatePanel>
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
                <asp:Label ID="lblMsgL" runat="server" Text="" CssClass="labelInfo" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblEL" runat="server" Text="" CssClass="labelError" Visible="false"></asp:Label>
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
                <asp:Button ID="btnVPrevia" runat="server" Text="Vista Previa" CssClass="btnRq" OnClick="btnVPrevia_Click"
                    Visible="False" />
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
                <asp:GridView ID="gv_ClBl" runat="server" CssClass="gvDCli" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="False" Visible="False" DataMember="DefaultView"
                    AllowPaging="True" OnPageIndexChanging="gv_ClEx_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="IdRsObr" HeaderText="CLIENTE">
                            <HeaderStyle Wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BlCenPed" HeaderText="BLOQ_CEN_PED" />
                        <asp:BoundField DataField="BlCenEnt" HeaderText="BLOQ_CEN_ENT" />
                    </Columns>
                    <HeaderStyle BackColor="#000066" ForeColor="White" />
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
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblMsgLDB" runat="server" Text="" CssClass="labelInfo" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Label ID="lblELDB" runat="server" Text="" CssClass="labelError" Visible="false"></asp:Label>
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
                <asp:Button ID="btnLoadDB" runat="server" Text="Cargar" CssClass="btnRq" OnClick="btnLoadDB_Click"
                    Visible="False" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnF" runat="server" Text="Finalizar" CssClass="btnRq" Visible="False"
                    OnClick="btnF_Click" />
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
            <td colspan="4" align="center">
            </td>
        </tr>
    </table>
</asp:Content>
