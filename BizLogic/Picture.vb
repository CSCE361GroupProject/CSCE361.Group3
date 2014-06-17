Public Class Picture
#Region "Private Variables"
    Private _sConnection As String
    Private _sGeoData As String
    Private _sCaption As String
    Private _sPictureID As String
    Private _dTime As Date
    Private _lCommentList As List(Of String)
    Private _sUserID As String
    Private _sImagePath As String
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

    Property GeoData As String
        Get
            Return _sGeoData
        End Get
        Set(ByVal value As String)
            _sGeoData = value
        End Set
    End Property

    Property Caption As String
        Get
            Return _sCaption
        End Get
        Set(ByVal value As String)
            _sCaption = value
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

    Property UserID As String
        Get
            Return _sUserID
        End Get
        Set(ByVal value As String)
            _sUserID = value
        End Set
    End Property

    Property CommentList As List(Of String)
        Get
            Return _lCommentList
        End Get
        Set(ByVal value As List(Of String))
            _lCommentList = value
        End Set
    End Property

    Property ImagePath As String
        Get
            Return _sImagePath
        End Get
        Set(ByVal value As String)
            _sImagePath = value
        End Set
    End Property

#End Region

#Region "Constructors"
    Sub New(ByVal sConnection As String)
        Connection = sConnection
    End Sub

    Sub New(ByVal sConnection As String, ByVal sGeoData As String, ByVal sCaption As String, ByVal dTime As Date, ByVal sUserID As String, ByVal sImagePath As String)
        Connection = sConnection
        GeoData = sGeoData
        Caption = sCaption
        DateTime = dTime
        UserID = sUserID
        ImagePath = sImagePath
    End Sub

#End Region

    'TODO
#Region "Validation"

#End Region

    'TODO
#Region "Add/Delete/Update/Search Pictures"

#End Region
End Class
