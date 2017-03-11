using Camera;
using System;

namespace WebApi.Classes
{
    public class Camera
    {
        private readonly ICameraService _cameraService;
        public Camera(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public Guid CurrentTarget => _cameraService.CurrentTarget;
    }
}
