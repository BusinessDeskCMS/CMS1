<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchDisplay1.aspx.cs" Inherits="BD_CMS2.SearchDisplay1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Story </h3>
    <div>
        <a href='javascript:history.go(-1)' class="btn btn-default">Go Back to Previous Page</a>
    </div>
    <br />
    
    <div>
        <asp:TextBox ID="tbHeading" runat="server" BorderStyle="None" Font-Bold="True" Font-Size="Medium" ForeColor="#3333CC" Width="931px"></asp:TextBox>
    </div>
    <div>     
        <asp:TextBox ID="tbStory" runat="server" Height="369px" TextMode="MultiLine" Width="1056px"></asp:TextBox>
    </div>
</asp:Content>
