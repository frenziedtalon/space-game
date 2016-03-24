Imports System.Windows.Media.Media3D
Imports TurnTracker

Namespace Classes

    Public Class Orbit
        Implements IOrbit

        Private ReadOnly _turnTracker As ITurnTracker

        Public Sub New(radius As Integer,
                       period As TimeSpan,
                       angle As Double,
                       turnTracker As ITurnTracker)
            Me.New(radius, period, angle, New Point3D(0, 0, 0), turnTracker)
            SetInitialPosition()
        End Sub

        Private Sub New(radius As Integer,
                       period As TimeSpan,
                       angle As Double,
                       position As Point3D,
                       turnTracker As ITurnTracker)
            _radius = radius
            _period = period
            _angle = angle
            _position = position
            _turnTracker = turnTracker
        End Sub

        Private ReadOnly _radius As Integer
        Public ReadOnly Property Radius As Integer Implements IOrbit.Radius
            Get
                Return _radius
            End Get
        End Property

        Private _speed As Double = 0
        Public ReadOnly Property Speed As Double Implements IOrbit.Speed
            Get
                If Double.Equals(_speed, 0.0) AndAlso _turnTracker IsNot Nothing Then
                    _speed = (2 * Math.PI) / TurnsPerOrbit()
                End If
                Return _speed
            End Get
        End Property

        Private ReadOnly Property _period As TimeSpan
        Public ReadOnly Property Period As TimeSpan Implements IOrbit.Period
            Get
                Return _period
            End Get
        End Property

        Private _angle As Double
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
            ' currently a circular orbit

            If Speed > 0.0 Then
                Dim x = Radius * Math.Sin(Angle)
                Dim y = 0
                Dim z = Radius * Math.Cos(Angle)

                _position = New Point3D(x, y, z)

                _angle += Speed
            End If
        End Sub

        Private Sub SetInitialPosition()
            ' TODO: Calculate properly including the angle
            _position = New Point3D(0, 0, _radius)
        End Sub

        Private Function TurnsPerOrbit() As Double
            Return Period.Ticks / _turnTracker.TurnLength.Ticks
        End Function
    End Class
End Namespace