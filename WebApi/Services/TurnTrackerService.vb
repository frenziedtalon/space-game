Namespace Services
    Public Class TurnTrackerService

        Private Shared _instance As TurnTrackerService
        Private Shared _number As Integer = 0


        Public Shared ReadOnly Property Instance() As TurnTrackerService
            Get
                If _instance Is Nothing Then
                    _instance = New TurnTrackerService()
                End If
                Return _instance
            End Get
        End Property

        Private Sub New()
        End Sub

        Public ReadOnly Property Number As Integer
            Get
                Return _number
            End Get
        End Property

        Public Sub Update()
            _number += 1
        End Sub

    End Class
End Namespace