using Core.Classes;
using NUnit.Framework;
using System.Collections.Generic;

namespace Data.SqlDataProvider.Tests.Data
{
    public class MappingsTestsData
    {
        public static List<TestCaseData> TextureGroup_MapTo_Textures_Data()
        {
            TextureGroup source = new TextureGroup { Id = 1, Name = "Group" };

            TexturePath lowPath = new TexturePath() { Path = "low/path/" };
            TexturePath mediumPath = new TexturePath() { Path = "medium/path/" };
            TexturePath highPath = new TexturePath() { Path = "high/path" };

            TextureType lowTextureType = new TextureType() { Type = "Low" };
            TextureType mediumTextureType = new TextureType() { Type = "Medium" };
            TextureType highTextureType = new TextureType() { Type = "High" };

            Texture lowTexture = new Texture() { Name = "/Low.jpg", TexturePath = lowPath, TextureType = lowTextureType };
            Texture mediumTexture = new Texture() { Name = "Medium.jpg", TexturePath = mediumPath, TextureType = mediumTextureType };
            Texture highTexture = new Texture() { Name = "/High.jpg", TexturePath = highPath, TextureType = highTextureType };

            TextureGroupToTexture lowTextureGroupToTexture = new TextureGroupToTexture() { Texture = lowTexture, TextureGroup = source };
            TextureGroupToTexture mediumTextureGroupToTexture = new TextureGroupToTexture() { Texture = mediumTexture, TextureGroup = source };
            TextureGroupToTexture highTextureGroupToTexture = new TextureGroupToTexture() { Texture = highTexture, TextureGroup = source };


            source.TextureGroupToTextures.Add(lowTextureGroupToTexture);
            source.TextureGroupToTextures.Add(mediumTextureGroupToTexture);
            source.TextureGroupToTextures.Add(highTextureGroupToTexture);

            Textures expected = new Textures { Low = "low/path/Low.jpg", Medium = "medium/path/Medium.jpg", High = "high/path/High.jpg" };

            return new List<TestCaseData> { new TestCaseData(source, expected) };
        }

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
