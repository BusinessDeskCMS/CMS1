<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerContactEdit.aspx.cs" Inherits="BD_CMS2.CustomerContactEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            padding-left:10px;
            padding-right:10px;
        }
        .auto-style2 {
            width: 426px;
        }
        .auto-style3 {
            width: 93px;
        }
        .auto-style4 {
            width: 98px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <h2><%: Title %>.</h2>
    <h3>EDIT a Customer Contact.</h3>
    <table>
        <tr>
            <td>
                <asp:Label ID="lbContactID" runat="server" Text=""></asp:Label></td>
            <td>
                <asp:Button ID="btUpdate" runat="server" CssClass="btn btn-default" Text="Update" OnClick="btUpdate_Click" />
            </td>
        </tr>
    </table>

      <table class="auto-style1">
          <tr>
              <td class="auto-style3">

      <asp:Label ID="lbFirstName" runat="server" Text="First Name"></asp:Label></td>
              <td class="auto-style2"><asp:TextBox ID="tbFirstName" runat="server" Width="313px"></asp:TextBox>
              </td>
              <td class="auto-style4">
       <asp:Label ID="lbLastName" runat="server" Text="Last Name"></asp:Label></td>
              <td><asp:TextBox ID="tbLastName" runat="server" Width="330px"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td class="auto-style3">
       <asp:Label ID="lbPhome" runat="server" Text="Phone"></asp:Label></td>
              <td class="auto-style2"><asp:TextBox ID="tbPhone" runat="server" Width="311px"></asp:TextBox>
              </td>
              <td class="auto-style4">
       <asp:Label ID="lbemail" runat="server" Text="e-mail"></asp:Label></td>
              <td><asp:TextBox ID="tbemail" runat="server" Width="326px"></asp:TextBox>
              </td>
          </tr>
          <tr>
              <td class="auto-style3">
       <asp:Label ID="Notes" runat="server" Text="Notes"></asp:Label></td>
              <td class="auto-style2"><asp:TextBox ID="tbNotes" runat="server" Height="84px" TextMode="MultiLine" Width="375px"></asp:TextBox>
              </td>
              <td class="auto-style4">
       <asp:Label ID="lbActive" runat="server" Text="Active"></asp:Label></td>
              <td>
                  <asp:DropDownList ID="ddlActive" runat="server">
                      <asp:ListItem>Yes</asp:ListItem>
                      <asp:ListItem>No</asp:ListItem>
                  </asp:DropDownList>
              </td>
          </tr>
      </table>
    <br />

      <br />
    

</asp:Content>
