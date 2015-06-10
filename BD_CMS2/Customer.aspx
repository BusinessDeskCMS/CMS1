<%@ Page Title="Customer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="BD_CMS2.Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
     <style type="text/css">
        .contactcell {           
            padding-left:10px;
            padding-right:10px;
            font-size:small;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <h3>Customer Master Information.</h3>
    <br />
     <asp:ListView ID="ListView1" runat="server" DataKeyNames="CustomerID"  DataSourceID="SqlCustomer" InsertItemPosition="LastItem">
        
         <EditItemTemplate>
             <tr style="">
                 <td>
                     <asp:Button ID="UpdateButton" runat="server"  CssClass="btn btn-default btn-xs" CommandName="Update" Text="Update" />
                    
                 </td>
                 <td>
                      <asp:Button ID="CancelButton" runat="server"  CssClass="btn btn-default btn-xs" CommandName="Cancel" Text="Cancel" />
                 </td>
                 <td>
                     <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="Address_1TextBox" runat="server" Text='<%# Bind("Address_1") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="Address_2TextBox" runat="server" Text='<%# Bind("Address_2") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="PostCodeTextBox" runat="server" Text='<%# Bind("PostCode") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="CountryTextBox" runat="server" Text='<%# Bind("Country") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="PrimaryContactTextBox" runat="server" Text='<%# Bind("PrimaryContact") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="PrimaryContactPhoneTextBox" runat="server" Text='<%# Bind("PrimaryContactPhone") %>' />
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
                 <td><asp:Button ID="CancelButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Cancel" Text="Clear" /></td>
                 <td>
                     <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="Address_1TextBox" runat="server" Text='<%# Bind("Address_1") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="Address_2TextBox" runat="server" Text='<%# Bind("Address_2") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="PostCodeTextBox" runat="server" Text='<%# Bind("PostCode") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="CountryTextBox" runat="server" Text='<%# Bind("Country") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="PrimaryContactTextBox" runat="server" Text='<%# Bind("PrimaryContact") %>' />
                 </td>
                 <td>
                     <asp:TextBox ID="PrimaryContactPhoneTextBox" runat="server" Text='<%# Bind("PrimaryContactPhone") %>' />
                 </td>
             </tr>
         </InsertItemTemplate>
         <ItemTemplate>
             <tr style="font-size:small" >
                 <td>
                     <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Delete" Text="Delete" />
                     
                 </td>
                 <td>
                     <asp:Button ID="EditButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Edit" Text="Edit" />
                 </td>
                 <td>
                     <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                 </td>
                 <td>
                     <asp:Label ID="Address_1Label" runat="server" Text='<%# Eval("Address_1") %>' />
                 </td>
                 <td>
                     <asp:Label ID="Address_2Label" runat="server" Text='<%# Eval("Address_2") %>' />
                 </td>
                 <td>
                     <asp:Label ID="PostCodeLabel" runat="server" Text='<%# Eval("PostCode") %>' />
                 </td>
                 <td>
                     <asp:Label ID="CountryLabel" runat="server" Text='<%# Eval("Country") %>' />
                 </td>
                 <td>
                     <asp:Label ID="CityLabel" runat="server" Text='<%# Eval("City") %>' />
                 </td>
                 <td>
                     <asp:Label ID="PrimaryContactLabel" runat="server" Text='<%# Eval("PrimaryContact") %>' />
                 </td>
                 <td>
                     <asp:Label ID="PrimaryContactPhoneLabel" runat="server" Text='<%# Eval("PrimaryContactPhone") %>' />
                 </td>
             </tr>
         </ItemTemplate>
         <LayoutTemplate>
             <table runat="server">
                 <tr runat="server">
                     <td runat="server">
                         <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                             <tr runat="server" style="font-size:small">
                                 <th runat="server"></th>
                                 <th runat="server"></th>
                                 <th runat="server">Name</th>
                                 <th runat="server">Address1</th>
                                 <th runat="server">Address2</th>
                                 <th runat="server">PostCode</th>
                                 <th runat="server">Country</th>
                                 <th runat="server">City</th>
                                 <th runat="server">Primary Contact</th>
                                 <th runat="server">Primary Contact Phone</th>
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
                                 <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-default" ShowFirstPageButton="True" ShowLastPageButton="True" />
                             </Fields>
                         </asp:DataPager>
                     </td>
                 </tr>
             </table>
         </LayoutTemplate>
       
     </asp:ListView>
     <asp:SqlDataSource ID="SqlCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" DeleteCommand="DELETE FROM [Customer] WHERE [CustomerID] = @CustomerID" InsertCommand="INSERT INTO [Customer] ([Name], [Address_1], [Address_2], [PostCode], [Country], [City], [PrimaryContact], [PrimaryContactPhone]) VALUES (@Name, @Address_1, @Address_2, @PostCode, @Country, @City, @PrimaryContact, @PrimaryContactPhone)" SelectCommand="SELECT [CustomerID], [Name], [Address_1], [Address_2], [PostCode], [Country], [City], [PrimaryContact], [PrimaryContactPhone] FROM [Customer]" UpdateCommand="UPDATE [Customer] SET [Name] = @Name, [Address_1] = @Address_1, [Address_2] = @Address_2, [PostCode] = @PostCode, [Country] = @Country, [City] = @City, [PrimaryContact] = @PrimaryContact, [PrimaryContactPhone] = @PrimaryContactPhone WHERE [CustomerID] = @CustomerID">
         <DeleteParameters>
             <asp:Parameter Name="CustomerID" Type="Int32" />
         </DeleteParameters>
         <InsertParameters>
             <asp:Parameter Name="Name" Type="String" />
             <asp:Parameter Name="Address_1" Type="String" />
             <asp:Parameter Name="Address_2" Type="String" />
             <asp:Parameter Name="PostCode" Type="String" />
             <asp:Parameter Name="Country" Type="String" />
             <asp:Parameter Name="City" Type="String" />
             <asp:Parameter Name="PrimaryContact" Type="String" />
             <asp:Parameter Name="PrimaryContactPhone" Type="String" />
         </InsertParameters>
         <UpdateParameters>
             <asp:Parameter Name="Name" Type="String" />
             <asp:Parameter Name="Address_1" Type="String" />
             <asp:Parameter Name="Address_2" Type="String" />
             <asp:Parameter Name="PostCode" Type="String" />
             <asp:Parameter Name="Country" Type="String" />
             <asp:Parameter Name="City" Type="String" />
             <asp:Parameter Name="PrimaryContact" Type="String" />
             <asp:Parameter Name="PrimaryContactPhone" Type="String" />
             <asp:Parameter Name="CustomerID" Type="Int32" />
         </UpdateParameters>
     </asp:SqlDataSource>
</asp:Content>
