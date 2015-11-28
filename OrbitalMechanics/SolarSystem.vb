Imports OrbitalMechanics.CelestialObjects

Public Class SolarSystem
    Private ReadOnly _objects As List(Of ICelestialObject)

    Public Sub New(objects As List(Of ICelestialObject))
        _objects = objects
    End Sub

    Public ReadOnly Property Objects() As List(Of ICelestialObject)
        Get
            Return _objects
        End Get
    End Property

    Public ReadOnly Property Stars() As List(Of ICelestialObject)
        Get
            Return _objects.Where(Function(o) o.GetType() Is GetType(Star)).ToList()
        End Get
    End Property

    Public ReadOnly Property Planets() As List(Of ICelestialObject)
        Get
            Return _objects.Where(Function(o) o.GetType() Is GetType(Planet)).ToList()
        End Get
    End Property

End Class