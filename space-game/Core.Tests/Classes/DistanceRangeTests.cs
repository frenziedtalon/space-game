using Core.Classes;
using NUnit.Framework;
using System;

namespace Core.Tests.Classes
{
    [TestFixture]
    public class DistanceRangeTests
    {
        [Test]
        public void ctor_WithInvalidDefaultValues_ThrowsArgumentOutOfRangeException()
        {
            Distance defaultLowerBound = Distance.FromAstronomicalUnits(10.4544);
            Distance defaultUpperBound = Distance.FromAstronomicalUnits(-50.4534);

            Type expectedExceptionType = typeof(ArgumentOutOfRangeException);

            Exception ex = Assert.Catch<Exception>(() => new DistanceRange(defaultLowerBound, defaultUpperBound));

            Assert.AreEqual(ex.GetType(), expectedExceptionType);
        }

        [Test]
        public void LowerBound_WithDefaultValue_ReturnsDefaultValue()
        {
            Distance defaultLowerBound = Distance.FromAstronomicalUnits(-10.3434);
            Distance defaultUpperBound = Distance.FromAstronomicalUnits(50.5465);
            DistanceRange r = new DistanceRange(defaultLowerBound, defaultUpperBound);

            Distance result = r.LowerBound;

            Assert.AreEqual(defaultLowerBound.Kilometers, result.Kilometers);
        }

        [Test]
        public void UpperBound_WithDefaultValue_ReturnsDefaultValue()
        {
            Distance defaultLowerBound = Distance.FromAstronomicalUnits(-10.3434);
            Distance defaultUpperBound = Distance.FromAstronomicalUnits(50.7676);
            DistanceRange r = new DistanceRange(defaultLowerBound, defaultUpperBound);

            Distance result = r.UpperBound;

            Assert.AreEqual(defaultUpperBound.Kilometers, result.Kilometers);
        }

        [Test]
        public void LowerBound_WithValues_ReturnsCorrectValue()
        {
            Distance defaultLowerBound = Distance.FromAstronomicalUnits(-10.7767);
            Distance defaultUpperBound = Distance.FromAstronomicalUnits(50.3434);
            DistanceRange r = new DistanceRange(defaultLowerBound, defaultUpperBound);

            Distance expectedValue = Distance.FromAstronomicalUnits(-20.2323);
            r.AddValue(Distance.FromAstronomicalUnits(50.767));
            r.AddValue(Distance.FromAstronomicalUnits(-12.5656));
            r.AddValue(Distance.FromAstronomicalUnits(17.6565));
            r.AddValue(expectedValue);
            r.AddValue(Distance.FromAstronomicalUnits(0));
            r.AddValue(Distance.FromAstronomicalUnits(13));

            Distance result = r.LowerBound;

            Assert.AreEqual(expectedValue.Kilometers, result.Kilometers);
        }

        [Test]
        public void UpperBound_WithValues_ReturnsCorrectValue()
        {
            Distance defaultLowerBound = Distance.FromAstronomicalUnits(-10);
            Distance defaultUpperBound = Distance.FromAstronomicalUnits(50.656);
            DistanceRange r = new DistanceRange(defaultLowerBound, defaultUpperBound);

            Distance expectedValue = Distance.FromAstronomicalUnits(200.878);
            r.AddValue(Distance.FromAstronomicalUnits(50));
            r.AddValue(Distance.FromAstronomicalUnits(-12));
            r.AddValue(Distance.FromAstronomicalUnits(17.878));
            r.AddValue(expectedValue);
            r.AddValue(Distance.FromAstronomicalUnits(0));
            r.AddValue(Distance.FromAstronomicalUnits(13));

            Distance result = r.UpperBound;

            Assert.AreEqual(expectedValue.Kilometers, result.Kilometers);
        }
    }
}
