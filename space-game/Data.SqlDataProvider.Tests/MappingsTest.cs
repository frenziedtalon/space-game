using Data.Classes;
using Data.SqlDataProvider.Tests.Data;
using Mapster;
using NUnit.Framework;
using System.Collections.Generic;

namespace Data.SqlDataProvider.Tests
{
    [TestFixture]
    class MappingsTest
    {
        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.TextureGroup_MapTo_ListOfTexture_Data))]
        public void TextureGroup_MapTo_ListOfTexture(TextureGroup source, List<Core.Classes.Texture> expected)
        {
            TypeAdapterConfig config = new TypeAdapterConfig { RequireExplicitMapping = true };
            new global::Data.SqlDataProvider.Mappings().Register(config);
            config.Compile();

            List<Core.Classes.Texture> result = source.Adapt<List<Core.Classes.Texture>>(config);

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.SqlDataProviderCelestialObjectType_MapTo_ClassesCelestialObjectType_Data))]
        public void SqlDataProviderCelestialObjectType_MapTo_ClassesCelestialObjectType(SqlDataProvider.CelestialObjectType source, global::Data.Classes.CelestialObjectType expected)
        {
            TypeAdapterConfig config = new TypeAdapterConfig { RequireExplicitMapping = true };
            new global::Data.SqlDataProvider.Mappings().Register(config);
            config.Compile();

            var result = source.Adapt<global::Data.Classes.CelestialObjectType>(config);

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.SqlDataProviderCelestialObject_MapTo_DataPhysicalData_Data))]
        public void SqlDataProviderCelestialObject_MapTo_DataPhysicalData(CelestialObject source, PhysicalData expected)
        {
            TypeAdapterConfig config = new TypeAdapterConfig { RequireExplicitMapping = true };
            new global::Data.SqlDataProvider.Mappings().Register(config);
            new Core.Mappings().Register(config);
            config.Compile();

            var result = source.Adapt<PhysicalData>(config);

            Assert.AreEqual(expected, result);
        }

        [TestCaseSource(typeof(MappingsTestsData), nameof(MappingsTestsData.SqlDataProviderCelestialObject_MapTo_DataOrbitData_Data))]
        public void SqlDataProviderCelestialObject_MapTo_DataOrbitData(CelestialObject source, OrbitData expected)
        {
            TypeAdapterConfig config = new TypeAdapterConfig { RequireExplicitMapping = true };
            new global::Data.SqlDataProvider.Mappings().Register(config);
            new Core.Mappings().Register(config);
            config.Compile();

            var result = source.Adapt<OrbitData>(config);

            Assert.AreEqual(expected.ArgumentOfPeriapsis, result.ArgumentOfPeriapsis);
            Assert.AreEqual(expected.Eccentricity, result.Eccentricity);
            Assert.AreEqual(expected.Inclination, result.Inclination);
            Assert.AreEqual(expected.LongitudeOfAscendingNode, result.LongitudeOfAscendingNode);
            Assert.AreEqual(expected.MeanAnomalyZero, result.MeanAnomalyZero);
            Assert.AreEqual(expected.SemiMajorAxis, result.SemiMajorAxis);
        }
    }
}
