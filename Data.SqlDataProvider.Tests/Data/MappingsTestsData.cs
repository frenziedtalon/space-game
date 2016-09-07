using NUnit.Framework;
using System.Collections.Generic;

namespace Data.SqlDataProvider.Tests.Data
{
    public class MappingsTestsData
    {
        public static List<TestCaseData> SqlDataProviderCelestialObjectType_MapTo_ClassesCelestialObjectType_Data()
        {
            var result = new List<TestCaseData>
            {
                new TestCaseData(new CelestialObjectType() {Name = "Moon"},
                    global::Data.Classes.CelestialObjectType.Moon),
                new TestCaseData(new CelestialObjectType() {Name = "Planet"},
                    global::Data.Classes.CelestialObjectType.Planet),
                new TestCaseData(new CelestialObjectType() {Name = "Star"},
                    global::Data.Classes.CelestialObjectType.Star)
            };
            return result;
        }
    }
}
