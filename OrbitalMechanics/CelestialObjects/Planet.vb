
Imports Core
Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class Planet
        Inherits BaseCelestialObject
        Implements ISphere

        Public Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       radius As Integer,
                       orbit As Orbit,
                       Optional moons As List(Of Moon) = Nothing)

            MyBase.New(name, mass, texture)
            _radius = radius
            _moons = moons
            _orbit = orbit
        End Sub

        Public Overrides Sub Update()
            Throw New NotImplementedException()
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

        Private ReadOnly _moons As List(Of Moon)
        Public ReadOnly Property Moons As List(Of Moon)
            Get
                Return _moons
            End Get
        End Property

        Public Function ShouldSerializeMoons() As Boolean
            Return Moons IsNot Nothing
        End Function

        Private _volume As Double = 0

        Public ReadOnly Property Volume As Double Implements I3DObject.Volume
            Get
                If Double.Equals(_volume, 0.0) AndAlso Radius > 0 Then
                    _volume = Helpers.Shapes.ShapeHelper.VolumeOfASphere(Radius)
                End If
                Return _volume
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