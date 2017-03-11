using Core.Enums;
using Newtonsoft.Json;
using System;

namespace Core.Classes
{
    public class Texture : IEquatable<Texture>
    {

        [JsonIgnore()]
        public TextureType TypeEnum;
        public string Path;
        [JsonIgnore()]
        public TextureQuality QualityEnum;
        public string Type => TypeEnum.ToString();

        public string Quality => QualityEnum.ToString();

        public virtual bool Equals(Texture other)
        {
            if (other != null)
            {
                return TypeEnum.Equals(other.TypeEnum) && Path.Equals(other.Path, StringComparison.OrdinalIgnoreCase) && QualityEnum.Equals(other.QualityEnum);
            }
            return false;
        }
    }
}
