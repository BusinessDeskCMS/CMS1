<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerMessageControl.aspx.cs" Inherits="BD_CMS2.CustomerMessageControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>CMS Customer Message Control Settings.</h3>
    <br />
    <div class="row">
        <div class="col-md-6">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="CustomerMessageID" DataSourceID="SqlDataCustomerMessage" InsertItemPosition="LastItem">

                <EditItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Update" Text="Update" />
                        </td>
                        <td>
                            <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-default btn-xs" CommandName="Cancel" Text="Cancel" />
                        </td>
                        <td>
                            <asp:TextBox ID="FTP_HostTextBox" runat="server" Text='<%# Bind("FTP_Host") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="FTP_PasswordTextBox" runat="server" Text='<%# Bind("FTP_Password") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="FTP_UserNameTextBox" runat="server" Text='<%# Bind("FTP_UserName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TechnicalContactTextBox" runat="server" Text='<%# Bind("TechnicalContact") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TechnicalContactPhoneTextBox" runat="server" Text='<%# Bind("TechnicalContactPhone") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="messagetypeTextBox" runat="server" Text='<%# Bind("messagetype") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="activeTextBox" runat="server" Text='<%# Bind("active") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="SFTP_PathTextBox" runat="server" Text='<%# Bind("SFTP_Path") %>' />
                        </td>
                        <td>
                            <asp:Label ID="CustomerMessageIDLabel1" runat="server" Text='<%# Eval("CustomerMessageID") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="CustomerIDTextBox" runat="server" Text='<%# Bind("CustomerID") %>' />
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
                        <td>
                            <asp:TextBox ID="FTP_HostTextBox" runat="server" Text='<%# Bind("FTP_Host") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="FTP_PasswordTextBox" runat="server" Text='<%# Bind("FTP_Password") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="FTP_UserNameTextBox" runat="server" Text='<%# Bind("FTP_UserName") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TechnicalContactTextBox" runat="server" Text='<%# Bind("TechnicalContact") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="TechnicalContactPhoneTextBox" runat="server" Text='<%# Bind("TechnicalContactPhone") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="messagetypeTextBox" runat="server" Text='<%# Bind("messagetype") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="activeTextBox" runat="server" Text='<%# Bind("active") %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="SFTP_PathTextBox" runat="server" Text='<%# Bind("SFTP_Path") %>' />
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:TextBox ID="CustomerIDTextBox" runat="server" Text='<%# Bind("CustomerID") %>' />
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
                            <asp:Label ID="FTP_HostLabel" runat="server" Text='<%# Eval("FTP_Host") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FTP_PasswordLabel" runat="server" Text='<%# Eval("FTP_Password") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FTP_UserNameLabel" runat="server" Text='<%# Eval("FTP_UserName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TechnicalContactLabel" runat="server" Text='<%# Eval("TechnicalContact") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TechnicalContactPhoneLabel" runat="server" Text='<%# Eval("TechnicalContactPhone") %>' />
                        </td>
                        <td>
                            <asp:Label ID="messagetypeLabel" runat="server" Text='<%# Eval("messagetype") %>' />
                        </td>
                        <td>
                            <asp:Label ID="activeLabel" runat="server" Text='<%# Eval("active") %>' />
                        </td>
                        <td>
                            <asp:Label ID="SFTP_PathLabel" runat="server" Text='<%# Eval("SFTP_Path") %>' />
                        </td>
                        <td>
                            <asp:Label ID="CustomerMessageIDLabel" runat="server" Text='<%# Eval("CustomerMessageID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="CustomerIDLabel" runat="server" Text='<%# Eval("CustomerID") %>' />
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
                                        <th runat="server"></th>
                                        <th runat="server">FTP Host</th>
                                        <th runat="server">FTP Password</th>
                                        <th runat="server">FTP UserName</th>
                                        <th runat="server">Technical Contact</th>
                                        <th runat="server">Technical Contact Phone</th>
                                        <th runat="server">messagetype</th>
                                        <th runat="server">active</th>
                                        <th runat="server">SFTP Path</th>
                                        <th runat="server">CustomerMessageID</th>
                                        <th runat="server">CustomerID</th>
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
                            <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-default" CommandName="Delete" Text="Delete" />
                            <asp:Button ID="EditButton" runat="server" CssClass="btn btn-default" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Label ID="FTP_HostLabel" runat="server" Text='<%# Eval("FTP_Host") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FTP_PasswordLabel" runat="server" Text='<%# Eval("FTP_Password") %>' />
                        </td>
                        <td>
                            <asp:Label ID="FTP_UserNameLabel" runat="server" Text='<%# Eval("FTP_UserName") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TechnicalContactLabel" runat="server" Text='<%# Eval("TechnicalContact") %>' />
                        </td>
                        <td>
                            <asp:Label ID="TechnicalContactPhoneLabel" runat="server" Text='<%# Eval("TechnicalContactPhone") %>' />
                        </td>
                        <td>
                            <asp:Label ID="messagetypeLabel" runat="server" Text='<%# Eval("messagetype") %>' />
                        </td>
                        <td>
                            <asp:Label ID="activeLabel" runat="server" Text='<%# Eval("active") %>' />
                        </td>
                        <td>
                            <asp:Label ID="SFTP_PathLabel" runat="server" Text='<%# Eval("SFTP_Path") %>' />
                        </td>
                        <td>
                            <asp:Label ID="CustomerMessageIDLabel" runat="server" Text='<%# Eval("CustomerMessageID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="CustomerIDLabel" runat="server" Text='<%# Eval("CustomerID") %>' />
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataCustomerMessage" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" DeleteCommand="DELETE FROM [CustomerMessage] WHERE [CustomerMessageID] = @CustomerMessageID" InsertCommand="INSERT INTO [CustomerMessage] ([FTP_Host], [FTP_Password], [FTP_UserName], [TechnicalContact], [TechnicalContactPhone], [messagetype], [active], [SFTP_Path], [CustomerID]) VALUES (@FTP_Host, @FTP_Password, @FTP_UserName, @TechnicalContact, @TechnicalContactPhone, @messagetype, @active, @SFTP_Path, @CustomerID)" SelectCommand="SELECT [FTP_Host], [FTP_Password], [FTP_UserName], [TechnicalContact], [TechnicalContactPhone], [messagetype], [active], [SFTP_Path], [CustomerMessageID], [CustomerID] FROM [CustomerMessage]" UpdateCommand="UPDATE [CustomerMessage] SET [FTP_Host] = @FTP_Host, [FTP_Password] = @FTP_Password, [FTP_UserName] = @FTP_UserName, [TechnicalContact] = @TechnicalContact, [TechnicalContactPhone] = @TechnicalContactPhone, [messagetype] = @messagetype, [active] = @active, [SFTP_Path] = @SFTP_Path, [CustomerID] = @CustomerID WHERE [CustomerMessageID] = @CustomerMessageID">
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
                    <asp:Parameter Name="SFTP_Path" Type="String" />
                    <asp:Parameter Name="CustomerID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="FTP_Host" Type="String" />
                    <asp:Parameter Name="FTP_Password" Type="String" />
                    <asp:Parameter Name="FTP_UserName" Type="String" />
                    <asp:Parameter Name="TechnicalContact" Type="String" />
                    <asp:Parameter Name="TechnicalContactPhone" Type="String" />
                    <asp:Parameter Name="messagetype" Type="String" />
                    <asp:Parameter Name="active" Type="String" />
                    <asp:Parameter Name="SFTP_Path" Type="String" />
                    <asp:Parameter Name="CustomerID" Type="Int32" />
                    <asp:Parameter Name="CustomerMessageID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>

</asp:Content>
