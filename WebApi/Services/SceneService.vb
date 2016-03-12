Imports Entities
Imports OrbitalMechanics
Imports WebApi.Models

Namespace Services
    Public Class SceneService
        Implements ISceneService

        Private ReadOnly _entityManager As IEntityManager

        Public Sub New(entityManager As IEntityManager)
            _entityManager = entityManager
        End Sub

        End Sub

        Public ReadOnly Property CurrentSceneState As SolarSystem Implements ISceneService.CurrentSceneState
            Get
                ' TODO: load only relevant entities from the EM
                ' use the entity manager
                Throw New NotImplementedException
            End Get
        End Property

    End Class
End Namespace