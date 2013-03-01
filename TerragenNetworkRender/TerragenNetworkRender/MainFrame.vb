Imports System.Text
Imports System.Net.Sockets
Imports System.IO
Imports System.Diagnostics
Imports System.Net.NetworkInformation


Public Class MainFrame

    Dim IP As String
    Dim listener As New System.Net.Sockets.TcpListener(12337)
    Dim Filelistener As New System.Net.Sockets.TcpListener(12339)
    Dim tcpClient As New System.Net.Sockets.TcpClient
    Dim stream As NetworkStream
    Dim bytes(tcpClient.ReceiveBufferSize) As Byte
    Dim clientdata As String
    Dim response As String
    Dim sendBytes As [Byte]()
    Dim clientip As String
    Dim Renderline As Integer
    Dim cwriter As BinaryWriter
    Dim fs, socketstream, sreader
    Dim output As NetworkStream
    Dim outputstring As String
    Dim help As String
    Dim ReceivedFile As Boolean = False
    Dim ReceivedNumber As String
    Dim project As RenderProject
    Dim startDate As Date


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            End If
        End If

        Console.Items.Add("Your IP is: " + IP.ToString)
        Filelistener.Start()
        Timer2.Start()


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try

            If listener.Pending Then
                tcpClient = listener.AcceptTcpClient()


                stream = tcpClient.GetStream()

                stream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
                clientdata = Encoding.ASCII.GetString(bytes)
                clientdata = clientdata.Substring(0, clientdata.IndexOf("!"))
                If clientdata.Contains("ineedserver") Then
                    clientip = clientdata.Substring(clientdata.IndexOf("_") + 1)
                    outputstring = "Client " + clientip + " ringing the doorbell"
                    Console.Items.Add(outputstring)
                    response = IP
                ElseIf clientdata.Contains("needfile") Then

                    clientip = clientdata.Substring(clientdata.IndexOf("_") + 1)
                    response = "itscoming"
                Else
                    Console.Items.Add("unknown command:" + clientdata)
                End If

                sendBytes = Encoding.ASCII.GetBytes(response)
                stream.Write(sendBytes, 0, sendBytes.Length)

                If response = "itscoming" Then
                    Console.Items.Add("File Requested by " + clientip)
                    SendNextFile(clientip)
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SendNextFile(ByVal clientip As String)
        Dim Subnumber As String
        Dim jobList As List(Of RenderJob) = project.JobList.ItemList

        For i = 0 To jobList.Count - 1
            If jobList.Item(i).Status = "Untouched" Then
                Subnumber = jobList.Item(i).Number

                Dim fileDir As String = Directory.GetParent(project.Savepath).FullName
                File.WriteAllLines(project.Savepath.Substring(0, project.Savepath.Length - 4) + "_" + Subnumber + ".tgd", jobList.Item(i).SourceCode.ToArray)
                Sendfile(project.Savepath.Substring(0, project.Savepath.Length - 4) + "_" + Subnumber + ".tgd", Subnumber + ".tgd", clientip)
                If File.Exists(project.Savepath.Substring(0, project.Savepath.Length - 4) + "_" + Subnumber + ".tgd") Then
                    File.Delete(project.Savepath.Substring(0, project.Savepath.Length - 4) + "_" + Subnumber + ".tgd")
                End If

                jobList.Item(i).Status = "Working"
                Exit For
            End If
        Next

        UpdateStatusList()

    End Sub



    Private Sub UpdateStatusList()
        DataGridView1.Refresh()
        DataGridView1.AutoResizeColumns()
    End Sub

    Public Function Sendfile(ByVal FilePath As String, ByVal Filename As String, ByVal fileIP As String) As Boolean
        Dim Ergebnis As Boolean = True

        Try
            Dim fn As String = Filename
            Dim fs As New FileStream(FilePath, FileMode.Open)
            Dim b(fs.Length - 1) As Byte
            Dim client As TcpClient
            client = New TcpClient()
            client.Connect(fileIP, 12338)
            output = client.GetStream()
            cwriter = New BinaryWriter(output)
            fs.Read(b, 0, b.Length)
            fs.Close()
            cwriter.Write(fn)
            cwriter.Write(CInt(b.Length))
            cwriter.Write(b)
            client.Close()
            Console.Items.Add("File sent to " + fileIP)
        Catch ex As Exception
            Ergebnis = False
        End Try

        Return Ergebnis

    End Function



    Private Sub ReceiveFile_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles ReceiveFile.DoWork
        Dim counter As Integer = 1
        Dim connection As Socket
        Receivedfile = False
        Try

            connection = Filelistener.AcceptSocket()
            socketstream = New NetworkStream(connection)
            sreader = New BinaryReader(Socketstream)

            Dim theRequest As String = ""

            Try
                Dim fn As String = sreader.ReadString()
                Dim l As Integer = sreader.ReadInt32()
                Dim b() As Byte = sreader.ReadBytes(l)
                ReceivedNumber = fn.Substring(0, fn.IndexOf("."))
                Dim ReceivedFileName = (project.Savepath.Substring(0, project.Savepath.LastIndexOf("\") + 1) & fn)
                Dim fs As New FileStream(ReceivedFileName, FileMode.Create, _
                  FileAccess.Write, FileShare.None)
                fs.Write(b, 0, l)
                fs.Close()
                Dim item = project.JobList.ItemList.Item(ReceivedNumber - 1)
                ReceivedFile = True
                item.Status = "Finished"
                Dim bm = New Bitmap(ReceivedFileName)

                Dim Weite = project.Width
                Dim Höhe = project.Height

                Dim Left = Math.Round(item.CropLeftValue * Weite, 0)

                Dim Right = Math.Round(item.CropRightValue * Weite, 0)

                Dim Bottom = Math.Round(item.CropBottomValue * Höhe, 0)

                Dim Top = Math.Round(item.CropTopValue * Höhe, 0)
                item.Picture = CropBitmap(bm, Left, Höhe - Top, Right - Left, Top - Bottom)
                bm.Dispose()
                If File.Exists(ReceivedFileName) Then
                    File.Delete(ReceivedFileName)
                End If

                CheckProcessFile()
            Finally

                sreader.Close()
                socketstream.Close()
                'nach msdn erst shutdown und dann close
                'connection.Shutdown(SocketShutdown.Both)
                connection.Close()


            End Try

        Finally

        End Try

    End Sub



    Private Sub CheckProcessFile()
        Dim help As Integer = 0
        Dim items As List(Of RenderJob) = project.JobList.ItemList

        For i = 0 To items.Count - 1
            If items.Item(i).Status = "Finished" Then
                help += 1
            End If
        Next
        If help = items.Count Then

            PutPicTogether()
        End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        If ReceivedFile = True Then
            Console.Items.Add("File Received")
            ReceivedFile = False
            Dim manager As New ProjectManager
            manager.SaveProject(project.Savepath, project)
        End If
        If ReceiveFile.IsBusy = False Then
            ReceiveFile.RunWorkerAsync()
        End If
    End Sub

    Private Function CropBitmap(ByRef bmp As Bitmap, ByVal cropX As Integer, ByVal cropY As Integer, ByVal cropWidth As Integer, ByVal cropHeight As Integer) As Bitmap
        Dim rect As New Rectangle(cropX, cropY, cropWidth, cropHeight)
        Dim cropped As Bitmap = bmp.Clone(rect, bmp.PixelFormat)
        Return cropped
    End Function


    Private Sub PutPicTogether()

        Dim left, right, bottom, top As Single
        Dim Zahl As Integer
        Dim bm
        Dim Weite = project.Width
        Dim Höhe = project.Height
        Dim Savepath = project.Savepath
        Dim thumb As New Bitmap(Weite, Höhe)
        Dim g As Graphics = Graphics.FromImage(thumb)
        Dim items = project.JobList.ItemList

        For i = 0 To items.Count - 1
            Dim item = items(i)
            Zahl = i + 1
            'bm = New Bitmap(Savepath.Substring(0, Savepath.LastIndexOf("\") + 1) + "cropped" + Zahl.ToString + ".bmp")
            bm = item.Picture

            left = Math.Round(item.CropLeftValue * Weite, 0)

            right = Math.Round(item.CropRightValue * Weite, 0)

            bottom = Math.Round(item.CropBottomValue * Höhe, 0)

            top = Math.Round(item.CropTopValue * Höhe, 0)

            g.DrawImage(bm, left, Höhe - top, bm.Width, bm.Height)
        Next


        g.Dispose()
        Dim fullPath As String
        fullPath = Savepath.Substring(0, Savepath.LastIndexOf("\") + 1) + "finished.bmp"
        thumb.Save(fullPath, System.Drawing.Imaging.ImageFormat.Bmp)

        thumb.Dispose()

        durationTimer.Stop()
        previewBox.Image = Image.FromFile(fullPath)
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


    Private Sub ReceiveFile_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ReceiveFile.RunWorkerCompleted
        UpdateProgressBar()
    End Sub

    Private Sub UpdateProgressBar()
        Dim help As Integer = 0
        Dim items As List(Of RenderJob) = project.JobList.ItemList

        For i = 0 To items.Count - 1
            If items.Item(i).Status = "Finished" Then
                help += 1
            End If
        Next
        ProgressBar1.Value = help / items.Count * 100
    End Sub


    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles durationTimer.Tick
        Dim passedSecs As Long = DateAndTime.DateDiff(DateInterval.Second, startDate, DateAndTime.Now)
        Dim h, m, s As Integer
        h = (passedSecs - (passedSecs Mod 3600)) / 3600
        passedSecs = passedSecs - 3600 * h
        m = (passedSecs - (passedSecs Mod 60)) / 60
        s = passedSecs - m * 60

        durationlabel.Text = "Duration: " + h.ToString + "h " + m.ToString + "m " + s.ToString + "s"
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

            listener.Start()
            Timer1.Start()
            Console.Items.Add("Server was started")
            UpdateStatusList()
            UpdateProgressBar()
            startDate = DateAndTime.Now
            durationTimer.Start()
        Else
            MsgBox("No Project selected.")
        End If
    End Sub

    Private Sub StopServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopServerToolStripMenuItem.Click

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
