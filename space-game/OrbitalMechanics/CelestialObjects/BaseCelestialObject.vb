﻿Imports Core.Classes
Imports Core.Extensions
Imports Entities

Namespace CelestialObjects

    Public MustInherit Class BaseCelestialObject
        Inherits BaseGameEntity
        Implements ICelestialObject

        Protected Sub New(name As String,
                        mass As Mass,
                        textures As List(Of Texture),
                        entityManager As IEntityManager)
            MyBase.New(entityManager)
            _name = name
            _mass = mass
            _textures = textures
        End Sub

        Private ReadOnly _mass As Mass
        Public ReadOnly Property Mass As Mass Implements ICelestialObject.Mass
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

        Private ReadOnly _textures As List(Of Texture)
        Public ReadOnly Property Textures As List(Of Texture) Implements ICelestialObject.Textures
            Get
                Return _textures.GetHighestAvailableResolutionForEachType()
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

        Public Sub AddSatellite(s As OrbitingCelestialObjectBase, o As IOrbit)

            If s Is Nothing Then
                Throw New ArgumentNullException(NameOf(s))
            ElseIf o Is Nothing Then
                Throw New ArgumentNullException(NameOf(o))
            End If

            s.SetOrbit(Me, o)
            _satellites.Add(s)
        End Sub
    End Class
End Namespace