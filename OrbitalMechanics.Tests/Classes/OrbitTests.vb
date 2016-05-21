Imports System.Windows.Media.Media3D
Imports Core.Classes
Imports Data.Classes
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

        ' TODO: Need tests for near parabolic and parabolic orbits

        <Test, TestCaseSource(NameOf(PositionTestCases))>
        Public Sub Postion_WhenCalled_CalculatesCorrectValue(orbitData As OrbitData,
                                                             days As Integer,
                                                             expected As Point3D)

            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            turnTracker.TimeSinceStart.Returns(TimeSpan.FromDays(days))

            Dim orbit As New Orbit(turnTracker:=turnTracker,
                                   data:=orbitData)

            Dim result = orbit.Position

            Assert.AreEqual(expected.X, result.X)
            Assert.AreEqual(expected.Y, result.Y)
            Assert.AreEqual(expected.Z, result.Z)

        End Sub

        Private Shared ReadOnly Property PositionTestCases As List(Of TestCaseData)
            Get
                Dim result As New List(Of TestCaseData)

                Dim data = MercuryOrbitData()

                Dim expectedPosition = New Point3D(0, 0, 0)

                result.Add(New TestCaseData(data, 24, expectedPosition))

                Return result
            End Get
        End Property

        <Test()>
        Public Sub Position_WhenCalledRepeatedlyWithoutIncrementingTurn_ReturnsSameValue()

            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            turnTracker.TimeSinceStart.Returns(TimeSpan.FromDays(57))

            Dim orbitData = MercuryOrbitData()
            Dim orbit = New Orbit(turnTracker, orbitData)

            Dim result1 = orbit.Position
            Dim result2 = orbit.Position

            Assert.AreEqual(result1.X, result2.X)
            Assert.AreEqual(result1.Y, result2.Y)
            Assert.AreEqual(result1.Z, result2.Z)
        End Sub

        <Test()>
        Public Sub OrbitPath_WhenCalled_ReturnsMultipleValues()

            Dim orbitData = MercuryOrbitData()
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim orbit = New Orbit(turnTracker, orbitData)

            Dim result = orbit.OrbitPath

            Assert.AreNotEqual(Nothing, result)
            Assert.AreNotEqual(0, result.Count)
        End Sub

        <Test()>
        Public Sub OrbitPath_WhenCalled_ReturnsSameValueInFirstAndLastPosition()

            Dim orbitData = MercuryOrbitData()
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim orbit = New Orbit(turnTracker, orbitData)

            Dim result = orbit.OrbitPath

            Dim last = result.Count - 1

            Assert.AreEqual(result(0).X, result(last).X)
            Assert.AreEqual(result(0).Y, result(last).Y)
            Assert.AreEqual(result(0).Z, result(last).Z)
        End Sub

        Private Shared Function MercuryOrbitData() As OrbitData
            Return New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                                  inclination:=Angle.FromDegrees(7.0047),
                                  argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                                  semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                                  eccentricity:=0.205635,
                                  meanAnomalyZero:=Angle.FromDegrees(168.6562))
        End Function

    End Class
End Namespace