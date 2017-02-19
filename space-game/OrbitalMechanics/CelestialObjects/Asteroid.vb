Imports Core.Classes
Imports Data.Classes
Imports Entities

Namespace CelestialObjects
    Public Class Asteroid
        Inherits OrbitingCelestialObjectBase

        Public Sub New(name As String,
                       mass As Mass,
                       textures As List(Of Texture),
                       entityManager As IEntityManager,
                       rings As RingData)

            MyBase.New(name, mass, textures, entityManager, rings)
        End Sub

    End Class
End Namespace