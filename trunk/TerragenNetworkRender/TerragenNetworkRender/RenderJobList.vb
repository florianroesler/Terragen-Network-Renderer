<Serializable()> Public Class RenderJobList
    Private renderjoblist As List(Of RenderJob)

    Sub New()
        renderjoblist = New List(Of RenderJob)
    End Sub

    Property ItemList() As List(Of RenderJob)
        Get
            Return renderjoblist
        End Get
        Set(ByVal value As List(Of RenderJob))
            renderjoblist = value
        End Set
    End Property
End Class
