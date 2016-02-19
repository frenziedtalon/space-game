Imports NUnit.Framework
Imports WebApi.Services

<TestFixture>
Public Class TurnTrackerServiceTests

    <TestCase(1, 1)>
    <TestCase(50, 50)>
    <TestCase(1000, 1000)>
    Public Sub Update_WhenCalled_IncrementsTurnNumber(turns As Integer, expectedTurn As Integer)

        ' Arrange
        Dim service = New TurnTrackerService()

        ' Act
        For i = 1 To turns
            service.Update()
        Next

        ' Assert
        Assert.AreEqual(expectedTurn, service.TurnNumber)

    End Sub

End Class
