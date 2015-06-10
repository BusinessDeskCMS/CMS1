<%@ Page Title="NZSX" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ANZSX_Add.aspx.cs" Inherits="BD_CMS2.ANZSX_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Ticker Information.</h3>
    <br />
    <asp:Button ID="btAdd" runat="server" Text="Insert" OnClick="btAdd_Click" />
    <table class="auto-style1">
        <tr>
            <td>
                <asp:Label ID="lbTicker" runat="server" Text="Ticker"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbTicker" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lbName" runat="server" Text="Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>

</asp:Content>
