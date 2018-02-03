using System.Collections.Generic;

namespace Data.ConfigDataProvider.Classes
{
    public class CelestialObject
    {
        public Physical Physical { get; set; }
        public Orbit Orbit { get; set; }
        public List<CelestialObject> Satellites { get; set; }
    }
}