<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_FTP.aspx.cs" Inherits="BD_CMS2.Test_FTP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Message ID :<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="SFTP Test" />
        <br />
        <br />
        Error:
        <asp:TextBox ID="TextBox2" runat="server" Height="184px" TextMode="MultiLine" Width="735px"></asp:TextBox>
    </form>
</body>
</html>
