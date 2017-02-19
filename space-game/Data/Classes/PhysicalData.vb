Imports Core.Classes
Imports Data.Interfaces
Imports Core.Extensions.EnumerableExtensions

Namespace Classes
    Public Class PhysicalData
        Implements IPhysicalData

        Sub New(name As String,
                radius As Distance,
                mass As Mass,
                textures As List(Of Texture),
                type As CelestialObjectType,
                rings As RingData)
            Me.Radius = radius
            Me.Name = name
            Me.Mass = mass
            Me.Textures = textures
            Me.Type = type
            Me.Rings = rings
        End Sub

        Private Sub New()
            ' For Mapster
        End Sub

        Public Property Name As String Implements IPhysicalData.Name
        Public Property Mass As Mass Implements IPhysicalData.Mass
        Public Property Textures As List(Of Texture) Implements IPhysicalData.Textures
        Public Property Type As CelestialObjectType Implements IPhysicalData.Type
        Public Property Rings As RingData
        Public Property Radius As Distance Implements IPhysicalData.Radius
    End Class
End Namespace