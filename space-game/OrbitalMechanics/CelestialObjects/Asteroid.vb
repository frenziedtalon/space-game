Imports Core.Classes
Imports Entities

Namespace CelestialObjects
    Public Class Asteroid
        Inherits OrbitingCelestialObjectBase

        Public Sub New(name As String,
                       mass As Mass,
                       textures As List(Of Texture),
                       entityManager As IEntityManager)

            MyBase.New(name, mass, textures, entityManager)
        End Sub

    End Class
End Namespace