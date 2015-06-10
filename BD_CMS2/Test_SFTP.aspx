<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test_SFTP.aspx.cs" Inherits="BD_CMS2.Test_SFTP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <h3>

    <asp:Label ID="Label1" runat="server" Text="Select a Test Story to Send :"></asp:Label>
    
    </h3>
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    <asp:DropDownList ID="ddlStory" runat="server" DataSourceID="SqlData_Test_Story" DataTextField="Headline" DataValueField="StoryID">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlData_Test_Story" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [StoryID], [Headline] FROM [Test_Story]"></asp:SqlDataSource>
    <asp:Button ID="Button1" runat="server" Text="Select" OnClick="Button1_Click" />
    <br />
    <h4>Headline</h4>
    : <asp:TextBox ID="tbHeadline" runat="server" Width="816px"></asp:TextBox>
    <br />
    <br />
    <br />
    <h4>Body</h4>
    :     <asp:TextBox ID="tbBody" runat="server" Height="120px" TextMode="MultiLine" Width="596px"></asp:TextBox>
    <br />
    <hr />
    <br />
    <h3>Select who to send to</h3>
    <asp:RadioButtonList ID="rbList1" runat="server" Width="177px">
        <asp:ListItem Selected="True">APN</asp:ListItem>
        <asp:ListItem>ShareChat</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Select" />
</asp:Content>
