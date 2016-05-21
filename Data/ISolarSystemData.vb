Imports Data.Data

Public Interface ISolarSystemData
    Function Mercury() As OrbitingObjectData
    Function Venus() As OrbitingObjectData
    Function Earth() As OrbitingObjectData
    Function Moon() As OrbitingObjectData
    Function Mars() As OrbitingObjectData
    Function Jupiter() As OrbitingObjectData
    Function Saturn() As OrbitingObjectData
    Function Uranus() As OrbitingObjectData
    Function Neptune() As OrbitingObjectData
    Function Pluto() As OrbitingObjectData
End Interface
