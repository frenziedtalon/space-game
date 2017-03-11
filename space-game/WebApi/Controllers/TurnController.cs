using Camera;
using Entities;
using Scene;
using System.Web.Http;
using TurnTracker;
using WebApi.Classes;

namespace WebApi.Controllers
{
    public class TurnController : ApiController
    {
        private readonly ITurnTracker _turnTracker;
        private readonly IEntityManager _entityManager;
        private readonly ISceneService _sceneService;

        private readonly ICameraService _cameraService;

        public TurnController(ITurnTracker turnTracker, IEntityManager entityManager, ISceneService sceneService, ICameraService cameraService)
        {
            _turnTracker = turnTracker;
            _entityManager = entityManager;
            _sceneService = sceneService;
            _cameraService = cameraService;
        }

        [HttpGet]
        public TurnResult EndTurn()
        {

            if (_turnTracker.TurnNumber == 0)
            {
                _sceneService.CreateStartingScene();
            }
            else
            {
                _entityManager.UpdateAll();
            }

            _turnTracker.Update();

            return new TurnResult(_sceneService, _cameraService);
        }
    }
}
