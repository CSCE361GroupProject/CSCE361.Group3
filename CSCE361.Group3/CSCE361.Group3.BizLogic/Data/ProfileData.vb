Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ProfileData
    'DONE: works 
    Public Sub AddProfile(ByVal username As String, ByVal firstName As String, ByVal lastName As String, ByVal age As Integer)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName, Age) VALUES ('" & username & "', '" & firstName & "', '" & lastName & "', " & age & ");"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub

    'needs testing
    Public Sub AddProfileWithPic(ByVal username As String, ByVal firstName As String, ByVal lastName As String, ByVal profilePictureLoc As String, ByVal age As Integer)
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName, ProfilePictureFileLoc, Age) VALUES ('" & username & "', '" & firstName & "', '" & lastName & "', '" & profilePictureLoc & "', " & age & ");"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub


    'Implement delete method of comment by user
    'TODO: Implement delete comments of photos by users, delete photos by user, delete user profile.
    Public Sub DeleteProfile(ByVal sUserID As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim deleteCommentsQuery As String = "DELETE FROM Comment WHERE UserID = " & sUserID & ";"
        Dim myCommand As New MySqlCommand(deleteCommentsQuery)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()

        'Dim deletePhotoCommentQuery As String = "DELETE FROM Photo WHERE PhotoID = " & photoID & ";"
        'myConnection.Open()
        'myCommand.ExecuteNonQuery()
        'myCommand.Connection.Close()

    End Sub

    'TODO: Search Profile method - we may not need a full search function like this - we can evaluate later
    Public Function SearchProfile(ByVal searchString As String) As DataTable

        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT Username FROM User WHERE FirstName = " & searchString & " OR Lastname = " & searchString & " OR Username = " & searchString & ";"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable

    End Function

    'DONE: tested and works
    Public Function SearchProfileByUsername(ByVal sUsername As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM User WHERE `Username` = '" & sUsername & "';"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function


    'DONE: works
    Public Function SearchProfileByID(ByVal sUserID As String) As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM User WHERE `UserID` = '" & sUserID & "';"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function


    'TODO: Edit Profile method - may not need this - we dont really have a spot where this is useful

    'Example of working query
    'Dim oDataTable As New DataTable

    'Dim myConnection As MySqlConnection = New MySqlConnection("server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361")
    'Dim myDataAdapter As MySqlDataAdapter

    'Dim strSQL As String = "SELECT * FROM User;"

    'myDataAdapter = New MySqlDataAdapter(strSQL, myConnection)

    'myDataAdapter.Fill(oDataTable)
    'Return oDataTable

End Class
