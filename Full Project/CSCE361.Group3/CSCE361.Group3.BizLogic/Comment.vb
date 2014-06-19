Public Class Comment

#Region "Private Variables"
    Private _sConnection As String
    Private _sComment As String
    Private _sPictureID As String
    Private _dTime As Date
    Private _sCommentID As String
    Private _sUserID As String
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
    Property CommentContent As String
        Get
            Return _sComment
        End Get
        Set(ByVal value As String)
            _sComment = value
        End Set
    End Property

    Property PictureID As String
        Get
            Return _sPictureID
        End Get
        Set(ByVal value As String)
            _sPictureID = value
        End Set
    End Property

    Property DateTime As Date
        Get
            Return _dTime
        End Get
        Set(ByVal value As Date)
            _dTime = value
        End Set
    End Property

    Property CommentID As String
        Get
            Return _sCommentID
        End Get
        Set(ByVal value As String)
            _sCommentID = value
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

#End Region

#Region "Constructors"
    Sub New(ByVal sConnection As String)
        Connection = sConnection
    End Sub

    Sub New(ByVal sConnection As String, ByVal sComment As String, ByVal dTime As Date, ByVal sPictureID As String, ByVal sUserID As String)
        Connection = sConnection
        CommentContent = sComment
        DateTime = dTime
        PictureID = sPictureID
        UserID = sUserID
    End Sub
#End Region

    'TODO
#Region "Validation"

#End Region

    'TODO
#Region "Add/Delete/Update/Search Comments"

#End Region


End Class
