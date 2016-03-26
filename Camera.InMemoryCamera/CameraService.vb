Imports Core.Extensions

Public Class CameraService
    Implements ICameraService
    Private _currentTarget As Guid
    Private _lastTarget As Guid

    Public ReadOnly Property CurrentTarget As Guid Implements ICameraService.CurrentTarget
        Get
            Return _currentTarget
        End Get
    End Property

    Public ReadOnly Property LastTarget As Guid Implements ICameraService.LastTarget
        Get
            Return _lastTarget
        End Get
    End Property

    Public Sub SetNewTarget(target As Guid) Implements ICameraService.SetNewTarget
        If Not target.IsEmpty() Then
            _lastTarget = _currentTarget
            _currentTarget = target
        End If
    End Sub

End Class
