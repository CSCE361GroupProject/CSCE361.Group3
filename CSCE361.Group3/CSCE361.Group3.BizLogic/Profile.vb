﻿Imports MySql.Data.MySqlClient

Public Class Profile

#Region "Private Variables"
    Private _sUsername As String
    Private _sFirstName As String
    Private _sLastName As String
    Private _sUserID As String
    Private _sAge As String
    Private _sProfilePicturePath As String
    Private _lCommentList As List(Of Comment)
    Private _lPictureList As List(Of Picture)
#End Region

#Region "Properties"
    Property Username As String
        Get
            Return _sUsername
        End Get
        Set(ByVal value As String)
            _sUsername = validateInput(value)
        End Set
    End Property

    Property FirstName As String
        Get
            Return _sFirstName
        End Get
        Set(ByVal value As String)
            _sFirstName = validateInput(value)
        End Set
    End Property

    Property LastName As String
        Get
            Return _sLastName
        End Get
        Set(ByVal value As String)
            _sLastName = validateInput(value)
        End Set
    End Property

    Property UserID As String
        Get
            Return _sUserID
        End Get
        Set(ByVal value As String)
            _sUserID = validateInput(value)
        End Set
    End Property

    Property ProfilePicturePath As String
        Get
            Return _sProfilePicturePath
        End Get
        Set(ByVal value As String)
            _sProfilePicturePath = validateInput(value)
        End Set
    End Property

    Property CommentList As List(Of Comment)
        Get
            Return _lCommentList
        End Get
        Set(ByVal value As List(Of Comment))
            _lCommentList = value
        End Set
    End Property

    Property PictureList As List(Of Picture)
        Get
            Return _lPictureList
        End Get
        Set(ByVal value As List(Of Picture))
            _lPictureList = value
        End Set
    End Property

    Property Age As String
        Get
            Return _sAge
        End Get
        Set(ByVal value As String)
            _sAge = validateInput(value)
        End Set
    End Property
#End Region

#Region "Constructors"
    'Constructor for checking if user exists
    Sub New(ByVal sUsername As String)
        Username = sUsername
    End Sub

    'Full Constructor
    Sub New(ByVal sUsername As String, ByVal sFirstName As String, ByVal sLastName As String, ByVal sAge As String)
        Username = sUsername
        FirstName = sFirstName
        LastName = sLastName
        Age = sAge
    End Sub

    Sub New(ByVal sUserID As String, ByVal sUsername As String)
        UserID = sUserID
        Username = sUsername
    End Sub

#End Region

    'TODO
#Region "Validation"
    'Ensures that no extra whitespace is added to database - also make sure there are no null values
    Public Function validateInput(ByVal value) As String
        Dim validatedInput As String
        validatedInput = Trim(value & "")
        Return validatedInput
    End Function

    'Makes sure that username is not empty
    Public Function validateUsername() As Results
        Dim oResults As New Results

        If Trim(Username & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Username cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that firstname is not empty
    Public Function validateFirstname() As Results
        Dim oResults As New Results

        If Trim(FirstName & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Firstname cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that lastname is not empty
    Public Function validateLastname() As Results
        Dim oResults As New Results

        If Trim(LastName & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Lastname cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that age is not empty
    Public Function validateAge() As Results
        Dim oResults As New Results

        If Trim(Age & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Age cannot be blank."
        End If

        Return oResults
    End Function

    'Verifies that all fields have values
    Public Function validateAllFields() As Results
        Dim oResults As New Results
        oResults.bSuccess = False 'false until proven true

        If validateUsername().bSuccess Then
            If validateFirstname.bSuccess Then
                If validateLastname.bSuccess Then
                    If validateAge.bSuccess Then
                        oResults.bSuccess = True
                    Else
                        oResults.sMessage = validateAge.sMessage
                    End If
                Else
                    oResults.sMessage = validateLastname().sMessage
                End If
            Else
                oResults.sMessage = validateFirstname.sMessage
            End If
        Else
            oResults.sMessage = validateUsername.sMessage
        End If

        Return oResults
    End Function

#End Region

    'TODO
#Region "Add/Delete/Update/Search Profiles"

<<<<<<< HEAD
#End Region
    Public Sub AddProfile(ByVal username As String, ByVal firstName As String, ByVal lastName As String, ByVal profilePictureLoc As String, ByVal age As Integer)

        Dim myConnectionStr As String = "server=cse-group3-mysql-instance1.c2qzromubl3x.us-east-1.rds.amazonaws.com; user=group3_master; password=group3_master; database=CSCE361"

        Dim myConnection As New MySqlConnection(myConnectionStr)

        Dim strSQL As String = "INSERT INTO User (Username, FirstName, LastName, ProfilePictureFileLoc, Age) VALUES ('" & username & "', '" & firstName & "', '" & lastName & "', '" & profilePictureLoc & "', " & age & ");"
        Dim myCommand As New MySqlCommand(strSQL)
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myCommand.Connection.Close()

=======
    'DONE: works
    Public Function addProfile() As Results
        Dim oResults As Results = validateAllFields()
        Dim oProfileData As New ProfileData

        If oResults.bSuccess Then
            oProfileData.AddProfile(Username, FirstName, LastName, Age)
        End If

        Return oResults
    End Function

    'DONE: works
    Public Function searchProfileByUsername() As IntegerResults
        Dim oIntResults As New IntegerResults
        Dim oDatatable As New DataTable
        Dim oProfileData As New ProfileData
        oIntResults.bSuccess = False 'resets success to false so no false positives

        'Makes sure that username is not empty
        Dim oResults As Results = validateUsername()
        If oResults.bSuccess Then
            oDatatable = oProfileData.SearchProfileByUsername(Username)
        End If

        'If returned datatable has a single row, set success to true and return userID
        If oDatatable.Rows.Count = 1 Then
            oIntResults.bSuccess = True
            oIntResults.lID = oDatatable.Rows(0).Item("UserID")
        End If

        Return oIntResults
    End Function


    Public Sub deleteProfile()
        'Delete comments by user first
        'Then delete photos by user
        'Then delete actual profile
>>>>>>> origin/FullProject.V2
    End Sub

#End Region





End Class
