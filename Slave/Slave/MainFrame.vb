Imports System.Net.Sockets
Imports Microsoft.Win32
Imports System.Text
Imports System.IO

Public Class MainFrame
    Dim exepath As String
    Dim Ownpath As String
    Private client As Client

    Property appPath As String
        Get
            Return Ownpath
        End Get
        Set(ByVal value As String)
            Ownpath = value
        End Set
    End Property

    Property terragenPath As String
        Get
            Return exepath
        End Get
        Set(ByVal value As String)
            exepath = value
        End Set
    End Property

    Private Sub Main_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        client = New Client(Me)
        checkRegistry()

        Ownpath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) & "\"

        addLineToConsole("Your IP: " + client.clientIp)
        TextBox1.Text = client.clientIp.Substring(0, client.clientIp.LastIndexOf(".") + 1)


    End Sub

    Public Sub addLineToConsole(ByVal text As String)
        console.Items.Add(text)
    End Sub

    Private Sub checkRegistry()
        If Registry.GetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", String.Empty) = "" Then
            MsgBox("This is the first time you start the slave on this machine. Please search for the tgdclie.exe of your Terragen 2 Deep version.")
            If searchexe.ShowDialog = Windows.Forms.DialogResult.OK Then
                exepath = searchexe.FileName
                Registry.SetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", exepath)
                addLineToConsole("Registry key has been set!")
            Else
                MsgBox("Invalid tgdclie.exe! The program will shutdown!")
                Me.Close()
            End If
        Else

            exepath = Registry.GetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", "False")
            If File.Exists(exepath) Then
                addLineToConsole(exepath)
            Else
                MsgBox("The saved filepath in the registry is invalid. Please set the new filepath to your tgdclie.exe.")
                If searchexe.ShowDialog = Windows.Forms.DialogResult.OK Then
                    exepath = searchexe.FileName
                    Registry.SetValue("HKEY_CURRENT_USER\Software\Terragen2", "Path", exepath)
                    addLineToConsole("Registry key has been set!")
                Else
                    MsgBox("Invalid tgdclie.exe! The program will shutdown!")
                    Me.Close()
                End If
            End If
            addLineToConsole(exepath)
        End If
    End Sub

    Private Sub pingServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pingServer.Click
        If client.sendmessage(TextBox1.Text, "ineedserver_" + client.clientIp + "!") <> "error" Then
            client.targetIp = TextBox1.Text
            addLineToConsole(client.targetIp + " is a valid Server.")
        Else
            addLineToConsole(TextBox1.Text + " is not valid Server.")
        End If

    End Sub


    Private Sub start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles startButton.Click
        client.targetIp = TextBox1.Text
        client.RequestTask()
    End Sub

End Class
