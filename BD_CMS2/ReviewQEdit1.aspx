<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewQEdit1.aspx.cs" Inherits="BD_CMS2.ReviewQEdit1" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Review Q</title>
    <link rel="stylesheet" href="css/ui-lightness/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="css/jquery-ui-timepicker-addon.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery-ui-timepicker-addon.js"></script>

    <script>
        $(function () {
            var availableTags = [
             "agricultural technology  ",
"agriculture  ",
"agriculture aquaculture ",
"agriculture arable farming ",
"agriculture fishing industry ",
"agriculture forestry and timber ",
"agriculture livestock farming ",
"agriculture viniculture ",
"air pollution  ",
"business finance  ",
"business finance accounting and audit ",
"business finance analysts comment ",
"business finance bankruptcy ",
"business finance buyback ",
"business finance credit rating ",
"business finance dividend announcement ",
"business finance earnings ",
"business finance earnings earnings forecast",
"business finance financial statement ",
"business finance financial statement proxy filing",
"business finance financially distressed company ",
"business finance financing and stock offering ",
"business finance restructuring and recapitalisation ",
"business finance shareholder ",
"business finance stock activity ",
"business finance stock flotation ",
"chemicals  ",
"chemicals biotechnology business ",
"chemicals fertiliser ",
"chemicals health and beauty product ",
"chemicals inorganic chemical ",
"chemicals organic chemical ",
"chemicals pharmaceutical ",
"chemicals synthetic and plastic ",
"civil and public service  ",
"civil and public service public employees ",
"civil and public service public officials ",
"civil engineering  ",
"commodity market  ",
"commodity market energy market ",
"commodity market metal ",
"commodity market soft commodity ",
"computing and information technology  ",
"computing and information technology hardware ",
"computing and information technology networking ",
"computing and information technology satellite technology ",
"computing and information technology security ",
"computing and information technology semiconductors and active components ",
"computing and information technology software ",
"computing and information technology telecommunication equipment ",
"computing and information technology telecommunication service ",
"computing and information technology wireless technology ",
"construction and property  ",
"construction and property design and engineering ",
"construction and property farms ",
"construction and property heavy construction ",
"construction and property house building ",
"construction and property land price ",
"construction and property real estate ",
"construction and property renovation ",
"consumer goods  ",
"consumer goods beverage ",
"consumer goods clothing ",
"consumer goods department store ",
"consumer goods electronic commerce ",
"consumer goods food ",
"consumer goods luxury good ",
"consumer goods mail order ",
"consumer goods non-durable good ",
"consumer goods retail ",
"consumer goods speciality store ",
"consumer goods toy ",
"consumer goods wholesale ",
"corporate crime  ",
"corporate crime anti-trust crime ",
"corporate crime breach of contract ",
"corporate crime embezzlement ",
"corporate crime insider trading ",
"corporate crime restraint of trade ",
"corruption  ",
"corruption bribery ",
"court  ",
"court appeal ",
"court court administration ",
"court judge ",
"court trial ",
"court trial court preliminary",
"court trial defendant",
"court trial litigation",
"court trial witness",
"debt market  ",
"defence  ",
"diplomacy  ",
"diplomacy summit ",
"diplomacy treaty ",
"discovery and innovation  ",
"economic policy  ",
"economic policy privatisation ",
"economic policy state-owned enterprise ",
"economic sanction  ",
"ecosystem  ",
"electronics  ",
"endangered species  ",
"energy and resource  ",
"energy and resource alternative energy ",
"energy and resource coal ",
"energy and resource diesel fuel ",
"energy and resource electricity production and distribution ",
"energy and resource energy industry ",
"energy and resource kerosene/paraffin ",
"energy and resource natural gas ",
"energy and resource nuclear power ",
"energy and resource oil and gas - downstream activities ",
"energy and resource oil and gas - upstream activities ",
"energy and resource petrol ",
"energy and resource waste management and pollution control ",
"energy and resource water supply ",
"energy resources  ",
"energy saving  ",
"environmental cleanup  ",
"executive (government)  ",
"explosion accident  ",
"famine  ",
"financial and business service  ",
"financial and business service accountancy and auditing ",
"financial and business service auction service ",
"financial and business service banking ",
"financial and business service consultancy service ",
"financial and business service employment agency ",
"financial and business service funeral parlour and crematorium ",
"financial and business service healthcare provider ",
"financial and business service insurance ",
"financial and business service investment service ",
"financial and business service janitorial service ",
"financial and business service legal service ",
"financial and business service market research ",
"financial and business service market trend ",
"financial and business service personal finance ",
"financial and business service personal income ",
"financial and business service personal investing ",
"financial and business service personal service ",
"financial and business service printing/promotional service ",
"financial and business service rental service ",
"financial and business service shipping service ",
"financial and business service stock broking ",
"fire  ",
"foreign aid  ",
"foreign exchange market  ",
"fraud  ",
"global warming  ",
"government budget  ",
"government budget public finance ",
"government department  ",
"hazardous materials  ",
"heads of state  ",
"health and safety at work  ",
"higher education  ",
"higher education university ",
"homelessness  ",
"human resources  ",
"human resources layoffs and downsizing ",
"human resources management ",
"human resources stock option ",
"impeachment  ",
"industrial accident  ",
"industrial accident nuclear accident ",
"interior policy  ",
"interior policy data protection ",
"interior policy housing and urban planning ",
"interior policy indigenous people ",
"interior policy pension and welfare ",
"interior policy personal data collection ",
"international organisation  ",
"invasive species  ",
"job layoffs  ",
"land resources  ",
"land resources forests ",
"land resources mountains ",
"loan market  ",
"loan market loans ",
"lobbying  ",
"local authority  ",
"macro economics  ",
"macro economics bonds ",
"macro economics budgets and budgeting ",
"macro economics business enterprise ",
"macro economics central bank ",
"macro economics consumers ",
"macro economics consumers consumer confidence",
"macro economics consumers consumer issue",
"macro economics credit and debt ",
"macro economics currency value ",
"macro economics deflation ",
"macro economics economic growth ",
"macro economics economic indicator ",
"macro economics economic indicator gross domestic product",
"macro economics economic indicator industrial production",
"macro economics economic indicator inventories",
"macro economics economic indicator productivity",
"macro economics economic organisation ",
"macro economics emerging market ",
"macro economics employment statistics ",
"macro economics exports ",
"macro economics government aid ",
"macro economics government debt ",
"macro economics imports ",
"macro economics inflation ",
"macro economics interest rate ",
"macro economics international economic institution ",
"macro economics international trade ",
"macro economics international trade trade agreements",
"macro economics international trade trade balance",
"macro economics international trade trade dispute",
"macro economics international trade trade policy",
"macro economics investments ",
"macro economics money and monetary policy ",
"macro economics mortgage ",
"macro economics mutual funds ",
"macro economics prices ",
"macro economics recession ",
"macro economics tariff ",
"manufacturing and engineering  ",
"manufacturing and engineering aerospace ",
"manufacturing and engineering automotive equipment ",
"manufacturing and engineering defence equipment ",
"manufacturing and engineering electrical appliance ",
"manufacturing and engineering heavy engineering ",
"manufacturing and engineering industrial component ",
"manufacturing and engineering instrument engineering ",
"manufacturing and engineering machine manufacturing ",
"manufacturing and engineering shipbuilding ",
"media  ",
"media advertising ",
"media book industry ",
"media cinema industry ",
"media music industry ",
"media news agency ",
"media newspaper and magazine ",
"media online ",
"media public relations ",
"media radio industry ",
"media satellite and cable service ",
"media television industry ",
"medical research  ",
"metal and mineral  ",
"metal and mineral building material ",
"metal and mineral gold and precious material ",
"metal and mineral iron and steel ",
"metal and mineral mining ",
"metal and mineral non ferrous metal ",
"migration  ",
"ministers (government)  ",
"national elections  ",
"national government  ",
"natural disasters  ",
"natural disasters drought ",
"natural disasters earthquake ",
"natural disasters flood ",
"natural disasters landslide ",
"natural disasters landslide avalanche",
"natural disasters meteorological disaster ",
"natural disasters meteorological disaster windstorms",
"natural disasters volcanic eruption ",
"news media  ",
"newspaper  ",
"online media  ",
"out of court procedures  ",
"parks  ",
"parliament  ",
"parliament lower house ",
"parties and movements  ",
"pension  ",
"periodical  ",
"political campaigns  ",
"political campaigns campaign finance ",
"political candidates  ",
"political development  ",
"political system  ",
"political system democracy ",
"political system dictatorship ",
"population growth  ",
"poverty  ",
"process industry  ",
"process industry distiller and brewer ",
"process industry food industry ",
"process industry furnishings and furniture ",
"process industry paper and packaging product ",
"process industry rubber product ",
"process industry soft drinks ",
"process industry textile and clothing ",
"process industry tobacco ",
"radio  ",
"regional authority  ",
"regulatory policy and organisation  ",
"regulatory policy and organisation food and drink regulations ",
"renewable energy  ",
"scientific exploration  ",
"scientific exploration space programme ",
"scientific paper  ",
"securities  ",
"securities derivative securities ",
"stocks  ",
"strategy and marketing  ",
"strategy and marketing annual and special corporate meeting ",
"strategy and marketing annual report ",
"strategy and marketing board of directors ",
"strategy and marketing commercial contract ",
"strategy and marketing company spin-off ",
"strategy and marketing globalisation ",
"strategy and marketing governance ",
"strategy and marketing joint venture ",
"strategy and marketing leveraged buyout ",
"strategy and marketing licensing agreement ",
"strategy and marketing management buyout ",
"strategy and marketing merger or acquisition ",
"strategy and marketing new product or service ",
"strategy and marketing patent, copyright and trademark ",
"strategy and marketing product recall ",
"strategy and marketing research and development ",
"structural failures  ",
"taxation  ",
"television  ",
"tourism and leisure  ",
"tourism and leisure casino and gambling ",
"tourism and leisure hotel and accommodation ",
"tourism and leisure recreational and sporting goods ",
"tourism and leisure restaurant and catering ",
"tourism and leisure tour operator ",
"transport  ",
"transport accident  ",
"transport accident air and space accident ",
"transport accident maritime accident ",
"transport accident railway accident ",
"transport accident road accident ",
"transport air transport ",
"transport commuting ",
"transport incident  ",
"transport incident air and space incident ",
"transport incident maritime incident ",
"transport incident railway incident ",
"transport incident road incident ",
"transport railway transport ",
"transport road transport ",
"transport traffic ",
"transport waterway and maritime transport ",
"unemployment benefits  ",
"waste  ",
"water  ",
"water oceans ",
"water pollution  ",
"water rivers ",
"water wetlands ",
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
            $('#tbHoldToDate').datetimepicker({ dateFormat: "dd-M-yy" });
        });
    </script>
    <script type="text/javascript" src="Scripts/ckeditor/ckeditor.js"></script>
    <script>
        $(function () {
            $("#progressbar").progressbar({
                value: false
            });
            $("btnSaveStory").on("click", function (event) {
                var target = $(event.target),
                  progressbar = $("#progressbar"),
                  progressbarValue = progressbar.find(".ui-progressbar-value");
                if (target.is("#numButton")) {
                    progressbar.progressbar("option", {
                        value: Math.floor(Math.random() * 100)
                    });
                } else if (target.is("#colorButton")) {
                    progressbarValue.css({
                        "background": '#' + Math.floor(Math.random() * 16777215).toString(16)
                    });
                } else if (target.is("#btnSaveStory")) {
                    progressbar.progressbar("option", "value", false);
                }
            });
        });
    </script>
    <style>
        #progressbar .ui-progressbar-value {
            background-color: #ccc;
        }
    </style>

    <style type="text/css">
        .auto-style1 {
            height: 50px;
        }

        .auto-style2 {
            height: 50px;
            width: 246px;
        }

        .auto-style3 {
            width: 246px;
        }

        .auto-style4 {
            height: 50px;
            width: 319px;
        }

        .auto-style5 {
            width: 319px;
        }

        .auto-style6 {
            height: 50px;
            width: 193px;
        }

        .auto-style7 {
            width: 193px;
        }
        .auto-style8 {
            width: 81px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2><%: Title %>.</h2>
            <h3>Review a Story from the Review Queue.</h3>
            <asp:Label ID="lbStoryID" runat="server" Visible="false" Text=""></asp:Label>
            <br />
            <div class="ui-widget">
                <table class="nav-justified">
                    <tr>
                        <td class="auto-style6">Type:&nbsp;
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem Selected="True">News</asp:ListItem>
                        <asp:ListItem>Correction</asp:ListItem>
                        <asp:ListItem>Update</asp:ListItem>
                        <asp:ListItem>Kill</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                        </td>
                        <td class="auto-style4">Location:&nbsp;
                    <asp:DropDownList ID="ddlLocation" runat="server">
                        <asp:ListItem Selected="True">Wellington</asp:ListItem>
                        <asp:ListItem>Auckland</asp:ListItem>
                        <asp:ListItem>Christchurch</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                        </td>
                        <td class="auto-style2">NZX:&nbsp;   
                            <asp:DropDownList ID="ddlTicker" runat="server" DataSourceID="SqlDataSource2" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Ticker" Height="27px" Width="184px">
                             <asp:ListItem Value="0" Text=" "></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                        <td class="auto-style1">
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-large" Text="Cancel" OnClick="btnCancel_Click" /></td>
                    </tr>
                    <tr>
                        <td class="auto-style7">Publish Hold?&nbsp;<asp:DropDownList ID="ddlHold" runat="server">
                            <asp:ListItem Selected="True">No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td class="auto-style5">Hold<br />
                            &nbsp;Date/Time
                            <asp:TextBox ID="tbHoldToDate" runat="server" TextMode="DateTimeLocal"></asp:TextBox>

                        </td>
                        <td class="auto-style3">ASX:&nbsp; 
                            <asp:DropDownList ID="ddlASX" runat="server" DataSourceID="SqlDataSource1" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Ticker" Height="34px" Width="188px">
                            <asp:ListItem Value="0" Text=" "></asp:ListItem>
                                 </asp:DropDownList>
                        </td>
                        <td>Send to Publish?&nbsp;
                    <asp:DropDownList ID="ddlReviewAction" runat="server">
                        <asp:ListItem>Publish</asp:ListItem>
                        <asp:ListItem>Draft</asp:ListItem>
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList></td>
                        <td>
                            <div id="progressbar"></div>
                            <asp:Button ID="btnSaveStory" runat="server" CssClass="btn btn-primary btn-large" Text="SaveStory" OnClick="btnSaveStory_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div class="ui-widget">
                    <label>Taxonomy: </label>
                    <asp:TextBox ID="tags" runat="server" Font-Size="X-Small" Width="897px" Height="29px"></asp:TextBox>
                </div>
                <div class="ui-widget">
                    <table class="nav-justified">
                        <tr>
                            <td class="auto-style8">
                                <asp:Label ID="Label2" runat="server" Text="Heading   "></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="tbHeading" runat="server" Width="895px" Height="27px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <CKEditor:CKEditorControl ID="CKStory" runat="server" Height="500px" Toolbar="Source
Bold|Italic|Underline|Strike|-|Subscript|Superscript 
            Cut|Copy|Paste|PasteFromWord 
            Undo|Redo
/
Styles|Format|Font|FontSize|TextColor|BGColor|-|About|Scayt"
                    DisableNativeSpellChecker="False">
 
                </CKEditor:CKEditorControl>
            </div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Name], [Ticker] FROM [ASX] ORDER BY [Ticker]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:BDCMSConnectionString %>" SelectCommand="SELECT [Ticker], [Name] FROM [ANZSX] ORDER BY [Ticker]"></asp:SqlDataSource>

    </form>
</body>
</html>
