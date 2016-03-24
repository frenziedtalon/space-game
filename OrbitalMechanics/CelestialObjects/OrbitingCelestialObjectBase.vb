Imports Entities
Imports OrbitalMechanics.Classes
Imports Core.Extensions

Namespace CelestialObjects
    Public Class OrbitingCelestialObjectBase
        Inherits BaseCelestialObject
        Implements IOrbitingObject

        Protected Sub New(name As String,
                          mass As Integer,
                          texture As String,
                          orbit As IOrbit,
                          entityManager As IEntityManager)
            MyBase.New(name, mass, texture, entityManager)
            _orbit = orbit
        End Sub

        Private ReadOnly _orbit As IOrbit
        Public ReadOnly Property Orbit As IOrbit Implements IOrbitingObject.Orbit
            Get
                Return _orbit
            End Get
        End Property

        Public Property Primary As Guid Implements IOrbitingObject.Primary

        Public Function ShouldSerializePrimary() As Boolean
            Return Not Primary.IsEmpty()
        End Function

        Public Overrides Sub Update()
            If _orbit IsNot Nothing Then
                _orbit.Update()
            End If
        End Sub
    End Class
End Namespace