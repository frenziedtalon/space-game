using Core.Classes;
using NUnit.Framework;
using System;

namespace Core.Tests.Classes
{
    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void ctor_WithInvalidDefaultValues_ThrowsArgumentOutOfRangeException()
        {
            const  double defaultLowerBound = 10.4544;
            const double defaultUpperBound = -50.4534;
            Type expectedExceptionType = typeof(ArgumentOutOfRangeException);

            Exception ex = Assert.Catch<Exception>(() => new Range(defaultLowerBound, defaultUpperBound));

            Assert.AreEqual(ex.GetType(), expectedExceptionType);
        }

        [Test]
        public void LowerBound_WithDefaultValue_ReturnsDefaultValue()
        {
            const double defaultLowerBound = -10.3434;
            const double defaultUpperBound = 50.5465;
            Range r = new Range(defaultLowerBound, defaultUpperBound);

            double result = r.LowerBound;

            Assert.AreEqual(defaultLowerBound, result);
        }

        [Test]
        public void UpperBound_WithDefaultValue_ReturnsDefaultValue()
        {
            const double defaultLowerBound = -10.3434;
            const double defaultUpperBound = 50.7676;
            Range r = new Range(defaultLowerBound, defaultUpperBound);

            double result = r.UpperBound;

            Assert.AreEqual(defaultUpperBound, result);
        }

        [Test]
        public void LowerBound_WithValues_ReturnsCorrectValue()
        {
            const double defaultLowerBound = -10.7767;
            const double defaultUpperBound = 50.3434;
            Range r = new Range(defaultLowerBound, defaultUpperBound);

            const double expectedValue = -20.2323;
            r.AddValue(50.767);
            r.AddValue(-12.5656);
            r.AddValue(17.6565);
            r.AddValue(expectedValue);
            r.AddValue(0);
            r.AddValue(13);

            double result = r.LowerBound;

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void UpperBound_WithValues_ReturnsCorrectValue()
        {
            const double defaultLowerBound = -10;
            const double defaultUpperBound = 50.656;
            Range r = new Range(defaultLowerBound, defaultUpperBound);

            const double expectedValue = 200.878;
            r.AddValue(50);
            r.AddValue(-12);
            r.AddValue(17.878);
            r.AddValue(expectedValue);
            r.AddValue(0);
            r.AddValue(13);

            double result = r.UpperBound;

            Assert.AreEqual(expectedValue, result);
        }
    }
}
