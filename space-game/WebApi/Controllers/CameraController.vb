Imports System.Web.Http
Imports Camera

Namespace Controllers
    Public Class CameraController
        Inherits ApiController

        Private ReadOnly _cameraService As ICameraService

        Public Sub New(cameraService As ICameraService)
            _cameraService = cameraService
        End Sub

        <HttpGet()>
        Public Sub SetTarget(target As String)
            _cameraService.SetTarget(target)
        End Sub
    End Class
End Namespace