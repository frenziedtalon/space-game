using Core.Extensions;
using Entities;
using OrbitalMechanics.CelestialObjects;
using System.Collections.Generic;

namespace Scene
{
    public class SceneState : ISceneState
    {

        private readonly List<BaseGameEntity> _objects = new List<BaseGameEntity>();

        public SceneState(IEnumerable<BaseGameEntity> objects)
        {
            // Prevent satellites being returned twice, once as own entity and again in the "Satellites" collection on the primary
            foreach (BaseGameEntity e in objects)
            {
                OrbitingCelestialObjectBase o = e as OrbitingCelestialObjectBase;
                if (o == null || o.Primary.IsEmpty())
                {
                    _objects.Add(o);
                }
                Scaling.ProcessEntity(e);
            }
        }

        // Object types will need to be split when we have more than just celestial bodies, e.g. ships
        public List<BaseGameEntity> CelestialObjects => _objects;

        public SceneScaling Scaling { get; } = new SceneScaling();
    }
}

