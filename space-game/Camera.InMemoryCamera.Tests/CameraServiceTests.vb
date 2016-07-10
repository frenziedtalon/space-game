Imports NUnit.Framework

<TestFixture>
Public Class CameraServiceTests

    <Test>
    Public Sub SetNewTarget_WhenTargetGuidNotEmpty_ChangesTarget()
        Dim validTarget As Guid = Guid.NewGuid()

        Dim service As New CameraService
        service.SetTarget(validTarget)

        Dim result = service.CurrentTarget

        Assert.AreEqual(validTarget, result)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenTargetGuidEmpty_DoesNotChangeTarget()
        Dim validTarget As Guid = Guid.NewGuid()
        Dim emptyTarget As Guid = Guid.Empty

        Dim service As New CameraService
        service.SetTarget(validTarget)
        service.SetTarget(emptyTarget)

        Dim result = service.CurrentTarget

        Assert.AreEqual(validTarget, result)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenCalledMultipleTimes_RetainsLastTarget()
        Dim firstTarget As Guid = Guid.NewGuid()
        Dim secondTarget As Guid = Guid.NewGuid()
        Dim thirdTarget As Guid = Guid.NewGuid()

        Dim service As New CameraService
        service.SetTarget(firstTarget)
        service.SetTarget(secondTarget)
        service.SetTarget(thirdTarget)

        Dim lastTargetResult = service.LastTarget
        Dim currentTargetResult = service.CurrentTarget

        Assert.AreEqual(secondTarget, lastTargetResult)
        Assert.AreEqual(thirdTarget, currentTargetResult)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenTargetStringValidGuid_ChangesTarget()
        Dim validTarget As Guid = Guid.NewGuid()

        Dim service As New CameraService
        service.SetTarget(validTarget.ToString())

        Dim result = service.CurrentTarget.ToString()

        Assert.AreEqual(validTarget.ToString(), result)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenTargetStringEmptyGuid_DoesNotChangeTarget()
        Dim validTarget As Guid = Guid.NewGuid()
        Dim emptyTarget As Guid = Guid.Empty

        Dim service As New CameraService
        service.SetTarget(validTarget)
        service.SetTarget(emptyTarget.ToString())

        Dim result = service.CurrentTarget

        Assert.AreEqual(validTarget, result)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenTargetStringEmpty_DoesNotChangeTarget()
        Dim validTarget As Guid = Guid.NewGuid()
        Dim emptyTarget As String = String.Empty

        Dim service As New CameraService
        service.SetTarget(validTarget)
        service.SetTarget(emptyTarget)

        Dim result = service.CurrentTarget

        Assert.AreEqual(validTarget, result)
    End Sub

    <Test>
    Public Sub SetNewTarget_WhenTargetStringInvalidGuid_DoesNotChangeTarget()
        Dim validTarget As Guid = Guid.NewGuid()
        Dim invalidString As String = "invalidGuid"

        Dim service As New CameraService
        service.SetTarget(validTarget)
        service.SetTarget(invalidString)

        Dim result = service.CurrentTarget

        Assert.AreEqual(validTarget, result)
    End Sub

End Class
