Public Class Picture
#Region "Private Variables"
    Private _sLongitude As String
    Private _sLatitude As String
    Private _sCaption As String
    Private _sPictureID As String
    Private _lCommentList As DataTable
    Private _sUserID As String
    Private _sImagePath As String
#End Region


#Region "Properties"
    Property Longitude As String
        Get
            Return _sLongitude
        End Get
        Set(ByVal value As String)
            _sLongitude = validateInput(value)
        End Set
    End Property

    Property Latitude As String
        Get
            Return _sLatitude
        End Get
        Set(ByVal value As String)
            _sLatitude = validateInput(value)
        End Set
    End Property

    Property Caption As String
        Get
            Return _sCaption
        End Get
        Set(ByVal value As String)
            _sCaption = validateInput(value)
        End Set
    End Property

    Property PictureID As String
        Get
            Return _sPictureID
        End Get
        Set(ByVal value As String)
            _sPictureID = validateInput(value)
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

    Property CommentList As DataTable
        Get
            Return _lCommentList
        End Get
        Set(ByVal value As DataTable)
            _lCommentList = value
        End Set
    End Property

    Property ImagePath As String
        Get
            Return _sImagePath
        End Get
        Set(ByVal value As String)
            _sImagePath = validateInput(value)
        End Set
    End Property

#End Region

#Region "Constructors"
    Sub New(ByVal sPictureID As String)
        PictureID = sPictureID
    End Sub

    'Full constructor
    Sub New(ByVal sLongitude As String, ByVal sLatitude As String, ByVal sCaption As String, ByVal sUserID As String, ByVal sImagePath As String)
        Longitude = sLongitude
        Latitude = sLatitude
        Caption = sCaption
        UserID = sUserID
        ImagePath = sImagePath
    End Sub

#End Region


#Region "Validation"
    'Ensures that no extra whitespace is added to database - also make sure there are no null values
    Public Function validateInput(ByVal value) As String
        Dim validatedInput As String
        validatedInput = Trim(value & "")
        Return validatedInput
    End Function

    'Makes sure that ImagePath is not empty
    Public Function validateImagePath() As Results
        Dim oResults As New Results

        If Trim(ImagePath & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "ImagePath cannot be blank."
        End If

        Return oResults
    End Function

    'Makes sure that UserID is not empty
    Public Function validateUserID() As Results
        Dim oResults As New Results

        If Trim(UserID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "UserID be blank."
        End If

        Return oResults
    End Function

    'Makes sure that PictureID is not empty
    Public Function validatePictureID() As Results
        Dim oResults As New Results

        If Trim(PictureID & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "PictureID be blank."
        End If

        Return oResults
    End Function

    'Makes sure that Caption is not empty
    Public Function validateCaption() As Results
        Dim oResults As New Results

        If Trim(Caption & "") = "" Then
            oResults.bSuccess = False
            oResults.sMessage = "Caption be blank."
        End If

        Return oResults
    End Function

    'If lat/long is empty - set to middle of avery hall
    Public Sub validateLatitude()
        Dim oResults As New Results

        'TODO: generate a random longitude within a small area in avery so that points aren't overlaid each other if longitude not provided
        If Trim(Latitude & "") = "" Then
            Latitude = "40.819452"
            oResults.sMessage = "Latitude set to middle of Avery."
        End If

    End Sub

    'If lat/long is empty - set to middle of avery hall 
    Public Sub validateLongitude()
        Dim oResults As New Results

        'TODO: generate a random longitude within a small area in avery so that points aren't overlaid each other if longitude not provided
        If Trim(Longitude & "") = "" Then
            Longitude = "-96.704503"
            oResults.sMessage = "Longitude set to middle of Avery."
        End If
    End Sub

    'Verifies that all fields required for a database add have values
    Public Function validateAllFields() As Results
        Dim oResults As New Results
        oResults.bSuccess = False 'false until proven true

        If validateUserID().bSuccess Then
            If validateImagePath.bSuccess Then
                    oResults.bSuccess = True
                Else
                    oResults.sMessage = validateImagePath.sMessage
                End If
        Else
            oResults.sMessage = validateUserID.sMessage
        End If

        validateLatitude()
        validateLongitude()

        Return oResults
    End Function
#End Region

    'TODO
#Region "Add/Delete/Update/Search Pictures"

    Public Function addPicture() As Results
        Dim oPictureData As New PictureData
        Dim oResults As Results = validateAllFields()

        If oResults.bSuccess Then
            oPictureData.AddPicture(ImagePath, Longitude, Latitude, Caption, UserID)
            oResults.sMessage = "Upload successful!"
        End If

        Return oResults
    End Function

    'Doesn't require any valid parameters - just gets every photo from the db
    Public Function getAllPictures() As DataTable
        Dim oPictureData As New PictureData
        Dim oDataTable As DataTable = oPictureData.getAllPictures
        Return oDataTable
    End Function

    'Updates the picture properties from the database when given a photoid - used for loading picture to display
    Public Sub getPicture()
        Dim oPictureData As New PictureData
        Dim oDataTable As DataTable = oPictureData.getPictureByID(PictureID)

        If oDataTable.Rows.Count = 1 Then
            Caption = oDataTable.Rows(0).Item("Caption")
            UserID = oDataTable.Rows(0).Item("UserID")
            ImagePath = oDataTable.Rows(0).Item("ImageFileLoc")

            CommentList = getCommentList()
            'TODO: add method call to get comment list 
        End If

    End Sub


#End Region


    'Public Sub getCommentList()
    '    Dim lComment As New List(Of Comment)
    '    'TODO: add query to pull list of comments by pictureid
    '    CommentList = lComment
    'End Sub


    'DONE: works
    Public Function getCommentList() As DataTable
        Dim oDataTable As DataTable
        Dim oCommentData As New PictureData
        oDataTable = oCommentData.GetCommentUserJoinTable(PictureID)

        Return oDataTable
    End Function

End Class
