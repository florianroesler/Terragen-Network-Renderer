Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class ProjectManager

    Public Sub SaveProject(ByVal FilePath As String, ByRef project As RenderProject)
        Dim TestFileStream As Stream = File.Create(FilePath)
        Dim serializer As New BinaryFormatter
        serializer.Serialize(TestFileStream, project)
        TestFileStream.Close()
    End Sub

    Public Function OpenProject(ByVal FilePath As String) As RenderProject


        If File.Exists(FilePath) Then
            Dim project As RenderProject
            Dim TestFileStream As Stream = File.OpenRead(FilePath)
            Dim deserializer As New BinaryFormatter
            project = CType(deserializer.Deserialize(TestFileStream), RenderProject)
            TestFileStream.Close()
            Return project
        End If

        Return Nothing

    End Function

End Class
