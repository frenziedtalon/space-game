using NUnit.Framework;
using System.Collections.Generic;
using MassFrom = Data.ConfigDataProvider.Classes.Mass;
using MassTo = Core.Classes.Mass;

namespace Data.ConfigDataProvider.Tests.Data
{
    public class MappingsTestsData
    {
        public static IEnumerable<TestCaseData> ConfigMass_MapTo_CoreMass_Data()
        {
            yield return new TestCaseData(new MassFrom() { Kilograms = 1 }, MassTo.FromKilograms(1));
            yield return new TestCaseData(new MassFrom() { EarthMasses = 1 }, MassTo.FromEarthMasses(1));
            yield return new TestCaseData(new MassFrom() { SolarMasses = 1 }, MassTo.FromSolarMasses(1));
        }
    }
}
