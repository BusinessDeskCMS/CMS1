<%@ Page Title="MessageResend" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MessageResend.aspx.cs" Inherits="BD_CMS2.MessageResend" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <h3>Resend a Message(s) to a Customer</h3>
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server" Height="28px" Width="142px">
        <asp:ListItem>Today</asp:ListItem>
        <asp:ListItem>Yesterday</asp:ListItem>
        <asp:ListItem Value="LW">Last Week</asp:ListItem>
        <asp:ListItem Value="1M">Last Month</asp:ListItem>
        <asp:ListItem Value="3M">Last 3 Months</asp:ListItem>
     </asp:DropDownList>

     <asp:Button ID="btSelect" runat="server" Text="Select" class="btn btn-default btn-sm" OnClick="btSelect_Click" />

     <br />
     <br />

     <br />
     <asp:ListView ID="lvResend" runat="server"  DataKeyNames="StoryID" OnItemCommand="lvResendView_OnItemCommand" DataSourceID="SqlResend">
         
         <EmptyDataTemplate>
             <table runat="server" style="">
                 <tr>
                     <td>No data was returned.</td>
                 </tr>
             </table>
         </EmptyDataTemplate>
        
         <ItemTemplate>
             <tr style="">
                  <td style="width:80px">
              <asp:LinkButton runat="server" 
                ID="SelectStoryButton" 
                Text="Add To List" 
                CommandName="AddToList" 
                CommandArgument='<%#Eval("StoryID") + ", " + Eval("Headline") %>' />
            </td>

                 <td>
                     <asp:Label ID="StoryIDLabel" runat="server" Text='<%# Eval("StoryID") %>' />
                 </td>
                 <td>
                     <asp:Label ID="HeadlineLabel" runat="server" Text='<%# Eval("Headline") %>' />
                 </td>
                 <td>
                     <asp:Label ID="DatePublishedLabel" runat="server" Text='<%# Eval("DatePublished") %>' />
                 </td>
                 <td>
                     <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("Status") %>' />
                 </td>
             </tr>
         </ItemTemplate>
         <LayoutTemplate>
             <table runat="server">
                 <tr runat="server">
                     <td runat="server">
                         <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                             <tr runat="server" style="">
                                 <th runat="server">StoryID</th>
                                 <th runat="server">Headline</th>
                                 <th runat="server">DatePublished</th>
                                 <th runat="server">Status</th>
                             </tr>
                             <tr id="itemPlaceholder" runat="server">
                             </tr>
                         </table>
                     </td>
                 </tr>
                 <tr runat="server">
                     <td runat="server" style="">
                     </td>
                 </tr>
             </table>
         </LayoutTemplate>
         <SelectedItemTemplate>
             <tr style="">
                 <td>
                     <asp:Label ID="StoryIDLabel" runat="server" Text='<%# Eval("StoryID") %>' />
                 </td>
                 <td>
                     <asp:Label ID="HeadlineLabel" runat="server" Text='<%# Eval("Headline") %>' />
                 </td>
                 <td>
                     <asp:Label ID="DatePublishedLabel" runat="server" Text='<%# Eval("DatePublished") %>' />
                 </td>
                 <td>
                     <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("Status") %>' />
                 </td>
             </tr>
         </SelectedItemTemplate>
     </asp:ListView>
     <br /><br />
      <b>Selected Stories:</b><br />
      <asp:ListBox runat="server" ID="SelectedStoriesListBox" Rows="10" Width="804px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btSendStories" runat="server" Text="Send Stories" class="btn btn-default btn-sm" OnClick="btSendStories_Click" /> 

     <asp:SqlDataSource ID="SqlResend" runat="server" ConnectionString="<%$ ConnectionStrings:azureConnectionString %>" SelectCommand="SELECT TOP (50) [StoryID], [Headline], [DatePublished], [Status] FROM [Story] WHERE ([Status] = @Status) ORDER BY storyid desc">
         <SelectParameters>
             <asp:Parameter DefaultValue="Published" Name="Status" Type="String" />
         </SelectParameters>
     </asp:SqlDataSource>

</asp:Content>
