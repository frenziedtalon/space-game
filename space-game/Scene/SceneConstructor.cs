using Data;
using Data.Classes;
using Entities;
using OrbitalMechanics.CelestialObjects;
using OrbitalMechanics.Classes;
using System.Collections.Generic;
using System.Linq;
using TurnTracker;

namespace Scene
{
    public class SceneConstructor
    {
        private readonly IEntityManager _entityManager;
        private readonly ITurnTracker _turnTracker;
        private readonly IDataProvider _dataProvider;

        public SceneConstructor(IEntityManager entityManager, ITurnTracker turnTracker, IDataProvider dataProvider)
        {
            _entityManager = entityManager;
            _turnTracker = turnTracker;
            _dataProvider = dataProvider;
        }

        private ICelestialObject RecursiveCreateCelestialObject(CelestialObjectData o)
        {
            BaseCelestialObject primary = CreateCorrectCelestialObject(o);

            if (o.Satellites.Any())
            {
                foreach (CelestialObjectData satellite in o.Satellites)
                {
                    ICelestialObject s = RecursiveCreateCelestialObject(satellite);
                    if (s != null)
                    {
                        Orbit orbit = new Orbit(_turnTracker, satellite.Orbit, true);
                        primary.AddSatellite((OrbitingCelestialObjectBase)s, orbit);
                    }
                }
            }
            return primary;
        }

        private BaseCelestialObject CreateCorrectCelestialObject(CelestialObjectData o)
        {
            switch (o.Physical.Type)
            {
                case CelestialObjectType.Star:
                    return new Star(5500, o.Physical, _entityManager);
                case CelestialObjectType.Planet:
                    return new Planet(o.Physical, _entityManager);
                case CelestialObjectType.Moon:
                    return new Moon(o.Physical, _entityManager);
                default:
                    return null;
            }
        }

        public List<ICelestialObject> SolSystem()
        {
            List<CelestialObjectData> system = _dataProvider.SolarSystem;
            return (from o in system select RecursiveCreateCelestialObject(o)).ToList();
        }
    }
}
