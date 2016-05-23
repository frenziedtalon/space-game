Imports Camera
Imports Entities
Imports Scene

Namespace Classes
    Public Class TurnResult

        Private ReadOnly _sceneService As ISceneService
        Private ReadOnly _cameraService As ICameraService
        Private ReadOnly _camera As Camera

        Public Sub New(sceneService As ISceneService,
                       cameraService As ICameraService)
            _sceneService = sceneService
            _cameraService = cameraService
            _camera = New Camera(_cameraService)
        End Sub

        Public ReadOnly Property Scene As List(Of BaseGameEntity)
            Get
                Return _sceneService.CurrentSceneState
            End Get
        End Property

        Public ReadOnly Property Camera As Camera
            Get
                Return _camera
            End Get
        End Property

    End Class
End Namespace