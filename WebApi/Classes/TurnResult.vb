Imports Entities
Imports WebApi.Services

Namespace Classes
    Public Class TurnResult

        Private ReadOnly _sceneService As ISceneService

        Public Sub New(sceneService As ISceneService)
            _sceneService = sceneService
        End Sub

        Public ReadOnly Property Scene As List(Of BaseGameEntity)
            Get
                Return _sceneService.CurrentSceneState
            End Get
        End Property

    End Class
End NameSpace