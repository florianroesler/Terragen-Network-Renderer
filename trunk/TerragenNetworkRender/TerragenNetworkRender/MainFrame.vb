
Imports System.Net.Sockets


Public Class MainFrame

    Dim IP As String
    Dim project As RenderProject

    Private server As Server

    Private Sub MainMenu_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        getIpAddress()

        server = New Server(Me, IP)

        theConsole.Items.Add("Your IP is: " + IP.ToString)

    End Sub

    Private Sub getIpAddress()
        Dim IPtemp() = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName)
        Dim addresses = New List(Of String)()
        For Each address In IPtemp
            If (address.AddressFamily = AddressFamily.InterNetwork) Then
                addresses.Add(address.ToString)
            End If
        Next
        If (addresses.Count = 0) Then
            MsgBox("Please setup a network connection before using this software.")
            Application.Exit()
        End If
        If (addresses.Count = 1) Then
            IP = addresses(0)
        Else
            Dim ipDialog = New chooseIpDialog(addresses)
            If ipDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                IP = ipDialog.address
            Else
                Application.Exit()
            End If
        End If
    End Sub

    Public Sub addLineToConsole(ByVal text As String)
        theConsole.Items.Add(text)
    End Sub

    Public Sub setPreviewPicture(ByRef img As Image)
        previewBox.Image = img
    End Sub

    Public Sub setDurationLabel(ByVal text As String)
        durationlabel.Text = text
    End Sub

    Public Sub UpdateStatusList()
        DataGridView1.Refresh()
        DataGridView1.AutoResizeColumns()
    End Sub

    Public Sub UpdateProgressBar()
        Dim help As Integer = 0
        Dim items As List(Of RenderJob) = project.JobList.ItemList

        For i = 0 To items.Count - 1
            If items.Item(i).Status = "Finished" Then
                help += 1
            End If
        Next
        ProgressBar1.Value = help / items.Count * 100
    End Sub


    Private Sub CreateNewProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewProjectToolStripMenuItem.Click
        Dim projectDialog As NewProjectDialog
        projectDialog = New NewProjectDialog()
        If projectDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            project = projectDialog.Renderproject
            pathTextbox.Text = project.Savepath
            ItemListBindingSource.DataSource = project.JobList.ItemList
            UpdateStatusList()
        End If

    End Sub



    Private Sub OpenExistingProjectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenExistingProjectToolStripMenuItem.Click
        If OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim manager As ProjectManager = New ProjectManager
            project = manager.OpenProject(OpenFileDialog2.FileName)
            project.Savepath = OpenFileDialog2.FileName
            pathTextbox.Text = project.Savepath
            ItemListBindingSource.DataSource = project.JobList.ItemList
            UpdateStatusList()
        End If
    End Sub


    Private Sub StartServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartServerToolStripMenuItem.Click
        If project IsNot Nothing Then
            server.startServer(project)
        Else
            MsgBox("No Project selected.")
        End If
    End Sub

    Private Sub StopServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopServerToolStripMenuItem.Click
        server.stopServer()
    End Sub


    Private Sub reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles reset.Click
        Dim selected = DataGridView1.SelectedRows

        If selected.Count < 1 Then
            MsgBox("No rows selected!")
        Else
            MsgBox("Do you really want to reset the progress for the selected rows?", MsgBoxStyle.YesNo)
            If MsgBoxResult.Yes Then

                For Each item As DataGridViewRow In selected
                    project.JobList.ItemList.Item(item.Index).Status = "Untouched"
                Next

            End If

        End If
        UpdateStatusList()
    End Sub

    Private Sub processorLabelTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles processorLabelTimer.Tick
        processorTimeLabel.Text = "Processor Time: " + Math.Round(processorMeasure.NextValue, 1).ToString + "%"

    End Sub


End Class
