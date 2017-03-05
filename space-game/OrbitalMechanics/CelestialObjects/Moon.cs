using Core.Classes;
using Data.Classes;
using Entities;
using OrbitalMechanics.Interfaces;
using System;
using System.Collections.Generic;

namespace OrbitalMechanics.CelestialObjects
{
    public class Moon : OrbitingCelestialObjectBase, ISphere
    {
        public Moon(string name, Mass mass, List<Texture> textures, Distance radius, IEntityManager entityManager, RingData rings) : base(name, mass, textures, entityManager, rings)
        {
            Radius = radius;
        }


        public Moon(PhysicalData physicalData, IEntityManager entityManager) : base(physicalData.Name, physicalData.Mass, physicalData.Textures, entityManager, physicalData.Rings)
        {
            Radius = physicalData.Radius;
        }

        public Distance Radius { get; }

        public int AxialTilt
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int RotationSpeed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private double _volume = 0;
        public double Volume
        {
            get
            {
                if (double.Equals(_volume, 0.0) && Radius.Kilometers > 0)
                {
                    _volume = Core.Helpers.Shapes.ShapeHelper.VolumeOfASphere(Radius.Kilometers);
                }
                return _volume;
            }
        }
    }
}
