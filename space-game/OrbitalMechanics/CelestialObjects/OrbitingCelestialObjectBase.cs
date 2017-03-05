using Core.Classes;
using Core.Extensions;
using Data.Classes;
using Entities;
using OrbitalMechanics.Interfaces;
using System;
using System.Collections.Generic;

namespace OrbitalMechanics.CelestialObjects
{
    public class OrbitingCelestialObjectBase : BaseCelestialObject, IOrbitingObject
    {

        protected OrbitingCelestialObjectBase(string name, Mass mass, List<Texture> textures, IEntityManager entityManager, RingData rings) : base(name, mass, textures, entityManager, rings)
        {
        }

        public IOrbit Orbit { get; private set; }

        public Guid Primary { get; private set; }

        public bool ShouldSerializePrimary()
        {
            return !Primary.IsEmpty();
        }

        public override void Update()
        {
            Orbit?.Update();
        }

        public void SetOrbit(BaseCelestialObject p, IOrbit o)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            Orbit = o;

            // TODO: If parent / satellite mass can ever be changed in the future this needs to change
            Orbit.MassOfPrimary = p.Mass;
            Orbit.MassOfSatellite = this.Mass;

            Primary = p.Id;
        }

        private BaseCelestialObject GetPrimary()
        {
            return (BaseCelestialObject)_entityManager.GetEntityFromId(Primary);
        }
    }
}
