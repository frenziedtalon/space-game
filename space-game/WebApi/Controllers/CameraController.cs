using Camera;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CameraController : ApiController
    {
        private readonly ICameraService _cameraService;
        public CameraController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        [HttpGet]
        public void SetTarget(string target)
        {
            _cameraService.SetTarget(target);
        }
    }
}
