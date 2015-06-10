<%@ Page Title="Add Story" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StoryAdd1.aspx.cs" Inherits="BD_CMS2.StoryAdd1" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">  
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>

    <script>
        $(function () {
            var availableTags = [
             "Business Economy - Economy",
        "Business Markets - Derivatives",
        "Business Markets - NZX",
        "Business Economy - International Trade",
        "Business Economy - Budget (NZ)",
        "Business Economy - Business Confidence ",
        "Business Economy - commerce",
        "Business Economy - Commodities",
        "Business Economy - economic policy",
        "Business Economy - Economy",
        "Business Economy - Employment",
        "Business Economy - fiscal policy",
        "Business Economy - International Trade",
        "Business Economy - Monetary Policy",
        "Business Economy - Regulatory Framework",
        "Business Economy - Reserve Bank",
        "Business Economy - Trade Agreements",
        "Business Markets - banking and finance",
        "Business Markets - Company Results",
        "Business Markets - Data and Indices",
        "Business Markets - Derivatives",
        "Business Markets - Forex Currences (and) Currency",
        "Business Markets - Interest Rates Bonds",
        "Business Markets - NZX",
        "Education - general education",
        "Education - tertiary education",
        "Energy - alternative technology",
        "Energy - coal",
        "Energy - Energy Policy",
        "Energy - Gas",
        "Energy - Industry",
        "Energy - network",
        "Energy - oil",
        "General - Disasters and Natural Events",
        "General - Environment and conservation",
        "General- environment and conservation",
        "Industries - Employers",
        "Industries - healthcare industries",
        "Industries - insurance",
        "Industries - manufacturing",
        "Industries - retail",
        "Industries - service industries",
        "Industries -service industries",
        "Industry - advertising",
        "Industry - Advertoising",
        "Industry - construction",
        "Industry - Defence Industry",
        "Industry - Employers",
        "Industry - gaming",
        "Industry - healthcare industry",
        "Industry - Housing",
        "Industry - liquor",
        "Industry - Manfuactruing",
        "Industry-  manufacturing",
        "Industry - manufacturing",
        "Industry - Media",
        "Industry - mining",
        "Industry - Print Media",
        "Industry - Real Estate",
        "Industry - retail",
        "Industry - service industries",
        "Industry - State owned enterprises",
        "Industry - state sector",
        "Industry - Telecommunications",
        "Industry - tourism",
        "Industry - Transport",
        "Industry - Unions",
        "Industry -construction",
        "Industry- Media",
        "Industry-Media",
        "Legal Issues - civil law",
        "Legal Issues - Justice System",
        "Legal Issues - Police and Crime",
        "Politics - Elections",
        "Politics - General Politics",
        "Politics - local government",
        "Primary Industries - Ag Sheep and Beef",
        "Primary Industries - Agriculture",
        "Primary Industries - Fisheries",
        "Primary Industries - forestry",
        "Primary Industries - Horticulture",
        "Primary Industries - Viticulture",
        "Science and Tech - broadband network",
        "Science and Technology - computing and internet",
        "Science and Technology - Innovation",
        "Science and Technology - Science",
        "Science and Technology - Technology",
        "Science and Technology -computing and internet",
        "Social issues - climate change",
        "Social issues - housing issues",
        "Social Issues - Immigration",
        "Social issues - Maori",
        "Social issues - privacy",
        "Social issues - retirement",
        "Social Issues - Welfare",
        "World - Development and Aid",
        "World - Finance",
        "World - Food Security",
        "World - Foreign Affairs",
            ];
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
    <style type="text/css">
        .auto-style1 {
            width: 130px;
            margin: 10px;
        }

        .auto-style2 {
            width: 71px;
        }

        .auto-style4 {
            width: 170px;
            margin: 10px;
        }

        .auto-style9 {
            width: 93px;
        }

        .table-meta {
            width: 50%;
            vertical-align: top;
            margin-right: 20px;
        }

        .auto-style11 {
            width: 101px;
            margin-right: 15px;
        }

        .auto-style13 {
            width: 232px;
        }

        .auto-style14 {
            width: 334px;
        }

        .auto-style15 {
            width: 302px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Add a New Story to the Daily Story Queue.</h3>
    <br />
    <div>
        <table class="nav-justified">
            <tr class="table-meta">
                <td class="auto-style2">
                    <asp:Label ID="lbType" runat="server" Text="Type"></asp:Label>
                    &nbsp;
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlType" runat="server" Height="20px" Width="142px">
                        <asp:ListItem Selected="True">News</asp:ListItem>
                        <asp:ListItem>Correction</asp:ListItem>
                        <asp:ListItem>Update</asp:ListItem>
                        <asp:ListItem>Kill</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbLocation" runat="server" Text="Location"></asp:Label>
                    &nbsp;
                </td>
                <td class="auto-style4">
                    <asp:DropDownList ID="ddlLocation" runat="server">
                        <asp:ListItem Selected="True">Wellington</asp:ListItem>
                        <asp:ListItem>Auckland</asp:ListItem>
                        <asp:ListItem>Christchurch</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lbTicker" runat="server" Text="NZX"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlTicker" runat="server" Height="18px" Width="120px" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Ticker">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Ticker], [Name] FROM [ANZSX] ORDER BY [Ticker]"></asp:SqlDataSource>
                </td>
                <td class="auto-style13">
                    <asp:Label ID="lbTopic" runat="server" Text="Topic"></asp:Label>
                </td>
                <td class="auto-style14">
                    <div class="ui-widget">
                        <label for="tags">Taxonomy: </label>                       
                        <asp:TextBox ID="tags" runat="server" Font-Size="X-Small" Width="632px"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger btn-large" Text="Cancel" OnClick="btnCancel_Click" Width="89px" /></td>
            </tr>
            <tr class="table-meta">
                <td class="auto-style2">
                    <asp:Label ID="lbHold" runat="server" Text="Hold?"></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlHold" runat="server">
                        <asp:ListItem Selected="True">No</asp:ListItem>
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbHoldTo" runat="server" Text="Hold To:"></asp:Label>

                </td>
                <td class="auto-style4">
                    <asp:TextBox ID="bHoldTo" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="lbASX" runat="server" Text="ASX"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlASX" runat="server" Height="18px" Width="120px" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Ticker">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Name], [Ticker] FROM [ASX] ORDER BY [Ticker]"></asp:SqlDataSource>
                </td>
                <td class="auto-style13">
                    <asp:Label ID="lbSendToReview" runat="server" Text="Send To Review?"></asp:Label>
                </td>
                <td class="auto-style14">
                    <asp:DropDownList ID="ddlSendToReview" runat="server">
                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSaveStory" runat="server" CssClass="btn btn-primary btn-large" Text="SaveStory" OnClick="btnSaveStory_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div>

        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT DISTINCT [scoop] FROM [Taxo] ORDER BY [scoop]"></asp:SqlDataSource>
        <br />
        <table class="nav-justified">
            <tr class="table-meta">
                <td class="auto-style15">
                    <asp:Label ID="lbHeading" runat="server" Text="Heading   "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbHeading" runat="server" Width="450px"></asp:TextBox>
                </td>
            </tr>
        </table>

        <CKEditor:CKEditorControl ID="CKEditor1" runat="server" Height="500px" Toolbar="Source
Bold|Italic|Underline|Strike|-|Subscript|Superscript 
            Cut|Copy|Paste|PasteFromWord 
            Undo|Redo
/
Styles|Format|Font|FontSize|TextColor|BGColor|-|About|Scayt"
            DisableNativeSpellChecker="False">
 
        </CKEditor:CKEditorControl>




    </div>
</asp:Content>
