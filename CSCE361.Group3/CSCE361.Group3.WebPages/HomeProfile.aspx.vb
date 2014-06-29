Imports CSCE361.Group3.BizLogic

Public Class HomeProfile
    Inherits System.Web.UI.Page

    Private _sUserID
    Private _sUsername
    Private _sName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Load all pictures from database into markers and display on the map using javascript
        'TODO: add info window support - in info window have link to display the picture down below
        Dim markers As String = picturesToString(getAllPictures())
        literal1.Text = "<script type='text/javascript'>" & _
     "function initialize() {" & _
     "var mapOptions = {" & _
     "center: new google.maps.LatLng(40.82011, -96.700759)," & _
     "zoom: 16," & _
     "mapTypeId : google.maps.MapTypeId.HYBRID" & _
     "};" + _
     "var map = new google.maps.Map(document.getElementById('googlemap'), mapOptions);" & _
    markers & _
     "}" & _
     "</script>"""




        If Not IsPostBack Then
            'Parse querystring to get user data and save to private
            parseQueryString()

            'Fill user-specific fields
            fillFields()

            'If userid is not present in the querystring then go back to login (ensures not trying to access page without being a user)
            If _sUserID = "" Then
                Response.Redirect("~/Login.aspx")
            End If
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
            lblAge.Text = oDataTable.Rows(0).Item("Age") & " years old"

            If Not IsDBNull(oDataTable.Rows(0).Item("ProfilePictureFileLoc")) AndAlso Not oDataTable.Rows(0).Item("ProfilePictureFileLoc") = "" Then
                imgProfilePic.ImageUrl = oDataTable.Rows(0).Item("ProfilePictureFileLoc")
            Else
                imgProfilePic.ImageUrl = "http://tinyurl.com/qxx8of9"
            End If
        Else
            _sUserID = ""
        End If
    End Sub

    Private Sub parseQueryString()
        Dim sUserID As String = Request.QueryString("userid")
        If Trim(sUserID & "") = "" Then
            _sUserID = ""
        Else
            _sUserID = sUserID
            hiddenfield.Text = _sUserID 'testing whether id gets cleared on post back
        End If
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim oResults As New Results
        oResults.bSuccess = False
        oResults.sMessage = ""

        If Not tbCaption.Text = "" Then
            'WORKS: REUSE CODE IN PROFILE
            If fuPhoto.HasFile Then
                'Get fileupload item and convert to 64string before passing into API_Imgur upload helper class
                Dim imageLength As Integer = fuPhoto.PostedFile.ContentLength
                Dim imageBtye(imageLength) As Byte
                fuPhoto.PostedFile.InputStream.Read(imageBtye, 0, imageLength)

                oResults = API_Imgur.uploadImage(Convert.ToBase64String(imageBtye))
            End If

            'Will need to get geo data off image before upload
            Dim oPicture As New Picture("", "", tbCaption.Text, hiddenfield.Text, oResults.sMessage)
            oPicture.addPicture()
            lblSuccess.Text = "Upload successful!"
            lblSuccess.ForeColor = Drawing.Color.Green
        Else
            lblSuccess.Text = "Upload failed. Please provide a caption."
            lblSuccess.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Public Function getAllPictures() As DataTable
        Dim oDataTable As New DataTable
        Dim oPicture As New Picture("")
        oDataTable = oPicture.getAllPictures()
        Return oDataTable
    End Function

    Public Function picturesToString(ByVal oPictures As DataTable) As String
        Dim sMarkers As New StringBuilder

        For i As Integer = 0 To oPictures.Rows.Count - 1 Step 1
            Dim sbMarker As New StringBuilder
            Dim sLatLng As String = oPictures.Rows(i).Item("Latitude").ToString & ", " & oPictures.Rows(i).Item("Longitude").ToString
            Dim sCaption As String = oPictures.Rows(i).Item("Caption").ToString
            sbMarker.Append("var marker" & i.ToString & " = new google.maps.Marker({ position: new google.maps.LatLng( " & sLatLng & "), map: map, title:'Click me!'});")

            'sbMarker.Append("var infotext = '<div><div style='float:left;'> <span style = font-size:18px;font-weight:bold;'>New Delhi</span><hr>")
            'sbMarker.Append("New Delhi is the capital of the Republic of India, <br>")
            'sbMarker.Append("and the seat of executive, legislative, and judiciary <br>")
            'sbMarker.Append("Capital Territory of Delhi.<a href='http://en.wikipedia.org/wiki/New_Delhi' ")
            'sbMarker.Append("style='text-decoration:none;color:#cccccc;font-size:10px;'> Wikipedia</a>")
            'sbMarker.Append("</div><div style='float:right; padding:5px;'><img src='")
            'sbMarker.Append("http://upload.wikimedia.org/wikipedia/commons/thumb/d/d1/")
            'sbMarker.Append("India_Gate_clean.jpg/100px-India_Gate_clean.jpg'>")
            'sbMarker.Append("</img></div></div>;")

            'sbMarker.Append("var infowindow = new google.maps.InfoWindow(); infowindow.setContent(infotext);")
            'sbMarker.Append("google.maps.event.addListener(marker, 'click', function() {infowindow.open(map, marker);});")

            sMarkers.Append(sbMarker.ToString)
        Next


        Return sMarkers.ToString
    End Function

End Class