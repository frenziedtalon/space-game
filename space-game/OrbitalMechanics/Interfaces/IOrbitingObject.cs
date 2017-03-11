using OrbitalMechanics.CelestialObjects;
using System;

namespace OrbitalMechanics.Interfaces
{
    public interface IOrbitingObject
    {
        IOrbit Orbit { get; }
        Guid Primary { get; }
        void SetOrbit(BaseCelestialObject p, IOrbit o);
    }
}
