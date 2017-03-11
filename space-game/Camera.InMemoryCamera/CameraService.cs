using Core.Extensions;
using System;

namespace Camera.InMemoryCamera
{
    public class CameraService : ICameraService
    {
        public Guid CurrentTarget { get; private set; }

        public Guid LastTarget { get; private set; }

        public void SetTarget(string target)
        {
            if (!string.IsNullOrWhiteSpace(target))
            {
                Guid targetGuid;
                if (Guid.TryParse(target, out targetGuid))
                {
                    SetTarget(targetGuid);
                }
            }
        }

        public void SetTarget(Guid target)
        {
            if (!target.IsEmpty())
            {
                LastTarget = CurrentTarget;
                CurrentTarget = target;
            }
        }

    }
}
