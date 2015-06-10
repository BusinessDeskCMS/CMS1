<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoadData.aspx.cs" Inherits="BD_CMS2.LoadData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Bulk Load Content</h1>
    <asp:Button ID="Button1" runat="server" Text="Start Data Load" OnClick="Button1_Click" />
</asp:Content>
