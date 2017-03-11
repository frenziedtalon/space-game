using Core.Classes;
using Core.Enums;
using Entities;
using NSubstitute;
using NUnit.Framework;
using OrbitalMechanics.CelestialObjects;
using System.Collections.Generic;

namespace OrbitalMechanics.Tests.CelestialObjects
{
    [TestFixture]
    public class StarTests
    {
        [TestCase(33001, StarClassification.O)]
        [TestCase(33000, StarClassification.O)]
        [TestCase(10501, StarClassification.B)]
        [TestCase(10500, StarClassification.B)]
        [TestCase(7501, StarClassification.A)]
        [TestCase(7500, StarClassification.A)]
        [TestCase(6001, StarClassification.F)]
        [TestCase(6000, StarClassification.F)]
        [TestCase(5501, StarClassification.G)]
        [TestCase(5500, StarClassification.G)]
        [TestCase(4001, StarClassification.K)]
        [TestCase(4000, StarClassification.K)]
        [TestCase(1, StarClassification.M)]
        [TestCase(0, StarClassification.M)]
        public void Classification_WhenCalled_ReturnsCorrectValue(int temperature, StarClassification expected)
        {
            IEntityManager entityManager = Substitute.For<IEntityManager>();

            List<Texture> textures = new List<Texture> { new Texture {
                QualityEnum = TextureQuality.Low,
                TypeEnum = TextureType.Diffuse,
                Path = "low texture path"
            } };

            Star star = new Star("test", Mass.FromSolarMasses(1), temperature, textures, Distance.FromKilometers(1), entityManager, null);

            Assert.AreEqual(expected, star.Classification);
        }
    }
}
