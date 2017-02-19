using NUnit.Framework;
using System;

namespace Camera.InMemoryCamera.Tests
{
    [TestFixture()]
    public class CameraServiceTests
    {

        [Test()]
        public void SetNewTarget_WhenTargetGuidNotEmpty_ChangesTarget()
        {
            Guid validTarget = Guid.NewGuid();

            CameraService service = new CameraService();
            service.SetTarget(validTarget);

            Guid result = service.CurrentTarget;

            Assert.AreEqual(validTarget, result);
        }

        [Test()]
        public void SetNewTarget_WhenTargetGuidEmpty_DoesNotChangeTarget()
        {
            Guid validTarget = Guid.NewGuid();
            Guid emptyTarget = Guid.Empty;

            CameraService service = new CameraService();
            service.SetTarget(validTarget);
            service.SetTarget(emptyTarget);

            Guid result = service.CurrentTarget;

            Assert.AreEqual(validTarget, result);
        }

        [Test()]
        public void SetNewTarget_WhenCalledMultipleTimes_RetainsLastTarget()
        {
            Guid firstTarget = Guid.NewGuid();
            Guid secondTarget = Guid.NewGuid();
            Guid thirdTarget = Guid.NewGuid();

            CameraService service = new CameraService();
            service.SetTarget(firstTarget);
            service.SetTarget(secondTarget);
            service.SetTarget(thirdTarget);

            Guid lastTargetResult = service.LastTarget;
            Guid currentTargetResult = service.CurrentTarget;

            Assert.AreEqual(secondTarget, lastTargetResult);
            Assert.AreEqual(thirdTarget, currentTargetResult);
        }

        [Test()]
        public void SetNewTarget_WhenTargetStringValidGuid_ChangesTarget()
        {
            Guid validTarget = Guid.NewGuid();

            CameraService service = new CameraService();
            service.SetTarget(validTarget.ToString());

            string result = service.CurrentTarget.ToString();

            Assert.AreEqual(validTarget.ToString(), result);
        }

        [Test()]
        public void SetNewTarget_WhenTargetStringEmptyGuid_DoesNotChangeTarget()
        {
            Guid validTarget = Guid.NewGuid();
            Guid emptyTarget = Guid.Empty;

            CameraService service = new CameraService();
            service.SetTarget(validTarget);
            service.SetTarget(emptyTarget.ToString());

            Guid result = service.CurrentTarget;

            Assert.AreEqual(validTarget, result);
        }

        [Test()]
        public void SetNewTarget_WhenTargetStringEmpty_DoesNotChangeTarget()
        {
            Guid validTarget = Guid.NewGuid();
            string emptyTarget = string.Empty;

            CameraService service = new CameraService();
            service.SetTarget(validTarget);
            service.SetTarget(emptyTarget);

            Guid result = service.CurrentTarget;

            Assert.AreEqual(validTarget, result);
        }

        [Test()]
        public void SetNewTarget_WhenTargetStringInvalidGuid_DoesNotChangeTarget()
        {
            Guid validTarget = Guid.NewGuid();
            const string invalidString = "invalidGuid";

            CameraService service = new CameraService();
            service.SetTarget(validTarget);
            service.SetTarget(invalidString);

            Guid result = service.CurrentTarget;

            Assert.AreEqual(validTarget, result);
        }

    }
}
