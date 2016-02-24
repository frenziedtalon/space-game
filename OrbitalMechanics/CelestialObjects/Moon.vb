Imports Core
Imports Entities
Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class Moon
        Inherits OrbitingCelestialObjectBase
        Implements ISphere

        Public Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       radius As Integer,
                       orbit As Orbit,
                       entityManager As IEntityManager)

            MyBase.New(name, mass, texture, orbit, entityManager)
            _radius = radius
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

        Private _volume As Double = 0

        Public ReadOnly Property Volume As Double Implements I3DObject.Volume
            Get
                If Double.Equals(_volume, 0.0) AndAlso Radius > 0 Then
                    _volume = Helpers.Shapes.ShapeHelper.VolumeOfASphere(Radius)
                End If
                Return _volume
            End Get
        End Property

    End Class
End Namespace