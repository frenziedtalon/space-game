
Imports System.Windows.Media.Media3D

Namespace CelestialObjects
    Public Class Planet
        Inherits BaseCelestialObject
        Implements ISphere

        Public Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       position As Point3D,
                       motion As Vector3D,
                       radius As Integer,
                       moons As List(Of Moon))

            MyBase.New(name, mass, texture, position, motion)
            _radius = radius
            _moons = moons
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
    End Class
End Namespace