using Core.Classes;
using Core.Tests.Data;
using NUnit.Framework;
using System;

namespace Core.Tests.Classes
{
    [TestFixture]
    public class AngleTests
    {

        [TestCase(0)]
        [TestCase(90.102)]
        [TestCase(180)]
        [TestCase(270.8782737)]

        public void FromDegrees_WhenWithinLimits_ReturnsSameValue(double input)
        {
            Angle a = Angle.FromDegrees(input);

            double result = a.Degrees;

            Assert.AreEqual(input, result);
        }

        [TestCase(-1000, 80)]
        [TestCase(-30, 330)]
        [TestCase(390, 30)]
        [TestCase(4678, 358)]
        [TestCase(360, 0)]

        public void FromDegrees_WhenOutsideLimits_ReturnsCorrectedValue(double input, double expected)
        {
            Angle a = Angle.FromDegrees(input);

            double result = a.Degrees;

            Assert.AreEqual(expected, result);
        }

        [TestCase(0)]
        [TestCase(Math.PI / 400)]
        [TestCase(Math.PI / 4)]
        [TestCase(Math.PI / 2)]
        [TestCase(Math.PI)]
        [TestCase(3 * (Math.PI / 2))]

        public void FromRadians_WhenWithinLimits_ReturnsSameValue(double input)
        {
            Angle a = Angle.FromRadians(input);

            double result = a.Radians;

            double roundedExpected = Math.Round(input, a.DecimalPlaces, MidpointRounding.AwayFromZero);

            Assert.AreEqual(roundedExpected, result);
        }

        [TestCase(-Math.PI, Math.PI)]
        [TestCase(3 * Math.PI, Math.PI)]
        [TestCase(15 * (Math.PI / 4), 7 * (Math.PI / 4))]

        public void FromRadians_WhenOutsideLimits_ReturnsCorrectedValue(double input, double expected)
        {
            Angle a = Angle.FromRadians(input);

            double result = a.Radians;

            double roundedExpected = Math.Round(expected, a.DecimalPlaces, MidpointRounding.AwayFromZero);

            Assert.AreEqual(roundedExpected, result);
        }

        [TestCaseSource(typeof(AngleTestsData), nameof(AngleTestsData.Equals_WhenComparing_ReturnsExpected_Data))]
        public void Equals_WhenComparing_ReturnsExpected(Angle @this, Angle that, bool expected)
        {
            bool result = @this.Equals(that);

            Assert.AreEqual(expected, result);
        }
    }
}
