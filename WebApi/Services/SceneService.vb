Imports Entities
Imports OrbitalMechanics
Imports WebApi.Models

Namespace Services
    Public Class SceneService
        Implements ISceneService

        Private ReadOnly _entityManager As IEntityManager

        Public Sub New(entityManager As IEntityManager)
            _entityManager = entityManager
        End Sub

        Public Sub CreateStartingScene() Implements ISceneService.CreateStartingScene
            Dim constructor = New SceneConstructor
            constructor.SolSystem(_entityManager)
        End Sub

        Public ReadOnly Property CurrentSceneState As List(Of BaseGameEntity) Implements ISceneService.CurrentSceneState
            Get
                Return _entityManager.GetAllEntities()
            End Get
        End Property

    End Class
End Namespace