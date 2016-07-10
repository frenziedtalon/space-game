Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Interface IOrbitingObject
        ReadOnly Property Orbit As IOrbit
        ReadOnly Property Primary As Guid
        Sub SetOrbit(p As BaseCelestialObject, o As IOrbit)
    End Interface
End NameSpace