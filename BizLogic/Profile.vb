Public Class Profile

#Region "Private Variables"
    Private _sConnection As String
    Private _sUsername As String
    Private _sName As String
    Private _sProfileID As String
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

    Property Name As String
        Get
            Return _sName
        End Get
        Set(ByVal value As String)
            _sName = value
        End Set
    End Property

    Property ProfileID As String
        Get
            Return _sProfileID
        End Get
        Set(ByVal value As String)
            _sProfileID = value
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

    Sub New(ByVal sConnection As String, ByVal sUsername As String, ByVal sName As String)
        Connection = sConnection
        Username = sUsername
        Name = sName
    End Sub

    Sub New(ByVal sConnection As String, ByVal sProfileID As String)
        Connection = sConnection
        ProfileID = sProfileID
    End Sub

#End Region

    'TODO
#Region "Validation"

#End Region

    'TODO
#Region "Add/Delete/Update/Search Profiles"

#End Region


End Class
