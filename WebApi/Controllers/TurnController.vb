Imports System.Web.Http
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

        Public Sub New(turnTracker As ITurnTracker,
                        entityManager As IEntityManager,
                        sceneService As ISceneService)

            _turnTracker = turnTracker
            _entityManager = entityManager
            _sceneService = sceneService
        End Sub

        <HttpGet()>
        Public Function EndTurn() As TurnResult

            If _turnTracker.TurnNumber = 0 Then
                _sceneService.CreateStartingScene()
            Else
                _entityManager.UpdateAll()
            End If

            _turnTracker.Update()

            Return New TurnResult(_sceneService)
        End Function

    End Class
End Namespace