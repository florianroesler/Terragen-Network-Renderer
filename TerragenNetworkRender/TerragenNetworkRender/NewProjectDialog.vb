Imports System.Windows.Forms

Public Class NewProjectDialog

    Private Project As RenderProject

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        CreateJobList()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CreateJobList()

        Dim Teilhöhe As Integer = Project.Partheight
        Dim Teilweite As Integer = Project.Partwidth
        Dim cropleft, cropright, croptop, cropbottom As Double
        Dim cropleftstr, croprightstr, croptopstr, cropbottomstr As String

        Dim ProgressText As New List(Of String)
        Dim Anzahl As Integer

        Dim renderjob As RenderJob

        Dim p As Point = New Point(0, 0)

        Anzahl = Project.Picturecount

        Dim Weite = Project.Width
        Dim Höhe = Project.Height
        Dim SavePath As String = Project.Savepath
        Dim DrawWidth, Drawheight As Integer
        For i = 1 To Anzahl
            Dim ModifiedSource As List(Of String) = Project.GetSourceValue

            If p.X + Teilweite >= Weite Then
                DrawWidth = Weite - p.X
            Else
                DrawWidth = Teilweite
            End If

            If p.Y + Teilhöhe >= Höhe Then
                Drawheight = Höhe - p.Y
            Else
                Drawheight = Teilhöhe
            End If

            cropleft = 0 + (p.X / Weite)
            cropright = 0 + (p.X + DrawWidth) / Weite
            cropbottom = 0 + (p.Y / Höhe)
            croptop = 0 + (p.Y + Drawheight) / Höhe

            If p.X + DrawWidth = Weite Then
                p.X = 0
                p.Y = p.Y + Teilhöhe
            Else
                p.X = p.X + Teilweite
            End If

            cropleftstr = cropleft.ToString
            croprightstr = cropright.ToString
            cropbottomstr = cropbottom.ToString
            croptopstr = croptop.ToString
            cropleftstr = Replace(cropleftstr, ",", ".")
            croprightstr = Replace(croprightstr, ",", ".")
            cropbottomstr = Replace(cropbottomstr, ",", ".")
            croptopstr = Replace(croptopstr, ",", ".")

            renderjob = New RenderJob(i, cropleft, cropright, croptop, cropbottom)

            'ProgressText.Add("Nr_" + i.ToString + ";" + cropleftstr & ";" & croprightstr & ";" & cropbottomstr & ";" & croptopstr & ";" & Chr(34) & "Untouched" & Chr(34))
            Dim cropline = Project.CroplineNumber

            ModifiedSource.Item(cropline) = "do_crop_region = " & Chr(34) & 1 & Chr(34)
            ModifiedSource.Item(cropline + 1) = "crop_left = " & Chr(34) & cropleftstr & Chr(34)
            ModifiedSource.Item(cropline + 2) = "crop_right = " & Chr(34) & croprightstr & Chr(34)
            ModifiedSource.Item(cropline + 3) = "crop_bottom = " & Chr(34) & cropbottomstr & Chr(34)
            ModifiedSource.Item(cropline + 4) = "crop_top = " & Chr(34) & croptopstr & Chr(34)

            renderjob.SourceCode = ModifiedSource
            Project.JobList.ItemList.Add(renderjob)

        Next

        Dim manager As ProjectManager = New ProjectManager
        manager.SaveProject(SavePath, Project)

       
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Path As String

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Path = OpenFileDialog1.FileName

            terragenFilePathText.Text = Path

            If Path.EndsWith(".tgd") Then

                Project = New RenderProject(Path)

                Dim Höhe, Weite As Integer
                Höhe = project.Height
                Weite = project.Width
                partwidthspinner.Maximum = Weite
                partheightspinner.Maximum = Höhe

                Dim Teiler As Integer = ggT(Weite, Höhe)
                partwidthspinner.Value = Teiler
                partheightspinner.Value = Teiler
                project.Partheight = Teiler
                project.Partwidth = Teiler

                project.Picturecount = (Weite / Teiler) * (Höhe / Teiler)
                Anzahllabel.Text = "Anzahl Teilbilder: " & project.Picturecount


            Else
                MsgBox("Keine Terragen2-Datei ausgewählt")
            End If

        End If
    End Sub


    Private Sub UpdateLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateLabel.Click
        project.Partheight = partheightspinner.Value
        Project.Partwidth = partwidthspinner.Value
        Dim picCount As Integer = Bildzahl(Project.Width, Project.Height, partwidthspinner.Value, partheightspinner.Value)
        Anzahllabel.Text = "Anzahl Teilbilder: " & picCount.ToString
        Project.Picturecount = picCount
    End Sub

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            project.Savepath = SaveFileDialog1.FileName
            TextBox1.Text = project.Savepath
        End If
    End Sub



    Public Function ggT(ByVal Zahl1 As Integer, ByVal Zahl2 As Integer) As Integer
        Dim Result As Integer = Zahl1 Mod Zahl2

        While Result <> 0
            Zahl1 = Zahl2 ' Diese Beiden Zuweisungen ersetzen den Rekursiven Aufruf
            Zahl2 = Result
            Result = Zahl1 Mod Zahl2
        End While
        Return Zahl2
    End Function


    Public Function Bildzahl(ByVal Weite As Integer, ByVal Höhe As Integer, ByVal Teilweite As Integer, ByVal Teilhöhe As Integer) As Integer
        Dim Weitehelp, Höhehelp As Integer
        If Teilweite > 0 And Teilhöhe > 0 Then


            If Weite Mod Teilweite <> 0 Then
                Weitehelp = (Weite / (Teilweite + Weite Mod Teilweite)) + 1
            Else
                Weitehelp = Weite / Teilweite
            End If

            If Höhe Mod Teilhöhe <> 0 Then
                Höhehelp = (Höhe / (Teilhöhe + Höhe Mod Teilhöhe)) + 1
            Else
                Höhehelp = Höhe / Teilhöhe
            End If

            Return Weitehelp * Höhehelp
        End If
        Return 0
    End Function


    Property Renderproject() As RenderProject
        Get
            Return Project
        End Get
        Set(ByVal value As RenderProject)
            Project = value
        End Set
    End Property

End Class
