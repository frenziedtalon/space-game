Imports Entities
Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class OrbitingCelestialObjectBase
        Inherits BaseCelestialObject
        Implements IOrbitingObject

        Protected Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       orbit As Orbit)

            MyBase.New(name, mass, texture)
            _orbit = orbit
        End Sub

        Private ReadOnly _orbit As Orbit
        Public ReadOnly Property Orbit As Orbit Implements IOrbitingObject.Orbit
            Get
                return _orbit
            End Get
        End Property

        Public Overrides Sub Update()
            Throw New NotImplementedException()
        End Sub
    End Class
End NameSpace