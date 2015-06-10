<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyQEdit4.aspx.cs" Inherits="BD_CMS2.DailyQEdit4" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Q</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="css/ui-lightness/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="css/jquery-ui-timepicker-addon.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 150px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

    <script>
        $(function () {
            var stringData = $.ajax({
                url: "/taxo.txt",
                async: false
            }).responseText;
            //Split values of string data
            var availableTags = stringData.split(",");
            function split(val) {
                return val.split(/,\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }
            $("#tags")
              // don't navigate away from the field on tab when selecting an item
              .bind("keydown", function (event) {
                  if (event.keyCode === $.ui.keyCode.TAB &&
                      $(this).data("ui-autocomplete").menu.active) {
                      event.preventDefault();
                  }
              })
              .autocomplete({
                  minLength: 0,
                  source: function (request, response) {
                      // delegate back to autocomplete, but extract the last term
                      response($.ui.autocomplete.filter(
                        availableTags, extractLast(request.term)));
                  },
                  focus: function () {
                      // prevent value inserted on focus
                      return false;
                  },
                  select: function (event, ui) {
                      var terms = split(this.value);
                      // remove the current input
                      terms.pop();
                      // add the selected item
                      terms.push(ui.item.value)
                      // add placeholder to get the comma-and-space at the end
                      terms.push("");
                      this.value = terms.join(", ");
                      return false;
                  }
              });
        });
    </script>
    <script type="text/javascript" src="Scripts/ckeditor/ckeditor.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div>
                <h2><%: Title %>.</h2>
                <h3>Edit a Story from the Daily Queue.</h3>
                <asp:Label ID="lbStoryID" runat="server" Visible="false" Text=""></asp:Label>
                <br />
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-3">
                            <asp:Label ID="lbType" runat="server" Text="Type: "></asp:Label>
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Selected="True">News</asp:ListItem>
                                <asp:ListItem>Correction</asp:ListItem>
                                <asp:ListItem>Update</asp:ListItem>
                                <asp:ListItem>Kill</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label1" runat="server" Text="Location: "></asp:Label>
                            <asp:DropDownList ID="ddlLocation" runat="server">
                                <asp:ListItem Selected="True">Wellington</asp:ListItem>
                                <asp:ListItem>Auckland</asp:ListItem>
                                <asp:ListItem>Christchurch</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label2" runat="server" Text="NZX: "></asp:Label>
                            <asp:DropDownList ID="ddlTicker" runat="server" DataSourceID="SqlDataSource2" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Ticker" Height="27px" Width="184px">
                                <asp:ListItem Value="0" Text=" "></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">                           
                            <asp:Button ID="btnReviewStory" runat="server" CssClass="btn btn-success btn-large" Text="Send to Review" OnClick="btnReviewStory_Click" />
                            <asp:Button ID="btnSaveDraft" runat="server" CssClass="btn btn-info btn-large" Text="Save Draft" OnClick="btnSaveDraft_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-3">
                            <asp:Label ID="Label3" runat="server" Text="Publish Hold?"></asp:Label>
                            <asp:DropDownList ID="ddlHold" runat="server">
                                <asp:ListItem Selected="True">No</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label4" runat="server" Text="Hold To: "></asp:Label>
                            <asp:TextBox ID="tbHoldToDate" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label5" runat="server" Text="ASX: "></asp:Label>
                            <asp:DropDownList ID="ddlASX" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Ticker" Width="188px">
                                <asp:ListItem Value="0" Text=" "></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning btn-large" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-12">
                            <asp:Label ID="Label7" runat="server" Text="Taxonomy: "></asp:Label>
                            <asp:TextBox ID="tags" runat="server" Font-Size="X-Small" Width="897px" Height="29px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="loading" align="center">
                    Processing Story. Please wait.<br />
                    <br />
                    <img src="Images/loader.gif" alt="" />
                </div>
                <div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label8" runat="server" Text="Headline:  "></asp:Label>
                            <asp:TextBox ID="tbHeading" runat="server" Width="897px" Height="29px"></asp:TextBox>
                        </div>
                    </div>
                   <div>
                    <textarea name="editor1" id="editor1"><%= StoryValue%></textarea>
                    <script>
                        CKEDITOR.replace('editor1');
                    </script>
                </div>
                </div>
                
            </div>
            <div>
                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-large" Text="Delete Story" OnClick="btnDelete_Click" />
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Name], [Ticker] FROM [ASX] ORDER BY [Ticker]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Ticker], [Name] FROM [ANZSX] ORDER BY [Ticker]"></asp:SqlDataSource>

        </div>
    </form>
</body>
</html>
