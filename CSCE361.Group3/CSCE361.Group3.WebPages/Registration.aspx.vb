Imports CSCE361.Group3.BizLogic

Public Class Registration
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Parse query string and fill username textbox
            Dim sUsername As String = Request.QueryString("username")
            tbUsername.Text = sUsername
        End If
    End Sub

    Protected Sub btnRegister_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRegister.Click
        'Add user from fields
        Dim oProfile As New BizLogic.Profile(tbUsername.Text, tbFirstName.Text, tbLastName.Text, tbAge.Text)
        Dim oResults As Results = oProfile.addProfile()

        'Validates user - checks for missing fields - displays error if fails
        If oResults.bSuccess Then
            'Build query string to pass variables between pages
            Dim sQueryString As String
            sQueryString = "?userid=" & getUserID(oProfile.Username)

            'Loads profile
            Response.Redirect("~\HomeProfile.aspx" & sQueryString)
        Else
            'Show error message is add not successful
            lblSuccess.Text = oResults.sMessage
            lblSuccess.Visible = True
            lblSuccess.ForeColor = Drawing.Color.Red
        End If
    End Sub

    Private Function getUserID(ByVal sUsername) As String
        Dim oProfile As New Profile(sUsername)
        Dim oIntResults As IntegerResults = oProfile.searchProfileByUsername()
        Return oIntResults.lID
    End Function
End Class