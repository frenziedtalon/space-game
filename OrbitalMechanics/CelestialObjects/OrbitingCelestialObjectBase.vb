Imports Core.Classes
Imports Entities
Imports Core.Extensions

Namespace CelestialObjects
    Public Class OrbitingCelestialObjectBase
        Inherits BaseCelestialObject
        Implements IOrbitingObject

        Protected Sub New(name As String,
                          mass As Mass,
                          texture As String,
                          entityManager As IEntityManager)
            MyBase.New(name, mass, texture, entityManager)
        End Sub

        Private _orbit As IOrbit
        Public ReadOnly Property Orbit As IOrbit Implements IOrbitingObject.Orbit
            Get
                Return _orbit
            End Get
        End Property

        Private _primary As Guid
        Public ReadOnly Property Primary As Guid Implements IOrbitingObject.Primary
            Get
                Return _primary
            End Get
        End Property

        Public Function ShouldSerializePrimary() As Boolean
            Return Not Primary.IsEmpty()
        End Function

        Public Overrides Sub Update()
            If _orbit IsNot Nothing Then
                _orbit.Update()
            End If
        End Sub

        Public Sub SetOrbit(p As BaseCelestialObject, o As IOrbit) Implements IOrbitingObject.SetOrbit
            If p Is Nothing Then
                Throw New ArgumentNullException(NameOf(p))
            ElseIf o Is Nothing Then
                Throw New ArgumentNullException(NameOf(o))
            End If

            _orbit = o
            _primary = p.Id
        End Sub

        Private Function GetPrimary() As BaseCelestialObject
            Return DirectCast(_entityManager.GetEntityFromId(Primary), BaseCelestialObject)
        End Function
    End Class
End Namespace