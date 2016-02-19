Imports System.Web.Http
Imports Entities
Imports WebApi.Classes
Imports WebApi.Services

Namespace Controllers
    Public Class TurnController
        Inherits ApiController

        Public Function EndTurn(turnService As ITurnTrackerService) As TurnResult

            ' TODO: implement IoC container for turnservice using a singleton
            turnService.Update()
            EntityManager.Instance().UpdateAll()

            Return New TurnResult()
        End Function

    End Class
End Namespace