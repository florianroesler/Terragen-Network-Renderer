Imports System.Windows.Forms

Public Class chooseIpDialog

    Private chosenAddress As String

    Public Sub New(ByRef availableAddresses As List(Of String))
        InitializeComponent()
        For Each s In availableAddresses
            addressesCombo.Items.Add(s)
        Next
        If (availableAddresses.Count > 0) Then
            addressesCombo.SelectedIndex = 0
        End If
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        chosenAddress = addressesCombo.SelectedItem
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub chooseIpDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Property address()
        Get
            Return chosenAddress
        End Get
        Set(ByVal value)
            chosenAddress = value
        End Set
    End Property
End Class
