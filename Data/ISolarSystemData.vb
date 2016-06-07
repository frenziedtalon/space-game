Imports Data.Data

Public Interface ISolarSystemData
    ReadOnly Property Sun As PhysicalData
    ReadOnly Property Mercury As OrbitingObjectData
    ReadOnly Property Venus As OrbitingObjectData
    ReadOnly Property Earth As OrbitingObjectData
    ReadOnly Property Moon As OrbitingObjectData
    ReadOnly Property Mars As OrbitingObjectData
    ReadOnly Property Jupiter As OrbitingObjectData
    ReadOnly Property Saturn As OrbitingObjectData
    ReadOnly Property Uranus As OrbitingObjectData
    ReadOnly Property Neptune As OrbitingObjectData
    ReadOnly Property Pluto As OrbitingObjectData
End Interface
