<%@ Page Title="BusinessDesk Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="BD_CMS2.Contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <h2><%: Title %>.</h2>
    <h3>Contact List.</h3>
    <br />
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="ContactsID" DataSourceID="SqlDataSourceContacts" InsertItemPosition="LastItem">
    <AlternatingItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-default" />
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-default" />
            </td>
            <td>
                <asp:Label ID="ContactsIDLabel" runat="server" Text='<%# Eval("ContactsID") %>' />
            </td>
            <td>
                <asp:Label ID="CustomerLabel" runat="server" Text='<%# Eval("Customer") %>' />
            </td>
            <td>
                <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
            </td>
            <td>
                <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
            </td>
            <td>
                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
            </td>
            <td>
                <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Phone") %>' />
            </td>
            <td>
                <asp:Label ID="MobilePhoneLabel" runat="server" Text='<%# Eval("MobilePhone") %>' />
            </td>
            <td>
                <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("email") %>' />
            </td>
            <td>
                <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
            </td>
        </tr>
    </AlternatingItemTemplate>
    <EditItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-default" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-default" />
            </td>
            <td>
                <asp:Label ID="ContactsIDLabel1" runat="server" Text='<%# Eval("ContactsID") %>' />
            </td>
            <td>
                <asp:TextBox ID="CustomerTextBox" runat="server" Text='<%# Bind("Customer") %>' />
            </td>
            <td>
                <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            </td>
            <td>
                <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
            </td>
            <td>
                <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
            </td>
            <td>
                <asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' />
            </td>
            <td>
                <asp:TextBox ID="MobilePhoneTextBox" runat="server" Text='<%# Bind("MobilePhone") %>' />
            </td>
            <td>
                <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>' />
            </td>
            <td>
                <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
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
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="btn btn-default"/>
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="btn btn-default" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:TextBox ID="CustomerTextBox" runat="server" Text='<%# Bind("Customer") %>' />
            </td>
            <td>
                <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
            </td>
            <td>
                <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
            </td>
            <td>
                <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
            </td>
            <td>
                <asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' />
            </td>
            <td>
                <asp:TextBox ID="MobilePhoneTextBox" runat="server" Text='<%# Bind("MobilePhone") %>' />
            </td>
            <td>
                <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>' />
            </td>
            <td>
                <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
            </td>
        </tr>
    </InsertItemTemplate>
    <ItemTemplate>
        <tr style="">
            <td>
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-default" />
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-default" />
            </td>
            <td>
                <asp:Label ID="ContactsIDLabel" runat="server" Text='<%# Eval("ContactsID") %>' />
            </td>
            <td>
                <asp:Label ID="CustomerLabel" runat="server" Text='<%# Eval("Customer") %>' />
            </td>
            <td>
                <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
            </td>
            <td>
                <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
            </td>
            <td>
                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
            </td>
            <td>
                <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Phone") %>' />
            </td>
            <td>
                <asp:Label ID="MobilePhoneLabel" runat="server" Text='<%# Eval("MobilePhone") %>' />
            </td>
            <td>
                <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("email") %>' />
            </td>
            <td>
                <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
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
                            <th runat="server">ContactsID</th>
                            <th runat="server">Customer</th>
                            <th runat="server">Title</th>
                            <th runat="server">FirstName</th>
                            <th runat="server">LastName</th>
                            <th runat="server">Phone</th>
                            <th runat="server">MobilePhone</th>
                            <th runat="server">email</th>
                            <th runat="server">Notes</th>
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
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-default" />
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-default" />
            </td>
            <td>
                <asp:Label ID="ContactsIDLabel" runat="server" Text='<%# Eval("ContactsID") %>' />
            </td>
            <td>
                <asp:Label ID="CustomerLabel" runat="server" Text='<%# Eval("Customer") %>' />
            </td>
            <td>
                <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
            </td>
            <td>
                <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") %>' />
            </td>
            <td>
                <asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName") %>' />
            </td>
            <td>
                <asp:Label ID="PhoneLabel" runat="server" Text='<%# Eval("Phone") %>' />
            </td>
            <td>
                <asp:Label ID="MobilePhoneLabel" runat="server" Text='<%# Eval("MobilePhone") %>' />
            </td>
            <td>
                <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("email") %>' />
            </td>
            <td>
                <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
            </td>
        </tr>
    </SelectedItemTemplate>
</asp:ListView>
<asp:SqlDataSource ID="SqlDataSourceContacts" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" DeleteCommand="DELETE FROM [Contacts] WHERE [ContactsID] = @ContactsID" InsertCommand="INSERT INTO [Contacts] ([Customer], [Title], [FirstName], [LastName], [Phone], [MobilePhone], [email], [Notes]) VALUES (@Customer, @Title, @FirstName, @LastName, @Phone, @MobilePhone, @email, @Notes)" SelectCommand="SELECT [ContactsID], [Customer], [Title], [FirstName], [LastName], [Phone], [MobilePhone], [email], [Notes] FROM [Contacts]" UpdateCommand="UPDATE [Contacts] SET [Customer] = @Customer, [Title] = @Title, [FirstName] = @FirstName, [LastName] = @LastName, [Phone] = @Phone, [MobilePhone] = @MobilePhone, [email] = @email, [Notes] = @Notes WHERE [ContactsID] = @ContactsID">
    <DeleteParameters>
        <asp:Parameter Name="ContactsID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Customer" Type="String" />
        <asp:Parameter Name="Title" Type="String" />
        <asp:Parameter Name="FirstName" Type="String" />
        <asp:Parameter Name="LastName" Type="String" />
        <asp:Parameter Name="Phone" Type="String" />
        <asp:Parameter Name="MobilePhone" Type="String" />
        <asp:Parameter Name="email" Type="String" />
        <asp:Parameter Name="Notes" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Customer" Type="String" />
        <asp:Parameter Name="Title" Type="String" />
        <asp:Parameter Name="FirstName" Type="String" />
        <asp:Parameter Name="LastName" Type="String" />
        <asp:Parameter Name="Phone" Type="String" />
        <asp:Parameter Name="MobilePhone" Type="String" />
        <asp:Parameter Name="email" Type="String" />
        <asp:Parameter Name="Notes" Type="String" />
        <asp:Parameter Name="ContactsID" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>

</asp:Content>
