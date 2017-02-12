Imports Core.Classes
Imports Data.Interfaces
Imports Core.Extensions.EnumerableExtensions

Namespace Classes
    Public Class PhysicalData
        Implements IPhysicalData
        Implements IEquatable(Of PhysicalData)

        Sub New(name As String,
                radius As Distance,
                mass As Mass,
                textures As List(Of Texture),
                type As CelestialObjectType)
            Me.Radius = radius
            Me.Name = name
            Me.Mass = mass
            Me.Textures = textures
            Me.Type = type
        End Sub

        Private Sub New()
            ' For Mapster
        End Sub

        Public Property Name As String Implements IPhysicalData.Name
        Public Property Mass As Mass Implements IPhysicalData.Mass
        Public Property Textures As List(Of Texture) Implements IPhysicalData.Textures
        Public Property Type As CelestialObjectType Implements IPhysicalData.Type
        Public Property Radius As Distance Implements IPhysicalData.Radius

        Public Shadows Function Equals(other As PhysicalData) As Boolean Implements IEquatable(Of PhysicalData).Equals
            If other IsNot Nothing Then
                Return Name.Equals(other.Name) AndAlso
                    Mass.Equals(other.Mass) AndAlso
                    Type.Equals(other.Type) AndAlso
                    Radius.Equals(other.Radius) AndAlso
                    Textures.IsEquivalent(other.Textures) AndAlso 
                    Rings.Equals(other.Rings)
            End If
            Return False
        End Function
    End Class
End Namespace