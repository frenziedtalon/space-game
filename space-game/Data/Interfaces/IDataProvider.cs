using Data.Classes;
using System.Collections.Generic;

namespace Data
{
    public interface IDataProvider
    {
        List<CelestialObjectData> SolarSystem { get; }
    }
}