<%@ Page Title="ASX" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ASX.aspx.cs" Inherits="BD_CMS2.ASX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <h2><%: Title %>.</h2>
    <h3>Australian Stock Exchange List.</h3>
    <br />
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="ASXID" DataSourceID="SqlDataSourceASX" InsertItemPosition="LastItem">
    <AlternatingItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            </td>
            <td>
                <asp:Label ID="ASXIDLabel" runat="server" Text='<%# Eval("ASXID") %>' />
            </td>
            <td>
                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
            </td>
            <td>
                <asp:Label ID="TickerLabel" runat="server" Text='<%# Eval("Ticker") %>' />
            </td>
            <td>
                <asp:Label ID="GICSLabel" runat="server" Text='<%# Eval("GICS") %>' />
            </td>
        </tr>
    </AlternatingItemTemplate>
    <EditItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
            </td>
            <td>
                <asp:Label ID="ASXIDLabel1" runat="server" Text='<%# Eval("ASXID") %>' />
            </td>
            <td>
                <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            </td>
            <td>
                <asp:TextBox ID="TickerTextBox" runat="server" Text='<%# Bind("Ticker") %>' />
            </td>
            <td>
                <asp:TextBox ID="GICSTextBox" runat="server" Text='<%# Bind("GICS") %>' />
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
    <InsertItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            </td>
            <td>
                <asp:TextBox ID="TickerTextBox" runat="server" Text='<%# Bind("Ticker") %>' />
            </td>
            <td>
                <asp:TextBox ID="GICSTextBox" runat="server" Text='<%# Bind("GICS") %>' />
            </td>
        </tr>
    </InsertItemTemplate>
    <ItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            </td>
            <td>
                <asp:Label ID="ASXIDLabel" runat="server" Text='<%# Eval("ASXID") %>' />
            </td>
            <td>
                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
            </td>
            <td>
                <asp:Label ID="TickerLabel" runat="server" Text='<%# Eval("Ticker") %>' />
            </td>
            <td>
                <asp:Label ID="GICSLabel" runat="server" Text='<%# Eval("GICS") %>' />
            </td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table runat="server">
            <tr runat="server">
                <td runat="server">
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                        <tr runat="server" style="">
                            <th runat="server"></th>
                            <th runat="server">ASXID</th>
                            <th runat="server">Name</th>
                            <th runat="server">Ticker</th>
                            <th runat="server">GICS</th>
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
                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                        </Fields>
                    </asp:DataPager>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
    <SelectedItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
            </td>
            <td>
                <asp:Label ID="ASXIDLabel" runat="server" Text='<%# Eval("ASXID") %>' />
            </td>
            <td>
                <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
            </td>
            <td>
                <asp:Label ID="TickerLabel" runat="server" Text='<%# Eval("Ticker") %>' />
            </td>
            <td>
                <asp:Label ID="GICSLabel" runat="server" Text='<%# Eval("GICS") %>' />
            </td>
        </tr>
    </SelectedItemTemplate>
</asp:ListView>
<asp:SqlDataSource ID="SqlDataSourceASX" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" DeleteCommand="DELETE FROM [ASX] WHERE [ASXID] = @ASXID" InsertCommand="INSERT INTO [ASX] ([Name], [Ticker], [GICS]) VALUES (@Name, @Ticker, @GICS)" SelectCommand="SELECT [ASXID], [Name], [Ticker], [GICS] FROM [ASX] ORDER BY [Ticker]" UpdateCommand="UPDATE [ASX] SET [Name] = @Name, [Ticker] = @Ticker, [GICS] = @GICS WHERE [ASXID] = @ASXID">
    <DeleteParameters>
        <asp:Parameter Name="ASXID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="Ticker" Type="String" />
        <asp:Parameter Name="GICS" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="Ticker" Type="String" />
        <asp:Parameter Name="GICS" Type="String" />
        <asp:Parameter Name="ASXID" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
</asp:Content>
