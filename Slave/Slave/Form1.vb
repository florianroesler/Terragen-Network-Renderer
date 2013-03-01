Imports System.Net.Sockets
Imports Microsoft.Win32
Imports System.Text
Imports System.IO
Public Class Form1
    Dim exepath As String
    Dim IPtemp() As System.Net.IPAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName)
    Dim IP As String
    Dim listener As New System.Net.Sockets.TcpListener(12337)
    Dim Filelistener As New System.Net.Sockets.TcpListener(12338)
    Dim sender As New System.Net.Sockets.TcpClient
    Dim serverip As String
    Dim shortip As String
    Dim stream As NetworkStream
    Dim sendBytes As [Byte]()
    Dim bytes(sender.ReceiveBufferSize) As Byte
    Dim returndata As String
    Dim Pinger As New System.Net.NetworkInformation.Ping()
    Dim ServerList As New List(Of String)
    Dim Progress As Integer
    Dim TcpClient, output, cwriter
    Dim clientdata As String
    Dim FilePath As String
    Dim Ownpath As String
    Dim Socketstream, sreader
    Dim ReceivedFile As Boolean = False
    Dim p As New Process
    Dim mydrive As String
    Dim FileName As String
    Dim Picname As String


    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Registry.GetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", String.Empty) = "" Then
            MsgBox("This is the first time you start the slave on this machine. Please search for the tgdclie.exe of your Terragen 2 Deep version.")
            If searchexe.ShowDialog = Windows.Forms.DialogResult.OK Then
                exepath = searchexe.FileName
                Registry.SetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", exepath)
                ListBox1.Items.Add("Registry key has been set!")
            Else
                MsgBox("Invalid tgdclie.exe! The program will shutdown!")
                Me.Close()
            End If
        Else
            'ListBox1.Items.Add("Registry Key existiert bereits: ")

            exepath = Registry.GetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", "False")
            If File.Exists(exepath) Then
                ListBox1.Items.Add(exepath)
            Else
                MsgBox("The saved filepath in the registry is invalid. Please set the new filepath to your tgdclie.exe.")
                If searchexe.ShowDialog = Windows.Forms.DialogResult.OK Then
                    exepath = searchexe.FileName
                    Registry.SetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", exepath)
                    ListBox1.Items.Add("Registry key has been set!")
                Else
                    MsgBox("Invalid tgdclie.exe! The program will shutdown!")
                    Me.Close()
                End If
            End If
            ListBox1.Items.Add(exepath)
        End If
        Ownpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) & "\"
        mydrive = System.IO.Path.GetPathRoot(Application.ExecutablePath)
        'Windows 7
        IP = IPtemp(2).ToString

        'Windows XP
        'IP = IPtemp(0).ToString

        ListBox1.Items.Add("Your IP: " + IP)
        shortip = IP.Substring(0, IP.LastIndexOf(".") + 1)

        TextBox1.Text = shortip

        ListBox1.Items.Add("Your Root: " + mydrive)
        Filelistener.Start()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search.Click
        If sendmessage(TextBox1.Text, "ineedserver_" + IP + "!") <> "error" Then
            serverip = TextBox1.Text
            ListBox1.Items.Add(serverip + " is a valid Server.")
        Else
            ListBox1.Items.Add(TextBox1.Text + " is not valid Server.")
        End If

    End Sub

    Private Function sendmessage(ByVal sendIP As String, ByVal Message As String) As String
        Try
            sender = New System.Net.Sockets.TcpClient
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


    Private Sub start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles start.Click
        serverip = TextBox1.Text
        RequestTask()

    End Sub

    Private Sub RequestTask()
        If sendmessage(serverip, "needfile_" + IP + "!").Contains("itscoming") Then
            Timer1.Start()
            ListBox1.Items.Add("File Transfer initiated")
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick


        If ReceivedFile = True Then
            ListBox1.Items.Add("File Received")
            StartRendering()
            ListBox1.Items.Add("Rendering Started")
            ReceivedFile = False
            Timer1.Stop()
        End If
        If ReceiveFile.IsBusy = False Then
            ReceiveFile.RunWorkerAsync()
        End If




    End Sub

    Private Sub ReceiveFile_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles ReceiveFile.DoWork

        Dim counter As Integer = 1
        Dim connection As Socket
        Receivedfile = False
        Try

            connection = Filelistener.AcceptSocket()
            Socketstream = New NetworkStream(connection)
            sreader = New BinaryReader(Socketstream)

            Dim theRequest As String = ""

            Try
                Dim fn As String = sreader.ReadString()
                Dim l As Integer = sreader.ReadInt32()
                Dim b() As Byte = sreader.ReadBytes(l)
                Dim fs As New FileStream(mydrive & fn, FileMode.Create, _
                  FileAccess.Write, FileShare.None)
                FileName = fn
                FilePath = Ownpath & fn
                fs.Write(b, 0, l)
                fs.Close()
                ReceivedFile = True

            Finally

                sreader.Close()
                Socketstream.Close()
                'nach msdn erst shutdown und dann close
                'connection.Shutdown(SocketShutdown.Both)
                connection.Close()


            End Try

        Finally

        End Try

    End Sub

    Private Sub StartRendering()
        Picname = FileName.Substring(0, FileName.IndexOf(".")) + ".bmp"
        ListBox1.Items.Add(exepath + " -p " + mydrive + FileName + " -hide -exit -r -o " + mydrive + Picname)
        p.StartInfo.FileName = exepath
        p.StartInfo.Arguments = "-p " + mydrive + FileName + " -hide -exit -r -o " + mydrive + Picname
        p.Start()
        TerragenWorking.Start()

    End Sub

    Private Sub TerragenWorking_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TerragenWorking.Tick
        If p.HasExited Then
            'Sleep used to be sure that terragen writes all necessary data, otherwise some pictures might be sent unfinished
            Threading.Thread.Sleep(2000)
            ListBox1.Items.Add("Rendering finished")
            TerragenWorking.Stop()
            Sendfile(mydrive + Picname, Picname, serverip)
            TerragenWorking.Stop()
            RequestTask()
        End If

    End Sub

    Public Function Sendfile(ByVal FilePath As String, ByVal Filename As String, ByVal fileIP As String) As Boolean
        Dim Ergebnis As Boolean = True

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
            ListBox1.Items.Add("File sent to " + fileIP)
        Catch ex As Exception
            Ergebnis = False
        End Try

        Return Ergebnis

    End Function

End Class
