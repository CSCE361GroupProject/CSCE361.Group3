Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class PictureData

    'TODO: Add Picture method
    Public Sub AddPicture(ByVal imageFileLoc As String, ByVal longitude As String, ByVal latitude As String, ByVal caption As String, ByVal username As String)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName, Age) VALUES ('" & username & "', '" & firstName & "', '" & lastName & "', " & age & ");"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()
    End Sub

    'TODO: Delete Picture method

    'TODO: Search Picture method

End Class
