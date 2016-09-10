using Core.Classes;
using Data.Data;
using Data.SqlDataProvider.Tests.Data;
using Mapster;
using NUnit.Framework;

namespace Data.SqlDataProvider.Tests
{
    [TestFixture]
    class MappingsTest
    {
        private TypeAdapterConfig _config;

        [OneTimeSetUp]
        public void SetupMappings()
        {
            new global::Data.SqlDataProvider.Mappings().Register(TypeAdapterConfig.GlobalSettings);
            new Core.Mappings().Register(TypeAdapterConfig.GlobalSettings);

            TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = true;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = false;
            TypeAdapterConfig.GlobalSettings.Rules.Clear();
            TypeAdapterConfig.GlobalSettings.RuleMap.Clear();
        }


        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.TextureGroup_MapTo_Textures_Data))]
        public void TextureGroup_MapTo_Textures(TextureGroup source, Textures expected)
        {
            Textures result = source.Adapt<Textures>();

            Assert.AreEqual(expected.Low, result.Low);
            Assert.AreEqual(expected.Medium, result.Medium);
            Assert.AreEqual(expected.High, result.High);
        }

        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.SqlDataProviderCelestialObjectType_MapTo_ClassesCelestialObjectType_Data))]
        public void SqlDataProviderCelestialObjectType_MapTo_ClassesCelestialObjectType(SqlDataProvider.CelestialObjectType source, global::Data.Classes.CelestialObjectType expected)
        {
            var result = source.Adapt<global::Data.Classes.CelestialObjectType>();

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.SqlDataProviderCelestialObject_MapTo_DataPhysicalData_Data))]
        public void SqlDataProviderCelestialObject_MapTo_DataPhysicalData(CelestialObject source, PhysicalData expected)
        {
            var result = source.Adapt<PhysicalData>();

            Assert.AreEqual(expected.Texture.Low, result.Texture.Low);
            Assert.AreEqual(expected.Texture.Medium, result.Texture.Medium);
            Assert.AreEqual(expected.Texture.High, result.Texture.High);

            Assert.AreEqual(expected.Mass.Kilograms, result.Mass.Kilograms);
            Assert.AreEqual(expected.Mass.EarthMasses, result.Mass.EarthMasses);
            Assert.AreEqual(expected.Mass.SolarMasses, result.Mass.SolarMasses);

            Assert.AreEqual(expected.Radius.Kilometers, result.Radius.Kilometers);
            Assert.AreEqual(expected.Radius.AstronomicalUnits, result.Radius.AstronomicalUnits);

            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Type, result.Type);
        }
    }
}
