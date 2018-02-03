using System.Collections.Generic;

namespace Data.ConfigDataProvider.Classes
{
    public class Physical
    {
        public string Name { get; set; }
        public Mass Mass { get; set; }
        public List<Texture> Textures { get; set; }
        public string Type { get; set; }
        public Rings Rings { get; set; }
        public Distance Radius { get; set; }
    }
}