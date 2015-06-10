<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewsScroller.aspx.cs" Inherits="BD_CMS2.NewsScroller" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .marquee {
            width: 850px;
            margin: 0 auto;
            white-space: nowrap;
            overflow: hidden;
            box-sizing: border-box;
            color:#ff6a00;
        }

            .marquee span {
                display: inline-block;
                padding-left: 100%; /* show the marquee just outside the paragraph */
                animation: marquee 120s linear infinite;
            }

                .marquee span:hover {
                    animation-play-state: paused;
                }

        /* Make it move */
        @keyframes marquee {
            0% {
                transform: translate(0, 0);
            }

            100% {
                transform: translate(-100%, 0);
            }
        }

        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="news_pannel">
        <p class="microsoft marquee"><span><%= NewsTicker%></span></p>

    </div>

</asp:Content>
