Imports Newtonsoft.Json

Public Class BreezeEntityType
    Public Property name As String
    Public Property key As BreezeEntityTypeKey
    Public Property [property] As List(Of BreezeEntityTypeProperty)
    Public Property navigationProperty As Object

    <Newtonsoft.Json.JsonIgnoreAttribute>
    Private Property navigationProperties_list As List(Of BreezeEntityTypeNavigationProperty)

    <Newtonsoft.Json.JsonIgnoreAttribute>
    Public ReadOnly Property navigationProperties As List(Of BreezeEntityTypeNavigationProperty)
        Get
            'When we've not built the list yet ...
            If navigationProperties_list Is Nothing Then

                'Serialize to json ...
                Dim strJson As String = JsonConvert.SerializeObject(Me.navigationProperty)

                'Desrialize back ...
                If strJson = "null" Then
                    Me.navigationProperties_list = New List(Of BreezeEntityTypeNavigationProperty)()
                ElseIf TypeOf navigationProperty Is Newtonsoft.Json.Linq.JArray Then
                    Me.navigationProperties_list = New List(Of BreezeEntityTypeNavigationProperty)(JsonConvert.DeserializeObject(Of List(Of BreezeEntityTypeNavigationProperty))(strJson))
                Else
                    Me.navigationProperties_list = New List(Of BreezeEntityTypeNavigationProperty)({JsonConvert.DeserializeObject(Of BreezeEntityTypeNavigationProperty)(strJson)})
                End If
            End If

            Return navigationProperties_list
        End Get
    End Property
End Class
