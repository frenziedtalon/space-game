

Public Class TurnTracker
    Implements ITurnTracker

    Private _number As Integer = 0

    Public ReadOnly Property TurnNumber As Integer Implements ITurnTracker.TurnNumber
        Get
            Return _number
        End Get
    End Property

    Public Sub Update() Implements ITurnTracker.Update
        _number += 1
    End Sub

End Class