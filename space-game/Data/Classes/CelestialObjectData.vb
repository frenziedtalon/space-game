

Namespace Classes
    Public Class CelestialObjectData
        Private ReadOnly _physical As PhysicalData
        Private ReadOnly _orbit As OrbitData

        Sub New(orbit As OrbitData, physical As PhysicalData)
            _orbit = orbit
            _physical = physical
        End Sub

        Public ReadOnly Property Physical As PhysicalData
            Get
                Return _physical
            End Get
        End Property

        Public ReadOnly Property Orbit As OrbitData
            Get
                Return _orbit
            End Get
        End Property

        Private ReadOnly _satellites As New List(Of CelestialObjectData)
        Public ReadOnly Property Satellites As List(Of CelestialObjectData)
            Get
                Return _satellites
            End Get
        End Property

        Public Sub AddSatellite(s As CelestialObjectData)
            _satellites.Add(s)
        End Sub

        Public Sub AddSatellite(o As OrbitData, p As PhysicalData)
            _satellites.Add(New CelestialObjectData(o, p))
        End Sub
    End Class
End Namespace