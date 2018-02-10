using Data.ConfigDataProvider.Tests.Data;
using Mapster;
using NUnit.Framework;
using MassFrom = global::Data.ConfigDataProvider.Classes.Mass;
using MassTo = global::Core.Classes.Mass;

namespace Data.ConfigDataProvider.Tests
{
    [TestFixture]
    public class MappingsTests
    {
        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.ConfigMass_MapTo_CoreMass_Data))]
        public void ConfigMass_MapTo_CoreMass(MassFrom source, MassTo expected)
        {
            TypeAdapterConfig config = new TypeAdapterConfig();

            Mappings mappings = new Mappings();
            mappings.Register(config);
            config.Compile();

            MassTo result = source.Adapt<MassTo>();
            
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.Kilograms, result.Kilograms);
                Assert.AreEqual(expected.EarthMasses, result.EarthMasses);
                Assert.AreEqual(expected.SolarMasses, result.SolarMasses);
            });



            //MassFrom result = expected.Adapt<MassFrom>();

            //Assert.AreEqual(1, result.Kilograms);
        }
    }
}
