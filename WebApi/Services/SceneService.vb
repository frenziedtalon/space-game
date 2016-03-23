Imports Core.Extensions
Imports Entities
Imports OrbitalMechanics.CelestialObjects
Imports TurnTracker
Imports WebApi.Models

Namespace Services
    Public Class SceneService
        Implements ISceneService

        Private ReadOnly _entityManager As IEntityManager
        Private ReadOnly _turnTracker As ITurnTracker

        Public Sub New(entityManager As IEntityManager,
                       turnTracker As ITurnTracker)
            _entityManager = entityManager
            _turnTracker = turnTracker
        End Sub

        Public Sub CreateStartingScene() Implements ISceneService.CreateStartingScene
            Dim constructor = New SceneConstructor(_entityManager, _turnTracker)
            constructor.SolSystem()
        End Sub

        Public ReadOnly Property CurrentSceneState As List(Of BaseGameEntity) Implements ISceneService.CurrentSceneState
            Get
                ' Prevent satellites being returned twice, once as own entity and again in the "Satellites" collection on the primary
                Dim entities = _entityManager.GetAllEntities()

                Dim result As New List(Of BaseGameEntity)

                For Each e In entities
                    Dim o = TryCast(e, OrbitingCelestialObjectBase)
                    If o Is Nothing OrElse o.Primary.IsEmpty() Then
                        result.Add(o)
                    End If
                Next

                Return result
            End Get
        End Property

    End Class
End Namespace