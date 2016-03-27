Imports System.Web.Http
Imports Camera
Imports Entities
Imports TurnTracker
Imports WebApi.Classes
Imports WebApi.Services

Namespace Controllers
    Public Class TurnController
        Inherits ApiController

        Private ReadOnly _turnTracker As ITurnTracker
        Private ReadOnly _entityManager As IEntityManager
        Private ReadOnly _sceneService As ISceneService
        Private ReadOnly _cameraService As ICameraService

        Public Sub New(turnTracker As ITurnTracker,
                        entityManager As IEntityManager,
                        sceneService As ISceneService,
                       cameraService As ICameraService)

            _turnTracker = turnTracker
            _entityManager = entityManager
            _sceneService = sceneService
            _cameraService = cameraService
        End Sub

        <HttpGet()>
        Public Function EndTurn() As TurnResult

            If _turnTracker.TurnNumber = 0 Then
                _sceneService.CreateStartingScene()
            Else
                _entityManager.UpdateAll()
            End If

            _turnTracker.Update()

            Return New TurnResult(_sceneService, _cameraService)
        End Function

    End Class
End Namespace