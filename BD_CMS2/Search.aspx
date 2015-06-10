<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="BD_CMS2.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Search for word, phrase or name in the body of a story</h3>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Enter Search String"></asp:Label>
        :<br />
        <asp:TextBox ID="txtSearch" runat="server" Width="655px" Height="30px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btSearch" runat="server" CssClass="btn btn-default" Text="Search" OnClick="btSearch_Click" />

    </div>
    <div>
        <br />
        <asp:GridView ID="gvResults" runat="server" OnRowDataBound="gvResults_RowDataBound" AutoGenerateColumns="False">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="storyid" HeaderText="Headline" DataTextField="headline" DataNavigateUrlFormatString="~\SearchDisplay1.aspx?StoryID={0}" /> 
                <asp:BoundField DataField="DatePublished" DataFormatString="{0:dd-M-yyyy}" HeaderText="Date" ItemStyle-Width="80px" />               
                <asp:TemplateField HeaderText="Story">
                    <ItemTemplate>
                        <asp:Label ID="lblBody" runat="server" Text='<%# Eval("body").ToString().Length > 100 ? Eval("body").ToString().Substring(0,100) : Eval("body") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
