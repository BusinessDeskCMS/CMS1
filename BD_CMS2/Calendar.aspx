<%@ Page Title="Calendar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="BD_CMS2.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <h3>BusinessDesk Calendar.</h3>
    <br />
    <div class="padding15">
        <iframe id="iCalendar" runat="server" ></iframe>
    </div>
</asp:Content>
