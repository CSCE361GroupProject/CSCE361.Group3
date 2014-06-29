﻿<%@ Page Title="Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="HomeProfile.aspx.vb" Inherits="CSCE361.Group3.WebPages.HomeProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyD2rcIH-iLZGgm8xneO7lNqPq5HlBYjbd4&sensor=false" >
    </script>

    <!--Center of campus lat/lng: 40.82011, -96.700759 -->
    <!--Center of avery lat/lng: 40.819452, -96.704503 -->
<%--    <script type="text/javascript">
        function InitializeMap() {
            var myLatLng = new google.maps.LatLng(40.82011, -96.700759);
            var mapOptions = {
                zoom: 16,
                center: myLatLng,
                mapTypeId: google.maps.MapTypeId.HYBRID
            };
            var map = new google.maps.Map(document.getElementById("googlemap"), mapOptions);

            var marker = new google.maps.Marker({
                position: myLatLng,
                map: map,
                title: "Hello world!",
                animation: google.maps.Animation.DROP
            });

            var myLatLng1 = new google.maps.LatLng(40.819452, -96.704503);
            var marker = new google.maps.Marker({
                position: myLatLng1,
                map: map,
                title: "Hello world!",
                animation: google.maps.Animation.DROP
            });

            var contentString = '<div id="content">' +
              '<div id="siteNotice">' +
              '</div>' +
              '<h1 id="firstHeading" class="firstHeading">Uluru</h1>' +
              '<div id="bodyContent">' +
              '<p><b>Uluru</b>, also referred to as <b>Ayers Rock</b>, is a large ' +
              'Heritage Site.</p>' +
              '<p>Attribution: Uluru, <a href="http://en.wikipedia.org/w/index.php?title=Uluru&oldid=297882194">' +
              'http://en.wikipedia.org/w/index.php?title=Uluru</a> ' +
              '(last visited June 22, 2009).</p>' +
              '</div>' +
              '</div>';

            var infowindow = new google.maps.InfoWindow({
                content: contentString
            });

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });

            google.maps.event.addDomListener(window, 'load', initialize);

        }
        window.onload = InitializeMap
    </script>  --%>  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="profilemenu">
    <center>

    <div id="profileinfo"> 
        <asp:Image ID="imgProfilePic" runat="server" CssClass="img2" /> <br />
        <asp:Label ID="lblName" runat="server" Text="FirstName LastName"/> <br /> <br />
        <asp:Label ID="lblAge" runat="server" Text="Age years old"/>
    </div>
    <br /> <br />  Menu: <br /> <br />
       
    <div>
        <asp:Label ID="lblCaption" runat="server" Text="Caption:"></asp:Label>
        <asp:TextBox ID="tbCaption" runat="server"></asp:TextBox> <br />
        <asp:FileUpload ID="fuPhoto" runat="server" /> <br /> <br />

        <asp:Button ID="btnUpload" runat="server" Text="Upload photo" />  <br />  
        
        <asp:Label ID="lblSuccess" runat="server"></asp:Label>

        <br /> <br /> <br />
        <asp:Button ID="btnViewAllPhotos" runat="server" Text="View all photos" /> <br />
        <asp:Button ID="btnViewMyPhotos" runat="server" Text="View my photos" /> <br /> 
        <asp:Button ID="btnViewCommentPhotos" runat="server" Text="View photo's I've commented on" /> <br />
    </div>

    </center>
</div>


<div id="googlemap" style="width:740px; height:620px" onload="initialize()">
    <center>Google Map's Div Placeholder</center>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</div>


<div id="search" >
    <br /> <br />
    <center>Filter Photos: <br /> <br />
        <asp:Label ID="lblSelectUser" runat="server" Text="Select User:"></asp:Label> 
        <asp:DropDownList ID="ddlUsers" runat="server"> </asp:DropDownList> <br />
        <asp:Button ID="btnByPhoto" runat="server" Text="View photo's uploaded by selected user" /> <br />
        <asp:Button ID="btnByComment" runat="server" Text="View photo's commented on by selected user" /> <br /> <br />

        <asp:Label ID="lblDistance" runat="server" Text="Distance in feet:"></asp:Label>
        <asp:TextBox ID="tbDistance" runat="server"></asp:TextBox> <br />
        <asp:Button ID="btnByDistance" runat="server" Text="View comments within selected distance" />

    
        <br /> <br /> <br />
        <asp:Label ID="hiddenfield" runat="server" Visible="false"></asp:Label>
    
    </center>
</div>

<div id="comments" style="background-color:#99CC00">
    <center>Comment's and Selected Photo Div Placeholder</center>
</div>

</asp:Content>
