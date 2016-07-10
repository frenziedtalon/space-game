Imports NUnit.Framework

<TestFixture>
Public Class TurnTrackerTests

    <TestCase(1, 1)>
    <TestCase(50, 50)>
    <TestCase(1000, 1000)>
    Public Sub Update_WhenCalled_IncrementsTurnNumber(turns As Integer, expectedTurn As Integer)

        ' Arrange
        Dim service = New TurnTrackerMock()

        ' Act
        For i = 1 To turns
            service.Update()
        Next

        ' Assert
        Assert.AreEqual(expectedTurn, service.TurnNumber)

    End Sub

    <Test()>
    Public Sub StartDate_WhenCalled_ReturnsExpectedValue()
        Dim turnTracker = New TurnTrackerMock
        Dim expectedStartDate = New Date(1990, 1, 1)

        Dim result = turnTracker.StartDate

        Assert.AreEqual(expectedStartDate, result)
    End Sub

    <Test, TestCaseSource(NameOf(CurrentDateTestCases))>
    Public Sub CurrentDate_WhenUpdateCalled_ReturnsExpectedValue(turnLength As TimeSpan, turns As Integer, expectedCurrentDate As Date)
        Dim turnTracker = New TurnTrackerMock(turnLength)

        For i = 1 To turns
            turnTracker.Update()
        Next

        Dim result = turnTracker.CurrentDate

        Assert.AreEqual(expectedCurrentDate, result)
    End Sub

    Private Shared ReadOnly Property CurrentDateTestCases As List(Of TestCaseData)
        Get
            ' Starting date is 1st January 1990
            Dim result As New List(Of TestCaseData)
            result.Add(New TestCaseData(TimeSpan.FromDays(1), 0, New Date(1990, 1, 1)))
            result.Add(New TestCaseData(TimeSpan.FromDays(1), 10, New Date(1990, 1, 11)))
            result.Add(New TestCaseData(TimeSpan.FromDays(1), 35, New Date(1990, 2, 5)))
            result.Add(New TestCaseData(TimeSpan.FromDays(30), 5, New Date(1990, 5, 31)))
            result.Add(New TestCaseData(TimeSpan.FromDays(30), 1000, New Date(2072, 2, 20)))
            Return result
        End Get
    End Property

    Private Class TurnTrackerMock
        Inherits TurnTrackerBase

        Public Sub New(Optional turnLength As TimeSpan = Nothing)
            MyBase.New(turnLength)
        End Sub
    End Class
End Class
