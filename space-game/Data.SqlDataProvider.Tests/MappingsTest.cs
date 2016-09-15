using Core.Classes;
using Data.Classes;
using Data.Data;
using Data.SqlDataProvider.Tests.Data;
using Mapster;
using NUnit.Framework;

namespace Data.SqlDataProvider.Tests
{
    [TestFixture]
    class MappingsTest
    {
        [OneTimeSetUp]
        public void SetupMappings()
        {
            new global::Data.SqlDataProvider.Mappings().Register(TypeAdapterConfig.GlobalSettings);
            new Core.Mappings().Register(TypeAdapterConfig.GlobalSettings);

            TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = true;
            TypeAdapterConfig.GlobalSettings.Compile();
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

            Assert.AreEqual(expected, result);
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

            Assert.AreEqual(expected.Texture, result.Texture);
            Assert.AreEqual(expected.Mass, result.Mass);
            Assert.AreEqual(expected.Radius, result.Radius);
            Assert.AreEqual(expected.Name, result.Name);
            Assert.AreEqual(expected.Type, result.Type);
        }

        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.SqlDataProviderCelestialObject_MapTo_DataOrbitData_Data))]
        public void SqlDataProviderCelestialObject_MapTo_DataOrbitData(CelestialObject source, OrbitData expected)
        {
            var result = source.Adapt<OrbitData>();

            Assert.AreEqual(expected.ArgumentOfPeriapsis, result.ArgumentOfPeriapsis);
            Assert.AreEqual(expected.Eccentricity, result.Eccentricity);
            Assert.AreEqual(expected.Inclination, result.Inclination);
            Assert.AreEqual(expected.LongitudeOfAscendingNode, result.LongitudeOfAscendingNode);
            Assert.AreEqual(expected.MeanAnomalyZero, result.MeanAnomalyZero);
            Assert.AreEqual(expected.SemiMajorAxis, result.SemiMajorAxis);
        }
    }
}
