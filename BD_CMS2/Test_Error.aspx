<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test_Error.aspx.cs" Inherits="BD_CMS2.Test_Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3> Reset the test story so error processing can be viewed.</h3>

    <asp:Button ID="Button1" runat="server" Text="Reset" OnClick="Button1_Click" />

    <br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

</asp:Content>
