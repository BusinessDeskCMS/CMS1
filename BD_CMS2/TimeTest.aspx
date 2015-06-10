<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeTest.aspx.cs" Inherits="BD_CMS2.TimeTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="DateTimeNow  "></asp:Label><asp:TextBox ID="tbDateNow" runat="server" Width="214px"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="UTCTimeNow  "></asp:Label><asp:TextBox ID="tbUTCNow" runat="server" Width="211px"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="LocalTimeNow  "></asp:Label><asp:TextBox ID="tbLocalNow" runat="server" Width="211px"></asp:TextBox>

    <br />
    <asp:Label ID="Label4" runat="server" Text="UTC converted to (LocalNow)  "></asp:Label>
    <br />
    Enter time to Convert 
    <asp:TextBox ID="tbTimeToConvert" runat="server"></asp:TextBox>
    <br />
    <asp:TextBox ID="tbUTCNZ" runat="server" Width="211px"></asp:TextBox>
    <br />
    <%--      <asp:Label ID="Label5" runat="server" Text="Eval function  "></asp:Label><asp:TextBox ID="TextBox1" runat="server" Text="<%# GetNZ("12/12/2012 7:09:52 AM")%>" Width="211px"></asp:TextBox>--%>
    <br />
    <asp:Button ID="Button1" runat="server" Text="GetTime" OnClick="Button1_Click" />

    <asp:ListView ID="ListView1" runat="server" DataKeyNames="StoryID" DataSourceID="SqlDataSource1">
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="StoryIDLabel" runat="server" Width="40" Text='<%# Eval("StoryID") %>' />
                </td>
                <td>
                    <asp:Label ID="HeadlineLabel" runat="server" Width="200" Text='<%# Eval("Headline") %>' />
                </td>
                <td>
                    <asp:Label ID="DateCreatedLabel" runat="server" Width="100" Text='<%# Eval("datecreated")  %>' />
                </td>
                <td>
                    <asp:Label ID="NZLabel" runat="server" Width="100" Text='<%# GetNZ(Eval("datecreated"))  %>' />
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
                                <th runat="server">DateCreated</th>
                                <th runat="server">NZ time</th>

                            </tr>
                            <tr runat="server" id="itemPlaceholder">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style=""></td>
                </tr>
            </table>
        </LayoutTemplate>

    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [StoryID], [Headline], [DateCreated] FROM [Story] ">
        <SelectParameters>
            <asp:Parameter DefaultValue="2037" Name="StoryID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
