Imports Core.Extensions
Imports Entities

Namespace CelestialObjects

    Public MustInherit Class BaseCelestialObject
        Inherits BaseGameEntity
        Implements ICelestialObject

        Protected Sub New(name As String,
                        mass As Integer,
                        texture As String,
                        entityManager As IEntityManager)
            MyBase.New(entityManager)
            _name = name
            _mass = mass
            _texture = texture
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

        Private ReadOnly _satellites As New List(Of OrbitingCelestialObjectBase)
        Public ReadOnly Property Satellites As List(Of OrbitingCelestialObjectBase)
            Get
                Return _satellites
            End Get
        End Property

        Public Function ShouldSerializeSatellites() As Boolean
            Return _satellites.HasAny()
        End Function

        Public Sub AddSatellite(s As OrbitingCelestialObjectBase)
            If s IsNot Nothing Then
                s.Primary = Id
                _satellites.Add(s)
            End If
        End Sub

        Public Sub AddSatellite(s As List(Of OrbitingCelestialObjectBase))
            If s.HasAny() Then
                For Each o In s
                    AddSatellite(o)
                Next
            End If
        End Sub
    End Class
End Namespace