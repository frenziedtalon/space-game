﻿
Public Interface ITurnTracker
    ReadOnly Property TurnNumber As Integer
    Sub Update()
    ReadOnly Property StartDate As Date
    ReadOnly Property CurrentDate As Date
    ReadOnly Property TurnLength As TimeSpan
    ReadOnly Property TimeSinceStart As TimeSpan
End Interface