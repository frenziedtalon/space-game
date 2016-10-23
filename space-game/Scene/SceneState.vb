Imports Core.Extensions
Imports Entities
Imports OrbitalMechanics.CelestialObjects

Public Class SceneState
    Implements ISceneState

    Private ReadOnly _objects As New List(Of BaseGameEntity)
    Private ReadOnly _scaling As New SceneScaling

    Public Sub New(objects As List(Of BaseGameEntity))
        ' Prevent satellites being returned twice, once as own entity and again in the "Satellites" collection on the primary
        For Each e In objects
            Dim o = TryCast(e, OrbitingCelestialObjectBase)
            If o Is Nothing OrElse o.Primary.IsEmpty() Then
                _objects.Add(o)
            End If

            _scaling.ProcessEntity(e)
        Next
    End Sub

    Public ReadOnly Property CelestialObjects As List(Of BaseGameEntity) Implements ISceneState.CelestialObjects
        Get
            ' Object types will need to be split when we have more than just celestial bodies, e.g. ships
            Return _objects
        End Get
    End Property

    Public ReadOnly Property Scaling As SceneScaling Implements ISceneState.Scaling
        Get
            Return _scaling
        End Get
    End Property
End Class
