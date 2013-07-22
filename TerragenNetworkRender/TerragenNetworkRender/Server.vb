Imports System.Text

Imports System.IO
Imports System.Diagnostics
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.ComponentModel
Public Class Server


    Dim listener As New System.Net.Sockets.TcpListener(12337)
    Dim Filelistener As New System.Net.Sockets.TcpListener(12339)
    Dim tcpClient As New System.Net.Sockets.TcpClient
    Dim stream As NetworkStream
    Dim bytes(tcpClient.ReceiveBufferSize) As Byte
    Dim clientdata As String
    Dim ReceivedFile As Boolean = False
    Dim ReceivedNumber As String
    Dim cwriter As BinaryWriter
    Dim fs, socketstream, sreader
    Dim output As NetworkStream
    Dim outputstring As String
    Dim response As String
    Dim sendBytes As [Byte]()

    Private main As MainFrame
    Private ip As String
    Private startDate As Date

    Private serverListener As Timer
    Private durationTimer As Timer
    Private ReceiveFileTimer As Timer
    Private ReceiveFile As BackgroundWorker

    Private project As RenderProject


    Sub New(ByRef mainFrame As MainFrame, ByVal myIp As String)
        Me.main = mainFrame
        Me.ip = myIp

        serverListener = New Timer()
        serverListener.Interval = 3000
        AddHandler serverListener.Tick, AddressOf serverListener_Tick

        durationTimer = New Timer()
        durationTimer.Interval = 1000
        AddHandler durationTimer.Tick, AddressOf durationTimer_Tick

        ReceiveFileTimer = New Timer()
        ReceiveFileTimer.Interval = 3000
        AddHandler ReceiveFileTimer.Tick, AddressOf ReceiveFileTimer_Tick

        ReceiveFile = New BackgroundWorker()
        AddHandler ReceiveFile.DoWork, AddressOf ReceiveFile_DoWork
        AddHandler ReceiveFile.RunWorkerCompleted, AddressOf ReceiveFile_RunWorkerCompleted
    End Sub

    Public Sub startServer(ByRef project As RenderProject)
        Me.project = project

        If (isFinished()) Then
            finishProject()
            main.addLineToConsole("Project has been finished")
            Return
        End If

        listener.Start()
        Filelistener.Start()

        serverListener.Start()
        durationTimer.Start()

        ReceiveFileTimer.Start()

        main.UpdateStatusList()
        main.UpdateProgressBar()

        startDate = DateAndTime.Now
        main.addLineToConsole("Server was started")

    End Sub

    Public Sub stopServer()
        listener.Stop()
        Filelistener.Stop()

        serverListener.Stop()
        durationTimer.Stop()

        ReceiveFileTimer.Stop()
        main.addLineToConsole("Server was stopped")
    End Sub


    Private Sub serverListener_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim senderIp As String

        Try

            If listener.Pending Then
                tcpClient = listener.AcceptTcpClient()

                stream = tcpClient.GetStream()

                stream.Read(bytes, 0, CInt(tcpClient.ReceiveBufferSize))
                clientdata = Encoding.ASCII.GetString(bytes)
                clientdata = clientdata.Substring(0, clientdata.IndexOf("!"))
                senderIp = clientdata.Substring(clientdata.IndexOf("_") + 1)

                If clientdata.Contains("ineedserver") Then

                    outputstring = "Client " + senderIp + " pinging the server"
                    main.addLineToConsole(outputstring)
                    response = ip
                ElseIf clientdata.Contains("needfile") Then
                    main.addLineToConsole("File Requested by " + senderIp)
                    response = "itscoming"
                Else
                    main.addLineToConsole("unknown command:" + clientdata)
                End If

                sendBytes = Encoding.ASCII.GetBytes(response)
                stream.Write(sendBytes, 0, sendBytes.Length)

                If response = "itscoming" Then
                    SendNextFile(senderIp)
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub



    Private Sub SendNextFile(ByVal senderIp As String)
        Dim Subnumber As String
        Dim jobList As List(Of RenderJob) = project.JobList.ItemList

        For i = 0 To jobList.Count - 1
            If jobList.Item(i).Status = "Untouched" Then
                Subnumber = jobList.Item(i).Number

                Dim subPath As String = Subnumber + ".tgd"
                Dim fullPath As String = project.Savepath.Substring(0, project.Savepath.Length - 4) + subPath

                File.WriteAllLines(fullPath, jobList.Item(i).SourceCode.ToArray)
                Sendfile(fullPath, subPath, senderIp)
                If File.Exists(fullPath) Then
                    File.Delete(fullPath)
                End If

                jobList.Item(i).Status = "Working"
                Exit For
            End If
        Next

        main.UpdateStatusList()

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
            main.addLineToConsole("File sent to " + fileIP)
        Catch ex As Exception
            Ergebnis = False
        End Try

        Return Ergebnis

    End Function





    Private Sub ReceiveFile_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim counter As Integer = 1
        Dim connection As Socket
        ReceivedFile = False
        Try

            connection = Filelistener.AcceptSocket()
            socketstream = New NetworkStream(connection)
            sreader = New BinaryReader(socketstream)

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

            Finally

                sreader.Close()
                socketstream.Close()
                connection.Close()

            End Try

        Finally

        End Try

        If (isFinished()) Then
            finishProject()
        End If

    End Sub

    Private Sub finishProject()
        PutPicTogether()
        stopServer()
    End Sub

    Private Function isFinished() As Boolean
        Dim help As Integer = 0
        Dim items As List(Of RenderJob) = project.JobList.ItemList

        For i = 0 To items.Count - 1
            If items.Item(i).Status = "Finished" Then
                help += 1
            End If
        Next
        If help = items.Count Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ReceiveFileTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If ReceivedFile = True Then
            main.addLineToConsole("File Received")
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
        Dim bm
        Dim Weite = project.Width
        Dim Höhe = project.Height
        Dim Savepath = project.Savepath
        Dim thumb As New Bitmap(Weite, Höhe)
        Dim g As Graphics = Graphics.FromImage(thumb)
        Dim items = project.JobList.ItemList

        For i = 0 To items.Count - 1
            Dim item = items(i)

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
        main.setPreviewPicture(Image.FromFile(fullPath))
    End Sub


    Private Sub ReceiveFile_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        main.UpdateProgressBar()
    End Sub

    Private Sub durationTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim passedSecs As Long = DateAndTime.DateDiff(DateInterval.Second, startDate, DateAndTime.Now)
        Dim h, m, s As Integer
        h = (passedSecs - (passedSecs Mod 3600)) / 3600
        passedSecs = passedSecs - 3600 * h
        m = (passedSecs - (passedSecs Mod 60)) / 60
        s = passedSecs - m * 60

        main.setDurationLabel("Duration: " + h.ToString + "h " + m.ToString + "m " + s.ToString + "s")
    End Sub

End Class
