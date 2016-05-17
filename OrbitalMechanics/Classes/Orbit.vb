Imports System.Windows.Media.Media3D
Imports Core.Classes
Imports TurnTracker

Namespace Classes
    Public Class Orbit
        Implements IOrbit

        Private ReadOnly _turnTracker As ITurnTracker

        Public Sub New(turnTracker As ITurnTracker,
                        semiMajorAxis As Distance,
                        eccentricity As Double,
                        inclination As Angle,
                        argumentOfPeriapsis As Angle,
                        longitudeOfAscendingNode As Angle,
                        meanAnomalyZero As Angle)
            _turnTracker = turnTracker
            _semiMajorAxis = semiMajorAxis
            _eccentricity = eccentricity
            _inclination = inclination
            _argumentOfPeriapsis = argumentOfPeriapsis
            _longitudeOfAscendingNode = longitudeOfAscendingNode
            _meanAnomalyZero = meanAnomalyZero
        End Sub

        Public Sub Update() Implements IOrbit.Update
            _recalculatePosition = True
        End Sub

        Private _period As TimeSpan = TimeSpan.Zero
        Public ReadOnly Property Period As TimeSpan Implements IOrbit.Period
            Get
                If _period = TimeSpan.Zero AndAlso SemiMajorAxis IsNot Nothing Then
                    Dim years = Math.Pow(SemiMajorAxis.AstronomicalUnits, 1.5)
                    _period = TimeSpan.FromDays(years * Constants.DaysInJulianYear)
                End If
                Return _period
            End Get
        End Property

        Private ReadOnly _longitudeOfAscendingNode As Angle
        Public ReadOnly Property LongitudeOfAscendingNode As Angle Implements IOrbit.LongitudeOfAscendingNode
            Get
                Return _longitudeOfAscendingNode
            End Get
        End Property

        Private ReadOnly _inclination As Angle
        Public ReadOnly Property Inclination As Angle Implements IOrbit.Inclination
            Get
                Return _inclination
            End Get
        End Property

        Private ReadOnly _argumentOfPeriapsis As Angle
        Public ReadOnly Property ArgumentOfPeriapsis As Angle Implements IOrbit.ArgumentOfPeriapsis
            Get
                Return _argumentOfPeriapsis
            End Get
        End Property

        Private ReadOnly _semiMajorAxis As Distance
        ''' <summary>
        ''' One half of the major axis, represents the mean distance from the primary 
        ''' </summary>
        Public ReadOnly Property SemiMajorAxis As Distance Implements IOrbit.SemiMajorAxis
            Get
                Return _semiMajorAxis
            End Get
        End Property

        Private ReadOnly _eccentricity As Double
        Public ReadOnly Property Eccentricity As Double Implements IOrbit.Eccentricity
            Get
                Return _eccentricity
            End Get
        End Property

        Private _periapsisDistance As Distance
        Public ReadOnly Property PeriapsisDistance As Distance Implements IOrbit.PeriapsisDistance
            Get
                If _periapsisDistance Is Nothing Then
                    _periapsisDistance = Distance.FromAstronomicalUnits(SemiMajorAxis.AstronomicalUnits * (1 - Eccentricity))
                End If
                Return _periapsisDistance
            End Get
        End Property

        Private _apapsisDistance As Distance
        Public ReadOnly Property ApapsisDistance As Distance Implements IOrbit.ApapsisDistance
            Get
                If _apapsisDistance Is Nothing Then
                    _apapsisDistance = Distance.FromAstronomicalUnits(SemiMajorAxis.AstronomicalUnits * (1 + Eccentricity))
                End If
                Return _apapsisDistance
            End Get
        End Property

        Private ReadOnly _meanAnomalyZero As Angle
        Public ReadOnly Property MeanAnomalyZero As Angle Implements IOrbit.MeanAnomalyZero
            Get
                Return _meanAnomalyZero
            End Get
        End Property

        Private Function TurnsPerOrbit() As Double
            Return Period.Ticks / _turnTracker.TurnLength.Ticks
        End Function

        Private Function LongitudeOfPeriapsis() As Angle
            Return Angle.FromRadians(LongitudeOfAscendingNode.Radians + ArgumentOfPeriapsis.Radians)
        End Function

        Private Function MeanLongitude(meanAnomaly As Angle) As Angle
            Return Angle.FromRadians(meanAnomaly.Radians + LongitudeOfAscendingNode.Radians)
        End Function

        Private _meanAngularMotion As Double
        ''' <summary>
        ''' Radians moved in the orbit per day
        ''' </summary>
        Private ReadOnly Property MeanAngularMotion As Double
            Get
                If Double.Equals(_meanAngularMotion, 0.0) Then
                    _meanAngularMotion = (2 * Math.PI) / Period.TotalDays
                End If
                Return _meanAngularMotion
            End Get
        End Property

        Dim _recalculatePosition As Boolean = True
        Dim _position As Point3D
        Public ReadOnly Property Position As Point3D Implements IOrbit.Position
            Get
                If _recalculatePosition Then
                    Dim meanAnomaly As Angle = CalculateMeanAnomaly()
                    Dim eccentricAnomaly As Angle = CalculateEccentricAnomaly(meanAnomaly)
                    Dim trueAnomaly As Angle = CalculateTrueAnomaly(eccentricAnomaly)
                    Dim range As Distance = CalculateRange(trueAnomaly)

                    Dim x = CalculateX(range, trueAnomaly)
                    Dim y = CalculateY(range, trueAnomaly)
                    Dim z = CalculateZ(range, trueAnomaly)

                    _position = New Point3D(x, y, z)
                    _recalculatePosition = False
                End If
                Return _position
            End Get
        End Property

        ''' <summary>
        ''' Angle of average orbital motion.
        ''' </summary>
        ''' <remarks>0 at periapsis. Increases uniformly with time.</remarks>
        Private Function CalculateMeanAnomaly() As Angle
            Dim radians = MeanAnomalyZero.Radians + (MeanAngularMotion * _turnTracker.TimeSinceStart.TotalDays)
            Return Angle.FromRadians(radians)
        End Function

        Private Function CalculateEccentricAnomaly(meanAnomaly As Angle) As Angle

            Dim threshold = Angle.FromDegrees(0.001).Radians
            Dim maxIterations = 50

            Dim iterations = 0

            ' E = M + e * sin(M) * ( 1.0 + e * cos(M) )
            ' E = M + (e * sin(M) * ( 1.0 + (e * cos(M)) ))
            'Dim E0 = MeanAnomaly().Radians + (
            '    Eccentricity * Math.Sin(MeanAnomaly().Radians) *
            '    (1 + (Eccentricity * Math.Cos(MeanAnomaly().Radians)))
            ')

            ' initial guess
            Dim E0 As Double = If(Eccentricity > 0.8, Math.PI, meanAnomaly.Radians)

            Do Until iterations >= maxIterations
                ' E1 = E0 - ( E0 - e * sin(E0) - M ) / ( 1 - e * cos(E0) )
                Dim E1 As Double = 0.0


                If E1 - E0 < threshold Then
                    Return Angle.FromRadians(E1)
                End If

                iterations += 1

            Loop

            ' values are not converging, eccentricity probably near to 1. Use calculation for a near-parabolic or parabolic orbit.

            If Double.Equals(Eccentricity, 1.0) Then
                ' http://www.stjarnhimlen.se/comp/ppcomp.html#18

            Else
                ' http://www.stjarnhimlen.se/comp/ppcomp.html#19
            End If
        End Function

        ''' <summary>
        ''' Angle between planet and perihelion.
        ''' </summary>
        ''' <remarks>0 at perihelion. Unit is radians. Changes most rapidly at perihelion.</remarks>
        Private Function CalculateTrueAnomaly(eccentricAnomaly As Angle) As Angle
            Throw New NotImplementedException()
            ' uses _eccentricAnomaly
            ' cosn(t) = (cosE(t)-e) / (1 - ecosE(t))
            ' n(t) = true anomaly
            ' E(t) = eccentric anomaly
            ' e = eccentricity
        End Function

        Private Function CalculateRange(trueAnomaly As Angle) As Distance
            Dim aus = (SemiMajorAxis.AstronomicalUnits * (1 - (Math.Pow(Eccentricity, 2)))) / (1 + (Eccentricity * (Math.Cos(trueAnomaly.Radians))))
            Return Distance.FromAstronomicalUnits(aus)
        End Function

        'R,X,Y,Z-Heliocentric Distances
        'TA - True Anomaly
        'N - Longitude of the Ascending Node
        'w - Argument of the Perihelion
        'i - inclination

        'X = R * (Cos(N) * Cos(TA + w) - Sin(N) * Sin(TA+w)*Cos(i)
        Private Function CalculateX(range As Distance, trueAnomaly As Angle) As Double
            Return range.Kilometers * ((Math.Cos(LongitudeOfAscendingNode.Radians) * Math.Cos(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) - (Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians))) * Math.Cos(Inclination.Radians)
        End Function

        'Y = R * (Sin(N) * Cos(TA+w) + Cos(N) * Sin(TA+w)) * Cos(i))
        Private Function CalculateY(range As Distance, trueAnomaly As Angle) As Double
            Return range.Kilometers * ((Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Cos(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) + (Math.Cos(LongitudeOfAscendingNode.Radians)) * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) * Math.Cos(Inclination.Radians)
        End Function

        'Z = R * Sin(TA+w) * Sin(i)
        Private Function CalculateZ(range As Distance, trueAnomaly As Angle) As Double
            Return range.Kilometers * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)
        End Function

    End Class
End Namespace