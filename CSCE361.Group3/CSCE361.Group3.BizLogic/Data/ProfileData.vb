Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ProfileData
    'DONE: works 
    Public Sub AddProfile(ByVal username As String, ByVal firstName As String, ByVal lastName As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName) VALUES ('" & username & "', '" & firstName & "', '" & lastName & "');"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub

    'needs testing
    Public Sub AddProfileWithPic(ByVal username As String, ByVal firstName As String, ByVal lastName As String, ByVal profilePictureLoc As String)
        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName, ProfilePictureFileLoc) VALUES ('" & username & "', '" & firstName & "', '" & lastName & "', '" & profilePictureLoc & "');"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub


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

    'DONE: works
    Public Function getAllUsers() As DataTable
        Dim oDataTable As New DataTable

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)
        Dim myDataAdapter As MySqlDataAdapter

        Dim query As String = "SELECT * FROM User;"
        myDataAdapter = New MySqlDataAdapter(query, myConnection)

        myDataAdapter.Fill(oDataTable)

        Return oDataTable
    End Function

End Class
