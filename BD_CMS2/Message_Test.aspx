<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message_Test.aspx.cs" Inherits="BD_CMS2.Message_Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 150px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
       
        
    }
</style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Send a Test Message to a Customer</h3>
    <br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <br />
    <div class="row">
        <div class="col-md-6">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="name">
            </asp:RadioButtonList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="Select c.name,active from customermessage cm, customer c where cm.customerid = c.customerid order by c.name"></asp:SqlDataSource>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Select Customer" CssClass="btn btn-default" />
            <br />
        </div>
        <div class="col-md-6">
            <h4> This is the story content that will go to the customer</h4>
            
            <br />
            <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </div>
    <div class="row">
        <h4>Host Server Responses</h4>
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        <br />
    </div>
    <div class="loading" align="center" >
    Sending Story. Please wait.<br />
    <br />
    <img src="Images/loader.gif" alt="" />
</div>
</asp:Content>
