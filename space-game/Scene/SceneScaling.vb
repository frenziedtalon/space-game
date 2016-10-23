Imports Core.Classes
Imports Core.Extensions
Imports Entities
Imports OrbitalMechanics
Imports OrbitalMechanics.CelestialObjects

Public Class SceneScaling

    Private ReadOnly _semiMajorAxisRange As DistanceRange
    Private ReadOnly _celestialObjectRadiusRange As DistanceRange

    Public Sub New()
        _semiMajorAxisRange = New DistanceRange(Distance.FromAstronomicalUnits(1), Distance.FromAstronomicalUnits(1.1))
        _celestialObjectRadiusRange = New DistanceRange(Distance.FromKilometers(6000), Distance.FromKilometers(6500))
    End Sub

    Public Sub ProcessEntity(entity As BaseGameEntity)
        Dim interfaceList As List(Of Type) = entity.GetType().GetInterfaces().ToList()
        If interfaceList.HasAny() Then
            If interfaceList.Contains(GetType(ISphere)) Then
                ProcessSphericalEntity(DirectCast(entity, ISphere))
            End If
            If interfaceList.Contains(GetType(IOrbitingObject)) Then
                ProcessOrbitingEntity(DirectCast(entity, IOrbitingObject))
            End If
        End If
    End Sub

    Private Sub ProcessSphericalEntity(sphericalEntity As ISphere)
        _celestialObjectRadiusRange.AddValue(sphericalEntity.Radius)
    End Sub

    Private Sub ProcessOrbitingEntity(orbitingEntity As IOrbitingObject)
        If orbitingEntity.Orbit IsNot Nothing Then
            _semiMajorAxisRange.AddValue(orbitingEntity.Orbit.SemiMajorAxis)
        End If
    End Sub

    ReadOnly Property SemiMajorAxis() As DistanceRange
        Get
            Return _semiMajorAxisRange
        End Get
    End Property

    ReadOnly Property CelestialObjectRadius() As DistanceRange
        Get
            Return _celestialObjectRadiusRange
        End Get
    End Property
End Class
