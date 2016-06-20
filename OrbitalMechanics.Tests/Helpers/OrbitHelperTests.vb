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

        Public Sub CalculatePeriod_WhenCalled_ReturnsExpectedValue(massOfPrimary As Double, massOfSatellite As Double)


        End Sub

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

    End Class
End Namespace