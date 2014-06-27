Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class CommentData

    'TODO: Test
    Public Sub AddComment(ByVal userID As String, ByVal photoID As String, ByVal content As String, ByVal commentDate As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO Comment (Content, PhotoID, UserID) VALUES ('" & content & "', " & photoID & ", " & userID & ");"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub

    'DeleteComment method assume validation has been done
    'TODO: Test
    Public Sub DeleteComment(ByVal commentID As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"
        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "DELETE FROM Comment WHERE CommentID = " & commentID & ";"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()

    End Sub


    'TODO: Test
    Public Function SearchCommentByCommenterUsername(ByVal sUsername As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim userID As String = GetUserID(sUsername)

        Dim query As String = "SELECT * FROM Comment WHERE `UserID` = '" & userID & "';"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function

    'Helper Methods
    'TODO: Test
    Private Function GetUserID(ByVal username As String)

        Dim userID As String
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As Integer = "SELECT UserID FROM User WHERE Username = '" & username & "';"

        Dim myCommand As New MySqlCommand(strSQL, myConnection)
        myConnection.Open()
        Dim myReader As MySqlDataReader
        myReader = myCommand.ExecuteReader()
        Try
            userID = myReader.GetString(0)
        Finally
            myReader.Close()
            myConnection.Close()
        End Try

        Return userID

    End Function

    'TODO: Test
    Private Function GetPhotoID(ByVal pictureFileLoc As String)

        Dim photoID As String

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "SELECT PhotoID FROM Photo WHERE ImageFileLoc = '" & pictureFileLoc & "';"

        Dim myCommand As New MySqlCommand(strSQL, myConnection)
        myConnection.Open()
        Dim myReader As MySqlDataReader
        myReader = myCommand.ExecuteReader()
        Try
            photoID = myReader.GetString(0)
        Finally
            myReader.Close()
            myConnection.Close()
        End Try

        Return photoID

    End Function

End Class
