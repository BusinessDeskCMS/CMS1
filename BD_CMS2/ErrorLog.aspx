<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorLog.aspx.cs" Inherits="BD_CMS2.ErrorLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Review Error Log </h3>
    <br />
      <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="142px">
        <asp:ListItem>Today</asp:ListItem>
        <asp:ListItem>Yesterday</asp:ListItem>
        <asp:ListItem Value="LW">Last Week</asp:ListItem>
        <asp:ListItem Value="1M">Last Month</asp:ListItem>
        <asp:ListItem Value="3M">Last 3 Months</asp:ListItem>
     </asp:DropDownList>

     <asp:Button ID="btSelect" runat="server" Text="Select" class="btn btn-default btn-sm" OnClick="btSelect_Click" />
</asp:Content>
