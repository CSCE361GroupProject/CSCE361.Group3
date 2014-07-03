Imports CSCE361.Group3.BizLogic

Public Class HomeProfile
    Inherits System.Web.UI.Page

    Private _sUserID As String
    Private _sUsername As String
    Private _sName
    Private _sPhotoID

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'NOTE: click event from map marker is not a postback

        'resaves userid into variable every page load to keep it in scope
        _sUserID = Session("userid").ToString

        'Load all pictures from database into markers and display on the map using javascript
        'literal1.Text = populateGoogleMap(getAllPictures)
        literal1.Text = API_Google.populateGoogleMap(getAllPictures)



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

    'voodoo magic to handle duplicate query string parameters - DO NOT EDIT
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

    'needs testing
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click

        Dim sLongitude As String = "" 'set longitude from geo data here
        Dim sLatitude As String = "" 'set latitude from geo data here
        Dim geoData() As Double = {Nothing, Nothing}

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
                geoData = API_ExifLib.getGeoData(sOriginalPictureLink)
                'geoData = API_ExifLib.getGeoData("C:\Users\Cody Desktop\Downloads\IMAG0145.jpg")
                'TODO: works when given a direct path to the file. does not work with just file name. need to figure out a solution

                sLongitude = geoData(1).ToString
                sLatitude = geoData(0).ToString

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

    Public Function getAllPictures() As DataTable
        Dim oDataTable As New DataTable
        Dim oPicture As New Picture("")
        oDataTable = oPicture.getAllPictures()
        Return oDataTable
    End Function

    Public Sub loadPicture(ByVal sPhotoID As String)
        Dim oPicture As New Picture(sPhotoID)
        oPicture.getPicture()
        imagePhoto.ImageUrl = oPicture.ImagePath

        'bind comment list to grid
        'TODO: need on row commands to determine whether a delete button can be enabled (and comment delete logic needs implemented)
        rptComments.DataSource = oPicture.CommentList
        rptComments.DataBind()

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
            'TODO: display error message
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

    End Sub

    Protected Sub btnPicDelete_Click(sender As Object, e As EventArgs) Handles btnPicDelete.Click
        'TODO: add delete photo logic
    End Sub
End Class