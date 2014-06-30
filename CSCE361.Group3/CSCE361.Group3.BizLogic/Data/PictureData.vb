Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class PictureData

    'DONE: works
    Public Sub AddPicture(ByVal imageFileLoc As String, ByVal longitude As String, ByVal latitude As String, ByVal caption As String, ByVal userID As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO `Photo` (ImageFileLoc, Longitude, Latitude, Caption, UserID) VALUES ('" & imageFileLoc & "'," & longitude & "," & latitude & ",'" & caption & "'," & userID & ")"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub

    'Not sure how to implement multiple queries. Opened and closed different connection for each query
    'TODO: Test
    Public Sub DeletePicture(ByVal photoID As String)
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim deleteCommentsQuery As String = "DELETE FROM Comment WHERE PhotoID = " & photoID & ";"
        Dim myCommand As New MySqlCommand(deleteCommentsQuery)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()

        Dim strSQL As String = "DELETE FROM Photo WHERE PhotoID = " & photoID & ";"
        Dim myCommand2 As New MySqlCommand(strSQL)
        myCommand2.Connection = myConnection
        myConnection.Open()
        myCommand2.ExecuteNonQuery()
        myCommand2.Connection.Close()
    End Sub


    'Only search pictures uploaded by a specific user
    'TODO: Test
    Public Function SearchPictureByUploaderUsername(ByVal sUsername As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim userID As String = GetUserID(sUsername)

        Dim query As String = "SELECT * FROM Photo WHERE `UserID` = '" & userID & "';"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function


    Public Function getAllPictures() As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter


        Dim query As String = "SELECT * FROM Photo;"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function

    'TODO: test
    Public Function getPictureByID(ByVal sPhotoID As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM Photo WHERE photoID =" & Convert.ToInt32(sPhotoID) & ";"
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

End Class
