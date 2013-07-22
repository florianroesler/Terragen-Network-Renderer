Public Class RenderWorker

    Private main As MainFrame
    Dim renderingProcess As New Process
    Dim Picname As String
    Private client As Client
    Private terragenActivityCheck As Timer

    Public Sub New(ByRef mainFrame As MainFrame, ByRef theClient As Client)
        main = mainFrame
        client = theClient
        terragenActivityCheck = New Timer()
        terragenActivityCheck.Interval = 1000
        AddHandler terragenActivityCheck.Tick, AddressOf terragenActivityCheck_Tick
    End Sub

    Public Sub StartRendering(ByVal FileName As String)
        Dim appPath = main.appPath
        Dim terragenPath = main.terragenPath
        Picname = FileName.Substring(0, FileName.IndexOf(".")) + ".bmp"
        main.addLineToConsole(terragenPath + " -p " + appPath + FileName + " -hide -exit -r -o " + appPath + Picname)
        renderingProcess.StartInfo.FileName = terragenPath
        renderingProcess.StartInfo.Arguments = "-p " + appPath + FileName + " -hide -exit -r -o " + appPath + Picname
        renderingProcess.Start()
        terragenActivityCheck.Start()

    End Sub

    Private Sub terragenActivityCheck_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If renderingProcess.HasExited Then
            'Sleep used to be sure that terragen writes all necessary data, otherwise some pictures might be sent unfinished
            Threading.Thread.Sleep(2000)
            main.addLineToConsole("Rendering finished")
            client.Sendfile(main.appPath + Picname, Picname, client.targetIp)
            terragenActivityCheck.Stop()
            client.RequestTask()
        End If

    End Sub
End Class
