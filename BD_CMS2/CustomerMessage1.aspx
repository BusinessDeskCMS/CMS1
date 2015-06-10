<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerMessage1.aspx.cs" Inherits="BD_CMS2.CustomerMessage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-6">
            <div>
                <h3>Customer Message Status</h3>
            </div>
            <asp:GridView ID="gvCustomer" runat="server" AllowPaging="True" AllowSorting="True" PageSize="30" AutoGenerateColumns="False" OnSelectedIndexChanging="gvCustomer_SelectedIndexChanging" CellPadding="4" DataKeyNames="CustomerMessageID" DataSourceID="CustomerMessageRead" ForeColor="#333333" GridLines="None" Width="381px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="CustomerMessageID" HeaderText="CustomerMessageID" InsertVisible="False" ReadOnly="True" SortExpression="CustomerMessageID" Visible="False" />
                    <asp:BoundField DataField="customername" HeaderText="customername" SortExpression="customername" />
                    <asp:BoundField DataField="active" HeaderText="active" SortExpression="active" />
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
            <asp:SqlDataSource ID="CustomerMessageRead" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [CustomerMessageID], [customername], [active] FROM [CustomerMessage] ORDER BY [customername]"></asp:SqlDataSource>
        </div>
        <div class="col-md-6">
            <div>
                <h3>Edit Customer contact details.</h3>
            </div>
            <asp:DetailsView ID="dvCustomer" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="CustomerMessageID" OnItemUpdated="dvCustomer_ItemUpdated" OnItemDeleted="dvCustomer_ItemDeleted" DataSourceID="CustomerMessageUpdate" ForeColor="#333333" GridLines="None" Height="50px" Width="433px" AllowPaging="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                <EditRowStyle BackColor="#999999" />
                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                <Fields>
                    <asp:BoundField DataField="CustomerMessageID" HeaderText="CustomerMessageID" InsertVisible="False" ReadOnly="True" SortExpression="CustomerMessageID" Visible="False" />
                    <asp:TemplateField HeaderText="Customer" SortExpression="customername">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("customername") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("customername") %>'></asp:TextBox>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("customername") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FTP_Host" HeaderText="FTP_Host" SortExpression="FTP_Host" />
                    <asp:BoundField DataField="FTP_Password" HeaderText="FTP_Password" SortExpression="FTP_Password" />
                    <asp:BoundField DataField="FTP_UserName" HeaderText="FTP_UserName" SortExpression="FTP_UserName" />
                    <asp:BoundField DataField="TechnicalContact" HeaderText="Technical Contact" SortExpression="TechnicalContact" />
                    <asp:BoundField DataField="TechnicalContactPhone" HeaderText="Technical Contact Phone" SortExpression="TechnicalContactPhone" />
                    <asp:BoundField DataField="messagetype" HeaderText="messagetype" SortExpression="messagetype" />
                    <asp:BoundField DataField="active" HeaderText="active" SortExpression="active" />
                    <asp:TemplateField HeaderText="Message Name" SortExpression="messagename">
                        <EditItemTemplate>
                            <br />
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SQLMessage" DataTextField="MessageName" DataValueField="MessageName" SelectedValue='<%# Bind("messagename") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <InsertItemTemplate>
                            <br />
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SQLMessage" DataTextField="MessageName" DataValueField="MessageName" SelectedValue='<%# Bind("messagename") %>'>
                            </asp:DropDownList>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("messagename") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="False" ShowEditButton="True" ShowInsertButton="True" />
                </Fields>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            </asp:DetailsView>
            <asp:SqlDataSource ID="CustomerMessageUpdate" runat="server" OnInserted="ContactDetailsUpdate_OnInserted" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" DeleteCommand="DELETE FROM [CustomerMessage] WHERE [CustomerMessageID] = @CustomerMessageID" InsertCommand="INSERT INTO [CustomerMessage] ([FTP_Host], [FTP_Password], [FTP_UserName], [TechnicalContact], [TechnicalContactPhone], [messagetype], [active], [messagename], [customername]) VALUES (@FTP_Host, @FTP_Password, @FTP_UserName, @TechnicalContact, @TechnicalContactPhone, @messagetype, @active, @messagename, @customername)" SelectCommand="SELECT [CustomerMessageID], [FTP_Host], [FTP_Password], [FTP_UserName], [TechnicalContact], [TechnicalContactPhone], [messagetype], [active], [messagename], [customername] FROM [CustomerMessage] WHERE [CustomerMessageID] = @CustomerMessageId ORDER BY [customername]" UpdateCommand="UPDATE [CustomerMessage] SET [FTP_Host] = @FTP_Host, [FTP_Password] = @FTP_Password, [FTP_UserName] = @FTP_UserName, [TechnicalContact] = @TechnicalContact, [TechnicalContactPhone] = @TechnicalContactPhone, [messagetype] = @messagetype, [active] = @active, [messagename] = @messagename, [customername] = @customername WHERE [CustomerMessageID] = @CustomerMessageID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="gvCustomer" PropertyName="SelectedValue"
                        Name="CustomerMessageID" Type="Int32" DefaultValue="0" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="CustomerMessageID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="FTP_Host" Type="String" />
                    <asp:Parameter Name="FTP_Password" Type="String" />
                    <asp:Parameter Name="FTP_UserName" Type="String" />
                    <asp:Parameter Name="TechnicalContact" Type="String" />
                    <asp:Parameter Name="TechnicalContactPhone" Type="String" />
                    <asp:Parameter Name="messagetype" Type="String" />
                    <asp:Parameter Name="active" Type="String" />
                    <asp:Parameter Name="messagename" Type="String" />
                    <asp:Parameter Name="customername" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="FTP_Host" Type="String" />
                    <asp:Parameter Name="FTP_Password" Type="String" />
                    <asp:Parameter Name="FTP_UserName" Type="String" />
                    <asp:Parameter Name="TechnicalContact" Type="String" />
                    <asp:Parameter Name="TechnicalContactPhone" Type="String" />
                    <asp:Parameter Name="messagetype" Type="String" />
                    <asp:Parameter Name="active" Type="String" />
                    <asp:Parameter Name="messagename" Type="String" />
                    <asp:Parameter Name="customername" Type="String" />
                    <asp:Parameter Name="CustomerMessageID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
            <asp:SqlDataSource ID="Company" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [Name] FROM [Customer] ORDER BY [Name]"></asp:SqlDataSource>


            <asp:SqlDataSource ID="SQLMessage" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [MessageName] FROM [Message] ORDER BY [MessageName]"></asp:SqlDataSource>

        </div>
    </div>
</asp:Content>
