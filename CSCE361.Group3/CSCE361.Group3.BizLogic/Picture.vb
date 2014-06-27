Public Class Picture
#Region "Private Variables"
    Private _sLongitude As String
    Private _sLatitude As String
    Private _sCaption As String
    Private _sPictureID As String
    Private _lCommentList As List(Of String)
    Private _sUserID As String
    Private _sImagePath As String
#End Region


#Region "Properties"
    Property Longitude As String
        Get
            Return _sLongitude
        End Get
        Set(ByVal value As String)
            _sLongitude = value
        End Set
    End Property

    Property Latitude As String
        Get
            Return _sLatitude
        End Get
        Set(ByVal value As String)
            _sLatitude = value
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

    'TODO
#Region "Validation"

#End Region

    'TODO
#Region "Add/Delete/Update/Search Pictures"

#End Region
End Class
