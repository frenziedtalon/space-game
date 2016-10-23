Imports Core.Classes
Imports Entities

Namespace CelestialObjects
    Public Class Asteroid
        Inherits OrbitingCelestialObjectBase

        Public Sub New(name As String,
                       mass As Mass,
                       texture As Textures,
                       entityManager As IEntityManager)

            MyBase.New(name, mass, texture, entityManager)
        End Sub

    End Class
End Namespace