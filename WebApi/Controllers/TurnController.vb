Imports System.Web.Http
Imports Entities
Imports WebApi.Classes
Imports WebApi.Services

Namespace Controllers
    Public Class TurnController
        Inherits ApiController

        Public Function EndTurn(turnService As ITurnTrackerService,
                                entityManager As IEntityManager) As TurnResult

            ' TODO: implement IoC container for turnservice and entityManager using a singleton
            turnService.Update()
            entityManager.UpdateAll()

            Return New TurnResult()
        End Function

    End Class
End Namespace