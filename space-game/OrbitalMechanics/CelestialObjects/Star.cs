using Core.Classes;
using Data.Classes;
using Entities;
using Newtonsoft.Json;
using OrbitalMechanics.Interfaces;
using System;
using System.Collections.Generic;

namespace OrbitalMechanics.CelestialObjects
{
    public class Star : OrbitingCelestialObjectBase, ISphere
    {
        public Star(string name, Mass mass, int surfaceTemperature, List<Texture> textures, Distance radius, IEntityManager entityManager, RingData rings) : base(name, mass, textures, entityManager, rings)
        {
            SurfaceTemperature = surfaceTemperature;
            Radius = radius;
        }

        public Star(int surfaceTemperature, PhysicalData physicalData, IEntityManager entityManager) : base(physicalData.Name, physicalData.Mass, physicalData.Textures, entityManager, physicalData.Rings)
        {
            SurfaceTemperature = surfaceTemperature;
            Radius = physicalData.Radius;
        }

        public StarClassification Classification
        {
            get
            {
                if (SurfaceTemperature >= 33000)
                {
                    return StarClassification.O;
                }
                if (SurfaceTemperature >= 10500)
                {
                    return StarClassification.B;
                }
                if (SurfaceTemperature >= 7500)
                {
                    return StarClassification.A;
                }
                if (SurfaceTemperature >= 6000)
                {
                    return StarClassification.F;
                }
                if (SurfaceTemperature >= 5500)
                {
                    return StarClassification.G;
                }
                if (SurfaceTemperature >= 4000)
                {
                    return StarClassification.K;
                }
                return StarClassification.M;
            }
        }

        /// <summary>
        /// Surface temperature of the star. Unit is Kelvins.
        /// </summary>
        public int SurfaceTemperature { get; }

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

        [JsonIgnore]
        public int LightIntensity
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Star light intensity fades gradually until becoming zero at this range
        /// </summary>
        [JsonIgnore]
        public int LightRange
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

    public enum StarClassification
    {
        O,
        B,
        A,
        F,
        G,
        K,
        M
    }
}
