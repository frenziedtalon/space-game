Imports Core.Classes
Imports NSubstitute
Imports NUnit.Framework
Imports OrbitalMechanics.Classes
Imports TurnTracker
Imports OrbitalMechanics.Constants

Namespace Classes
    <TestFixture>
    Public Class OrbitTests

        <TestCase(0.1, 0.0316227766)>
        <TestCase(1, 1)>
        <TestCase(1.245, 1.389165622)>
        <TestCase(1.5, 1.837117307)>
        <TestCase(6, 14.69693846)>
        Public Sub Period_WhenCalled_CalculatesCorrectValue(semiMajorAxis As Double, expectedResultYears As Double)

            Const acceptableDelta = 1 / (1 * 60 * 60 * 24)
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim zeroAngle = Angle.FromRadians(0)

            Dim orbit As New Orbit(turnTracker:=turnTracker,
                                   semiMajorAxis:=Distance.FromAstronomicalUnits(semiMajorAxis),
                                   eccentricity:=0,
                                   inclination:=zeroAngle,
                                   argumentOfPeriapsis:=zeroAngle,
                                   longitudeOfAscendingNode:=zeroAngle,
                                   meanAnomalyZero:=zeroAngle)


            Dim expectedDays = expectedResultYears * DaysInJulianYear

            Dim resultDays = orbit.Period.TotalDays

            Assert.AreEqual(expectedDays, resultDays, acceptableDelta)

        End Sub

    End Class
End Namespace