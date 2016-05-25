Imports Core.Extensions
Imports Data
Imports Entities
Imports OrbitalMechanics.CelestialObjects
Imports TurnTracker

Public Class SceneService
    Implements ISceneService

    Private ReadOnly _entityManager As IEntityManager
    Private ReadOnly _turnTracker As ITurnTracker
    Private ReadOnly _dataProvider As IDataProvider
    Private ReadOnly _constructor As SceneConstructor

    Public Sub New(entityManager As IEntityManager,
                   turnTracker As ITurnTracker,
                   dataProvider As IDataProvider)
        _entityManager = entityManager
        _turnTracker = turnTracker
        _dataProvider = dataProvider
        _constructor = New SceneConstructor(_entityManager, _turnTracker, _dataProvider)
    End Sub

    Public Sub CreateStartingScene() Implements ISceneService.CreateStartingScene
        _constructor.SolSystem()
    End Sub

    Public ReadOnly Property CurrentSceneState As List(Of BaseGameEntity) Implements ISceneService.CurrentSceneState
        Get
            ' Prevent satellites being returned twice, once as own entity and again in the "Satellites" collection on the primary
            Dim entities = _entityManager.GetAllEntities()

            Dim result As New List(Of BaseGameEntity)

            For Each e In entities
                Dim o = TryCast(e, OrbitingCelestialObjectBase)
                If o Is Nothing OrElse o.Primary.IsEmpty() Then
                    result.Add(o)
                End If
            Next

            Return result
        End Get
    End Property

End Class