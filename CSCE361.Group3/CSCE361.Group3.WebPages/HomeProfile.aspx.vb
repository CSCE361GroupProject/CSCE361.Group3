Imports CSCE361.Group3.BizLogic

Public Class HomeProfile
    Inherits System.Web.UI.Page

    Private _sUserID
    Private _sUsername
    Private _sName

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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


End Class