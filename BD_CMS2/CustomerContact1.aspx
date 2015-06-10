<%@ Page Title="Subscribers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerContact1.aspx.cs" Inherits="BD_CMS2.CustomerContact1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .contactcell {
            padding-left: 10px;
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Email Subscriber Information.</h3>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Filter by Company"></asp:Label>
    &nbsp;
     <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" AutoPostBack="True">
         <asp:ListItem>All</asp:ListItem>
     </asp:DropDownList>
    <br />
    <asp:ListView ID="lvContacts" runat="server" DataKeyNames="CustomerContact" OnItemInserting="lvContacts_ItemInserting" OnItemEditing="lvContacts_ItemEditing" DataSourceID="SqlDataSource2" InsertItemPosition="LastItem">

    
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Update" Text="Update" />
                    
                </td>
                <td>
                    <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="CustomerTextBox" runat="server" Text='<%# Bind("Customer") %>' />
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
                    <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>' />
                </td>
                <td>
                    <asp:TextBox ID="ActiveTextBox" runat="server" Text='<%# Bind("Active") %>' />
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
                    <asp:Button ID="InsertButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Insert" Text="Insert" />
                    
                </td>
                <td>
                   <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Cancel" Text="Clear" />
                   
                </td>
                <td> <asp:DropDownList ID="ddlInsertCompany" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" AutoPostBack="True">
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>

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
                    <asp:TextBox ID="emailTextBox" runat="server" Text='<%# Bind("email") %>' />
                </td>
                <td>
                    <asp:TextBox ID="ActiveTextBox" runat="server" Text='<%# Bind("Active") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Delete" Text="Delete" />
                    
                </td>
                <td>
                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="CustomerLabel" runat="server" Text='<%# Eval("Customer") %>' />
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
                    <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("email") %>' />
                </td>
                <td>
                    <asp:Label ID="ActiveLabel" runat="server" Text='<%# Eval("Active") %>' />
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
                                <th runat="server">Customer</th>
                                <th runat="server">CustomerContact</th>
                                <th runat="server">FirstName</th>
                                <th runat="server">LastName</th>
                                <th runat="server">Phone</th>
                                <th runat="server">email</th>
                                <th runat="server">Active</th>
                                <th runat="server">Notes</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-default" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>       
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" DeleteCommand="DELETE FROM [CustomerContact] WHERE [CustomerContact] = @CustomerContact" InsertCommand="INSERT INTO [CustomerContact] ([Customer], [FirstName], [LastName], [Phone], [email], [Active], [Notes]) VALUES (@Customer, @FirstName, @LastName, @Phone, @email, @Active, @Notes)" SelectCommand="SELECT [Customer], [CustomerContact], [FirstName], [LastName], [Phone], [email], [Active], [Notes] FROM [CustomerContact] WHERE ([Customer] = @Customer)" UpdateCommand="UPDATE [CustomerContact] SET [Customer] = @Customer, [FirstName] = @FirstName, [LastName] = @LastName, [Phone] = @Phone, [email] = @email, [Active] = @Active, [Notes] = @Notes WHERE [CustomerContact] = @CustomerContact">
        <DeleteParameters>
            <asp:Parameter Name="CustomerContact" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Customer" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="Active" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" Name="Customer" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Customer" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="email" Type="String" />
            <asp:Parameter Name="Active" Type="String" />
            <asp:Parameter Name="Notes" Type="String" />
            <asp:Parameter Name="CustomerContact" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Name] FROM [Customer] ORDER BY [Name]"></asp:SqlDataSource>
</asp:Content>
