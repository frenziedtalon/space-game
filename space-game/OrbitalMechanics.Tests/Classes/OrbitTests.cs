using Core.Classes;
using Data.Classes;
using NSubstitute;
using NUnit.Framework;
using OrbitalMechanics.Classes;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using TurnTracker;

namespace OrbitalMechanics.Tests.Classes
{
    [TestFixture]
    public class OrbitTests
    {
        [TestCaseSource(nameof(PositionTestCases))]
        public void Postion_WhenCalled_CalculatesCorrectValue(OrbitData orbitData, int days, Point3D expected)
        {
            ITurnTracker turnTracker = Substitute.For<ITurnTracker>();
            turnTracker.TimeSinceStart.Returns(TimeSpan.FromDays(days));

            double acceptableDelta = Distance.FromKilometers(1800).Kilometers;

            Orbit orbit = new Orbit(turnTracker, orbitData)
            {
                MassOfPrimary = MassOfSun,
                MassOfSatellite = MassOfMercury
            };

            Point3D result = orbit.Position;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.X, result.X, acceptableDelta);
                Assert.AreEqual(expected.Y, result.Y, acceptableDelta);
                Assert.AreEqual(expected.Z, result.Z, acceptableDelta);
            });
        }

        private static List<TestCaseData> PositionTestCases
        {
            get
            {
                List<TestCaseData> result = new List<TestCaseData>();

                result.Add(new TestCaseData(CircleOrbitData(), 24, new Point3D(Distance.FromAstronomicalUnits(0.369928936893292).Kilometers, Distance.FromAstronomicalUnits(-0.103776979676777).Kilometers, Distance.FromAstronomicalUnits(-0.0424300359336039).Kilometers)));
                result.Add(new TestCaseData(EllipseOrbitData(), 24, new Point3D(Distance.FromAstronomicalUnits(0.316490543667733).Kilometers, Distance.FromAstronomicalUnits(-0.251640966090076).Kilometers, Distance.FromAstronomicalUnits(-0.0496036407071594).Kilometers)));
                result.Add(new TestCaseData(NearParabolicOrbitData(), 24, new Point3D(Distance.FromAstronomicalUnits(-0.102456758387709).Kilometers, Distance.FromAstronomicalUnits(-0.640085808608179).Kilometers, Distance.FromAstronomicalUnits(-0.0428818574544085).Kilometers)));
                //result.Add(New TestCaseData(ParabolicOrbitData(), 24, New Point3D(0, 0, 0)))
                //result.Add(New TestCaseData(HyperbolaOrbitData(), 24, New Point3D(0, 0, 0)))

                return result;
            }
        }

        private static Mass MassOfSun => Mass.FromSolarMasses(1);

        private static Mass MassOfMercury => Mass.FromEarthMasses(0.0553);

        [Test]
        public void Position_WhenCalledRepeatedlyWithoutIncrementingTurn_ReturnsSameValue()
        {
            ITurnTracker turnTracker = Substitute.For<ITurnTracker>();
            turnTracker.TimeSinceStart.Returns(TimeSpan.FromDays(57));

            OrbitData orbitData = EllipseOrbitData();
            Orbit orbit = new Orbit(turnTracker, orbitData)
            {
                MassOfPrimary = MassOfSun,
                MassOfSatellite = MassOfMercury
            };

            Point3D result1 = orbit.Position;
            Point3D result2 = orbit.Position;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(result1.X, result2.X);
                Assert.AreEqual(result1.Y, result2.Y);
                Assert.AreEqual(result1.Z, result2.Z);
            });
        }

        [Test]
        public void OrbitPath_WhenCalled_ReturnsMultipleValues()
        {
            OrbitData orbitData = EllipseOrbitData();
            ITurnTracker turnTracker = Substitute.For<ITurnTracker>();
            Orbit orbit = new Orbit(turnTracker, orbitData, shouldDisplayOrbit: true)
            {
                MassOfPrimary = MassOfSun,
                MassOfSatellite = MassOfMercury
            };

            List<Point3D> result = orbit.OrbitPath;

            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(null, result);
                Assert.AreNotEqual(0, result.Count);
            });
        }

        [Test]
        public void OrbitPath_WhenCalled_ReturnsSameValueInFirstAndLastPosition()
        {
            OrbitData orbitData = EllipseOrbitData();
            ITurnTracker turnTracker = Substitute.For<ITurnTracker>();
            Orbit orbit = new Orbit(turnTracker, orbitData, shouldDisplayOrbit: true)
            {
                MassOfPrimary = MassOfSun,
                MassOfSatellite = MassOfMercury
            };

            List<Point3D> result = orbit.OrbitPath;

            int last = result.Count - 1;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(result[0].X, result[last].X);
                Assert.AreEqual(result[0].Y, result[last].Y);
                Assert.AreEqual(result[0].Z, result[last].Z);
            });
        }

        private static OrbitData CircleOrbitData()
        {
            return new OrbitData(longitudeOfAscendingNode: Angle.FromDegrees(48.3313), inclination: Angle.FromDegrees(7.0047), argumentOfPeriapsis: Angle.FromDegrees(29.1241), semiMajorAxis: Distance.FromAstronomicalUnits(0.38709893), eccentricity: 0.0, meanAnomalyZero: Angle.FromDegrees(168.6562));
        }

        private static OrbitData EllipseOrbitData()
        {
            // Data is for Mercury
            return new OrbitData(longitudeOfAscendingNode: Angle.FromDegrees(48.3313), inclination: Angle.FromDegrees(7.0047), argumentOfPeriapsis: Angle.FromDegrees(29.1241), semiMajorAxis: Distance.FromAstronomicalUnits(0.38709893), eccentricity: 0.205635, meanAnomalyZero: Angle.FromDegrees(168.6562));
        }

        private static OrbitData NearParabolicOrbitData()
        {
            return new OrbitData(longitudeOfAscendingNode: Angle.FromDegrees(48.3313), inclination: Angle.FromDegrees(7.0047), argumentOfPeriapsis: Angle.FromDegrees(29.1241), semiMajorAxis: Distance.FromAstronomicalUnits(0.38709893), eccentricity: 0.99, meanAnomalyZero: Angle.FromDegrees(168.6562));
        }

        private static OrbitData ParabolicOrbitData()
        {
            return new OrbitData(longitudeOfAscendingNode: Angle.FromDegrees(48.3313), inclination: Angle.FromDegrees(7.0047), argumentOfPeriapsis: Angle.FromDegrees(29.1241), semiMajorAxis: Distance.FromAstronomicalUnits(0.38709893), eccentricity: 1.0, meanAnomalyZero: Angle.FromDegrees(168.6562));
        }

        private static OrbitData HyperbolaOrbitData()
        {
            return new OrbitData(longitudeOfAscendingNode: Angle.FromDegrees(48.3313), inclination: Angle.FromDegrees(7.0047), argumentOfPeriapsis: Angle.FromDegrees(29.1241), semiMajorAxis: Distance.FromAstronomicalUnits(0.38709893), eccentricity: 1.1, meanAnomalyZero: Angle.FromDegrees(168.6562));
        }

        [Test]
        public void ctor_ShouldDisplayOrbitIsFalse_DoesNotGenerateOrbitPath()
        {
            OrbitData orbitData = EllipseOrbitData();
            ITurnTracker turnTracker = Substitute.For<ITurnTracker>();
            Orbit orbit = new Orbit(turnTracker, orbitData, shouldDisplayOrbit: false);

            List<Point3D> result = orbit.OrbitPath;

            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(null, result);
                Assert.AreEqual(0, result.Count);
            });
        }

        [Test]
        public void StartDisplayingOrbitPath_WhenCalled_GeneratesOrbitPath()
        {
            OrbitData orbitData = EllipseOrbitData();
            ITurnTracker turnTracker = Substitute.For<ITurnTracker>();
            Orbit orbit = new Orbit(turnTracker, orbitData, shouldDisplayOrbit: false)
            {
                MassOfPrimary = MassOfSun,
                MassOfSatellite = MassOfMercury
            };

            List<Point3D> displayFalseResult = orbit.OrbitPath;

            orbit.StartDisplayingOrbitPath();
            List<Point3D> result = orbit.OrbitPath;

            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(null, displayFalseResult);
                Assert.AreEqual(0, displayFalseResult.Count);
                Assert.AreNotEqual(null, result);
                Assert.AreNotEqual(0, result.Count);
            });
        }

        [Test]
        public void StopDisplayingOrbitPath_WhenCalled_ClearsOrbitPath()
        {
            OrbitData orbitData = EllipseOrbitData();
            ITurnTracker turnTracker = Substitute.For<ITurnTracker>();
            Orbit orbit = new Orbit(turnTracker, orbitData, shouldDisplayOrbit: true)
            {
                MassOfPrimary = MassOfSun,
                MassOfSatellite = MassOfMercury
            };

            List<Point3D> displayTrueResult = orbit.OrbitPath;

            orbit.StopDisplayingOrbitPath();
            List<Point3D> result = orbit.OrbitPath;

            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(null, displayTrueResult);
                Assert.AreNotEqual(0, displayTrueResult.Count);
                Assert.AreNotEqual(null, result);
                Assert.AreEqual(0, result.Count);
            });
        }
    }
}
