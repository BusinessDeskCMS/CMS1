<%@ Page Title="System" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CMSControl.aspx.cs" Inherits="BD_CMS2.CMSControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>CMS System Control Settings</h3>
    <br />
    <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" Width="983px" DataKeyNames="CMSControlID">
        <EditItemTemplate>
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Calendar:
            <asp:TextBox ID="CalendarTextBox" runat="server" Text='<%# Bind("Calendar") %>' />
            <br />
            Messaging:
            <asp:TextBox ID="MessagingTextBox" runat="server" Text='<%# Bind("Messaging") %>' />
            <br />
            Email Active?:
            <asp:TextBox ID="email_activeTextBox" runat="server" Text='<%# Bind("email_active") %>' />
            <br />           
            Public Site Active?:
            <asp:TextBox ID="public_site_feed_activeTextBox" runat="server" Text='<%# Bind("public_site_feed_active") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />       
        <ItemTemplate>
            Name:
            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Calendar:
            <asp:Label ID="CalendarLabel" runat="server" Text='<%# Bind("Calendar") %>' />
            <br />
            Messaging:
            <asp:Label ID="MessagingLabel" runat="server" Text='<%# Bind("Messaging") %>' />
            <br />
            eMail Active:              
             <asp:Label ID="email_activeLabel" runat="server" Text='<%# Bind("email_active") %>' />
            <br />            
            Public Site Active?:
            <asp:Label ID="public_site_feed_activeLabel" runat="server" Text='<%# Bind("public_site_feed_active") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />           
        </ItemTemplate>
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [Name], [Calendar], [Messaging], [email_active], [CMSControlID], [public_site_feed_active] FROM [CMSControl]"  UpdateCommand="UPDATE [CMSControl] SET [Name] = @Name, [Calendar] = @Calendar, [Messaging] = @Messaging, [email_active] = @email_active, [public_site_feed_active] = @public_site_feed_active WHERE [CMSControlID] = @CMSControlID">
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Calendar" Type="String" />
            <asp:Parameter Name="Messaging" Type="String" />
            <asp:Parameter Name="email_active" Type="String" />
            <asp:Parameter Name="public_site_feed_active" Type="String" />
            <asp:Parameter Name="CMSControlID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>
