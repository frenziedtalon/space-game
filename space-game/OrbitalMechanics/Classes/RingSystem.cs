using Core.Classes;
using System.Collections.Generic;

namespace OrbitalMechanics.Classes
{
    public class RingSystem
    {
        public Distance InnerRadius { get; set; }
        public Distance OuterRadius { get; set; }
        public List<Texture> Textures { get; set; }

        public RingSystem(Distance innerRadius, Distance outerRadius, List<Texture> textures)
        {
            InnerRadius = innerRadius;
            OuterRadius = outerRadius;
            Textures = textures;
        }
    }
}
