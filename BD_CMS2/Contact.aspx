<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="BD_CMS2.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>This could be a help-desk page for Jonathan.</h3>
    <address>
        Content Ltd<br />
        <br />
        <abbr title="Phone">P:</abbr>
        +64 4 4xx xxxx
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@businessdesk.co.nz">Support@businessdesk.co.nz</a><br />
        <strong>Leads:</strong> <a href="mailto:Leads@businessdesk.co.nz">Leads@businessdesk.co.nz</a>
    </address>
</asp:Content>
