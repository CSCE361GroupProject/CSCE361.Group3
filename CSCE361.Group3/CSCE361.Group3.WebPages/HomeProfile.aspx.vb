Imports CSCE361.Group3.BizLogic

Public Class HomeProfile
    Inherits System.Web.UI.Page

    Private _sUserID
    Private _sUsername

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Parse querystring to get user data and save to private
            parseQueryString()

            If _sUserID = "" And _sUsername = "" Then
                Response.Redirect("~/Login.aspx")
            End If

            'Fill user-specific fields
            fillFields()
        End If

    End Sub


    Private Sub fillFields()
        Dim oProfile As New Profile(_sUserID, _sUsername)
        Dim oDataTable As DataTable = oProfile.searchProfileByID()

        lblName.Text = oDataTable.Rows(0).Item("FirstName") & " " & oDataTable.Rows(0).Item("LastName")
        lblAge.Text = oDataTable.Rows(0).Item("Age") & " years old"
        'TODO: later add profile picture loading
    End Sub

    Private Sub parseQueryString()
        Dim sUsername As String = Request.QueryString("username")
        If Trim(sUsername & "") = "" Then
            _sUsername = ""
        Else
            _sUsername = sUsername
        End If

        Dim sUserID As String = Request.QueryString("userid")
        If Trim(sUserID & "") = "" Then
            _sUserID = ""
        Else
            _sUserID = sUserID
        End If
    End Sub




End Class