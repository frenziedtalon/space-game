using Core.Classes;
using Core.Extensions;
using Data.Classes;
using Entities;
using OrbitalMechanics.Classes;
using OrbitalMechanics.Interfaces;
using System;
using System.Collections.Generic;

namespace OrbitalMechanics.CelestialObjects
{

    public abstract class BaseCelestialObject : BaseGameEntity, ICelestialObject
    {

        protected BaseCelestialObject(string name, Mass mass, List<Texture> textures, IEntityManager entityManager, RingData rings) : base(entityManager)
        {
            Name = name;
            Mass = mass;
            _textures = textures;
            Rings = rings != null ? new RingSystem(rings.InnerRadius, rings.OuterRadius, rings.Textures) : null;
        }

        public Mass Mass { get; }

        public string Name { get; }

        private readonly List<Texture> _textures;
        public List<Texture> Textures => _textures.GetHighestAvailableResolutionForEachType();

        public List<OrbitingCelestialObjectBase> Satellites { get; } = new List<OrbitingCelestialObjectBase>();

        public bool ShouldSerializeSatellites()
        {
            return Satellites.HasAny();
        }


        public void AddSatellite(OrbitingCelestialObjectBase s, IOrbit o)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            else if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }

            s.SetOrbit(this, o);
            Satellites.Add(s);
        }

        public RingSystem Rings { get; }

        public bool ShouldSerializeRings()
        {
            return Rings != null;
        }

    }
}
