Imports System.Web.Http
Imports Entities
Imports TurnTracker.Services
Imports WebApi.Classes
Imports WebApi.Services

Namespace Controllers
    Public Class TurnController
        Inherits ApiController

        Private ReadOnly _turnTrackerService As ITurnTrackerService
        Private ReadOnly _entityManager As IEntityManager
        Private ReadOnly _sceneService As ISceneService

        Public Sub New(turnTrackerService As ITurnTrackerService,
                        entityManager As IEntityManager,
                        sceneService As ISceneService)

            _turnTrackerService = turnTrackerService
            _entityManager = entityManager
            _sceneService = sceneService
        End Sub

        Public Function EndTurn() As TurnResult

            ' TODO: implement IoC container for turnservice and entityManager using a singleton
            _turnTrackerService.Update()
            _entityManager.UpdateAll()

            Return New TurnResult(_sceneService)
        End Function

    End Class
End Namespace