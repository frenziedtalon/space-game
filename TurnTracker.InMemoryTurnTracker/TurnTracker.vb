Public Class TurnTracker
    Inherits TurnTrackerBase

    Public Sub New()
        MyBase.New(TimeSpan.FromDays(30))
    End Sub

End Class