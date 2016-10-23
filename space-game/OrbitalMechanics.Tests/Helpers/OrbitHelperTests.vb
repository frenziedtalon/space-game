Imports System.Windows.Media.Media3D
Imports Core.Classes
Imports NSubstitute
Imports NUnit.Framework

Namespace Helpers
    <TestFixture>
    Public Class OrbitHelperTests

        <Test, TestCaseSource(NameOf(TotalMassTestCases))>
        Public Sub CalculateTotalMass_WhenCalled_ReturnsExpectedValue(massOfPrimary As Mass, massOfSatellite As Mass)

            Dim acceptableDelta = Mass.FromEarthMasses(0.00000000000001).EarthMasses

            Dim expected = Mass.FromEarthMasses(massOfPrimary.EarthMasses + massOfSatellite.EarthMasses)
            Dim orbitHelper = New OrbitHelper

            Dim result = orbitHelper.CalculateTotalMass(massOfPrimary, massOfSatellite)

            Assert.AreEqual(expected.EarthMasses, result.EarthMasses, acceptableDelta)
        End Sub

        Private Shared ReadOnly Property TotalMassTestCases As List(Of TestCaseData)
            Get
                Dim result As New List(Of TestCaseData)

                result.Add(New TestCaseData(MassOfSun, MassOfEarth))
                result.Add(New TestCaseData(MassOfSun, MassOfJupiter))
                result.Add(New TestCaseData(MassOfEarth, MassOfMoon))

                Return result
            End Get
        End Property

        <Test, TestCaseSource(NameOf(CalculatePeriodTestCases))>
        Public Sub CalculatePeriod_WhenCalled_ReturnsExpectedValue(massOfPrimary As Mass, massOfSatellite As Mass, semiMajorAxis As Distance, expected As TimeSpan)

            Dim acceptableDelta = expected.TotalDays * 0.005
            Dim orbitHelper = New OrbitHelper
            Dim totalMass = orbitHelper.CalculateTotalMass(massOfPrimary, massOfSatellite)

            Dim result = orbitHelper.CalculatePeriod(totalMass, semiMajorAxis)

            Assert.AreEqual(expected.TotalDays, result.TotalDays, acceptableDelta)
        End Sub

        Private Shared ReadOnly Property CalculatePeriodTestCases As List(Of TestCaseData)
            Get
                Dim result As New List(Of TestCaseData)

                result.Add(New TestCaseData(MassOfSun, MassOfEarth, SemiMajorAxisOfEarth, PeriodOfEarth))
                result.Add(New TestCaseData(MassOfSun, MassOfJupiter, SemiMajorAxisOfJupiter, PeriodOfJupiter))
                result.Add(New TestCaseData(MassOfEarth, MassOfMoon, SemiMajorAxisOfMoon, PeriodOfMoon))

                Return result
            End Get
        End Property

        Private Shared ReadOnly Property MassOfSun As Mass
            Get
                Return Mass.FromSolarMasses(1)
            End Get
        End Property

        Private Shared ReadOnly Property MassOfJupiter As Mass
            Get
                Return Mass.FromEarthMasses(317.83)
            End Get
        End Property

        Private Shared ReadOnly Property MassOfEarth As Mass
            Get
                Return Mass.FromEarthMasses(1)
            End Get
        End Property

        Private Shared ReadOnly Property MassOfMoon As Mass
            Get
                Return Mass.FromEarthMasses(0.0203)
            End Get
        End Property

        Private Shared ReadOnly Property SemiMajorAxisOfEarth As Distance
            Get
                Return Distance.FromAstronomicalUnits(1)
            End Get
        End Property

        Private Shared ReadOnly Property SemiMajorAxisOfJupiter As Distance
            Get
                Return Distance.FromAstronomicalUnits(5.20336301)
            End Get
        End Property

        Private Shared ReadOnly Property SemiMajorAxisOfMoon As Distance
            Get
                Return Distance.FromAstronomicalUnits(0.00257188152)
            End Get
        End Property

        Private Shared ReadOnly Property PeriodOfEarth As TimeSpan
            Get
                Return TimeSpan.FromDays(1 * Constants.DaysInJulianYear)
            End Get
        End Property

        Private Shared ReadOnly Property PeriodOfJupiter As TimeSpan
            Get
                Return TimeSpan.FromDays(4332.589)
            End Get
        End Property

        Private Shared ReadOnly Property PeriodOfMoon As TimeSpan
            Get
                Return TimeSpan.FromDays(27.321662)
            End Get
        End Property
    End Class
End Namespace