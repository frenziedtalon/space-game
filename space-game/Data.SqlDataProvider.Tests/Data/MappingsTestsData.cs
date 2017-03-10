using Core.Classes;
using Data.Classes;
using Data.SqlDataProvider.Model;
using NUnit.Framework;
using System.Collections.Generic;
using CelestialObjectType = Data.SqlDataProvider.Model.CelestialObjectType;
using Texture = Data.SqlDataProvider.Model.Texture;

namespace Data.SqlDataProvider.Tests.Data
{
    public class MappingsTestsData
    {
        public static List<TestCaseData> TextureGroup_MapTo_ListOfTexture_Data()
        {
            return new List<TestCaseData> { new TestCaseData(SampleTextureGroup, SampleListOfTexture) };
        }

        private static TextureGroup SampleTextureGroup
        {
            get
            {
                TextureGroup source = new TextureGroup { Id = 1, Name = "Group" };

                TexturePath lowPath = new TexturePath() { Path = "low/path/" };
                TexturePath mediumPath = new TexturePath() { Path = "medium/path/" };
                TexturePath highPath = new TexturePath() { Path = "high/path" };

                TextureQuality lowTextureType = new TextureQuality() { Quality = "Low" };
                TextureQuality mediumTextureType = new TextureQuality() { Quality = "Medium" };
                TextureQuality highTextureType = new TextureQuality() { Quality = "High" };

                TextureType diffuseTextureType = new TextureType() {Type = "Diffuse"};
                TextureType emissiveTextureType = new TextureType() { Type = "Emissive" };
                TextureType bumpTextureType = new TextureType() { Type = "Bump" };

                Texture lowTexture = new Texture() { Name = "/Low.jpg", TexturePath = lowPath, TextureQuality = lowTextureType, TextureType = diffuseTextureType };
                Texture mediumTexture = new Texture() { Name = "Medium.jpg", TexturePath = mediumPath, TextureQuality = mediumTextureType, TextureType = emissiveTextureType };
                Texture highTexture = new Texture() { Name = "/High.jpg", TexturePath = highPath, TextureQuality = highTextureType, TextureType = bumpTextureType };

                TextureGroupToTexture lowTextureGroupToTexture = new TextureGroupToTexture() { Texture = lowTexture, TextureGroup = source };
                TextureGroupToTexture mediumTextureGroupToTexture = new TextureGroupToTexture() { Texture = mediumTexture, TextureGroup = source };
                TextureGroupToTexture highTextureGroupToTexture = new TextureGroupToTexture() { Texture = highTexture, TextureGroup = source };

                source.TextureGroupToTextures.Add(lowTextureGroupToTexture);
                source.TextureGroupToTextures.Add(mediumTextureGroupToTexture);
                source.TextureGroupToTextures.Add(highTextureGroupToTexture);

                return source;
            }
        }

        private static List<Core.Classes.Texture> SampleListOfTexture => new List<Core.Classes.Texture>
        {
            new Core.Classes.Texture() {Path = "low/path/Low.jpg", QualityEnum = Core.Enums.TextureQuality.Low, TypeEnum = Core.Enums.TextureType.Diffuse},
            new Core.Classes.Texture() {Path = "medium/path/Medium.jpg", QualityEnum = Core.Enums.TextureQuality.Medium, TypeEnum = Core.Enums.TextureType.Emissive},
            new Core.Classes.Texture() {Path = "high/path/High.jpg", QualityEnum = Core.Enums.TextureQuality.High, TypeEnum = Core.Enums.TextureType.Bump}
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

            var expectedWithoutOrbit = new PhysicalData(
                name: "Sol",
                mass: Mass.FromKilograms(1.98855E+30),
                radius: Distance.FromKilometers(695700),
                textures: SampleListOfTexture,
                type: global::Data.Classes.CelestialObjectType.Star,
                rings: null
            );

            result.Add(new TestCaseData(CelestialObjectWithoutOrbit, expectedWithoutOrbit));

            var expectedWithOrbit = new PhysicalData(
                name: "Earth",
                mass: Mass.FromKilograms(5.9736E+24),
                radius: Distance.FromKilometers(6371),
                textures: SampleListOfTexture,
                type: global::Data.Classes.CelestialObjectType.Planet,
                rings: null
            );

            result.Add(new TestCaseData(CelestialObjectWithOrbit, expectedWithOrbit));

            var expectedWithRingSystem = new PhysicalData(
                name: "Sol",
                mass: Mass.FromKilograms(1.98855E+30),
                radius: Distance.FromKilometers(695700),
                textures: SampleListOfTexture,
                type: global::Data.Classes.CelestialObjectType.Star,
                rings: SampleRingData
            );

            result.Add(new TestCaseData(CelestialObjectWithRingSystem, expectedWithRingSystem));

            return result;
        }

        private static CelestialObject CelestialObjectWithoutOrbit => new CelestialObject()
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
            MeanAnomalyZero = null,
            RingSystem = null
        };

        private static CelestialObject CelestialObjectWithRingSystem => new CelestialObject()
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
            MeanAnomalyZero = null,
            RingSystem = SampleRingSystem
        };

        private static CelestialObject CelestialObjectWithOrbit => new CelestialObject()
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
            Inclination = 12.1234,
            ArgumentOfPeriapsis = 282.9404,
            LongitudeOfAscendingNode = 123.45,
            MeanAnomalyZero = 356.047,
            RingSystem = null
        };

        public static List<TestCaseData> SqlDataProviderCelestialObject_MapTo_DataOrbitData_Data()
        {
            var nullOrbit = new OrbitData(null, 0.00, null, null, null, null);

            var validOrbit = new OrbitData(semiMajorAxis: Distance.FromKilometers(149597870.691),
                                            eccentricity: 0.016709,
                                            inclination: Angle.FromDegrees(12.1234),
                                            argumentOfPeriapsis: Angle.FromDegrees(282.9404),
                                            longitudeOfAscendingNode: Angle.FromDegrees(123.45),
                                            meanAnomalyZero: Angle.FromDegrees(356.047));

            return new List<TestCaseData>
            {
                new TestCaseData(CelestialObjectWithoutOrbit, nullOrbit),
                new TestCaseData(CelestialObjectWithOrbit, validOrbit)
            };
        }


        private const int RingSystemInnerRadiusKilometers = 10000;
        private const int RingSystemOuterRadiusKilometers = 100000;

        private static RingSystem SampleRingSystem => new RingSystem()
        {
            InnerRadius = RingSystemInnerRadiusKilometers,
            OuterRadius = RingSystemOuterRadiusKilometers,
            TextureGroup = SampleTextureGroup
        };

        private static RingData SampleRingData => new RingData()
        {
            InnerRadius = Distance.FromKilometers(RingSystemInnerRadiusKilometers),
            OuterRadius = Distance.FromKilometers(RingSystemOuterRadiusKilometers),
            Textures = SampleListOfTexture
        };

        public static List<TestCaseData> RingSystem_MapTo_RingData_Data()
        {
            return new List<TestCaseData>
            {
                new TestCaseData(SampleRingSystem, SampleRingData)
            };
        }
    }
}
