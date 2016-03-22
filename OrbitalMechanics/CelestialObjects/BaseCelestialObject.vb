Imports Core.Extensions
Imports Entities

Namespace CelestialObjects

    Public MustInherit Class BaseCelestialObject
        Inherits BaseGameEntity
        Implements ICelestialObject

        Protected Sub New(name As String,
                        mass As Integer,
                        texture As String,
                        entityManager As IEntityManager,
                        Optional satellites As List(Of OrbitingCelestialObjectBase) = Nothing)
            MyBase.New(entityManager)
            _name = name
            _mass = mass
            _texture = texture
            _satellites = satellites
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

        Private ReadOnly _texture As String
        Public ReadOnly Property Texture As String Implements ICelestialObject.Texture
            Get
                Return _texture
            End Get
        End Property

        Private _satellites As List(Of OrbitingCelestialObjectBase)
        Public ReadOnly Property Satellites As List(Of OrbitingCelestialObjectBase)
            Get
                If _satellites Is Nothing Then
                    _satellites = New List(Of OrbitingCelestialObjectBase)
                End If
                Return _satellites
            End Get
        End Property

        Public Function ShouldSerializeSatellites() As Boolean
            Return _satellites.HasAny()
        End Function
    End Class
End Namespace