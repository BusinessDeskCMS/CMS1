<%@ Page Title="Error Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorDetail.aspx.cs" Inherits="BD_CMS2.ErrorDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <br />

    <div class="row">
        <div class="col-md-6">
        </div>
    </div>

    <h3>
        <asp:Label ID="lbHeadline" runat="server" Text=""></asp:Label>
    </h3>
    <br />
    Story ID:
    <asp:Label ID="lbStoryID" runat="server" Text=""></asp:Label>
    <br />
    Published:
    <asp:Label ID="lbPublishedDate" runat="server" Text=""></asp:Label>
    <p>
        <asp:TextBox ID="tbStory" runat="server" Height="271px" Width="908px" ReadOnly="True" Rows="10" TextMode="MultiLine"></asp:TextBox>
    </p>
    <hr />
    <div>
        <h2>Message Queue</h2>

        <asp:GridView ID="gvErrorQ" runat="server" AllowPaging="True" AllowSorting="True" OnRowCommand="gvErrorQ_RowCommand"
            OnSelectedIndexChanged="gvErrorQ_SelectedIndexChanged" OnRowDataBound="gvErrorQ_RowDataBound" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="MsgLogID" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:ButtonField CommandName="Ignore" Text="Ignore" ButtonType="Button" ControlStyle-CssClass="btn btn-primary" />
                <asp:CommandField ShowSelectButton="True" SelectText="Resend" ButtonType="Button" ControlStyle-CssClass="btn btn-danger">
                    <ControlStyle CssClass="btn btn-danger"></ControlStyle>
                </asp:CommandField>
                <asp:BoundField DataField="MsgLogID" HeaderText="MsgLogID" InsertVisible="False" ReadOnly="True" SortExpression="MsgLogID" />
                <asp:BoundField DataField="StoryID" HeaderText="StoryID" SortExpression="StoryID" />
                <asp:BoundField DataField="Headline" HeaderText="Headline" SortExpression="Headline" Visible="False" />
                <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" Visible="False" />
                <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer" />
                <asp:BoundField DataField="MsgStatus" HeaderText="MsgStatus" SortExpression="MsgStatus" />
                <asp:BoundField DataField="DateCreated" HeaderText="Date Pub" SortExpression="DateCreated" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT * FROM [MsgLog] WHERE ([StoryID] = @StoryID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="lbStoryID" Name="StoryID" PropertyName="Text" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>


    </div>
</asp:Content>
