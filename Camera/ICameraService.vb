Public Interface ICameraService
    ReadOnly Property CurrentTarget As Guid
    Sub SetNewTarget(target As Guid)
    ReadOnly Property LastTarget As Guid
End Interface
