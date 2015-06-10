<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewQEdit4.aspx.cs" Inherits="BD_CMS2.ReviewQEdit4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Review Q</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="css/ui-lightness/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="css/jquery-ui-timepicker-addon.css" />
    <link rel="stylesheet" href="css/multiple-select.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <script src="Scripts/jquery.multiple.select.js"></script>

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
            $('#tbHoldToDate').datetimepicker({ dateFormat: "dd-M-yy" });

        });
    </script>
    <script>
        $(function () {
            $('#ms').change(function () {
                console.log($(this).val());
            }).multipleSelect({
                width: '100%',
                placeholder: "Select Customers",
                selectAll: false
            });
        });
    </script>

    <script>
        $(function () {
            $('#ddlHold1').change(function () {
                console.log($(this).val());
            }).multipleSelect({
                width: '100%',
                placeholder: "Hold?",
                selectAll: false,
                single: true
            });
        });
    </script>
    <script>
        $(function () {
            $('#ddlType1').change(function () {
                console.log($(this).val());
            }).multipleSelect({
                width: '100%',
                placeholder: "Type",
                selectAll: false,
                single: true
            });
        });
    </script>
    <script>
        $(function () {
            $('#ddlLocation1').change(function () {
                console.log($(this).val());
            }).multipleSelect({
                width: '100%',
                placeholder: "Location",
                selectAll: false,
                single: true
            });
        });
    </script>
    <script>
        $(function () {
            $('#selNZX').change(function () {
            }).multipleSelect({
                width: '100%',
                placeholder: "Select NZX",
                selectAll: false
            });
        });
    </script>
    <script>
        $(function () {
            $('#selASX').change(function () {
            }).multipleSelect({
                width: '100%',
                placeholder: "Select ASX",
                selectAll: false
            });
        });
    </script>
    
    <script>
        $(function () {
            $("#<%=btnPublishStory.ClientID%>").click(function () {
                $("#wlListValue").val($("#ms").multipleSelect("getSelects"));
                $("#wlNZX").val($("#selNZX").multipleSelect("getSelects"));
                $("#wlASX").val($("#selASX").multipleSelect("getSelects"));
            });
        });
    </script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $(document).on("submit", 'form', function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript" src="Scripts/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="wlListValue" name="wlListValue" />
        <input type="hidden" id="wlNZX" name="wlNZX" />
        <input type="hidden" id="wlASX" name="wlASX" />      
        <asp:Label ID="lbStoryID" runat="server" Visible="false" Text=""></asp:Label>
        <div class="container">
            <div>
                <h3>Review a Story from the Review Queue</h3>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-2 text-left">
                            <select id="ddlType1" runat="server">
                                <option value="News" selected="selected">News</option>
                                <option value="Correction">Correction</option>
                                <option value="Update">Update</option>
                                <option value="Kill">Kill</option>
                            </select>
                        </div>
                        <div class="col-md-2 text-left">
                            <select id="ddlLocation1" runat="server">
                                <option value="Wellington" selected="selected">Wellington</option>
                                <option value="Auckland">Auckland</option>
                                <option value="Christchurch">Christchurch</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <div class="col-md-1  text-left">
                                <asp:Label ID="Label8" runat="server" Text="NZX:"></asp:Label>
                            </div>
                            <div class="col-md-3 text-left">
                                <select id="selNZX" multiple="true" runat="server">
                                </select>
                            </div>
                        </div>
                        <div class="col-md-2 text-right">
                            <asp:Button ID="btnSaveDraft" runat="server" CssClass="btn btn-info btn-large" Text="Save Draft" OnClick="btnSaveDraft_Click" />
                        </div>
                        <div class="col-md-1 text-right">
                            <asp:Button ID="btnPublishStory" runat="server" CssClass="btn btn-success btn-large" Text="Publish" OnClick="btnPublishStory_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-2 text-left">
                            <select id="ddlHold1" runat="server">
                                <option value="No">No</option>
                                <option value="Yes">Yes</option>
                            </select>
                        </div>

                        <div class="col-md-2 text-right">
                            <asp:TextBox ID="tbHoldToDate" runat="server" placeholder="Hold To:" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                        </div>

                        <div class="col-md-1 text-left">
                            <asp:Label ID="Label2" runat="server" Text="ASX:"></asp:Label>
                        </div>
                        <div class="col-md-3 text-left">
                            <select id="selASX" multiple="true" runat="server">
                            </select>
                        </div>
                        <div class="col-md-3 text-right">
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning btn-large" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-3">
                            <select id="ms" multiple="True" runat="server">
                                <option value="1">AAP</option>
                                <option value="2">APN</option>
                                <option value="4">NBR</option>
                                <option value="11">Sentia</option>
                                <option value="10">Yahoo</option>
                                <option value="13">TechDay</option>
                                <option value="5">ShareChat</option>
                                <option value="9">BusinessDesk</option>
                            </select>
                        </div>
                        <div class="col-md-4 text-right">
                            <%--<div class="bg-primary">
                                <input class="form-control" type="text" id="lbLockStatus2" runat="server" readonly />
                            </div>--%>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="tbHidden" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="tbHiddenHold" Visible="false" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row-height">

                        <div class="col-md-12 text-left">
                            <asp:TextBox ID="tags" runat="server" Font-Size="X-Small" placeholder="Taxonomy" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="row-height">
                        <div class="col-md-12 text-left">
                            <asp:TextBox ID="tbHeading" runat="server" CssClass="form-control" Font-Bold="True" BackColor="#FFFFCC" Font-Size="Larger"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="loading" align="center">
                        Processing Story. Please wait.
                        <br />
                        <br />
                        <img src="Images/loader.gif" alt="" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Name], [Ticker] FROM [ASX] ORDER BY [Ticker]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Ticker], [Name] FROM [ANZSX] ORDER BY [Ticker]"></asp:SqlDataSource>

    </form>
</body>
</html>
