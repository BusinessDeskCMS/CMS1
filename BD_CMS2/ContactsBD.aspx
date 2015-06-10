<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactsBD.aspx.cs" Inherits="BD_CMS2.ContactsBD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <h3>BusinessDesk Contacts</h3>
    <br />
     <div class="padding15">
        <iframe id="iCalendar" runat="server" src="https://docs.google.com/spreadsheet/pub?key=0AvemsBSLXZ2WdGlCQ2dPUUEyYmNxWjRDS1VHbzFPYVE&output=html" width="1200" height="500"></iframe>
    </div>
</asp:Content>
