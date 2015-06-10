<%@ Page Title="Error Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard_Error.aspx.cs" Inherits="BD_CMS2.Dashboard_Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <h2>Published Queue</h2>
                <asp:GridView ID="gvPublishedQ" runat="server" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="gvPublishedQ_SelectedIndexChanged" OnRowDataBound="gvPublishedQ_RowDataBound" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="MsgLogID" DataSourceID="SqlDataSource3" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
<ControlStyle CssClass="btn btn-default"></ControlStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="MsgLogID" HeaderText="MsgLogID" InsertVisible="False" ReadOnly="True" SortExpression="MsgLogID" Visible="False" />
                        <asp:BoundField DataField="StoryID" HeaderText="StoryID" SortExpression="StoryID" />
                        <asp:BoundField DataField="Headline" HeaderText="Headline" SortExpression="Headline" />
                        <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" Visible="False" />
                        <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer" />
                        <asp:BoundField DataField="MsgStatus" HeaderText="MsgStatus" SortExpression="MsgStatus" />
                        <asp:BoundField DataField="DateCreated" HeaderText="Time Published" SortExpression="DateCreated" />
                        <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
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
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT * FROM [MsgLog] WHERE ([status] = @status)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Publish OK" Name="status" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [StoryID], [Headline], [DatePublished], [Status] FROM [Story] ORDER BY [StoryID]"></asp:SqlDataSource>


            </div>
            <div class="col-md-6">
                <h2>Error Queue</h2>
                <asp:GridView ID="gvErrorQ" runat="server" AllowPaging="True" AllowSorting="True" OnSelectedIndexChanged="gvErrorQ_SelectedIndexChanged" OnRowDataBound="gvErrorQ_RowDataBound" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="MsgLogID" DataSourceID="SqlDataSource4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-danger" >
<ControlStyle CssClass="btn btn-danger"></ControlStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="MsgLogID" HeaderText="MsgLogID" InsertVisible="False" ReadOnly="True" SortExpression="MsgLogID" Visible="False" />
                        <asp:BoundField DataField="StoryID" HeaderText="StoryID" SortExpression="StoryID" />
                        <asp:BoundField DataField="Headline" HeaderText="Headline" SortExpression="Headline" />
                        <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" Visible="False" />
                        <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer" />
                        <asp:BoundField DataField="MsgStatus" HeaderText="MsgStatus" SortExpression="MsgStatus" ItemStyle-ForeColor="Red" >
<ItemStyle ForeColor="Red"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                        <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
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
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT * FROM [MsgLog] WHERE ([status] = @status)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Pub Error" Name="status" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT [StoryID], [Headline], [DateToReview] FROM [Story] WHERE ([SendToReview] = @SendToReview) ORDER BY [StoryID]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Y" Name="SendToReview" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
