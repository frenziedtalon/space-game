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

        <HttpGet()>
        Public Function EndTurn() As TurnResult

            If _turnTrackerService.TurnNumber = 0 Then
                _sceneService.CreateStartingScene()
            Else
                _entityManager.UpdateAll()
            End If

            _turnTrackerService.Update()

            Return New TurnResult(_sceneService)
        End Function

    End Class
End Namespace