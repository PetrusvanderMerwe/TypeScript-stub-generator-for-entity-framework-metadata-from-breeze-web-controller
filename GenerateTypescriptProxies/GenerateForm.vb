

Public Class GenerateForm
    Private DoNotSaveSettings As Boolean = False

    Private Sub Generate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

        'Check that the output folder exists ...
        If Not System.IO.Directory.Exists(txtOutputFolder.Text) Then
            MessageBox.Show("The specified Output folder does not exist.")
            Exit Sub
        End If

        'Generate the subs ...
        Dim typeScriptStubGenerator As New TypeScriptStubGenerator(txtMetadataUrl.Text, txtOutputFolder.Text)

        Try
            'Generate the stubs ...
            typeScriptStubGenerator.Generate()
        Catch ex As Exception
            MessageBox.Show("Problem generating stubs. Error: " & ex.Message)
            Exit Sub
        End Try

        'Notify user that the process completed successfully ...
        MessageBox.Show("TypeScript stubs generated successfully.")
    End Sub

    Private Sub btnDllPath_Click(sender As Object, e As EventArgs)
        Dim openFileDialog As OpenFileDialog

        'Create a new open file dialog ...
        openFileDialog = New OpenFileDialog

        'Set to check the the selected path exists ...
        openFileDialog.CheckFileExists = True
        openFileDialog.CheckPathExists = True

        'Set teh default extension to dll ...
        openFileDialog.DefaultExt = "dll"

        'Ste the default to the current path ...
        If txtMetadataUrl.Text.Trim <> "" Then openFileDialog.FileName = txtMetadataUrl.Text

        'Allow the user to select a file ...
        If openFileDialog.ShowDialog(Me) = DialogResult.OK Then

            'Set the input file path in the text box ...
            txtMetadataUrl.Text = openFileDialog.FileName

            'Save the settings ...
            SaveSettings()
        End If
    End Sub

    Private Sub btnOutputFolder_Click(sender As Object, e As EventArgs) Handles btnOutputFolder.Click
        Dim folderBrowserDialog As FolderBrowserDialog

        'Create a new folder browser dialog ...
        folderBrowserDialog = New FolderBrowserDialog

        'Allow the user to select a folder ...
        If folderBrowserDialog.ShowDialog(Me) = DialogResult.OK Then

            'Set the output folder path in the text box ...
            txtOutputFolder.Text = folderBrowserDialog.SelectedPath

            'Save the settings ...
            SaveSettings()
        End If
    End Sub

    Private Sub LoadSettings()

        'Load the settings ...
        DoNotSaveSettings = True
        txtMetadataUrl.Text = My.Settings.MetadataUrl
        txtOutputFolder.Text = My.Settings.OutputFolder
        DoNotSaveSettings = False
    End Sub

    Private Sub SaveSettings()

        'Update the settings ...
        If Not DoNotSaveSettings Then
            DoNotSaveSettings = True

            My.Settings.MetadataUrl = txtMetadataUrl.Text
            My.Settings.OutputFolder = txtOutputFolder.Text
            My.Settings.Save()

            DoNotSaveSettings = False
        End If
    End Sub

    Private Sub GenerateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load the settings ...
        LoadSettings()
    End Sub

    Private Sub txtMetadataUrl_TextChanged(sender As Object, e As EventArgs) Handles txtMetadataUrl.TextChanged

        'Save the settings ...
        SaveSettings()
    End Sub

    Private Sub txtOutputFolder_TextChanged(sender As Object, e As EventArgs) Handles txtOutputFolder.TextChanged

        'Save the settings ...
        SaveSettings()
    End Sub
End Class
