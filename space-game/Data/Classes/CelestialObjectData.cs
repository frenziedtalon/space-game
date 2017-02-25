using System.Collections.Generic;

namespace Data.Classes
{
    public class CelestialObjectData
    {
        private readonly PhysicalData _physical;

        private readonly OrbitData _orbit;
        public CelestialObjectData(OrbitData orbit, PhysicalData physical)
        {
            _orbit = orbit;
            _physical = physical;
        }

        public PhysicalData Physical
        {
            get { return _physical; }
        }

        public OrbitData Orbit
        {
            get { return _orbit; }
        }

        private readonly List<CelestialObjectData> _satellites = new List<CelestialObjectData>();
        public List<CelestialObjectData> Satellites
        {
            get { return _satellites; }
        }

        public void AddSatellite(CelestialObjectData s)
        {
            _satellites.Add(s);
        }

        public void AddSatellite(OrbitData o, PhysicalData p)
        {
            _satellites.Add(new CelestialObjectData(o, p));
        }

    }
}