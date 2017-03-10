using Core.Extensions;
using Data.Classes;
using Data.SqlDataProvider.Extensions;
using Data.SqlDataProvider.Model;
using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace Data.SqlDataProvider.Classes
{
    public class DataProvider : IDataProvider
    {
        public List<CelestialObjectData> SolarSystem
        {
            get
            {
                List<CelestialObjectData> result = new List<CelestialObjectData>();
                List<CelestialObject> system = GetSolarSystem();
                if (system.HasAny())
                {
                    result.AddRange(from co in system where !co.PrimaryId.HasValue select RecursiveCreateObject(co));
                }
                return result;
            }
        }

        private CelestialObjectData RecursiveCreateObject(CelestialObject co)
        {
            OrbitData orbit = co.PrimaryId.HasValue ? co.Adapt<OrbitData>() : null;
            PhysicalData physical = co.Adapt<PhysicalData>();

            CelestialObjectData primary = new CelestialObjectData(orbit, physical);

            if (co.CelestialObject1.HasAny())
            {
                foreach (CelestialObject satellite in co.CelestialObject1)
                {
                    CelestialObjectData s = RecursiveCreateObject(satellite);

                    if (s != null)
                    {
                        primary.AddSatellite(s);
                    }
                }
            }
            return primary;
        }

        private List<CelestialObject> GetSolarSystem()
        {
            return GetSolarSystemByName("Solar System");
        }

        private List<CelestialObject> GetSolarSystemByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                using (SolarSystemEntities con = new SolarSystemEntities())
                {
                    con.Configuration.LazyLoadingEnabled = false;
                    con.Configuration.ProxyCreationEnabled = false;

                    List<CelestialObject> result = con.CelestialObjects
                                    .Where(o => o.SolarSystem.Name == name)
                                    .IncludeAllTables()
                                    .OrderBy(p => p.PrimaryId).ToList();

                    return result;
                }
            }
            return null;
        }
    }
}
