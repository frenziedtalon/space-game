using Camera;
using Scene;

namespace WebApi.Classes
{
    public class TurnResult
    {
        private readonly ISceneService _sceneService;
        private readonly ICameraService _cameraService;

        public TurnResult(ISceneService sceneService, ICameraService cameraService)
        {
            _sceneService = sceneService;
            _cameraService = cameraService;
            Camera = new Camera(_cameraService);
        }

        public ISceneState Scene => _sceneService.CurrentSceneState;

        public Camera Camera { get; }
    }
}
