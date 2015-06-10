<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test_ServerMapPath.aspx.cs" Inherits="BD_CMS2.Test_ServerMapPath" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <h3>Test Server.MapPath </h3>
    <br />
    <p>Path</p>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <br />
    <p>Test File Create on Server</p>
    <asp:Button ID="Button1" runat="server" Text="File Create" OnClick="Button1_Click" />
     <br />
     <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
</asp:Content>
