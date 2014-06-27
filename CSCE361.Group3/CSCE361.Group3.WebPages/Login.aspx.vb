Imports CSCE361.Group3.BizLogic

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'TODO: Add twitter authentication logic in Sprint 3
    Protected Sub btnTwitter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTwitter.Click
        'Search if username exists in database
        If Not tbUserName.Text = "" Then
            Dim oProfile As New BizLogic.Profile(tbUserName.Text)
            Dim oIntResults As IntegerResults = oProfile.searchProfileByUsername

            'Build query string to pass variables between pages
            Dim sQueryStringUsername As String
            sQueryStringUsername = "?username=" & tbUserName.Text

            Dim sQueryStringID As String
            sQueryStringID = "?userid=" & oIntResults.lID

            'If user/profile does not exist - go to registration
            If oIntResults.bSuccess Then
                Response.Redirect("~/HomeProfile.aspx" & sQueryStringID)
            Else
                Response.Redirect("~/Registration.aspx" & sQueryStringUsername)
            End If
        Else
            lblSuccess.Visible = True
            lblSuccess.Text = "Please enter a username."
            lblSuccess.ForeColor = Drawing.Color.Red
        End If
    End Sub
End Class