Imports System.Windows.Media.Media3D

Namespace Classes

    Public Class Orbit
        Implements IOrbit

        Public Sub New(radius As Integer,
                       speed As Double,
                       angle As Double)
            Me.New(radius, speed, angle, New Point3D(0, 0, 0))
            SetInitialPosition()
        End Sub

        Public Sub New(radius As Integer,
                       speed As Double,
                       angle As Double,
                       position As Point3D)
            _radius = radius
            _speed = speed
            _angle = angle
            _position = position
        End Sub

        Private ReadOnly _radius As Integer
        Public ReadOnly Property Radius As Integer Implements IOrbit.Radius
            Get
                Return _radius
            End Get
        End Property

        Private ReadOnly _speed As Double
        Public ReadOnly Property Speed As Double Implements IOrbit.Speed
            Get
                Return _speed
            End Get
        End Property

        Private ReadOnly _angle As Double
        Public ReadOnly Property Angle As Double Implements IOrbit.Angle
            Get
                Return _angle
            End Get
        End Property

        Private _position As Point3D

        Public ReadOnly Property Position As Point3D Implements IOrbit.Position
            Get
                Return _position
            End Get
        End Property

        Public Sub Update() Implements IOrbit.Update
            ' update the Position based on the orbit
            Throw New NotImplementedException
        End Sub

        Private Sub SetInitialPosition()
            ' TODO: Calculate properly including the angle
            _position = New Point3D(0, 0, _radius)
        End Sub
    End Class
End Namespace