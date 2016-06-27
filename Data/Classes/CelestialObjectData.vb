Imports Data.Data

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
    End Class
End Namespace