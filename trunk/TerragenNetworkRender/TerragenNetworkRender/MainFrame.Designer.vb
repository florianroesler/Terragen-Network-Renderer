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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainFrame))
        Me.serverListener = New System.Windows.Forms.Timer(Me.components)
        Me.ReceiveFile = New System.ComponentModel.BackgroundWorker()
        Me.ReceiveFileTimer = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ProjectsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateNewProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenExistingProjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pathTextbox = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.NumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.durationTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.durationlabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.processorTimeLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.theConsole = New System.Windows.Forms.ListBox()
        Me.previewBox = New System.Windows.Forms.PictureBox()
        Me.reset = New System.Windows.Forms.Button()
        Me.processorMeasure = New System.Diagnostics.PerformanceCounter()
        Me.processorLabelTimer = New System.Windows.Forms.Timer(Me.components)
        Me.RenderJobListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RenderProjectBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.previewBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.processorMeasure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RenderJobListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RenderProjectBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'serverListener
        '
        Me.serverListener.Interval = 3000
        '
        'ReceiveFile
        '
        Me.ReceiveFile.WorkerReportsProgress = True
        '
        'ReceiveFileTimer
        '
        Me.ReceiveFileTimer.Interval = 3000
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.Filter = """Tnr-Dateien|*.tnr|Alle Dateien|*.*"""
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProjectsToolStripMenuItem, Me.ServerToolStripMenuItem})
        Me.MenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(759, 24)
        Me.MenuStrip1.TabIndex = 15
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ProjectsToolStripMenuItem
        '
        Me.ProjectsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateNewProjectToolStripMenuItem, Me.OpenExistingProjectToolStripMenuItem})
        Me.ProjectsToolStripMenuItem.Name = "ProjectsToolStripMenuItem"
        Me.ProjectsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ProjectsToolStripMenuItem.Text = "Projects"
        '
        'CreateNewProjectToolStripMenuItem
        '
        Me.CreateNewProjectToolStripMenuItem.Image = CType(resources.GetObject("CreateNewProjectToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CreateNewProjectToolStripMenuItem.Name = "CreateNewProjectToolStripMenuItem"
        Me.CreateNewProjectToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.CreateNewProjectToolStripMenuItem.Text = "Create new Project"
        '
        'OpenExistingProjectToolStripMenuItem
        '
        Me.OpenExistingProjectToolStripMenuItem.Image = CType(resources.GetObject("OpenExistingProjectToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenExistingProjectToolStripMenuItem.Name = "OpenExistingProjectToolStripMenuItem"
        Me.OpenExistingProjectToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.OpenExistingProjectToolStripMenuItem.Text = "Open Existing Project"
        '
        'ServerToolStripMenuItem
        '
        Me.ServerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartServerToolStripMenuItem, Me.StopServerToolStripMenuItem})
        Me.ServerToolStripMenuItem.Name = "ServerToolStripMenuItem"
        Me.ServerToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
        Me.ServerToolStripMenuItem.Text = "Server"
        '
        'StartServerToolStripMenuItem
        '
        Me.StartServerToolStripMenuItem.Image = CType(resources.GetObject("StartServerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.StartServerToolStripMenuItem.Name = "StartServerToolStripMenuItem"
        Me.StartServerToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.StartServerToolStripMenuItem.Text = "Start Server"
        '
        'StopServerToolStripMenuItem
        '
        Me.StopServerToolStripMenuItem.Image = CType(resources.GetObject("StopServerToolStripMenuItem.Image"), System.Drawing.Image)
        Me.StopServerToolStripMenuItem.Name = "StopServerToolStripMenuItem"
        Me.StopServerToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.StopServerToolStripMenuItem.Text = "Stop Server"
        '
        'pathTextbox
        '
        Me.pathTextbox.Enabled = False
        Me.pathTextbox.Location = New System.Drawing.Point(12, 34)
        Me.pathTextbox.Name = "pathTextbox"
        Me.pathTextbox.ReadOnly = True
        Me.pathTextbox.Size = New System.Drawing.Size(374, 20)
        Me.pathTextbox.TabIndex = 16
        '
        'DataGridView1
        '
        Me.DataGridView1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NumberDataGridViewTextBoxColumn, Me.StatusDataGridViewTextBoxColumn})
        Me.DataGridView1.DataSource = Me.ItemListBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(413, 27)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(319, 168)
        Me.DataGridView1.TabIndex = 18
        '
        'NumberDataGridViewTextBoxColumn
        '
        Me.NumberDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.NumberDataGridViewTextBoxColumn.DataPropertyName = "Number"
        Me.NumberDataGridViewTextBoxColumn.HeaderText = "Number"
        Me.NumberDataGridViewTextBoxColumn.Name = "NumberDataGridViewTextBoxColumn"
        Me.NumberDataGridViewTextBoxColumn.ReadOnly = True
        Me.NumberDataGridViewTextBoxColumn.Width = 69
        '
        'StatusDataGridViewTextBoxColumn
        '
        Me.StatusDataGridViewTextBoxColumn.DataPropertyName = "Status"
        Me.StatusDataGridViewTextBoxColumn.HeaderText = "Status"
        Me.StatusDataGridViewTextBoxColumn.Name = "StatusDataGridViewTextBoxColumn"
        Me.StatusDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ItemListBindingSource
        '
        Me.ItemListBindingSource.DataMember = "ItemList"
        Me.ItemListBindingSource.DataSource = GetType(WindowsApplication1.RenderJobList)
        '
        'durationTimer
        '
        Me.durationTimer.Interval = 1000
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar1, Me.durationlabel, Me.processorTimeLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 419)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(759, 22)
        Me.StatusStrip1.TabIndex = 21
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'durationlabel
        '
        Me.durationlabel.Name = "durationlabel"
        Me.durationlabel.Size = New System.Drawing.Size(0, 17)
        '
        'processorTimeLabel
        '
        Me.processorTimeLabel.Name = "processorTimeLabel"
        Me.processorTimeLabel.Size = New System.Drawing.Size(91, 17)
        Me.processorTimeLabel.Text = "Processor Time:"
        '
        'theConsole
        '
        Me.theConsole.FormattingEnabled = True
        Me.theConsole.HorizontalScrollbar = True
        Me.theConsole.Location = New System.Drawing.Point(12, 60)
        Me.theConsole.Name = "theConsole"
        Me.theConsole.ScrollAlwaysVisible = True
        Me.theConsole.Size = New System.Drawing.Size(374, 342)
        Me.theConsole.TabIndex = 22
        '
        'previewBox
        '
        Me.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.previewBox.Location = New System.Drawing.Point(413, 224)
        Me.previewBox.Name = "previewBox"
        Me.previewBox.Size = New System.Drawing.Size(319, 178)
        Me.previewBox.TabIndex = 23
        Me.previewBox.TabStop = False
        '
        'reset
        '
        Me.reset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.reset.Location = New System.Drawing.Point(646, 197)
        Me.reset.Name = "reset"
        Me.reset.Size = New System.Drawing.Size(75, 23)
        Me.reset.TabIndex = 24
        Me.reset.Text = "Reset Part"
        Me.reset.UseVisualStyleBackColor = True
        '
        'processorMeasure
        '
        Me.processorMeasure.CategoryName = "Prozessor"
        Me.processorMeasure.CounterName = "Prozessorzeit (%)"
        Me.processorMeasure.InstanceName = "_Total"
        '
        'processorLabelTimer
        '
        Me.processorLabelTimer.Enabled = True
        Me.processorLabelTimer.Interval = 2000
        '
        'RenderJobListBindingSource
        '
        Me.RenderJobListBindingSource.DataSource = GetType(WindowsApplication1.RenderJobList)
        '
        'RenderProjectBindingSource
        '
        Me.RenderProjectBindingSource.DataSource = GetType(WindowsApplication1.RenderProject)
        '
        'MainFrame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 441)
        Me.Controls.Add(Me.reset)
        Me.Controls.Add(Me.previewBox)
        Me.Controls.Add(Me.theConsole)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.pathTextbox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Location = New System.Drawing.Point(200, 400)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainFrame"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.previewBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.processorMeasure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RenderJobListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RenderProjectBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents serverListener As System.Windows.Forms.Timer
    Friend WithEvents ReceiveFile As System.ComponentModel.BackgroundWorker
    Friend WithEvents ReceiveFileTimer As System.Windows.Forms.Timer
    Friend WithEvents OpenFileDialog2 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ProjectsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateNewProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenExistingProjectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StopServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pathTextbox As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ItemListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RenderProjectBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents durationTimer As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents durationlabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents theConsole As System.Windows.Forms.ListBox
    Friend WithEvents previewBox As System.Windows.Forms.PictureBox
    Friend WithEvents reset As System.Windows.Forms.Button
    Friend WithEvents RenderJobListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents NumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StatusDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents processorMeasure As System.Diagnostics.PerformanceCounter
    Friend WithEvents processorTimeLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents processorLabelTimer As System.Windows.Forms.Timer

End Class
