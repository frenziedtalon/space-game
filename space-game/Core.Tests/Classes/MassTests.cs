using Core.Classes;
using Core.Tests.Data;
using NUnit.Framework;

namespace Core.Tests.Classes
{
    [TestFixture]
    public class MassTests
    {
        [TestCase(0.0)]
        [TestCase(56.0)]
        [TestCase(1E-08)]
        [TestCase(25444588655.0)]
        public void FromKilograms_WhenCalled_StoresCorrectValue(double kilograms)
        {
            Mass m = Mass.FromKilograms(kilograms);

            double result = m.Kilograms;

            Assert.AreEqual(kilograms, result);
        }

        [TestCase(0.0)]
        [TestCase(56.0)]
        [TestCase(1E-08)]
        [TestCase(25444588655.0)]
        public void FromSolarMasses_WhenCalled_StoresCorrectValue(double solarMasses)
        {
            double acceptableDelta = Mass.FromKilograms(1).Kilograms;
            Mass m = Mass.FromSolarMasses(solarMasses);

            double result = m.SolarMasses;

            Assert.AreEqual(solarMasses, result, acceptableDelta);
        }

        [TestCase(0.0)]
        [TestCase(56.0)]
        [TestCase(1E-08)]
        [TestCase(25444588655.0)]
        public void FromEarthMasses_WhenCalled_StoresCorrectValue(double earthMasses)
        {
            double acceptableDelta = Mass.FromKilograms(1).Kilograms;
            Mass m = Mass.FromEarthMasses(earthMasses);

            double result = m.EarthMasses;

            Assert.AreEqual(earthMasses, result, acceptableDelta);
        }

        [TestCaseSource(typeof(MassTestsData), nameof(MassTestsData.Equals_WhenComparing_ReturnsExpected_Data))]
        public void Equals_WhenComparing_ReturnsExpected(Mass @this, Mass that, bool expected)
        {
            bool result = @this.Equals(that);

            Assert.AreEqual(expected, result);
        }
    }
}
