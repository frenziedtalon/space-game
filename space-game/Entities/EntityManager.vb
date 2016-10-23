Imports System.Runtime.CompilerServices

<Assembly: InternalsVisibleTo("Entities.Tests")>
''' <summary>
''' Tracks all entities in the game
''' </summary>
Public Class EntityManager
    Implements IEntityManager

    Private ReadOnly _entityMap As New Dictionary(Of Guid, BaseGameEntity)

    Public Sub RegisterEntity(ByRef newEntity As BaseGameEntity) Implements IEntityManager.RegisterEntity
        If newEntity IsNot Nothing Then
            _entityMap.Add(newEntity.Id, newEntity)
        End If
    End Sub

    Public Function GetEntityFromId(id As Guid) As BaseGameEntity Implements IEntityManager.GetEntityFromId
        If _entityMap.ContainsKey(id) Then
            Return _entityMap(id)
        End If
        Return Nothing
    End Function

    Public Sub RemoveEntity(ByRef pEntity As BaseGameEntity) Implements IEntityManager.RemoveEntity
        If pEntity IsNot Nothing AndAlso _entityMap.ContainsKey(pEntity.Id) Then
            _entityMap.Remove(pEntity.Id)
        End If
    End Sub

    Public Sub UpdateAll() Implements IEntityManager.UpdateAll
        If _entityMap.Count > 0 Then
            For Each e In _entityMap
                e.Value.Update()
            Next
        End If
    End Sub

    Public ReadOnly Property Count As Integer
        Get
            Return _entityMap.Count
        End Get
    End Property

    Public Function GetAllEntities() As List(Of BaseGameEntity) Implements IEntityManager.GetAllEntities
        If _entityMap.Count > 0 Then
            Return _entityMap.Values.ToList()
        End If
        Return New List(Of BaseGameEntity)
    End Function

End Class