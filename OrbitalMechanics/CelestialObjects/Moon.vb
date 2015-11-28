Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class Moon
        Inherits BaseCelestialObject
        Implements ISphere

        Public Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       radius As Integer,
                       orbit As Orbit)

            MyBase.New(name, mass, texture)
            _radius = radius
            _orbit = orbit
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
            Orbit.Update()
        End Sub

        Private ReadOnly _radius As Integer
        Public ReadOnly Property Radius As Integer Implements ISphere.Radius
            Get
                Return _radius
            End Get
        End Property

        Public ReadOnly Property AxialTilt As Integer Implements ISphere.AxialTilt
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public ReadOnly Property RotationSpeed As Integer Implements ISphere.RotationSpeed
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Private ReadOnly _orbit As Orbit
        Public ReadOnly Property Orbit As Orbit
            Get
                Return _orbit
            End Get
        End Property

    End Class
End Namespace