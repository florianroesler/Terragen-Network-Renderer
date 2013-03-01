<Serializable()> Public Class RenderJob
    Private jobnumber As Integer
    Private StatusString As String
    Private cropleft As Double
    Private cropright As Double
    Private croptop As Double
    Private cropbottom As Double
    Private Source As List(Of String)
    Private pic As Bitmap

    Sub New(ByVal Number As Integer, ByVal CropLeft As Double, ByVal CropRight As Double, ByVal CropTop As Double, ByVal CropBottom As Double)
        jobnumber = Number
        Me.cropleft = CropLeft
        Me.cropright = CropRight
        Me.croptop = CropTop
        Me.cropbottom = CropBottom
        Me.Source = New List(Of String)
        Me.StatusString = "Untouched"

    End Sub

    Property Picture() As Bitmap
        Get
            If pic IsNot Nothing Then
                Return pic
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Bitmap)
            pic = value
        End Set
    End Property

    Property SourceCode() As List(Of String)
        Get
            Return Source
        End Get
        Set(ByVal value As List(Of String))
            Source = value
        End Set
    End Property

    Property Number() As Integer
        Get
            Return jobnumber
        End Get
        Set(ByVal value As Integer)
            jobnumber = value
        End Set
    End Property

    Property Status() As String
        Get
            Return StatusString
        End Get
        Set(ByVal value As String)
            StatusString = value
        End Set
    End Property

    Property CropLeftValue() As Double
        Get
            Return cropleft
        End Get
        Set(ByVal value As Double)
            cropleft = value
        End Set
    End Property

    Property CropRightValue() As Double
        Get
            Return cropright
        End Get
        Set(ByVal value As Double)
            cropright = value
        End Set
    End Property

    Property CropTopValue() As Double
        Get
            Return croptop
        End Get
        Set(ByVal value As Double)
            croptop = value
        End Set
    End Property

    Property CropBottomValue() As Double
        Get
            Return cropbottom
        End Get
        Set(ByVal value As Double)
            cropbottom = value
        End Set
    End Property


End Class
