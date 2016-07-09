Imports Core.Classes
Imports Data.Classes

Namespace Interfaces
    Public Interface IPhysicalData
        Property Mass As Mass
        Property Name As String
        Property Radius As Distance
        Property Texture As String
        Property Type As CelestialObjectType
    End Interface
End Namespace
