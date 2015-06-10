<%@ Page Title="Customer Contact Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactDetails1.aspx.cs" Inherits="BD_CMS2.ContactDetails1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        
        <div class="row">
            <div class="col-md-6">
                <div>            
            <h3>Display Customer contacts.</h3>
        </div>
                <asp:GridView ID="gvCustomer" runat="server" AllowPaging="True" AllowSorting="True" PageSize="30" AutoGenerateColumns="False" OnSelectedIndexChanging="gvCustomer_SelectedIndexChanging" CellPadding="4" DataKeyNames="CustomerContact" DataSourceID="ContactDetailsRead" ForeColor="#333333" GridLines="None" Width="381px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-default" />
                        <asp:BoundField DataField="CustomerContact" HeaderText="CustomerContact" InsertVisible="False" ReadOnly="True" SortExpression="CustomerContact" Visible="False" />
                        <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer" />
                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                        <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:SqlDataSource ID="ContactDetailsRead" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [CustomerContact], [Customer], [FirstName], [LastName], [Active] FROM [CustomerContact] ORDER BY [Customer], [LastName], [FirstName]"></asp:SqlDataSource>
            </div>
            <div class="col-md-6">
                <div>            
            <h3>Edit Customer contact details.</h3>
        </div>
                <asp:DetailsView ID="dvCustomer" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="CustomerContact" OnItemUpdated="dvCustomer_ItemUpdated" OnItemDeleted="dvCustomer_ItemDeleted" DataSourceID="ContactDetailsUpdate" ForeColor="#333333" GridLines="None" Height="50px" Width="433px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                    <EditRowStyle BackColor="#999999" />
                    <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />                    
                    <Fields>
                        <asp:BoundField DataField="CustomerContact" HeaderText="CustomerContact" InsertVisible="False" ReadOnly="True" SortExpression="CustomerContact" Visible="False" />
                        <asp:TemplateField HeaderText="Customer" SortExpression="Customer">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" DataSourceID="Company" DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Customer") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" DataSourceID="Company" DataTextField="Name" DataValueField="Name" SelectedValue='<%# Bind("Customer") %>'>
                                </asp:DropDownList>
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Customer") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                        <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                        <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-default" />
                    </Fields>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                </asp:DetailsView>
                <asp:SqlDataSource ID="ContactDetailsUpdate" runat="server" OnInserted="ContactDetailsUpdate_OnInserted" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" DeleteCommand="DELETE FROM [CustomerContact] WHERE [CustomerContact] = @CustomerContact" InsertCommand="INSERT INTO [CustomerContact] ([Customer], [FirstName], [LastName], [Phone], [email], [Active], [Notes]) VALUES (@Customer, @FirstName, @LastName, @Phone, @email, @Active, @Notes)" SelectCommand="SELECT [CustomerContact], [Customer], [FirstName], [LastName], [Phone], [email], [Active], [Notes] FROM [CustomerContact] WHERE [CustomerContact] = @CustomerContact" UpdateCommand="UPDATE [CustomerContact] SET [Customer] = @Customer, [FirstName] = @FirstName, [LastName] = @LastName, [Phone] = @Phone, [email] = @email, [Active] = @Active, [Notes] = @Notes WHERE [CustomerContact] = @CustomerContact">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="gvCustomer" PropertyName="SelectedValue"
                            Name="CustomerContact" Type="Int32" DefaultValue="0" />
                    </SelectParameters>
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
                <br />
                <asp:SqlDataSource ID="Company" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [Name] FROM [Customer] ORDER BY [Name]"></asp:SqlDataSource>

            </div>
        </div>
    </div>
</asp:Content>
