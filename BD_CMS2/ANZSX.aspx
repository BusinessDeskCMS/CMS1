<%@ Page Title="NZSX" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ANZSX.aspx.cs" Inherits="BD_CMS2.ANZSX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
     <style type="text/css">
        .contactcell {           
            padding-left:10px;
            padding-right:10px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Ticker Information.</h3>
    <br />
    <asp:Button ID="btAdd" runat="server" Text="New Ticker" CssClass="btn btn-default" OnClick="btAdd_Click" />
    <asp:ListView ID="lvANZSX" runat="server" DataKeyNames="ANZSXID" DataSourceID="SqlDataSource1">
        
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-default" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-default" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="ANZSXIDLabel1" runat="server" Text='<%# Eval("ANZSXID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TickerTextBox" runat="server" Text='<%# Bind("Ticker") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>       
        <ItemTemplate>
            <tr style="">
                <td class="contactcell">
                    <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-default" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-default" CommandName="Edit" Text="Edit" />
                </td>
                <td class="contactcell">
                    
                </td>
                <td class="contactcell">
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td class="contactcell">
                    <asp:Label ID="TickerLabel" runat="server" Text='<%# Eval("Ticker") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server" class="contactcell">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server"></th>
                                <th runat="server"> </th>
                                <th runat="server">Name</th>
                                <th runat="server">Ticker</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="50">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" ButtonCssClass="btn btn-default" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-default" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-default" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="ANZSXIDLabel" runat="server" Text='<%# Eval("ANZSXID") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="TickerLabel" runat="server" Text='<%# Eval("Ticker") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" DeleteCommand="DELETE FROM [ANZSX] WHERE [ANZSXID] = @ANZSXID" InsertCommand="INSERT INTO [ANZSX] ([Name], [Ticker]) VALUES (@Name, @Ticker)" SelectCommand="SELECT [ANZSXID], [Name], [Ticker] FROM [ANZSX] ORDER BY [Ticker]" UpdateCommand="UPDATE [ANZSX] SET [Name] = @Name, [Ticker] = @Ticker WHERE [ANZSXID] = @ANZSXID">
        <DeleteParameters>
            <asp:Parameter Name="ANZSXID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Ticker" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Ticker" Type="String" />
            <asp:Parameter Name="ANZSXID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
