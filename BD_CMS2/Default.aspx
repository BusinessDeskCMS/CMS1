<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BD_CMS2._Default" %>

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
        
    </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
         <div id="news_panel">
        <p class="microsoft marquee"><span><%= NewsTicker%></span></p>

    </div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logo.gif" />
<h1>BusinessDesk CMS</h1>
        <p class="lead">Access the BusinessDesk CMS anywhere, anytime.</p>
        <p><a href="" class="btn btn-primary btn-large">View &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Dashboard</h2>
            <p>
                The new dashboard provides a status on all stories published, or in the review queue.           
            </p>
            
            <p>
                <a class="btn btn-default" href="Dashboard1.aspx">View &raquo;</a>
            </p>
        </div>
        <div class="col-md-6">
            <h2>Stories</h2>
            <p>
                data entry screen to create a new story.
            </p>
            <p>
                <a class="btn btn-default" href="StoryAdd4.aspx">View &raquo;</a>
            </p>
        </div>
        
    </div>

</asp:Content>
