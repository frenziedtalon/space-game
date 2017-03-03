using Core.Classes;
using Core.Tests.Data;
using NUnit.Framework;

namespace Core.Tests.Classes
{
    [TestFixture]
    public class DistanceTests
    {
        [TestCase(0.0)]
        [TestCase(56.0)]
        [TestCase(1E-08)]
        [TestCase(25444588655.0)]
        public void FromKilometers_WhenCalled_StoresCorrectValue(double kilometers)
        {
            Distance d = Distance.FromKilometers(kilometers);

            double result = d.Kilometers;

            Assert.AreEqual(kilometers, result);
        }

        [TestCase(0.0)]
        [TestCase(56.0)]
        [TestCase(1E-08)]
        [TestCase(300.0)]

        public void FromAstronomicalUnits_WhenCalled_StoresCorrectValue(double aus)
        {
            Distance d = Distance.FromAstronomicalUnits(aus);

            double result = d.AstronomicalUnits;

            Assert.AreEqual(aus, result);
        }

        [TestCaseSource(typeof(DistanceTestData), nameof(DistanceTestData.Equals_WhenComparing_ReturnsExpected_Data))]
        public void Equals_WhenComparing_ReturnsExpected(Distance @this, Distance that, bool expected)
        {
            bool result = @this.Equals(that);

            Assert.AreEqual(expected, result);
        }
    }
}
