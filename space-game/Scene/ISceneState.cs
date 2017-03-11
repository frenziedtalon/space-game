using Entities;
using System.Collections.Generic;

namespace Scene
{
    public interface ISceneState
    {
        List<BaseGameEntity> CelestialObjects { get; }
        SceneScaling Scaling { get; }
    }
}
