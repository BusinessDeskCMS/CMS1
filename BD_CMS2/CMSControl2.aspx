<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CMSControl2.aspx.cs" Inherits="BD_CMS2.CMSControl2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="css/ui-lightness/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="css/multiple-select.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery.multiple.select.js"></script>
    <script>
        $(function () {
            $('#selMessaging').change(function () {
                console.log($(this).val());
            }).multipleSelect({
                width: '100%',
                placeholder: "Messaging?",
                selectAll: false,
                single: true
            });
        });
    </script>
    <script>
        $(function () {
            $('#selEmail').change(function () {
                console.log($(this).val());
            }).multipleSelect({
                width: '100%',
                placeholder: "Email?",
                selectAll: false,
                single: true
            });
        });
    </script>
    <script>
        $(function () {
            $('#selPublic').change(function () {
                console.log($(this).val());
            }).multipleSelect({
                width: '100%',
                placeholder: "Public?",
                selectAll: false,
                single: true
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div>
                <h3>System Control</h3>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-2 text-left">
                            <asp:Label ID="Label1" runat="server" Text="Messaging"></asp:Label>
                        </div>
                        <div class="col-md-2 text-left">
                            <select id="selMessaging" runat="server">
                                <option value="1" selected="selected">Yes</option>
                                <option value="2">No</option>
                            </select>
                        </div>
                        <div class="col-md-2 text-left">
                            <asp:Label ID="Label2" runat="server" Text="Email Active?"></asp:Label>
                        </div>
                        <div class="col-md-2 text-left">
                            <select id="selEmail" runat="server">
                                <option value="1" selected="selected">Yes</option>
                                <option value="2">No</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-2 text-left">
                            <asp:Label ID="Label3" runat="server" Text="Public Site"></asp:Label>
                        </div>
                        <div class="col-md-2 text-left">
                            <select id="selPublic" runat="server">
                                <option value="1" selected="selected">Yes</option>
                                <option value="2">No</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <asp:TextBox ID="tbCalendar" runat="server" TextMode="MultiLine" Height="150px" Width="963px"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-2 text-left">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-large" OnClick="btnUpdate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
