Imports System.Web.Http
Imports Entities
Imports WebApi.Classes
Imports WebApi.Services

Namespace Controllers
    Public Class TurnController
        Inherits ApiController

        Public Function EndTurn() As TurnResult
            TurnTrackerService.Instance.Update()
            EntityManager.Instance().UpdateAll()

            Return New TurnResult()
        End Function

    End Class
End Namespace