Public Interface ICameraService
    ReadOnly Property CurrentTarget As Guid
    Sub SetTarget(target As String)
    Sub SetTarget(target As Guid)
    ReadOnly Property LastTarget As Guid
End Interface
