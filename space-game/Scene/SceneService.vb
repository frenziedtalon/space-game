Imports Data
Imports Entities
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

    Public ReadOnly Property CurrentSceneState As ISceneState Implements ISceneService.CurrentSceneState
        Get
            Dim entities = _entityManager.GetAllEntities()
            Return New SceneState(entities)
        End Get
    End Property

End Class