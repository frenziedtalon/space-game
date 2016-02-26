
Imports System.Web.Http
Imports Entities
Imports OrbitalMechanics
Imports WebApi.Models

Namespace Controllers
    Public Class SceneController
        Inherits ApiController

        Private _entityManager As IEntityManager

        Public Sub SceneController(entityManager As IEntityManager)
            _entityManager = entityManager
        End Sub

        ' GET api/values
        Public Function GetSceneObjects(entityManager As IEntityManager) As SolarSystem
            Dim constructor = New SceneConstructor
            Return constructor.SolSystem(entityManager)
        End Function

    End Class
End Namespace