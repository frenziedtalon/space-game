using Core.Classes;
using NUnit.Framework;
using OrbitalMechanics.Helpers;
using System;
using System.Collections.Generic;

namespace OrbitalMechanics.Tests.Helpers
{
    [TestFixture]
    public class OrbitHelperTests
    {

        [TestCaseSource(nameof(TotalMassTestCases))]

        public void CalculateTotalMass_WhenCalled_ReturnsExpectedValue(Mass massOfPrimary, Mass massOfSatellite)
        {
            double acceptableDelta = Mass.FromEarthMasses(1E-14).EarthMasses;

            Mass expected = Mass.FromEarthMasses(massOfPrimary.EarthMasses + massOfSatellite.EarthMasses);
            OrbitHelper orbitHelper = new OrbitHelper();

            Mass result = orbitHelper.CalculateTotalMass(massOfPrimary, massOfSatellite);

            Assert.AreEqual(expected.EarthMasses, result.EarthMasses, acceptableDelta);
        }

        private static List<TestCaseData> TotalMassTestCases
        {
            get
            {
                List<TestCaseData> result = new List<TestCaseData>();

                result.Add(new TestCaseData(MassOfSun, MassOfEarth));
                result.Add(new TestCaseData(MassOfSun, MassOfJupiter));
                result.Add(new TestCaseData(MassOfEarth, MassOfMoon));

                return result;
            }
        }

        [Test, TestCaseSource(nameof(CalculatePeriodTestCases))]

        public void CalculatePeriod_WhenCalled_ReturnsExpectedValue(Mass massOfPrimary, Mass massOfSatellite, Distance semiMajorAxis, TimeSpan expected)
        {
            double acceptableDelta = expected.TotalDays * 0.005;
            OrbitHelper orbitHelper = new OrbitHelper();
            Mass totalMass = orbitHelper.CalculateTotalMass(massOfPrimary, massOfSatellite);

            TimeSpan result = orbitHelper.CalculatePeriod(totalMass, semiMajorAxis);

            Assert.AreEqual(expected.TotalDays, result.TotalDays, acceptableDelta);
        }

        private static List<TestCaseData> CalculatePeriodTestCases
        {
            get
            {
                List<TestCaseData> result = new List<TestCaseData>();

                result.Add(new TestCaseData(MassOfSun, MassOfEarth, SemiMajorAxisOfEarth, PeriodOfEarth));
                result.Add(new TestCaseData(MassOfSun, MassOfJupiter, SemiMajorAxisOfJupiter, PeriodOfJupiter));
                result.Add(new TestCaseData(MassOfEarth, MassOfMoon, SemiMajorAxisOfMoon, PeriodOfMoon));

                return result;
            }
        }

        private static Mass MassOfSun => Mass.FromSolarMasses(1);

        private static Mass MassOfJupiter => Mass.FromEarthMasses(317.83);

        private static Mass MassOfEarth => Mass.FromEarthMasses(1);

        private static Mass MassOfMoon => Mass.FromEarthMasses(0.0203);

        private static Distance SemiMajorAxisOfEarth => Distance.FromAstronomicalUnits(1);

        private static Distance SemiMajorAxisOfJupiter => Distance.FromAstronomicalUnits(5.20336301);

        private static Distance SemiMajorAxisOfMoon => Distance.FromAstronomicalUnits(0.00257188152);

        private static TimeSpan PeriodOfEarth => TimeSpan.FromDays(1 * Constants.DaysInJulianYear);

        private static TimeSpan PeriodOfJupiter => TimeSpan.FromDays(4332.589);

        private static TimeSpan PeriodOfMoon => TimeSpan.FromDays(27.321662);
    }
}
