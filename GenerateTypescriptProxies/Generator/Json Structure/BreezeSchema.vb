Public Class BreezeSchema
    Public Property [namespace] As String
    Public Property [alias] As String
    Public Property entityType As List(Of BreezeEntityType)
    Public Property association As List(Of BreezeAssociation)
    Public Property entityContainer As Object
End Class
