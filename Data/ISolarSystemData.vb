Imports Data.Classes
Imports Data.Data

Public Interface ISolarSystemData
    ReadOnly Property Sun As PhysicalData
    ReadOnly Property Mercury As CelestialObjectData
    ReadOnly Property Venus As CelestialObjectData
    ReadOnly Property Earth As CelestialObjectData
    ReadOnly Property Moon As CelestialObjectData
    ReadOnly Property Mars As CelestialObjectData
    ReadOnly Property Jupiter As CelestialObjectData
    ReadOnly Property Saturn As CelestialObjectData
    ReadOnly Property Uranus As CelestialObjectData
    ReadOnly Property Neptune As CelestialObjectData
    ReadOnly Property Pluto As CelestialObjectData
End Interface
