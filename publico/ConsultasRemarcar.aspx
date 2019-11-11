<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsultasRemarcar.aspx.cs" Inherits="publico_ConsultasRemarcar" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <h3><asp:Label ID="lbTitulo" runat="server" Text="CONSULTAS PARA REMARCAR"></asp:Label></h3>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

