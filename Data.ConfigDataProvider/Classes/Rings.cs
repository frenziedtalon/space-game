using System.Collections.Generic;

namespace Data.ConfigDataProvider.Classes
{
    public class Rings
    {
        public Distance InnerRadius { get; set; }
        public Distance OuterRadius { get; set; }
        public List<Texture> Textures { get; set; }
    }
}