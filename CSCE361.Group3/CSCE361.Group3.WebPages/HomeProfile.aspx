<%@ Page Title="Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="HomeProfile.aspx.vb" Inherits="CSCE361.Group3.WebPages.HomeProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyD2rcIH-iLZGgm8xneO7lNqPq5HlBYjbd4&sensor=false" >
    </script>

    <!--Center of campus lat/lng: 40.82011, -96.700759 -->
    <!--Center of avery lat/lng: 40.819452, -96.704503 -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="profilemenu">
    <center>

    <div id="profileinfo"> 
        <asp:Image ID="imgProfilePic" runat="server" CssClass="img2" /> <br />
        <asp:Label ID="lblName" runat="server" Text="FirstName LastName"/> <br /> <br />
        <asp:Label ID="lblAge" runat="server" Text="Age years old"/>
    </div>
    <br />  Menu: <br /> <br />
       
    <div>
        <asp:Label ID="lblCaption" runat="server" Text="Caption:"></asp:Label>
        <asp:TextBox ID="tbCaption" runat="server"></asp:TextBox> <br />
        <asp:FileUpload ID="fuPhoto" runat="server" /> <br /> <br />

        <asp:Button ID="btnUpload" runat="server" Text="Upload photo" />  <br />  
        
        <asp:Label ID="lblSuccess" runat="server"></asp:Label>

        <br /> <br />
        <asp:Button ID="btnViewAllPhotos" runat="server" Text="View all photos" /> <br />
        <asp:Button ID="btnViewMyPhotos" runat="server" Text="View my photos" /> <br /> 
        <asp:Button ID="btnViewCommentPhotos" runat="server" Text="View photo's I've commented on" /> <br />
    </div>

    </center>

    <div id="search" >
    <br /> <br />
    <center>Filter Photos: <br /> <br />
        <asp:Label ID="lblSelectUser" runat="server" Text="Select User:"></asp:Label> 
        <asp:DropDownList ID="ddlUsers" runat="server"> </asp:DropDownList> <br />
        <asp:Button ID="btnByPhoto" runat="server" Text="View photo's uploaded by selected user" /> <br />
        <asp:Button ID="btnByComment" runat="server" Text="View photo's commented on by selected user" /> <br /> <br />

        <asp:Label ID="selectedPoint" runat="server" Text="Selected Point: "/> <asp:Textbox ID="tbSelectedPointLatLng" runat="server" onfocus="this.blur()" /> <br />
        <asp:Label ID="lblDistance" runat="server" Text="Distance in feet:"></asp:Label>
        <asp:TextBox ID="tbDistance" runat="server"></asp:TextBox> <br />
        <asp:Button ID="btnByDistance" runat="server" Text="View photos within selected distance" />

    
        <br /> <br /> <br />
        <asp:Label ID="hiddenfield" runat="server" Visible="false"></asp:Label>
    </center>
</div>

</div>

<div id="googlemap" style="width:740px; height:620px" onload="initialize()">
    <center>Google Map's Div Placeholder</center>
    <asp:Literal ID="literal1" runat="server"></asp:Literal>
</div>




<div id="comments" style="background-color:#99CC00; width:1150px">
    <div id="image" style="height:325px; width:575px; background-color:Navy; float:left"><asp:Image ID="imagePhoto" runat="server" CssClass="imagePhoto"/></div>
    <div id="commentList" style="height:325px; width:575px; background-color:Red; float:left">
        <center>
        <asp:GridView ID="commentGrid" runat="server" 
            EmptyDataText="There are no comments on this picture." 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Username" />
                <asp:BoundField DataField="Content" />
                <asp:BoundField DataField="CommentData" />
                <asp:ButtonField ButtonType="Button" Text="Delete" />
            </Columns>
        </asp:GridView> 
        </center>
    </div>
    <div id="imageInfo" style="height:75px; width:575px; background-color:Orange; float:left"></div>
    <div id="commentSubmit" style="height:75px; width:575px; background-color:Silver; float:left"></div>
    
</div>

</asp:Content>
