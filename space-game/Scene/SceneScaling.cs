using Core.Classes;
using Core.Extensions;
using Entities;
using OrbitalMechanics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scene
{
    public class SceneScaling
    {
        public SceneScaling()
        {
            SemiMajorAxis = new DistanceRange(Distance.FromAstronomicalUnits(1), Distance.FromAstronomicalUnits(1.1));
            CelestialObjectRadius = new DistanceRange(Distance.FromKilometers(6000), Distance.FromKilometers(6500));
        }

        public void ProcessEntity(BaseGameEntity entity)
        {
            List<Type> interfaceList = entity.GetType().GetInterfaces().ToList();
            if (interfaceList.HasAny())
            {
                if (interfaceList.Contains(typeof(ISphere)))
                {
                    ProcessSphericalEntity((ISphere)entity);
                }
                if (interfaceList.Contains(typeof(IOrbitingObject)))
                {
                    ProcessOrbitingEntity((IOrbitingObject)entity);
                }
            }
        }

        private void ProcessSphericalEntity(ISphere sphericalEntity)
        {
            CelestialObjectRadius.AddValue(sphericalEntity.Radius);
        }

        private void ProcessOrbitingEntity(IOrbitingObject orbitingEntity)
        {
            if (orbitingEntity.Orbit != null)
            {
                SemiMajorAxis.AddValue(orbitingEntity.Orbit.SemiMajorAxis);
            }
        }

        public DistanceRange SemiMajorAxis { get; }

        public DistanceRange CelestialObjectRadius { get; }
    }
}
