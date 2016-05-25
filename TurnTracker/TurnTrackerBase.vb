Public MustInherit Class TurnTrackerBase
    Implements ITurnTracker

    Private _number As Integer = 0
    Private ReadOnly _turnLength As TimeSpan
    Private ReadOnly _defaultTurnLength As TimeSpan = TimeSpan.FromDays(30)
    Private ReadOnly _startdate As Date = New Date(1990, 1, 1)

    Public Sub New(Optional turnLength As TimeSpan = Nothing)
        If turnLength = Nothing Then
            _turnLength = _defaultTurnLength
        Else
            _turnLength = turnLength
        End If
    End Sub

    Public ReadOnly Property TurnNumber As Integer Implements ITurnTracker.TurnNumber
        Get
            Return _number
        End Get
    End Property

    Public Overridable Sub Update() Implements ITurnTracker.Update
        _number += 1
    End Sub

    Public ReadOnly Property StartDate As Date Implements ITurnTracker.StartDate
        Get
            Return _startdate
        End Get
    End Property

    Public ReadOnly Property CurrentDate() As Date Implements ITurnTracker.CurrentDate
        Get
            Return _startdate.AddDays(_turnLength.Days * _number)
        End Get
    End Property

    Public ReadOnly Property TurnLength As TimeSpan Implements ITurnTracker.TurnLength
        Get
            Return _turnLength
        End Get
    End Property

    Public ReadOnly Property TimeSinceStart As TimeSpan Implements ITurnTracker.TimeSinceStart
        Get
            Return TimeSpan.FromDays(_turnLength.Days * _number)
        End Get
    End Property
End Class
