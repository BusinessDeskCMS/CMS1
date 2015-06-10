<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Taxo1.aspx.cs" Inherits="BD_CMS2.Taxo1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <h3>Taxonomy Maintenance</h3>
    <br />
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="taxID" DataSourceID="SqlDataTaxo" InsertItemPosition="LastItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-default" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="taxIDLabel" runat="server" Text='<%# Eval("taxID") %>' />
                </td>
                <td>
                    <asp:Label ID="newscodeLabel" runat="server" Text='<%# Eval("newscode") %>' />
                </td>
                <td>
                    <asp:Label ID="level1Label" runat="server" Text='<%# Eval("level1") %>' />
                </td>
                <td>
                    <asp:Label ID="level2Label" runat="server" Text='<%# Eval("level2") %>' />
                </td>
                <td>
                    <asp:Label ID="level3Label" runat="server" Text='<%# Eval("level3") %>' />
                </td>
                <td>
                    <asp:Label ID="level4Label" runat="server" Text='<%# Eval("level4") %>' />
                </td>
                <td>
                    <asp:Label ID="level5Label" runat="server" Text='<%# Eval("level5") %>' />
                </td>
                <td>
                    <asp:Label ID="definitionLabel" runat="server" Text='<%# Eval("definition") %>' />
                </td>
                <td>
                    <asp:Label ID="bdquickcodeLabel" runat="server" Text='<%# Eval("bdquickcode") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-default" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-default" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="taxIDLabel1" runat="server" Text='<%# Eval("taxID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="newscodeTextBox" runat="server" Text='<%# Bind("newscode") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level1TextBox" runat="server" Text='<%# Bind("level1") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level2TextBox" runat="server" Text='<%# Bind("level2") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level3TextBox" runat="server" Text='<%# Bind("level3") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level4TextBox" runat="server" Text='<%# Bind("level4") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level5TextBox" runat="server" Text='<%# Bind("level5") %>' />
                </td>
                <td>
                    <asp:TextBox ID="definitionTextBox" runat="server" Text='<%# Bind("definition") %>' />
                </td>
                <td>
                    <asp:TextBox ID="bdquickcodeTextBox" runat="server" Text='<%# Bind("bdquickcode") %>' />
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
                    <asp:Button ID="InsertButton" runat="server" CssClass="btn btn-default" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-default" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="newscodeTextBox" runat="server" Text='<%# Bind("newscode") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level1TextBox" runat="server" Text='<%# Bind("level1") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level2TextBox" runat="server" Text='<%# Bind("level2") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level3TextBox" runat="server" Text='<%# Bind("level3") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level4TextBox" runat="server" Text='<%# Bind("level4") %>' />
                </td>
                <td>
                    <asp:TextBox ID="level5TextBox" runat="server" Text='<%# Bind("level5") %>' />
                </td>
                <td>
                    <asp:TextBox ID="definitionTextBox" runat="server" Text='<%# Bind("definition") %>' />
                </td>
                <td>
                    <asp:TextBox ID="bdquickcodeTextBox" runat="server" Text='<%# Bind("bdquickcode") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-default" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="taxIDLabel" runat="server" Text='<%# Eval("taxID") %>' />
                </td>
                <td>
                    <asp:Label ID="newscodeLabel" runat="server" Text='<%# Eval("newscode") %>' />
                </td>
                <td>
                    <asp:Label ID="level1Label" runat="server" Text='<%# Eval("level1") %>' />
                </td>
                <td>
                    <asp:Label ID="level2Label" runat="server" Text='<%# Eval("level2") %>' />
                </td>
                <td>
                    <asp:Label ID="level3Label" runat="server" Text='<%# Eval("level3") %>' />
                </td>
                <td>
                    <asp:Label ID="level4Label" runat="server" Text='<%# Eval("level4") %>' />
                </td>
                <td>
                    <asp:Label ID="level5Label" runat="server" Text='<%# Eval("level5") %>' />
                </td>
                <td>
                    <asp:Label ID="definitionLabel" runat="server" Text='<%# Eval("definition") %>' />
                </td>
                <td>
                    <asp:Label ID="bdquickcodeLabel" runat="server" Text='<%# Eval("bdquickcode") %>' />
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
                                <th runat="server">taxID</th>
                                <th runat="server">newscode</th>
                                <th runat="server">level1</th>
                                <th runat="server">level2</th>
                                <th runat="server">level3</th>
                                <th runat="server">level4</th>
                                <th runat="server">level5</th>
                                <th runat="server">definition</th>
                                <th runat="server">bdquickcode</th>
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
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="taxIDLabel" runat="server" Text='<%# Eval("taxID") %>' />
                </td>
                <td>
                    <asp:Label ID="newscodeLabel" runat="server" Text='<%# Eval("newscode") %>' />
                </td>
                <td>
                    <asp:Label ID="level1Label" runat="server" Text='<%# Eval("level1") %>' />
                </td>
                <td>
                    <asp:Label ID="level2Label" runat="server" Text='<%# Eval("level2") %>' />
                </td>
                <td>
                    <asp:Label ID="level3Label" runat="server" Text='<%# Eval("level3") %>' />
                </td>
                <td>
                    <asp:Label ID="level4Label" runat="server" Text='<%# Eval("level4") %>' />
                </td>
                <td>
                    <asp:Label ID="level5Label" runat="server" Text='<%# Eval("level5") %>' />
                </td>
                <td>
                    <asp:Label ID="definitionLabel" runat="server" Text='<%# Eval("definition") %>' />
                </td>
                <td>
                    <asp:Label ID="bdquickcodeLabel" runat="server" Text='<%# Eval("bdquickcode") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
</asp:ListView>
<asp:SqlDataSource ID="SqlDataTaxo" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" DeleteCommand="DELETE FROM [Taxo] WHERE [taxID] = @taxID" InsertCommand="INSERT INTO [Taxo] ([newscode], [level1], [level2], [level3], [level4], [level5], [definition], [bdquickcode]) VALUES (@newscode, @level1, @level2, @level3, @level4, @level5, @definition, @bdquickcode)" SelectCommand="SELECT [taxID], [newscode], [level1], [level2], [level3], [level4], [level5], [definition], [bdquickcode] FROM [Taxo]" UpdateCommand="UPDATE [Taxo] SET [newscode] = @newscode, [level1] = @level1, [level2] = @level2, [level3] = @level3, [level4] = @level4, [level5] = @level5, [definition] = @definition, [bdquickcode] = @bdquickcode WHERE [taxID] = @taxID">
    <DeleteParameters>
        <asp:Parameter Name="taxID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="newscode" Type="String" />
        <asp:Parameter Name="level1" Type="String" />
        <asp:Parameter Name="level2" Type="String" />
        <asp:Parameter Name="level3" Type="String" />
        <asp:Parameter Name="level4" Type="String" />
        <asp:Parameter Name="level5" Type="String" />
        <asp:Parameter Name="definition" Type="String" />
        <asp:Parameter Name="bdquickcode" Type="String" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="newscode" Type="String" />
        <asp:Parameter Name="level1" Type="String" />
        <asp:Parameter Name="level2" Type="String" />
        <asp:Parameter Name="level3" Type="String" />
        <asp:Parameter Name="level4" Type="String" />
        <asp:Parameter Name="level5" Type="String" />
        <asp:Parameter Name="definition" Type="String" />
        <asp:Parameter Name="bdquickcode" Type="String" />
        <asp:Parameter Name="taxID" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
</asp:Content>
