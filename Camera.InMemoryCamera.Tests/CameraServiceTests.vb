Imports NUnit.Framework

<TestFixture>
Public Class CameraServiceTests

    <Test>
    Public Sub SetNewTarget_WhenTargetNotEmpty_ChangesTarget()
        Dim validTarget As Guid = Guid.NewGuid()

        Dim service As New CameraService
        service.SetNewTarget(validTarget)

        Dim result = service.CurrentTarget

        Assert.AreEqual(validTarget, result)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenTargetEmpty_DoesNotChangeTarget()
        Dim validTarget As Guid = Guid.NewGuid()
        Dim emptyTarget As Guid = Guid.Empty

        Dim service As New CameraService
        service.SetNewTarget(validTarget)
        service.SetNewTarget(emptyTarget)

        Dim result = service.CurrentTarget

        Assert.AreEqual(validTarget, result)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenCalledMultipleTimes_RetainsLastTarget()
        Dim firstTarget As Guid = Guid.NewGuid()
        Dim secondTarget As Guid = Guid.NewGuid()
        Dim thirdTarget As Guid = Guid.NewGuid()

        Dim service As New CameraService
        service.SetNewTarget(firstTarget)
        service.SetNewTarget(secondTarget)
        service.SetNewTarget(thirdTarget)

        Dim lastTargetResult = service.LastTarget
        Dim currentTargetResult = service.CurrentTarget

        Assert.AreEqual(secondTarget, lastTargetResult)
        Assert.AreEqual(thirdTarget, currentTargetResult)
    End Sub

End Class
