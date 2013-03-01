<Serializable()> Public Class RenderProject

    Private SourceCode As List(Of String)
    Private fileInfo As List(Of String)
    Private RenderLine As Integer
    Private cropline As Integer
    Private Weite As Integer = 0
    Private Höhe As Integer = 0
    Private Teilweite As Integer = 0
    Private Teilhöhe As Integer = 0
    Private PicCount As Integer = 0
    Private SavePathString As String
    Private progress As RenderJobList

    Sub New(ByVal Path As String)
        Dim Rendername As String
        Dim Anfang, Ende As Integer
        Dim Master As String
        Dim Masterline As Integer

        progress = New RenderJobList()

        SourceCode = New List(Of String)
        fileInfo = New List(Of String)
        If IO.File.Exists(Path) Then SourceCode.AddRange(IO.File.ReadAllLines(Path, System.Text.Encoding.Default))

        fileInfo.Add(Path)

        For i = 0 To SourceCode.Count - 1
            If SourceCode(i).Trim() = "<render" Then
                RenderLine = i
                Rendername = SourceCode(RenderLine + 1)
                Anfang = Rendername.IndexOf(Chr(34))
                Ende = Rendername.LastIndexOf(Chr(34))
                Rendername = Rendername.Substring(Anfang + 1, Ende - Anfang - 1)

                Master = SourceCode(RenderLine + 5)

                If Master.Contains("1") Then
                    Masterline = RenderLine + 5
                    For n = Masterline To Masterline + 50
                        If SourceCode(n).Contains("do_crop_region") Then
                            cropline = n
                            Exit For
                        End If
                    Next
                    fileInfo.Add("MasterRender gefunden: Zeile" & RenderLine & " " & Rendername)
                    fileInfo.Add("")
                    fileInfo.Add("Renderqualität:")
                    fileInfo.Add(SourceCode(RenderLine + 6).Trim)
                    Anfang = SourceCode(RenderLine + 6).IndexOf(Chr(34))
                    Ende = SourceCode(RenderLine + 6).LastIndexOf(Chr(34))
                    Weite = SourceCode(RenderLine + 6).Substring(Anfang + 1, Ende - Anfang - 1)
                    fileInfo.Add(SourceCode(RenderLine + 8).Trim)
                    Anfang = SourceCode(RenderLine + 8).IndexOf(Chr(34))
                    Ende = SourceCode(RenderLine + 8).LastIndexOf(Chr(34))
                    Höhe = SourceCode(RenderLine + 8).Substring(Anfang + 1, Ende - Anfang - 1)
                    fileInfo.Add(SourceCode(RenderLine + 15).Trim)
                    fileInfo.Add(SourceCode(RenderLine + 16).Trim)
                    fileInfo.Add(SourceCode(RenderLine + 17).Trim)
                    fileInfo.Add(SourceCode(RenderLine + 18).Trim)
                    fileInfo.Add(SourceCode(RenderLine + 19).Trim)
                    fileInfo.Add(SourceCode(RenderLine + 20).Trim)

                End If
            End If
        Next
    End Sub

    Public Function GetSourceValue() As List(Of String)
        Dim list As List(Of String) = New List(Of String)

        For i = 0 To SourceCode.Count - 1
            list.Add(SourceCode.Item(i))
        Next

        Return list
    End Function

    Property JobList() As RenderJobList
        Get
            Return progress
        End Get
        Set(ByVal value As RenderJobList)
            progress = value
        End Set
    End Property

    Property Picturecount() As Integer
        Get
            Return PicCount
        End Get
        Set(ByVal value As Integer)
            PicCount = value
        End Set
    End Property
    Property Width() As Integer
        Get
            Return Weite
        End Get
        Set(ByVal value As Integer)
            Weite = value
        End Set
    End Property

    Property Height() As Integer
        Get
            Return Höhe
        End Get
        Set(ByVal value As Integer)
            Höhe = value
        End Set
    End Property

    Property Partwidth() As Integer
        Get
            Return Teilweite
        End Get
        Set(ByVal value As Integer)
            Teilweite = value
        End Set
    End Property

    Property Partheight() As Integer
        Get
            Return Teilhöhe
        End Get
        Set(ByVal value As Integer)
            Teilhöhe = value
        End Set
    End Property

    Property Savepath() As String
        Get
            Return SavePathString
        End Get
        Set(ByVal value As String)
            SavePathString = value
        End Set
    End Property

    Property CroplineNumber() As Integer
        Get
            Return cropline
        End Get
        Set(ByVal value As Integer)
            cropline = value
        End Set
    End Property

    Property Source() As List(Of String)
        Get
            Return SourceCode
        End Get
        Set(ByVal value As List(Of String))
            SourceCode = value
        End Set
    End Property
End Class
