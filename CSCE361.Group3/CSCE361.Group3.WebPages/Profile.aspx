<%@ Page Title="Profile" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Profile.aspx.vb" Inherits="CSCE361.Group3.WebPages.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="profilemenu">
    <center>

    <div id="profileinfo"> 
        <asp:Image ID="imgProfilePic" runat="server" CssClass="img2" ImageUrl="http://i.imgur.com/83iHK.jpg"/> <br />
        <asp:Label ID="lblName" runat="server" Text="FirstName LastName"/> <br /> <br />
        <asp:Label ID="lblAge" runat="server" Text="Age years old"/>
    </div>
    <br /> <br />  Menu: <br /> <br />
       
    <div>
        <asp:Label ID="lblCaption" runat="server" Text="Caption:"></asp:Label>
        <asp:TextBox ID="tbCaption" runat="server"></asp:TextBox> <br />
        <asp:FileUpload ID="fuPhoto" runat="server" /> <br />
                
        <asp:Button ID="btnUpload" runat="server" Text="Upload photo" /> <br /> <br /> <br />
        <asp:Button ID="btnViewPhotos" runat="server" Text="View my photos" /> <br /> 
        <asp:Button ID="btnViewCommentPhotos" runat="server" Text="View photo's I've commented on" /> <br />
    </div>

    </center>
</div>


<div id="googlemap" style="background-color:#33B5E5" >
    <center>Google Map's Div Placeholder</center>
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

    
    
    
    </center>
</div>

<div id="comments" style="background-color:#99CC00">
    <center>Comment's and Selected Photo Div Placeholder</center>
</div>

</asp:Content>
