using System;

namespace Camera
{
    public interface ICameraService
    {
        Guid CurrentTarget { get; }
        void SetTarget(string target);
        void SetTarget(Guid target);
        Guid LastTarget { get; }
    }
}
