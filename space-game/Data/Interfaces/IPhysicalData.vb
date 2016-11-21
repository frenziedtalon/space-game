Imports Core.Classes
Imports Data.Classes

Namespace Interfaces
    Public Interface IPhysicalData
        Property Mass As Mass
        Property Name As String
        Property Radius As Distance
        Property Textures As List(Of Texture)
        Property Type As CelestialObjectType
    End Interface
End Namespace
