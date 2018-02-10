using Data.Classes;
using Jespers.Config;
using Mapster;
using System.Collections.Generic;

namespace Data.ConfigDataProvider.Classes
{
    public class DataProvider : IDataProvider
    {
        public List<CelestialObjectData> SolarSystem
        {
            get
            {
                SolarSystem system = AppConfig.Provider.Get<SolarSystem>();
                return system.Adapt<List<CelestialObjectData>>();
            }
        }
    }
}
