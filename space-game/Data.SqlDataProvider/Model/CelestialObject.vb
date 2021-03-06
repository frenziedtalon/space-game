'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class CelestialObject
    Public Property Id As Integer
    Public Property Name As String
    Public Property SolarSystemId As Integer
    Public Property Mass As Double
    Public Property Radius As Double
    Public Property TextureGroupId As Integer
    Public Property TypeId As Integer
    Public Property PrimaryId As Nullable(Of Integer)
    Public Property SemiMajorAxis As Nullable(Of Double)
    Public Property Eccentricity As Nullable(Of Double)
    Public Property Inclination As Nullable(Of Double)
    Public Property ArgumentOfPeriapsis As Nullable(Of Double)
    Public Property LongitudeOfAscendingNode As Nullable(Of Double)
    Public Property MeanAnomalyZero As Nullable(Of Double)

    Public Overridable Property CelestialObjectType As CelestialObjectType
    Public Overridable Property CelestialObject1 As ICollection(Of CelestialObject) = New HashSet(Of CelestialObject)
    Public Overridable Property CelestialObject2 As CelestialObject
    Public Overridable Property SolarSystem As SolarSystem
    Public Overridable Property TextureGroup As TextureGroup

End Class
