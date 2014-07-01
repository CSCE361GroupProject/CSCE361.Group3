Imports CSCE361.Group3.BizLogic

Public Class HomeProfile
    Inherits System.Web.UI.Page

    Private _sUserID
    Private _sUsername
    Private _sName
    Private _sPhotoID

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'NOTE: click event from map marker is not a postback

        'Load all pictures from database into markers and display on the map using javascript
        literal1.Text = populateGoogleMap(getAllPictures)


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

    'voodoo magic to handle duplicate query string parameters - DO NOT EDIT
    Private Sub parseQueryString()
        Dim nCount = Request.QueryString.Count
        If nCount = 2 Then
            Dim sUserID1 As String = Request.QueryString("userID")
            Dim sPhotoId1 As String = Request.QueryString(1)

            If sPhotoId1.Contains(",") Then
                Dim sArray() As String = sPhotoId1.Split(",")
                Response.Redirect("/HomeProfile.aspx?userid=" & sUserID1 & "&photoid=" & sArray(1))
            End If
        End If

        Dim sUserID As String = Request.QueryString("userid")
        Dim sPhotoID As String = Request.QueryString("photoid")
        If Trim(sUserID & "") = "" Then
            _sUserID = ""
        Else
            _sUserID = sUserID
            hiddenfield.Text = _sUserID
        End If

        If Not Trim(sPhotoID & "") = "" Then
            _sPhotoID = sPhotoID
            loadPicture(sPhotoID)
        End If
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim oResults As New Results
        oResults.bSuccess = False
        oResults.sMessage = ""

        If fuPhoto.HasFile Then
            'Get fileupload item and convert to 64string before passing into API_Imgur upload helper class
            Dim imageLength As Integer = fuPhoto.PostedFile.ContentLength
            Dim imageBtye(imageLength) As Byte
            fuPhoto.PostedFile.InputStream.Read(imageBtye, 0, imageLength)

            oResults = API_Imgur.uploadImage(Convert.ToBase64String(imageBtye))
        End If

        'Will need to get geo data off image before upload
        Dim oPicture As New Picture("", "", tbCaption.Text, hiddenfield.Text, oResults.sMessage)
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

#Region "Google Map Loading/Plotting"
    'DONE: works
    Public Function populateGoogleMap(ByVal oPictures As DataTable) As String
        Dim sScript As New StringBuilder

        Dim sMarkers As String = getMarkers(oPictures)
        Dim sSetmap As String = getSetMapsForMarkers(oPictures)
        Dim sListeners As String = getSetListenersForMarkers(oPictures)

        sScript.Append("<script type='text/javascript'>")
        sScript.Append("var markerZ = new google.maps.Marker();") 'create blank marker
        sScript.Append(sMarkers)
        sScript.Append("function initialize() { var mapOptions = {center: new google.maps.LatLng(40.82011, -96.700759), zoom: 16, mapTypeId : google.maps.MapTypeId.HYBRID};")
        sScript.Append("var map = new google.maps.Map(document.getElementById('googlemap'), mapOptions);")
        sScript.Append(sSetmap)
        sScript.Append("markerZ.setMap(map);") 'set blank marker to the map
        sScript.Append("google.maps.event.addListener(map, 'click', function(event) {addMarker(event.latLng); });") 'allows marker to be placed on click
        sScript.Append("}")
        sScript.Append("google.maps.event.addDomListener(window, 'load', initialize);")
        sScript.Append(sListeners)
        'function/listner to clear additional user-added markers on the map and set new markers coordinates in textbox for use
        sScript.Append("function addMarker(location) { if(!markerZ) { markerZ = new google.maps.Marker({position: location, map: map}); } else {markerZ.setPosition(location);} document.getElementById('MainContent_tbSelectedPointLatLng').value = location; markerZ.setAnimation(google.maps.Animation.BOUNCE);}")
        sScript.Append("</script>")

        Return sScript.ToString
    End Function

    'DONE: works
    Public Function getMarkers(ByVal oPictures As DataTable) As String
        Dim sMarkers As New StringBuilder

        For i As Integer = 0 To oPictures.Rows.Count - 1 Step 1
            Dim sbMarker As New StringBuilder
            Dim sLatLng As String = oPictures.Rows(i).Item("Latitude").ToString & ", " & oPictures.Rows(i).Item("Longitude").ToString

            Dim sCaption As String = oPictures.Rows(i).Item("Caption").ToString 'setting caption as title doesnt work due to problems escaping '
            Dim sPhotoID As String = oPictures.Rows(i).Item("PhotoID").ToString

            sbMarker.Append("var marker" & i.ToString & " = new google.maps.Marker({ position: new google.maps.LatLng( " & sLatLng & "), url: '&photoid=" & sPhotoID & "', title:'Click to see photo " & sPhotoID & "', animation: google.maps.Animation.DROP});")

            sMarkers.Append(sbMarker.ToString)
        Next

        Return sMarkers.ToString
    End Function


    'DONE: works
    Public Function getSetMapsForMarkers(ByVal oPictures As DataTable) As String
        Dim sSetMap As New StringBuilder

        For i As Integer = 0 To oPictures.Rows.Count - 1 Step 1
            Dim sbMarker As New StringBuilder

            sbMarker.Append("marker")
            sbMarker.Append(i.ToString)
            sbMarker.Append(".setMap(map);")

            sSetMap.Append(sbMarker.ToString)
        Next

        Return sSetMap.ToString
    End Function

    'DONE: works
    Public Function getSetListenersForMarkers(ByVal oPictures As DataTable) As String
        Dim sListeners As New StringBuilder

        For i As Integer = 0 To oPictures.Rows.Count - 1 Step 1
            Dim sbMarker As New StringBuilder


            'google.maps.event.addListener(marker, 'click', function () { window.location.href = document.URL + marker.url; });

            sbMarker.Append("google.maps.event.addListener(marker")
            sbMarker.Append(i.ToString)
            sbMarker.Append(",'click', function () { window.location.href = window.document.URL + marker")
            sbMarker.Append(i.ToString)
            sbMarker.Append(".url; });")

            sListeners.Append(sbMarker.ToString)
        Next

        Return sListeners.ToString
    End Function
#End Region

    'TODO: loads photo from query string and displays in div
    Public Sub loadPicture(ByVal sPhotoID As String)
        Dim oPicture As New Picture(sPhotoID)
        oPicture.getPicture()
        imagePhoto.ImageUrl = oPicture.ImagePath

        'bind comment list to grid

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
        Dim latADegrees As Double
        Dim longADegrees As Double
        Dim latBDegrees As Double
        Dim longBDegrees As Double

        Dim temp1() As String = latA.Split
        latADegrees = Double.Parse(temp1(0)) + (Double.Parse(temp1(1)) / 60) + (Double.Parse(temp1(2)) / 3600)
        Dim temp2() As String = longA.Split
        longADegrees = Double.Parse(temp2(0)) + (Double.Parse(temp2(1)) / 60) + (Double.Parse(temp2(2)) / 3600)
        Dim temp3() As String = latA.Split
        latBDegrees = Double.Parse(temp3(0)) + (Double.Parse(temp3(1)) / 60) + (Double.Parse(temp3(2)) / 3600)
        Dim temp4() As String = longA.Split
        longBDegrees = Double.Parse(temp4(0)) + (Double.Parse(temp4(1)) / 60) + (Double.Parse(temp4(2)) / 3600)

        'all points will be in North America, so they will be North/West
        'west points are inverted
        longADegrees *= -1
        longBDegrees *= -1

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
        distance = Math.Acos(distance) / 360

        Return Math.Abs(distance)
    End Function

End Class