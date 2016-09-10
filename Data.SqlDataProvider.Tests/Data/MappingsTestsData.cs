using Core.Classes;
using Data.Data;
using NUnit.Framework;
using System.Collections.Generic;

namespace Data.SqlDataProvider.Tests.Data
{
    public class MappingsTestsData
    {
        public static List<TestCaseData> TextureGroup_MapTo_Textures_Data()
        {
            return new List<TestCaseData> { new TestCaseData(SampleTextureGroup, SampleTextures) };
        }

        private static TextureGroup SampleTextureGroup
        {
            get
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

                return source;
            }
        }

        private static Textures SampleTextures => new Textures
        {
            Low = "low/path/Low.jpg",
            Medium = "medium/path/Medium.jpg",
            High = "high/path/High.jpg"
        };

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

        public static List<TestCaseData> SqlDataProviderCelestialObject_MapTo_DataPhysicalData_Data()

        {
            var result = new List<TestCaseData>();

            var sourceWithoutOrbit = new CelestialObject()
            {
                Id = 1,
                Name = "Sol",
                SolarSystemId = 1,
                Mass = 1.98855E+30,
                Radius = 695700,
                TextureGroup = SampleTextureGroup,
                CelestialObjectType = new CelestialObjectType()
                {
                    Name = "Star"
                },
                PrimaryId = null,
                SemiMajorAxis = null,
                Eccentricity = null,
                Inclination = null,
                ArgumentOfPeriapsis = null,
                LongitudeOfAscendingNode = null,
                MeanAnomalyZero = null
            };

            var expectedWithoutOrbit = new PhysicalData()
            {
                Name = "Sol",
                Mass = Mass.FromKilograms(1.98855E+30),
                Radius = Distance.FromKilometers(695700),
                Texture = SampleTextures,
                Type = global::Data.Classes.CelestialObjectType.Star
            };

            result.Add(new TestCaseData(sourceWithoutOrbit, expectedWithoutOrbit));

            var sourceWithOrbit = new CelestialObject()
            {
                Id = 4,
                Name = "Earth",
                SolarSystemId = 1,
                Mass = 5.9736E+24,
                Radius = 6371,
                TextureGroup = SampleTextureGroup,
                CelestialObjectType = new CelestialObjectType()
                {
                    Name = "Planet"
                },
                PrimaryId = 1,
                SemiMajorAxis = 149597870.691,
                Eccentricity = 0.016709,
                Inclination = 0,
                ArgumentOfPeriapsis = 282.9404,
                LongitudeOfAscendingNode = 0,
                MeanAnomalyZero = 356.047
            };

            var expectedWithOrbit = new PhysicalData()
            {
                Name = "Earth",
                Mass = Mass.FromKilograms(5.9736E+24),
                Radius = Distance.FromKilometers(6371),
                Texture = SampleTextures,
                Type = global::Data.Classes.CelestialObjectType.Planet
            };

            result.Add(new TestCaseData(sourceWithOrbit, expectedWithOrbit));

            return result;
        }
    }
}
