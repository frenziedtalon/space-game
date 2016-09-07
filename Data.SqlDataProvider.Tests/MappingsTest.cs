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


        [Test]
        public void TextureGroup_MapTo_Textures()
        {
            TextureGroup source = new TextureGroup { Id = 1, Name = "Group" };

            TexturePath lowPath = new TexturePath() {Path = "low/path/"};
            TexturePath mediumPath = new TexturePath() { Path = "medium/path/" };
            TexturePath highPath = new TexturePath() { Path = "high/path" };

            TextureType lowTextureType = new TextureType() { Type = "Low" };
            TextureType mediumTextureType = new TextureType() { Type = "Medium" };
            TextureType highTextureType = new TextureType() { Type = "High" };

            Texture lowTexture = new Texture() {Name = "/Low.jpg", TexturePath = lowPath, TextureType = lowTextureType};
            Texture mediumTexture = new Texture() { Name = "Medium.jpg", TexturePath = mediumPath , TextureType = mediumTextureType};
            Texture highTexture = new Texture() {Name = "/High.jpg", TexturePath = highPath, TextureType = highTextureType};
            
            TextureGroupToTexture lowTextureGroupToTexture = new TextureGroupToTexture() {Texture = lowTexture, TextureGroup = source};
            TextureGroupToTexture mediumTextureGroupToTexture = new TextureGroupToTexture() { Texture = mediumTexture, TextureGroup = source };
            TextureGroupToTexture highTextureGroupToTexture = new TextureGroupToTexture() { Texture = highTexture, TextureGroup = source };


            source.TextureGroupToTextures.Add(lowTextureGroupToTexture);
            source.TextureGroupToTextures.Add(mediumTextureGroupToTexture);
            source.TextureGroupToTextures.Add(highTextureGroupToTexture);
            
            Textures expected = new Textures { Low = "low/path/Low.jpg", Medium = "medium/path/Medium.jpg", High = "high/path/High.jpg" };

            Textures result = source.Adapt<Textures>();

            Assert.AreEqual(expected.Low, result.Low);
            Assert.AreEqual(expected.Medium, result.Medium);
            Assert.AreEqual(expected.High, result.High);
        }

        [TestCaseSource(typeof(MappingsTestsData),nameof(MappingsTestsData.SqlDataProviderCelestialObjectType_MapTo_ClassesCelestialObjectType_Data))]
        public void SqlDataProviderCelestialObjectType_MapTo_ClassesCelestialObjectType(SqlDataProvider.CelestialObjectType input, global::Data.Classes.CelestialObjectType expected)
        {
            var result = input.Adapt<global::Data.Classes.CelestialObjectType>();

            Assert.AreEqual(expected, result);
        }

    }
}
