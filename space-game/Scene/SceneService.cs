using Data;
using Entities;
using System.Collections.Generic;
using TurnTracker;

namespace Scene
{
    public class SceneService : ISceneService
    {
        private readonly IEntityManager _entityManager;
        private readonly SceneConstructor _constructor;

        public SceneService(IEntityManager entityManager, ITurnTracker turnTracker, IDataProvider dataProvider)
        {
            _entityManager = entityManager;
            _constructor = new SceneConstructor(_entityManager, turnTracker, dataProvider);
        }

        public void CreateStartingScene()
        {
            _constructor.SolSystem();
        }

        public ISceneState CurrentSceneState
        {
            get
            {
                List<BaseGameEntity> entities = _entityManager.GetAllEntities();
                return new SceneState(entities);
            }
        }
    }
}
