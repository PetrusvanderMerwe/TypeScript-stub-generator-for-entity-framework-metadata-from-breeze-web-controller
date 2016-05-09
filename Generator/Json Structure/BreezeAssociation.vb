Imports Newtonsoft.Json

Public Class BreezeAssociation
    Public Property name As String
    Public Property [end] As Object
    Public Property referentialConstraint As Object

    <Newtonsoft.Json.JsonIgnoreAttribute>
    Private Property ends_list As List(Of BreezeAssociationEnd)

    <Newtonsoft.Json.JsonIgnoreAttribute>
    Public ReadOnly Property ends As List(Of BreezeAssociationEnd)
        Get
            'When we've not built the ends list yet ...
            If ends_list Is Nothing Then

                'Serialize to json ...
                Dim strJson As String = JsonConvert.SerializeObject(Me.[end])

                'Desrialize back ...
                If strJson = "null" Then
                    Me.ends_list = New List(Of BreezeAssociationEnd)()
                ElseIf TypeOf [end] Is Newtonsoft.Json.Linq.JArray Then
                    Me.ends_list = New List(Of BreezeAssociationEnd)(JsonConvert.DeserializeObject(Of List(Of BreezeAssociationEnd))(strJson))
                Else
                    Me.ends_list = New List(Of BreezeAssociationEnd)({JsonConvert.DeserializeObject(Of BreezeAssociationEnd)(strJson)})
                End If
            End If

            Return ends_list
        End Get
    End Property
End Class
