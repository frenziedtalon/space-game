Imports Entities

Public Interface ISceneService
    ReadOnly Property CurrentSceneState As ISceneState
    Sub CreateStartingScene()
End Interface