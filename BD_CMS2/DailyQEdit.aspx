<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyQEdit.aspx.cs" Inherits="BD_CMS2.DailyQEdit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
     <script type="text/javascript" src="Scripts/ckeditor/ckeditor.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Edit a Story from the Daily Story Queue.</h3>

    StoryID:
    <asp:Label ID="lbStoryID" runat="server" Text=""></asp:Label>
    <br />
    <div>
        <table class="nav-justified">
            <tr>
                <td>Type:&nbsp;
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Selected="True">News</asp:ListItem>
                        <asp:ListItem>Correction</asp:ListItem>
                        <asp:ListItem>Update</asp:ListItem>
                        <asp:ListItem>Kill</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Location:&nbsp;
                    <asp:DropDownList ID="ddlLocation" runat="server">
                        <asp:ListItem Selected="True">Wellington</asp:ListItem>
                        <asp:ListItem>Auckland</asp:ListItem>
                        <asp:ListItem>Christchurch</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Ticker:&nbsp;<asp:DropDownList ID="ddlTicker" runat="server">
                    <asp:ListItem>XERO</asp:ListItem>
                    <asp:ListItem>AIRNZ</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>People:&nbsp;</td>
                <td>Subject:&nbsp;<asp:DropDownList ID="ddlPeople" runat="server">
                    <asp:ListItem>Taxonomy</asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Hold?&nbsp;<asp:DropDownList ID="ddlHold" runat="server">
                    <asp:ListItem Selected="True">No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>Hold Date/Time
                    
                </td>
                <td>Send to Review?&nbsp;<asp:DropDownList ID="ddlSendToReview" runat="server">
                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>  <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-large" Text="Cancel" OnClick="btnCancel_Click" /></td>
                <td>
                    <asp:Button ID="btnSaveStory" runat="server" CssClass="btn btn-primary btn-large" Text="SaveStory" OnClick="btnSaveStory_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <br />
        <table class="nav-justified">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Heading   "></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="tbHeading" runat="server" Width="920px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <br />

        <CKEditor:CKEditorControl ID="CKStory" runat="server" Toolbar="Source
Bold|Italic|Underline|Strike|-|Subscript|Superscript 
            Cut|Copy|Paste|PasteFromWord 
            Undo|Redo
/
Styles|Format|Font|FontSize|TextColor|BGColor|-|About|Scayt" DisableNativeSpellChecker="False">
 
</CKEditor:CKEditorControl>


    </div>
    <div>
        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-large" Text="Delete Story" OnClick="btnDelete_Click" />
    </div>
</asp:Content>
