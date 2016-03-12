Imports OrbitalMechanics

Namespace Services
    Public Interface ISceneService
        ReadOnly Property CurrentSceneState As SolarSystem
        Sub CreateStartingScene()
    End Interface
End Namespace
