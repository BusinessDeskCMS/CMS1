<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard1.aspx.cs" Inherits="BD_CMS2.Dashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta http-equiv="Refresh" content="20" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p><a href="storyadd4.aspx" class="btn btn-success btn-large">New Story &raquo;</a></p>
            </div>
            <div class="col-md-6">
                <asp:Panel ID="PanError" runat="server" ForeColor="Red" Visible="false">
                    <h3>Error</h3>
                    <p>
                        <asp:Label ID="lbErrorWarning" runat="server" Text="At least one story has failed to send. Select Error List to view details."></asp:Label>
                    </p> 
                    <asp:Button ID="Button1" runat="server" Text="Error List" OnClick="Button1_Click" CssClass="btn btn-danger" />
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <h2>Daily Queue</h2>
                <asp:GridView ID="gvDailyQ" runat="server" AllowPaging="True" AllowSorting="True" PageSize="40" OnSelectedIndexChanged="gvDailyQ_SelectedIndexChanged" OnRowDataBound="gvDailyQ_RowDataBound" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="StoryID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-default" />
                        <asp:BoundField DataField="StoryID" HeaderText="StoryID" InsertVisible="False" ReadOnly="True" SortExpression="StoryID" />
                        <asp:BoundField DataField="Headline" HeaderText="Headline" SortExpression="Headline" />
                        <asp:BoundField DataField="DatePublished" HeaderText="DatePublished" SortExpression="DatePublished" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [StoryID], [Headline], [DatePublished], [Status] FROM [Story] ORDER BY [StoryID] DESC"></asp:SqlDataSource>

            </div>
            <div class="col-md-6">
                <h2>Review Queue</h2>

                <asp:GridView ID="gvReviewQ" runat="server" AllowPaging="True" AllowSorting="True" PageSize="20" EmptyDataText="No Stories for Review" ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="gvReviewQ_SelectedIndexChanged" OnRowDataBound="gvReviewQ_RowDataBound" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="StoryID" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Review" ControlStyle-CssClass="btn btn-default" />
                        <asp:BoundField DataField="StoryID" HeaderText="StoryID" InsertVisible="False" ReadOnly="True" SortExpression="StoryID" />
                        <asp:BoundField DataField="Headline" HeaderText="Headline" SortExpression="Headline" />
                        <asp:BoundField DataField="DateToReview" HeaderText="DateToReview" SortExpression="DateToReview" />
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
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [StoryID], [Headline], [DateToReview] FROM [Story] WHERE ([SendToReview] = @SendToReview) ORDER BY [StoryID] DESC">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Y" Name="SendToReview" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>


            </div>

        </div>
    </div>
</asp:Content>
