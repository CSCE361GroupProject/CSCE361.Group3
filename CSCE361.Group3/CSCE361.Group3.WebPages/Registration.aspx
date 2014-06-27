<%@ Page Title="Registration" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Registration.aspx.vb" Inherits="CSCE361.Group3.WebPages.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="primary">
        <asp:Image ID="Image1" runat="server" CssClass="img" ImageUrl="http://bloximages.chicago2.vip.townnews.com/journalstar.com/content/tncms/assets/v3/editorial/9/5d/95d6012d-c6c5-56ff-960d-58d066caf5a7/509b3691239ee.preview-620.jpg" />
    </div>

    <div id="content">
        <center>
            <p>You want to upload photos too? Well let's get started then! </p> 
            <p>Just fill out the fields below so we get you set up with your very own account. All the fields are required so be sure to get them all filled out.
                Once you get them all filled out, click "Register" and we will send you to your brand new profile! </p> <br />
            <asp:Label ID="lblUserName" runat="server" Text="Twitter Username:" Width="110px" style="text-align:right" /> <asp:TextBox runat="server" ID="tbUsername"  Enabled="false"/> <br />
            <asp:Label ID="lblFirstName" runat="server" Text="First Name:"  Width="110px" style="text-align:right"/> <asp:TextBox runat="server" ID="tbFirstName" /> <br />
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" Width="110px" style="text-align:right" /> <asp:TextBox runat="server" ID="tbLastName" /> <br />
            <asp:Label ID="lblAge" runat="server" Text="Age:" Width="110px" style="text-align:right"/> <asp:TextBox runat="server" ID="tbAge" /> <br />
            <asp:Label ID="lblProfilePic" runat="server" Text="Profile Picture:" Width="193px" style="text-align:right"/>
            <asp:FileUpload ID="fuProfilePic" runat="server" /> <br /> <br />
            <asp:Button ID="btnRegister" runat="server" Text="Register"  /> <br />
            <asp:Label ID="lblSuccess" runat="server" Visible="false" />

        </center>
        
    </div>

    <div id="secondary">
        <asp:Image ID="Image2" runat="server" CssClass="img" ImageUrl="http://ucomm.unl.edu/resources/downloads/photos/unl_plaza.jpg"/>
    </div>





</asp:Content>
