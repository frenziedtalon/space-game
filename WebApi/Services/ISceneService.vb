Imports Entities

Namespace Services
    Public Interface ISceneService
        ReadOnly Property CurrentSceneState As List(Of BaseGameEntity)
        Sub CreateStartingScene()
    End Interface
End Namespace
