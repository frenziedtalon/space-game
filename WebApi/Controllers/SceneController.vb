
Imports System.Web.Http
Imports Entities
Imports OrbitalMechanics
Imports WebApi.Models

Namespace Controllers
    Public Class SceneController
        Inherits ApiController

        Private ReadOnly _entityManager As IEntityManager

        Public Sub New(entityManager As IEntityManager)
            _entityManager = entityManager
        End Sub

        ' GET api/values
        Public Function GetSceneObjects() As SolarSystem
            Dim constructor = New SceneConstructor
            Return constructor.SolSystem(_entityManager)
        End Function

    End Class
End Namespace