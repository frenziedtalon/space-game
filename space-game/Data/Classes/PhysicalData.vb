﻿Imports Core.Classes
Imports Data.Interfaces

Namespace Classes
    Public Class PhysicalData
        Implements IPhysicalData

        Sub New(name As String,
                radius As Distance,
                mass As Mass,
                texture As Textures,
                type As CelestialObjectType)
            Me.Radius = radius
            Me.Name = name
            Me.Mass = mass
            Me.Texture = texture
            Me.Type = type
        End Sub

        Private Sub New ()
            ' For Mapster
        End Sub

        Public Property Name As String Implements IPhysicalData.Name
        Public Property Mass As Mass Implements IPhysicalData.Mass
        Public Property Texture As Textures Implements IPhysicalData.Texture
        Public Property Type As CelestialObjectType Implements IPhysicalData.Type
        Public Property Radius As Distance Implements IPhysicalData.Radius
    End Class
End Namespace