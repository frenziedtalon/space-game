
Imports System.Windows.Media.Media3D
Imports Entities

Namespace CelestialObjects

    Public MustInherit Class BaseCelestialObject
        Inherits BaseGameEntity
        Implements ICelestialObject
        Implements IMovingObject

        Protected Sub New(name As String,
                       mass As Integer,
                          texture As String,
                          position As Point3D,
                          motion As Vector3D)

            _name = name
            _mass = mass
            _texture = texture
            _postion = position
            _motion = motion

        End Sub

        Private ReadOnly _mass As Integer
        Public ReadOnly Property Mass As Integer Implements ICelestialObject.Mass
            Get
                Return _mass
            End Get
        End Property

        Private ReadOnly _name As String
        Public ReadOnly Property Name As String Implements ICelestialObject.Name
            Get
                Return _name
            End Get
        End Property

        Private _postion As Point3D
        Public ReadOnly Property Position As Point3D Implements IMovingObject.Position
            Get
                Return _postion
            End Get
        End Property

        Private _motion As Vector3D
        Public ReadOnly Property Motion As Vector3D Implements IMovingObject.Motion
            Get
                Return _motion
            End Get
        End Property

        Private _texture As String
        Public ReadOnly Property Texture As String Implements ICelestialObject.Texture
            Get
                Return _texture
            End Get
        End Property
    End Class
End Namespace