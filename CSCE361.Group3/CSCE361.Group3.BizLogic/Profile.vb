Public Class Profile

#Region "Private Variables"
    Private _sConnection As String
    Private _sUsername As String
    Private _sFirstName As String
    Private _sLastName As String
    Private _sUserID As String
    Private _nAge As Integer
    Private _sProfilePicturePath As String
    Private _lCommentList As List(Of Comment)
    Private _lPictureList As List(Of Picture)
#End Region

#Region "Properties"
    Property Connection As String
        Get
            Return _sConnection
        End Get
        Set(ByVal value As String)
            _sConnection = value
        End Set
    End Property
    Property Username As String
        Get
            Return _sUsername
        End Get
        Set(ByVal value As String)
            _sUsername = value
        End Set
    End Property

    Property FirstName As String
        Get
            Return _sFirstName
        End Get
        Set(ByVal value As String)
            _sFirstName = value
        End Set
    End Property

    Property LastName As String
        Get
            Return _sLastName
        End Get
        Set(ByVal value As String)
            _sLastName = value
        End Set
    End Property

    Property UserID As String
        Get
            Return _sUserID
        End Get
        Set(ByVal value As String)
            _sUserID = value
        End Set
    End Property

    Property ProfilePicturePath As String
        Get
            Return _sProfilePicturePath
        End Get
        Set(ByVal value As String)
            _sProfilePicturePath = value
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
#End Region

#Region "Constructors"
    Sub New(ByVal sConnection As String)
        Connection = sConnection
    End Sub

    Sub New(ByVal sConnection As String, ByVal sUsername As String, ByVal sFirstName As String, ByVal sLastName As String)
        Connection = sConnection
        Username = sUsername
        FirstName = sFirstName
        LastName = sLastName
    End Sub

    Sub New(ByVal sConnection As String, ByVal sUserID As String)
        Connection = sConnection
        UserID = sUserID
    End Sub

#End Region

    'TODO
#Region "Validation"

#End Region

    'TODO
#Region "Add/Delete/Update/Search Profiles"

#End Region


End Class
