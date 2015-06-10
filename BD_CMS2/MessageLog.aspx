<%@ Page Title="MessageLog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MessageLog.aspx.cs" Inherits="BD_CMS2.MessageLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Review Message Log </h3>
    <br />

    <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="142px">
        <asp:ListItem>Today</asp:ListItem>
        <asp:ListItem>Yesterday</asp:ListItem>
        <asp:ListItem Value="LW">Last Week</asp:ListItem>
        <asp:ListItem Value="1M">Last Month</asp:ListItem>
        <asp:ListItem Value="3M">Last 3 Months</asp:ListItem>
    </asp:DropDownList>

    <asp:Button ID="btSelect" runat="server" Text="Select" CssClass="btn btn-default btn-sm" OnClick="btSelect_Click" />

    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlMsgLog">

        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No log entries.</td>
                </tr>
            </table>
        </EmptyDataTemplate>

        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="StoryIDLabel" runat="server" Text='<%# Eval("StoryID") %>' />
                </td>
                <td>
                    <asp:Label ID="HeadlineLabel" runat="server" Text='<%# Eval("Headline") %>' />
                </td>
                <td>
                    <asp:Label ID="CustomerLabel" runat="server" Text='<%# Eval("Customer") %>' />
                </td>
                <td>
                    <asp:Label ID="MsgStatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' />
                </td>
                <td>
                    <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# GetNZ(Eval("DateCreated"))  %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server">StoryID</th>
                                <th runat="server">Headline</th>
                                <th runat="server">Customer</th>
                                <th runat="server">Msg Status</th>
                                <th runat="server">Date Sent</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-default btn-sm" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>

    </asp:ListView>
    <asp:SqlDataSource ID="SqlMsgLog" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [StoryID], [Headline], [Customer], [MsgStatus], [DateCreated] FROM [MsgLog] ORDER BY StoryID DESC"></asp:SqlDataSource>

</asp:Content>
