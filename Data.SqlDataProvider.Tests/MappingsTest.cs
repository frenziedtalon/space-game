using Core.Classes;
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
            var mappings = new Mappings();
            mappings.Register(TypeAdapterConfig.GlobalSettings);
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

    }
}
