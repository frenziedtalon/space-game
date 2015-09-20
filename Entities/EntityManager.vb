


''' <summary>
''' Tracks all entities in the game
''' </summary>
Public Class EntityManager

    Private Shared _instance As EntityManager
    Private Shared ReadOnly EntityMap As New Hashtable

    Private Sub New()
        ' private so that we must use the Instance
    End Sub


    Public Shared ReadOnly Property Instance() As EntityManager
        Get
            If _instance Is Nothing Then
                _instance = New EntityManager
            End If
            Return _instance
        End Get
    End Property

    Public Sub RegisterEntity(ByRef newEntity As BaseGameEntity)
        If newEntity IsNot Nothing Then
            EntityMap.Add(newEntity.Id, newEntity)
        End If
    End Sub

    Public Function GetEntityFromId(id As Guid) As BaseGameEntity
        If EntityMap.ContainsKey(id) Then
            Return EntityMap(id)
        End If
        Return Nothing
    End Function

    Public Sub RemoveEntity(ByRef pEntity As BaseGameEntity)
        If pEntity IsNot Nothing AndAlso EntityMap.ContainsKey(pEntity.Id) Then
            EntityMap.Remove(pEntity.Id)
        End If
    End Sub

    Public Sub UpdateAll()
        If EntityMap.Count > 0 Then
            For Each e In EntityMap
                e.Value.Update()
            Next
        End If
    End Sub

End Class