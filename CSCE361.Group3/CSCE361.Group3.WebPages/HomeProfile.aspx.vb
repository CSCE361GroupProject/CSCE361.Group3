﻿Imports CSCE361.Group3.BizLogic

Public Class HomeProfile
    Inherits System.Web.UI.Page

    Private _sUserID As String
    Private _sUsername As String
    Private _sName
    Private _sPhotoID As String
    Private _dtPictures As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'NOTE: click event from map marker is not a postback

        'resaves userid into variable every page load to keep it in scope
        _sUserID = Session("userid").ToString

        'TODO: add logic here to determine what datatable of pictures to use

        'Load all pictures from database into markers and display on the map using javascript
        _dtPictures = getAllPictures()
        literal1.Text = API_Google.populateGoogleMap(_dtPictures)



        If Not IsPostBack Then
            'Parse querystring to get user data and save to private
            parseQueryString()

            'Fill user-specific fields
            fillFields()

            'If userid is not present in the querystring then go back to login (ensures not trying to access page without being a user)
            If _sUserID = "" Then
                Response.Redirect("~/Login.aspx")
            End If
        Else
            'resets button handlers on postback (otherwise delete buttons dont work when page reloads)
            For Each repItem As RepeaterItem In rptComments.Items
                Dim rptDelete As Button = repItem.FindControl("rptbtnDelete")
                AddHandler rptDelete.Click, AddressOf repeaterDelete
            Next
        End If
    End Sub

    'Fills visible fields and private variables for other method use
    Private Sub fillFields()
        Dim oProfile As New Profile(_sUserID, _sUsername)
        Dim oDataTable As DataTable = oProfile.searchProfileByID()

        If oDataTable.Rows.Count = 1 Then
            _sName = oDataTable.Rows(0).Item("FirstName") & " " & oDataTable.Rows(0).Item("LastName")
            _sUsername = oDataTable.Rows(0).Item("Username")

            lblName.Text = _sName

            If Not IsDBNull(oDataTable.Rows(0).Item("ProfilePictureFileLoc")) AndAlso Not oDataTable.Rows(0).Item("ProfilePictureFileLoc") = "" Then
                imgProfilePic.ImageUrl = oDataTable.Rows(0).Item("ProfilePictureFileLoc")
            Else
                imgProfilePic.ImageUrl = "http://tinyurl.com/qxx8of9"
            End If
        Else
            _sUserID = ""
        End If
    End Sub

    'Logic to handle duplicate query string parameters - DO NOT EDIT
    Private Sub parseQueryString()
        Dim nCount = Request.QueryString.Count
        If nCount = 2 Then
            Dim sPhotoId1 As String = Request.QueryString("photoid")

            If sPhotoId1.Contains(",") Then
                Dim sArray() As String = sPhotoId1.Split(",")
                Response.Redirect("/HomeProfile.aspx?login=1&photoid=" & sArray(1))
            End If
        End If

        Dim sPhotoID As String = Request.QueryString("photoid")

        If Not Trim(sPhotoID & "") = "" Then
            _sPhotoID = sPhotoID
            loadPicture(sPhotoID)
        End If
    End Sub

    'needs fixing
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click

        Dim sLongitude As String = ""
        Dim sLatitude As String = ""
        Dim geoData() As String = {"", ""}

        Dim oResults As New Results
        oResults.bSuccess = False
        oResults.sMessage = ""

        If fuPhoto.HasFile Then
            'Get fileupload item and convert to 64string before passing into API_Imgur upload helper class
            Dim imageLength As Integer = fuPhoto.PostedFile.ContentLength
            Dim imageBtye(imageLength) As Byte
            fuPhoto.PostedFile.InputStream.Read(imageBtye, 0, imageLength)

            oResults = API_Imgur.uploadImage(Convert.ToBase64String(imageBtye))

            If oResults.bSuccess Then
                Dim sOriginalPictureLink As String = fuPhoto.PostedFile.FileName
                'geoData = API_ExifLib.getGeoData(sOriginalPictureLink)
                'geoData = API_ExifLib.getGeoData("C:\Users\Cody Desktop\Downloads\IMAG0145.jpg")
                'TODO: works when given a direct path to the file. does not work with just file name. need to figure out a solution

                'sLongitude = geoData(1)
                'sLatitude = geoData(0)

                'If both are zero then no exif data was found
                'If sLatitude = "" And sLongitude = "" Then
                '    sLatitude = ""
                '    sLongitude = ""
                'End If

            End If

        End If

        'Will need to get geo data off image before upload
        Dim oPicture As New Picture(sLongitude, sLatitude, tbCaption.Text, _sUserID, oResults.sMessage)
        Dim oResults2 As Results = oPicture.addPicture()
        If oResults2.bSuccess Then
            lblSuccess.ForeColor = Drawing.Color.Green
        Else
            lblSuccess.ForeColor = Drawing.Color.Red
        End If
        lblSuccess.Text = oResults2.sMessage

    End Sub


    'Returns all pictures in the database as a datatable for google maps use
    Public Function getAllPictures() As DataTable
        Dim oDataTable As New DataTable
        Dim oPicture As New Picture("")
        oDataTable = oPicture.getAllPictures()
        Return oDataTable
    End Function

    'Loads picture, picture info, and comments - is launched when an id is found in the query string
    Public Sub loadPicture(ByVal sPhotoID As String)
        Dim oPicture As New Picture(sPhotoID)
        oPicture.getPicture()
        imagePhoto.ImageUrl = oPicture.ImagePath

        'bind comment list to grid
        rptComments.DataSource = oPicture.CommentList
        rptComments.DataBind()

        'disable delete button on comments when necessary
        disableButtons(oPicture.UserID)

        'set caption and uploader info
        lblPicCaption.Text = oPicture.Caption
        Dim oProfile As New Profile(oPicture.UserID, "")
        oProfile.getUser()

        lblPicUploader.Text = "Uploaded by: " & oProfile.FirstName & " " & oProfile.LastName

        'enable/disable delete button for picture
        Dim sUserID As String = _sUserID
        If oPicture.UserID = sUserID Then
            btnPicDelete.Enabled = True
        Else
            btnPicDelete.Enabled = False
        End If
    End Sub

    'todo: test
    Private Function filterPicturesByDist(ByVal latitude As String, ByVal longitude As String, ByVal ndistFeet As Integer, ByVal oPictures As DataTable) As DataTable
        Dim oNewPictures As New DataTable
        For i As Integer = 0 To oPictures.Rows.Count - 1
            'check if the picture at this index is within the allowed distance
            If gpsDistanceInFeet(latitude, longitude, oNewPictures.Rows(i).Item("Latitude"), oNewPictures.Rows(i).Item("Longitude")) < ndistFeet Then
                oNewPictures.Rows.Add(oPictures.Rows(i))
            End If
        Next
        Return oNewPictures
    End Function

    'todo: test
    Private Function gpsDistanceInFeet(ByVal latA As String, ByVal longA As String, ByVal latB As String, ByVal longB As String) As Integer
        'finds the distance between two GPS coordinates in feet
        'first parse the string, break it down and convert from degrees.minutes.seconds to degrees
        Dim latADegrees As Double = Double.Parse(latA)
        Dim longADegrees As Double = Double.Parse(longA)
        Dim latBDegrees As Double = Double.Parse(latB)
        Dim longBDegrees As Double = Double.Parse(longB)

        'convert values to radians
        latADegrees *= Math.PI / 180
        longADegrees *= Math.PI / 180
        latBDegrees *= Math.PI / 180
        longBDegrees *= Math.PI / 180

        'distance = 131332796.6 x (ArcCos{Cos[a1]xCos[b1]xCos[a2]xCos[b2] + Cos[a1]xSin[b1]xCos[a2]xSin[b2] + Sin[a1]xSin[a2]}/360)
        'where 1 is latitude, 2 is longitude
        Dim distance As Integer
        distance = Math.Cos(latADegrees) * Math.Cos(latBDegrees) * Math.Cos(longADegrees) * Math.Cos(longBDegrees)
        distance += Math.Cos(latADegrees) * Math.Sin(latBDegrees) * Math.Cos(longADegrees) * Math.Sin(longBDegrees)
        distance += Math.Sin(latADegrees) * Math.Sin(longBDegrees)
        distance = (Math.Acos(distance)) / 360
        distance *= 131332796.6

        Return Math.Abs(distance)
    End Function

    Protected Sub btnAddComment_Click(sender As Object, e As EventArgs) Handles btnAddComment.Click
        Dim sPhotoID As String = Request.QueryString("photoid")
        Dim sUserID As String = Request.QueryString("userid")

        If tbAddComment.Text = "" Then
            'TODO: display error message - maybe js prompt?
        Else
            Dim oComment As New Comment(tbAddComment.Text, sPhotoID, _sUserID)
            oComment.addComment()

            'reload repeater
            Dim oPicture As New Picture(sPhotoID)
            oPicture.getPicture()

            'bind comment list to grid
            rptComments.DataSource = oPicture.CommentList
            rptComments.DataBind()
        End If

        'clears text 
        tbAddComment.Text = ""
    End Sub

    'Deletes selected picture and subsequent comments - if button is enabled
    Protected Sub btnPicDelete_Click(sender As Object, e As EventArgs) Handles btnPicDelete.Click
        Dim oPicture As New Picture(Request.QueryString("photoid"))
        oPicture.deletePhoto()

        Response.Redirect("~\HomeProfile.aspx?login=1")
    End Sub

    'Handler for delete button in comment repeater view
    'Deletes comment then reloads/rebinds data
    Protected Sub repeaterDelete(sender As Object, e As EventArgs)
        Dim button As Button = sender
        Dim repeaterItem As RepeaterItem = button.NamingContainer
        Dim index As Integer = repeaterItem.ItemIndex

        Dim labelID As Label = repeaterItem.FindControl("lblcommentid")
        Dim commentID As String = labelID.Text

        Dim comment As New Comment(commentID)
        comment.deleteComment()

        Dim sPhotoID As String = Request.QueryString("photoid")
        Dim oPicture As New Picture(sPhotoID)
        oPicture.getPicture()
        rptComments.DataSource = oPicture.CommentList
        rptComments.DataBind()

        loadPicture(Request.QueryString("photoid"))

        Page_Load(sender, e)
    End Sub

    'Disables/enables buttons in comments repeater based on picture and comment ownership rights
    Protected Sub disableButtons(ByVal sUserID As String)
        For Each repItem As RepeaterItem In rptComments.Items
            Dim rptDelete As Button = repItem.FindControl("rptbtnDelete")
            Dim lbluserid As Label = repItem.FindControl("lbluserid")

            If sUserID = Session("userid") Then
                rptDelete.Enabled = True
            ElseIf lbluserid.Text = Session("userid") Then
                rptDelete.Enabled = True
            Else
                rptDelete.Enabled = False
            End If
        Next
    End Sub

    'todo: test view my photos - not working - losing dtPictures somewhere
    Protected Sub btnViewMyPhotos_Click(sender As Object, e As EventArgs) Handles btnViewMyPhotos.Click
        Dim oPicture As New Picture("")
        oPicture.UserID = _sUserID
        Dim oDataTable As New DataTable
        oDataTable = oPicture.getPicturesByUserID()

        If oDataTable.Rows.Count > 0 Then
            _dtPictures = oDataTable
        End If

        Response.Redirect("~/HomeProfile.aspx?login=1")
    End Sub
End Class