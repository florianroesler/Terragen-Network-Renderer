Imports System.Net.Sockets
Imports System.Text
Imports System.IO
Imports System.ComponentModel
Public Class Client

    Private main As MainFrame
    Private receiveFileWorker As BackgroundWorker
    Private ReceivedFile As Boolean = False
    Dim listener As New System.Net.Sockets.TcpListener(12337)
    Dim Filelistener As New System.Net.Sockets.TcpListener(12338)
    Private fileReceivedTimer As Timer

    Dim FileName As String

    Dim IP As String
    Dim serverip As String

    Private renderWorker As RenderWorker

    Public Sub New(ByRef mainFrame As MainFrame)
        main = mainFrame
        renderWorker = New RenderWorker(mainFrame, Me)

        getIpAdress()

        Filelistener.Start()

        fileReceivedTimer = New Timer
        fileReceivedTimer.Interval = 1000
        AddHandler fileReceivedTimer.Tick, AddressOf Me.fileReceived_Tick
        receiveFileWorker = New BackgroundWorker()
        AddHandler receiveFileWorker.DoWork, AddressOf Me.receiveFileWorker_DoWork
    End Sub

    Public Function Sendfile(ByVal FilePath As String, ByVal Filename As String, ByVal fileIP As String) As Boolean
        Dim Ergebnis As Boolean = True
        Dim output As NetworkStream
        Dim cwriter As BinaryWriter
        Try
            Dim fn As String = Filename
            Dim fs As New FileStream(FilePath, FileMode.Open)
            Dim b(fs.Length - 1) As Byte
            Dim client As TcpClient
            client = New TcpClient()
            client.Connect(fileIP, 12339)
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



    Private Sub receiveFileWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

        ReceivedFile = False
        Try
            Dim Socketstream, sreader
            Dim connection = Filelistener.AcceptSocket()
            Socketstream = New NetworkStream(connection)
            sreader = New BinaryReader(Socketstream)

            Dim theRequest As String = ""

            Try
                Dim fn As String = sreader.ReadString()
                Dim l As Integer = sreader.ReadInt32()
                Dim b() As Byte = sreader.ReadBytes(l)
                Dim fs As New FileStream(main.appPath & fn, FileMode.Create, _
                  FileAccess.Write, FileShare.None)
                FileName = fn
                fs.Write(b, 0, l)
                fs.Close()
                ReceivedFile = True
            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                sreader.Close()
                Socketstream.Close()
                connection.Close()
            End Try

        Finally

        End Try

    End Sub

    Public Sub RequestTask()
        If sendmessage(serverip, "needfile_" + IP + "!").Contains("itscoming") Then
            fileReceivedTimer.Start()
            main.addLineToConsole("File Transfer initiated")
        End If
    End Sub

    Public Function sendmessage(ByVal sendIP As String, ByVal Message As String) As String

        Dim sender As New System.Net.Sockets.TcpClient
        Dim bytes(sender.ReceiveBufferSize) As Byte
        Dim stream As NetworkStream
        Dim sendBytes As [Byte]()
        Dim returndata As String
        Try

            sender.Connect(sendIP, 12337)
            stream = sender.GetStream()
            sendBytes = Encoding.ASCII.GetBytes(Message)
            stream.Write(sendBytes, 0, sendBytes.Length)

            stream.Read(bytes, 0, CInt(sender.ReceiveBufferSize))
            ' Return the data received from the client to the console.
            returndata = Encoding.ASCII.GetString(bytes)

            sender.Close()

        Catch ex As Exception
            Return "error"

        End Try
        Return returndata
    End Function

    Private Sub fileReceived_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ReceivedFile = True Then
            main.addLineToConsole("File Received")
            renderWorker.StartRendering(FileName)
            main.addLineToConsole("Rendering Started")
            ReceivedFile = False
            fileReceivedTimer.Stop()
        End If
        If receiveFileWorker.IsBusy = False Then
            receiveFileWorker.RunWorkerAsync()
        End If

    End Sub

    Private Sub getIpAdress()
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
    End Sub


    Property targetIp As String
        Set(ByVal value As String)
            serverip = value
        End Set
        Get
            Return serverip
        End Get
    End Property

    Property clientIp As String
        Set(ByVal value As String)
            IP = value
        End Set
        Get
            Return IP
        End Get
    End Property
End Class
