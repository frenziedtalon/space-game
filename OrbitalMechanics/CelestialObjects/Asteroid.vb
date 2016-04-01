
Imports Entities
Imports OrbitalMechanics.Classes

Namespace CelestialObjects
    Public Class Asteroid
        Inherits OrbitingCelestialObjectBase

        Public Sub New(name As String,
                       mass As Integer,
                       texture As String,
                       entityManager As IEntityManager)

            MyBase.New(name, mass, texture, entityManager)
        End Sub

    End Class
End Namespace