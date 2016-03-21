
Public Interface ITurnTracker
    ReadOnly Property TurnNumber As Integer
    Sub Update()
    ReadOnly Property StartDate As Date
    ReadOnly Property CurrentDate As Date
End Interface