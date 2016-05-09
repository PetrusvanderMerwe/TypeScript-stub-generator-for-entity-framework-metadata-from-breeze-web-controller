Module MainModule

    Sub Main(args As String())
        Dim typeScriptStubGenerator As TypeScriptStubGenerator
        Dim metaDataUrl As String
        Dim outputFolder As String

        Try
            'Check that we have at least 2 arguments ...
            If args.Length < 2 Then Console.Out.WriteLine("2 arguments required. The [Metadata url] and the [Output folder].") : Exit Sub

            'Get the arguments ...
            metaDataUrl = args(0)
            outputFolder = args(1)

            'Test if the output directory exists ...
            If Not IO.Directory.Exists(outputFolder) Then Console.Out.WriteLine("Invalid [Output folder].") : Exit Sub

            'Create a new typescript generator instance ...
            typeScriptStubGenerator = New TypeScriptStubGenerator(metaDataUrl, outputFolder)

            'Generate the output files ...
            typeScriptStubGenerator.Generate()

        Catch ex As Exception

            'Build the output message ...
            Dim message As String = "Exception: " & ex.Message

            'Add the messages from the output folder ...
            If Not typeScriptStubGenerator Is Nothing AndAlso typeScriptStubGenerator.Messages.Count > 0 Then
                For Each tsmessage As String In typeScriptStubGenerator.Messages
                    message &= ";" & tsmessage
                Next
            End If

            'Send exception message to output ...
            Console.Out.WriteLine(message)
        End Try
    End Sub

End Module

