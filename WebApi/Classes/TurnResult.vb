Imports OrbitalMechanics
Imports WebApi.Services

Namespace Classes
    Public Class TurnResult

        Public ReadOnly Property Scene As SolarSystem
            Get
                Return SceneService.Instance().CurrentSceneState
            End Get
        End Property

    End Class
End NameSpace