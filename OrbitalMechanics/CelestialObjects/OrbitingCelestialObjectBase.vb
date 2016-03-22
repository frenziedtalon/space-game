Imports Entities
Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class OrbitingCelestialObjectBase
        Inherits BaseCelestialObject
        Implements IOrbitingObject

        Protected Sub New(name As String,
                          mass As Integer,
                          texture As String,
                          orbit As IOrbit,
                          entityManager As IEntityManager,
                        Optional satellites As List(Of OrbitingCelestialObjectBase) = Nothing)
            MyBase.New(name, mass, texture, entityManager, satellites)
            _orbit = orbit
        End Sub

        Private ReadOnly _orbit As IOrbit
        Public ReadOnly Property Orbit As IOrbit Implements IOrbitingObject.Orbit
            Get
                Return _orbit
            End Get
        End Property

        Public Overrides Sub Update()
            _orbit.Update()
        End Sub
    End Class
End Namespace