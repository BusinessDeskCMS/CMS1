<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StoryAddCK.aspx.cs" Inherits="BD_CMS2.StoryAddCK" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
 <script type="text/javascript" src="Scripts/ckeditor/ckeditor.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<CKEditor:CKEditorControl ID="CKEditor1" runat="server" BackColor="#99FF33" Toolbar="Source
Bold|Italic|Underline|Strike|-|Subscript|Superscript
NumberedList|BulletedList|-|Outdent|Indent
/
Styles|Format|Font|FontSize|TextColor|BGColor|-|About|Scayt" DisableNativeSpellChecker="False">
 
</CKEditor:CKEditorControl>
<%--Button that executes the command to store updated data into database--%>
<asp:Button ID="SaveButton" runat="server" Text="Save Changes" 
    onclick="SaveButton_Click" />

    <asp:Label ID="lbText" runat="server" Text="Label"></asp:Label>
</asp:Content>
