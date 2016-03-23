Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Interface IOrbitingObject
        ReadOnly Property Orbit As IOrbit
        Property Primary As Guid
    End Interface
End NameSpace