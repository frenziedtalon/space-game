using Core.Classes;
using Data.Classes;
using Entities;
using System.Collections.Generic;

namespace OrbitalMechanics.CelestialObjects
{
    public class Asteroid : OrbitingCelestialObjectBase
    {
        public Asteroid(string name, Mass mass, List<Texture> textures, IEntityManager entityManager, RingData rings) : base(name, mass, textures, entityManager, rings)
        {
        }

    }
}
