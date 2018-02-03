using System.Collections.Generic;

namespace Data.Classes
{
    public class CelestialObjectData
    {
        public CelestialObjectData(OrbitData orbit, PhysicalData physical)
        {
            Orbit = orbit;
            Physical = physical;
        }

        public PhysicalData Physical { get; }

        public OrbitData Orbit { get; }

        public List<CelestialObjectData> Satellites { get; } = new List<CelestialObjectData>();

        public void AddSatellite(CelestialObjectData s)
        {
            Satellites.Add(s);
        }

        public void AddSatellite(OrbitData o, PhysicalData p)
        {
            Satellites.Add(new CelestialObjectData(o, p));
        }

    }
}