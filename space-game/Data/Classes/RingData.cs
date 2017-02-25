using Core.Classes;
using Core.Extensions;
using System;
using System.Collections.Generic;

namespace Data.Classes
{
    public class RingData : IEquatable<RingData>
    {

        public Distance InnerRadius { get; set; }
        public Distance OuterRadius { get; set; }
        public List<Texture> Textures { get; set; }

        public bool Equals(RingData other)
        {
            if (other != null)
            {
                return InnerRadius.Equals(other.InnerRadius) && OuterRadius.Equals(other.OuterRadius) && Textures.IsEquivalent(other.Textures);
            }
            return false;
        }
    }
}
