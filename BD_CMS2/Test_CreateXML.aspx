<%@ Page Title="Test XML Create" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test_CreateXML.aspx.cs" Inherits="BD_CMS2.Test_CreateXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>XML Test.</h3>
    <p>&nbsp;</p>

    <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Enter the Story ID: "></asp:Label>
                <asp:TextBox ID="tbStoryID" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Select" OnClick="Button1_Click" CssClass="btn btn-default" />
                <br />
                <asp:Button ID="Button2" runat="server" Text="Generate XML" OnClick="Button2_Click" CssClass="btn btn-default" />
            </div>
            <div class="col-md-6">
                <asp:Label ID="Label2" runat="server" Text="Story"></asp:Label>
                <br />
                <asp:TextBox ID="TextBox1" runat="server" Height="283px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                <br />
            </div>
        </div>

    </div>

</asp:Content>
