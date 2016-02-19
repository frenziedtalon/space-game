Imports OrbitalMechanics

Namespace Services
    Public Class SceneService

        Private Shared _instance As SceneService
        Private Shared _number As Integer = 0


        Public Shared ReadOnly Property Instance() As SceneService
            Get
                If _instance Is Nothing Then
                    _instance = New SceneService()
                End If
                Return _instance
            End Get
        End Property

        Private Sub New()
        End Sub

        Public ReadOnly Property CurrentSceneState As SolarSystem
            Get
                ' TODO: load only relevant entities from the EM
                ' use the entity manager
                Throw New NotImplementedException
            End Get
        End Property

    End Class
End Namespace