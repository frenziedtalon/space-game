Namespace Services
    Public Class TurnTrackerService
        Implements ITurnTrackerService

        Private _number As Integer = 0

        Public ReadOnly Property TurnNumber As Integer Implements ITurnTrackerService.TurnNumber
            Get
                Return _number
            End Get
        End Property

        Public Sub Update() Implements ITurnTrackerService.Update
            _number += 1
        End Sub

    End Class
End Namespace