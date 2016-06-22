﻿Imports System.Windows.Media.Media3D
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

        <Test, TestCaseSource(NameOf(PositionTestCases))>
        Public Sub Postion_WhenCalled_CalculatesCorrectValue(orbitData As OrbitData,
                                                             days As Integer,
                                                             expected As Point3D)

            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            turnTracker.TimeSinceStart.Returns(TimeSpan.FromDays(days))

            Dim acceptableDelta = Distance.FromKilometers(1).Kilometers

            Dim orbit As New Orbit(turnTracker:=turnTracker,
                                   data:=orbitData)

            Dim result = orbit.Position

            Assert.AreEqual(expected.X, result.X, acceptableDelta)
            Assert.AreEqual(expected.Y, result.Y, acceptableDelta)
            Assert.AreEqual(expected.Z, result.Z, acceptableDelta)

        End Sub

        Private Shared ReadOnly Property PositionTestCases As List(Of TestCaseData)
            Get
                Dim result As New List(Of TestCaseData)

                result.Add(New TestCaseData(CircleOrbitData(), 24,
                                            New Point3D(Distance.FromAstronomicalUnits(0.369928936893292).Kilometers,
                                                        Distance.FromAstronomicalUnits(-0.103776979676777).Kilometers,
                                                        Distance.FromAstronomicalUnits(-0.0424300359336039).Kilometers)
                                                        )
                                            )
                result.Add(New TestCaseData(EllipseOrbitData(), 24,
                                            New Point3D(Distance.FromAstronomicalUnits(0.316490543667733).Kilometers,
                                                        Distance.FromAstronomicalUnits(-0.251640966090076).Kilometers,
                                                        Distance.FromAstronomicalUnits(-0.0496036407071594).Kilometers)
                                                        )
                                            )
                result.Add(New TestCaseData(NearParabolicOrbitData(), 24,
                                            New Point3D(Distance.FromAstronomicalUnits(-0.102456758387709).Kilometers,
                                                        Distance.FromAstronomicalUnits(-0.640085808608179).Kilometers,
                                                        Distance.FromAstronomicalUnits(-0.0428818574544085).Kilometers)
                                                        )
                                            )
                'result.Add(New TestCaseData(ParabolicOrbitData(), 24, New Point3D(0, 0, 0)))
                'result.Add(New TestCaseData(HyperbolaOrbitData(), 24, New Point3D(0, 0, 0)))

                Return result
            End Get
        End Property

        <Test()>
        Public Sub Position_WhenCalledRepeatedlyWithoutIncrementingTurn_ReturnsSameValue()

            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            turnTracker.TimeSinceStart.Returns(TimeSpan.FromDays(57))

            Dim orbitData = EllipseOrbitData()
            Dim orbit = New Orbit(turnTracker, orbitData)

            Dim result1 = orbit.Position
            Dim result2 = orbit.Position

            Assert.AreEqual(result1.X, result2.X)
            Assert.AreEqual(result1.Y, result2.Y)
            Assert.AreEqual(result1.Z, result2.Z)
        End Sub

        <Test()>
        Public Sub OrbitPath_WhenCalled_ReturnsMultipleValues()

            Dim orbitData = EllipseOrbitData()
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim orbit = New Orbit(turnTracker, orbitData, shouldDisplayOrbit:=True)

            Dim result = orbit.OrbitPath

            Assert.AreNotEqual(Nothing, result)
            Assert.AreNotEqual(0, result.Count)
        End Sub

        <Test()>
        Public Sub OrbitPath_WhenCalled_ReturnsSameValueInFirstAndLastPosition()

            Dim orbitData = EllipseOrbitData()
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim orbit = New Orbit(turnTracker, orbitData, shouldDisplayOrbit:=True)

            Dim result = orbit.OrbitPath

            Dim last = result.Count - 1

            Assert.AreEqual(result(0).X, result(last).X)
            Assert.AreEqual(result(0).Y, result(last).Y)
            Assert.AreEqual(result(0).Z, result(last).Z)
        End Sub

        Private Shared Function CircleOrbitData() As OrbitData
            Return New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                                  inclination:=Angle.FromDegrees(7.0047),
                                  argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                                  semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                                  eccentricity:=0.0,
                                  meanAnomalyZero:=Angle.FromDegrees(168.6562))
        End Function

        Private Shared Function EllipseOrbitData() As OrbitData
            ' Data is for Mercury
            Return New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                                  inclination:=Angle.FromDegrees(7.0047),
                                  argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                                  semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                                  eccentricity:=0.205635,
                                  meanAnomalyZero:=Angle.FromDegrees(168.6562))
        End Function

        Private Shared Function NearParabolicOrbitData() As OrbitData
            Return New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                                  inclination:=Angle.FromDegrees(7.0047),
                                  argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                                  semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                                  eccentricity:=0.99,
                                  meanAnomalyZero:=Angle.FromDegrees(168.6562))
        End Function

        Private Shared Function ParabolicOrbitData() As OrbitData
            Return New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                                  inclination:=Angle.FromDegrees(7.0047),
                                  argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                                  semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                                  eccentricity:=1.0,
                                  meanAnomalyZero:=Angle.FromDegrees(168.6562))
        End Function

        Private Shared Function HyperbolaOrbitData() As OrbitData
            Return New OrbitData(longitudeOfAscendingNode:=Angle.FromDegrees(48.3313),
                                  inclination:=Angle.FromDegrees(7.0047),
                                  argumentOfPeriapsis:=Angle.FromDegrees(29.1241),
                                  semiMajorAxis:=Distance.FromAstronomicalUnits(0.38709893),
                                  eccentricity:=1.1,
                                  meanAnomalyZero:=Angle.FromDegrees(168.6562))
        End Function

        <Test()>
        Public Sub ctor_ShouldDisplayOrbitIsFalse_DoesNotGenerateOrbitPath()
            Dim orbitData = EllipseOrbitData()
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim orbit = New Orbit(turnTracker, orbitData, shouldDisplayOrbit:=False)

            Dim result = orbit.OrbitPath

            Assert.AreNotEqual(Nothing, result)
            Assert.AreEqual(0, result.Count)
        End Sub

        <Test()>
        Public Sub StartDisplayingOrbitPath_WhenCalled_GeneratesOrbitPath()
            Dim orbitData = EllipseOrbitData()
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim orbit = New Orbit(turnTracker, orbitData, shouldDisplayOrbit:=False)

            Dim displayFalseResult = orbit.OrbitPath

            Assert.AreNotEqual(Nothing, displayFalseResult)
            Assert.AreEqual(0, displayFalseResult.Count)

            orbit.StartDisplayingOrbitPath()

            Dim result = orbit.OrbitPath

            Assert.AreNotEqual(Nothing, result)
            Assert.AreNotEqual(0, result.Count)
        End Sub

        <Test()>
        Public Sub StopDisplayingOrbitPath_WhenCalled_ClearsOrbitPath()
            Dim orbitData = EllipseOrbitData()
            Dim turnTracker As ITurnTracker = Substitute.For(Of ITurnTracker)
            Dim orbit = New Orbit(turnTracker, orbitData, shouldDisplayOrbit:=True)

            Dim displayTrueResult = orbit.OrbitPath

            Assert.AreNotEqual(Nothing, displayTrueResult)
            Assert.AreNotEqual(0, displayTrueResult.Count)

            orbit.StopDisplayingOrbitPath()

            Dim result = orbit.OrbitPath

            Assert.AreNotEqual(Nothing, result)
            Assert.AreEqual(0, result.Count)
        End Sub

    End Class
End Namespace