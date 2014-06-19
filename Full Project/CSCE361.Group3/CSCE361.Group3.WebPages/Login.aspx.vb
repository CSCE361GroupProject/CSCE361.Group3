Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'TODO: Add twitter authentication logic
    'TODO: Add logic to check if profile exists or not then direct to the correct page
    Protected Sub btnTwitter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTwitter.Click
        'If user/profile does not exist - go to registration
        Response.Redirect("~\Registration.aspx")

        'If user/profile exists - go to profile
        'Response.Redirect("~\Profile.aspx")
    End Sub
End Class