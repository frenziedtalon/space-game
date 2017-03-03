using Core.Classes;
using Mapster;
using NUnit.Framework;

namespace Core.Tests
{
    [TestFixture]
    public class MappingsTests
    {
        [OneTimeSetUp]
        public void SetupMappings()
        {
            Mappings mappings = new Mappings();
            mappings.Register(TypeAdapterConfig.GlobalSettings);
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

        [Test]
        public void Integer_MapTo_Mass()
        {
            int source = 1234567890;
            Mass expected = Mass.FromKilograms(source);

            Mass result = source.Adapt<Mass>();

            Assert.AreEqual(expected.Kilograms, result.Kilograms);
            Assert.AreEqual(expected.EarthMasses, result.EarthMasses);
            Assert.AreEqual(expected.SolarMasses, result.SolarMasses);
        }

        [Test]
        public void Double_MapTo_Mass()
        {
            double source = 1234567890.12;
            Mass expected = Mass.FromKilograms(source);

            Mass result = source.Adapt<Mass>();

            Assert.AreEqual(expected.Kilograms, result.Kilograms);
            Assert.AreEqual(expected.EarthMasses, result.EarthMasses);
            Assert.AreEqual(expected.SolarMasses, result.SolarMasses);
        }

        [Test]
        public void Integer_MapTo_Distance()
        {
            int source = 1234567890;
            Distance expected = Distance.FromKilometers(source);

            Distance result = source.Adapt<Distance>();

            Assert.AreEqual(expected.Kilometers, result.Kilometers);
            Assert.AreEqual(expected.AstronomicalUnits, result.AstronomicalUnits);
        }

        [Test]
        public void NullableInteger_WithoutValue_MapTo_Distance()
        {
            int? source = default(int?);
            Angle expected = null;

            Distance result = source.Adapt<Distance>();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NullableInteger_WithValue_MapTo_Distance()
        {
            int? source = 45;
            Distance expected = Distance.FromKilometers(45);

            Distance result = source.Adapt<Distance>();

            Assert.AreEqual(expected.Kilometers, result.Kilometers);
        }

        [Test]
        public void Double_MapTo_Distance()
        {
            double source = 1234567890.12;
            Distance expected = Distance.FromKilometers(source);

            Distance result = source.Adapt<Distance>();

            Assert.AreEqual(expected.Kilometers, result.Kilometers);
            Assert.AreEqual(expected.AstronomicalUnits, result.AstronomicalUnits);
        }

        [Test]
        public void NullableDouble_WithoutValue_MapTo_Distance()
        {
            double? source = default(double?);
            Angle expected = null;

            Distance result = source.Adapt<Distance>();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NullableDouble_WithValue_MapTo_Distance()
        {
            double? source = 1.11;
            Distance expected = Distance.FromKilometers(1.11);

            Distance result = source.Adapt<Distance>();

            Assert.AreEqual(expected.Kilometers, result.Kilometers);
        }

        [Test]
        public void NullableInteger_WithoutValue_MapTo_Angle()
        {
            int? source = default(int?);
            Angle expected = null;

            Angle result = source.Adapt<Angle>();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NullableInteger_WithValue_MapTo_Angle()
        {
            int? source = 45;
            Angle expected = Angle.FromDegrees(45);

            Angle result = source.Adapt<Angle>();

            Assert.AreEqual(expected.Degrees, result.Degrees);
        }

        [Test]
        public void Integer_MapTo_Angle()
        {
            int source = 90;
            Angle expected = Angle.FromDegrees(source);

            Angle result = source.Adapt<Angle>();

            Assert.AreEqual(expected.Degrees, result.Degrees);
        }

        [Test]
        public void Double_MapTo_Angle()
        {
            double source = 90.11;
            Angle expected = Angle.FromDegrees(source);

            Angle result = source.Adapt<Angle>();

            Assert.AreEqual(expected.Degrees, result.Degrees);
        }

        [Test]
        public void NullableDouble_WithoutValue_MapTo_Angle()
        {
            double? source = default(double?);
            Angle expected = null;

            Angle result = source.Adapt<Angle>();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void NullableDouble_WithValue_MapTo_Angle()
        {
            double? source = 45.11;
            Angle expected = Angle.FromDegrees(45.11);

            Angle result = source.Adapt<Angle>();

            Assert.AreEqual(expected.Degrees, result.Degrees);
        }
    }
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
