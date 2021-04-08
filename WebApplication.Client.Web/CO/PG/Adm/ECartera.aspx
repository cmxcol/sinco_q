<%@ Page Title="Excepciones" Language="C#" MasterPageFile="~/CO/MP/AdmC.Master" AutoEventWireup="true"
    CodeBehind="ECartera.aspx.cs" Inherits="WebApplication.Client.Web.CO.PG.Adm.ECartera" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 97%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                <asp:updatepanel ID="Upnl_UEx" runat="server">
                <ContentTemplate>
                <fieldset style="border: none">
                   <asp:Button ID="btnUDoc" runat="server" Text="Cargar Archivo" Width="150px" CssClass="btnRq"
                    OnClick="btnUDoc_Click" />             
                </fieldset>
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID ="btnUDoc" />
                </Triggers>
                </asp:updatepanel>
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
                &nbsp;</td>
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
                <asp:Label ID="lblMsgLDB" runat="server" Text="" CssClass="labelInfo" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:GridView ID="gv_ClEx" runat="server" CssClass="gvAlertas" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="true" Visible="False" DataMember="DefaultView"
                    AllowPaging="True" OnPageIndexChanging="gv_ClEx_PageIndexChanging">
                    <Columns>
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
                <asp:Label ID="lblELDB" runat="server" Text="" CssClass="labelError" Visible="false"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                <asp:GridView ID="gv_Error" runat="server" CssClass="gvAlertas" HorizontalAlign="Center"
                    CellPadding="3" CellSpacing="1" AutoGenerateColumns="true" Visible="False" DataMember="DefaultView"
                    AllowPaging="True" onpageindexchanging="gv_Error_PageIndexChanging">
                    <Columns>
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
                <asp:Button ID="btnF" runat="server" Text="Finalizar" CssClass="btnRq" Visible="False"
                    OnClick="btnF_Click" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2" align="center">
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
    </table>
    <div class = "Bottom">    
    </div>
</asp:Content>
