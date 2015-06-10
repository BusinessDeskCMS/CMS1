<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="BD_CMS2.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta http-equiv="Refresh" content="20" />
    <style type="text/css">
        .listDQ {
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3></h3>
    <br />
    <p><a href="storyadd3.aspx" class="btn btn-primary btn-large">New Story &raquo;</a></p>
    <div class="row">

        <div class="col-md-6">
            <h2>Daily Story Queue</h2>
            <asp:ListView ID="lvDailyQ" runat="server" DataKeyNames="StoryID" OnItemEditing="lvDailyQ_ItemEditing" OnItemDataBound="lvDailyQ_ItemDataBound" DataSourceID="SqlDataSource1">


                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>No stories available.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <ItemTemplate>
                    <tr style="" class="">
                        <td>
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-default" />
                        </td>
                        <td>
                            <asp:Label ID="StoryIDLabel" runat="server" Text='<%# Eval("StoryID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="HeadlineLabel" runat="server" Text='<%# Eval("Headline") %>' />
                        </td>
                        <td>
                            <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("Status") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DatePublishedLabel" runat="server" Text='<%# GetNZ(Eval("datePublished"))  %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" style="padding-right: 25px">
                                    <tr runat="server" style="">
                                        <th runat="server"></th>
                                        <th runat="server">ID  </th>
                                        <th runat="server">Headline  </th>
                                        <th runat="server">Status  </th>
                                        <th runat="server">Date </th>
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
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" DeleteCommand="DELETE FROM [Story] WHERE [StoryID] = @original_StoryID AND (([Headline] = @original_Headline) OR ([Headline] IS NULL AND @original_Headline IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL)) AND (([DatePublished] = @original_DatePublished) OR ([DatePublished] IS NULL AND @original_DatePublished IS NULL))" InsertCommand="INSERT INTO [Story] ([Headline], [Status], [DatePublished]) VALUES (@Headline, @Status, @DatePublished)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [StoryID], [Headline], [Status], [DatePublished] FROM [Story] ORDER BY [StoryID] DESC" UpdateCommand="UPDATE [Story] SET [Headline] = @Headline, [Status] = @Status, [DatePublished] = @DatePublished WHERE [StoryID] = @original_StoryID AND (([Headline] = @original_Headline) OR ([Headline] IS NULL AND @original_Headline IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL)) AND (([DatePublished] = @original_DatePublished) OR ([DatePublished] IS NULL AND @original_DatePublished IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_StoryID" Type="Int32" />
                    <asp:Parameter Name="original_Headline" Type="String" />
                    <asp:Parameter Name="original_Status" Type="String" />
                    <asp:Parameter Name="original_DatePublished" Type="DateTime" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Headline" Type="String" />
                    <asp:Parameter Name="Status" Type="String" />
                    <asp:Parameter Name="DatePublished" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Headline" Type="String" />
                    <asp:Parameter Name="Status" Type="String" />
                    <asp:Parameter Name="DatePublished" Type="DateTime" />
                    <asp:Parameter Name="original_StoryID" Type="Int32" />
                    <asp:Parameter Name="original_Headline" Type="String" />
                    <asp:Parameter Name="original_Status" Type="String" />
                    <asp:Parameter Name="original_DatePublished" Type="DateTime" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
        <div class="col-md-6">
            <h2>Review Queue</h2>
            <asp:ListView ID="lvReviewQ" runat="server" DataKeyNames="StoryID" OnItemEditing="lvReviewQ_ItemEditing" OnItemDataBound="lvReviewQ_ItemDataBound" DataSourceID="SqlDataSource2">

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
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-default" />
                        </td>
                        <td>
                            <asp:Label ID="StoryIDLabel" runat="server" Text='<%# Eval("StoryID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="HeadlineLabel" runat="server" Text='<%# Eval("Headline") %>' />
                        </td>

                        <td>
                            <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("Status") %>' />
                        </td>
                        <td>
                            <asp:Label ID="DateToReviewLabel" runat="server" Text='<%# GetNZ(Eval("datetoreview"))  %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="0" style="padding-right: 5px">
                                    <tr runat="server" style="">
                                        <th runat="server"></th>
                                        <th runat="server">ID  </th>
                                        <th runat="server">Headline</th>
                                        <th runat="server">Status</th>
                                        <th runat="server">Time In</th>
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

            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" DeleteCommand="DELETE FROM [Story] WHERE [StoryID] = @original_StoryID AND (([Headline] = @original_Headline) OR ([Headline] IS NULL AND @original_Headline IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL)) AND (([DateToReview] = @original_DateToReview) OR ([DateToReview] IS NULL AND @original_DateToReview IS NULL))" InsertCommand="INSERT INTO [Story] ([Headline], [Status], [DateToReview]) VALUES (@Headline, @Status, @DateToReview)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [StoryID], [Headline], [Status], [DateToReview] FROM [Story] WHERE (([SendToReview] = @SendToReview) AND ([SendToReview] = @SendToReview2)) ORDER BY [StoryID] DESC" UpdateCommand="UPDATE [Story] SET [Headline] = @Headline, [Status] = @Status, [DateToReview] = @DateToReview WHERE [StoryID] = @original_StoryID AND (([Headline] = @original_Headline) OR ([Headline] IS NULL AND @original_Headline IS NULL)) AND (([Status] = @original_Status) OR ([Status] IS NULL AND @original_Status IS NULL)) AND (([DateToReview] = @original_DateToReview) OR ([DateToReview] IS NULL AND @original_DateToReview IS NULL))">
                <DeleteParameters>
                    <asp:Parameter Name="original_StoryID" Type="Int32" />
                    <asp:Parameter Name="original_Headline" Type="String" />
                    <asp:Parameter Name="original_Status" Type="String" />
                    <asp:Parameter Name="original_DateToReview" Type="DateTime" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Headline" Type="String" />
                    <asp:Parameter Name="Status" Type="String" />
                    <asp:Parameter Name="DateToReview" Type="DateTime" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter DefaultValue="Y" Name="SendToReview" Type="String" />
                    <asp:Parameter DefaultValue="Y" Name="SendToReview2" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Headline" Type="String" />
                    <asp:Parameter Name="Status" Type="String" />
                    <asp:Parameter Name="DateToReview" Type="DateTime" />
                    <asp:Parameter Name="original_StoryID" Type="Int32" />
                    <asp:Parameter Name="original_Headline" Type="String" />
                    <asp:Parameter Name="original_Status" Type="String" />
                    <asp:Parameter Name="original_DateToReview" Type="DateTime" />
                </UpdateParameters>
            </asp:SqlDataSource>

        </div>
    </div>
</asp:Content>
