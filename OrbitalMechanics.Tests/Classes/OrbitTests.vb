Imports Core.Classes
Imports Microsoft.VisualBasic.CompilerServices
Imports NSubstitute
Imports NUnit.Framework
Imports OrbitalMechanics.Classes
Imports TurnTracker
Imports OrbitalMechanics.Constants

Namespace Classes
    <TestFixture>
    Public Class OrbitTests

        <TestCase(0.1, 0.032)>
        <TestCase(1, 1)>
        <TestCase(1.245, 1.389)>
        <TestCase(1.5, 1.837)>
        <TestCase(6, 14.697)>
        Public Sub Period_WhenCalled_CalculatesCorrectValue(semiMajorAxis As Double, expectedResultYears As Double)

            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)

            Dim orbit As New Orbit(turnTracker:=turnTracker,
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(semiMajorAxis),
                                   eccentricity:=0,
                                   inclination:=Angle.FromRadians(0),
                                   argumentOfPeriapsis:=Angle.FromRadians(0),
                                   longitudeOfAscendingNode:=Angle.FromRadians(0),
                                   meanAnomalyZero:=Angle.FromRadians(0))

            Dim expectedDays = expectedResultYears * DaysInJulianYear

            Dim resultDays = orbit.Period.Days

            Assert.AreEqual(resultDays, expectedDays)

        End Sub

    End Class
End Namespace