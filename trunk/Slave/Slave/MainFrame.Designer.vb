<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainFrame
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.console = New System.Windows.Forms.ListBox()
        Me.searchexe = New System.Windows.Forms.OpenFileDialog()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.pingServer = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.startButton = New System.Windows.Forms.Button()
        Me.fileReceivedTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ReceiveFile = New System.ComponentModel.BackgroundWorker()
        Me.TerragenWorking = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'console
        '
        Me.console.FormattingEnabled = True
        Me.console.HorizontalScrollbar = True
        Me.console.Location = New System.Drawing.Point(12, 12)
        Me.console.Name = "console"
        Me.console.Size = New System.Drawing.Size(223, 238)
        Me.console.TabIndex = 0
        '
        'searchexe
        '
        Me.searchexe.FileName = "OpenFileDialog1"
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'pingServer
        '
        Me.pingServer.Location = New System.Drawing.Point(19, 46)
        Me.pingServer.Name = "pingServer"
        Me.pingServer.Size = New System.Drawing.Size(134, 23)
        Me.pingServer.TabIndex = 2
        Me.pingServer.Text = "Ping Server"
        Me.pingServer.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(19, 20)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(134, 20)
        Me.TextBox1.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.startButton)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.pingServer)
        Me.GroupBox1.Location = New System.Drawing.Point(258, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(162, 106)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Server"
        '
        'startButton
        '
        Me.startButton.Location = New System.Drawing.Point(19, 75)
        Me.startButton.Name = "startButton"
        Me.startButton.Size = New System.Drawing.Size(134, 23)
        Me.startButton.TabIndex = 4
        Me.startButton.Text = "Start Client"
        Me.startButton.UseVisualStyleBackColor = True
        '
        'fileReceivedTimer
        '
        Me.fileReceivedTimer.Interval = 2000
        '
        'ReceiveFile
        '
        Me.ReceiveFile.WorkerReportsProgress = True
        '
        'TerragenWorking
        '
        Me.TerragenWorking.Interval = 5000
        '
        'MainFrame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 285)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.console)
        Me.Name = "MainFrame"
        Me.Text = "Slave"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents console As System.Windows.Forms.ListBox
    Friend WithEvents searchexe As System.Windows.Forms.OpenFileDialog
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents pingServer As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents startButton As System.Windows.Forms.Button
    Friend WithEvents fileReceivedTimer As System.Windows.Forms.Timer
    Friend WithEvents ReceiveFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents TerragenWorking As System.Windows.Forms.Timer

End Class
