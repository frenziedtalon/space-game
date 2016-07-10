Imports Camera

Namespace Classes
    Public Class Camera

        Private ReadOnly _cameraService As ICameraService

        Public Sub New(cameraService As ICameraService)
            _cameraService = cameraService
        End Sub

        Public ReadOnly Property CurrentTarget As Guid
            Get
                Return _cameraService.CurrentTarget
            End Get
        End Property
    End Class
End NameSpace