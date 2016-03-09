Imports System.Web.Http
Imports Entities
Imports TurnTracker.Services
Imports WebApi.Classes

Namespace Controllers
    Public Class TurnController
        Inherits ApiController

        Private ReadOnly _turnTrackerService As ITurnTrackerService
        Private ReadOnly _entityManager As IEntityManager

        Public Sub New(turnTrackerService As ITurnTrackerService,
                                entityManager As IEntityManager)

            _turnTrackerService = turnTrackerService
            _entityManager = entityManager

        End Sub

        Public Function EndTurn() As TurnResult

            ' TODO: implement IoC container for turnservice and entityManager using a singleton
            _turnTrackerService.Update()
            _entityManager.UpdateAll()

            Return New TurnResult()
        End Function

    End Class
End Namespace