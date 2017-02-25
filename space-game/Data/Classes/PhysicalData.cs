using Core.Classes;
using System.Collections.Generic;

namespace Data.Classes
{
    public class PhysicalData : IPhysicalData
    {

        public PhysicalData(string name, Distance radius, Mass mass, List<Texture> textures, CelestialObjectType type, RingData rings)
        {
            this.Radius = radius;
            this.Name = name;
            this.Mass = mass;
            this.Textures = textures;
            this.Type = type;
            this.Rings = rings;
        }

        private PhysicalData()
        {
            // For Mapster
        }

        public string Name { get; set; }
        public Mass Mass { get; set; }
        public List<Texture> Textures { get; set; }
        public CelestialObjectType Type { get; set; }
        public RingData Rings { get; set; }
        public Distance Radius { get; set; }
    }
}
