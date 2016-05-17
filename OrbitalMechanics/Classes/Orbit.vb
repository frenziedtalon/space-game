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

        Public ReadOnly Property Position As Point3D Implements IOrbit.Position
            Get
                _eccentricAnomaly = CalculateEccentricAnomaly()
                Return New Point3D(CalculateX(), CalculateY(), CalculateZ())
            End Get
        End Property

        Private Function TrueAnomaly() As Angle Implements IOrbit.TrueAnomaly
            Throw New NotImplementedException()
            ' uses _eccentricAnomaly
            ' cosn(t) = (cosE(t)-e) / (1 - ecosE(t))
            ' n(t) = true anomaly
            ' E(t) = eccentric anomaly
            ' e = eccentricity
        End Function

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

        Private Function MeanAnomaly() As Angle Implements IOrbit.MeanAnomaly

            Dim radians = MeanAnomalyZero.Radians + (MeanAngularMotion() * _turnTracker.TimeSinceStart.TotalDays)

            Return Angle.FromRadians(radians)
        End Function

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
        Public ReadOnly Property MeanAnomalyZero() As Angle Implements IOrbit.MeanAnomalyZero
            Get
                Return _meanAnomalyZero
            End Get
        End Property

        Public Sub Update() Implements IOrbit.Update
            ' position is calculated on request
        End Sub

        Private Function TurnsPerOrbit() As Double
            Return Period.Ticks / _turnTracker.TurnLength.Ticks
        End Function

        Private Function Range() As Distance
            Dim aus = (SemiMajorAxis.AstronomicalUnits * (1 - (Math.Pow(Eccentricity, 2)))) / (1 + (Eccentricity * (Math.Cos(TrueAnomaly.Radians))))
            Return Distance.FromAstronomicalUnits(aus)
        End Function

        'R,X,Y,Z-Heliocentric Distances
        'TA - True Anomaly
        'N - Longitude of the Ascending Node
        'w - Argument of the Perihelion
        'i - inclination

        'X = R * (Cos(N) * Cos(TA + w) - Sin(N) * Sin(TA+w)*Cos(i)
        Private Function CalculateX() As Double
            Return Range().Kilometers * ((Math.Cos(LongitudeOfAscendingNode.Radians) * Math.Cos(TrueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) - (Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Sin(TrueAnomaly.Radians + ArgumentOfPeriapsis.Radians))) * Math.Cos(Inclination.Radians)
        End Function

        'Y = R * (Sin(N) * Cos(TA+w) + Cos(N) * Sin(TA+w)) * Cos(i))
        Private Function CalculateY() As Double
            Return Range().Kilometers * ((Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Cos(TrueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) + (Math.Cos(LongitudeOfAscendingNode.Radians)) * Math.Sin(TrueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) * Math.Cos(Inclination.Radians)
        End Function

        'Z = R * Sin(TA+w) * Sin(i)
        Private Function CalculateZ() As Double
            Return Range().Kilometers * Math.Sin(TrueAnomaly.Radians + ArgumentOfPeriapsis.Radians)
        End Function

        Private Function LongitudeOfPeriapsis() As Angle
            Return Angle.FromRadians(LongitudeOfAscendingNode.Radians + ArgumentOfPeriapsis.Radians)
        End Function

        Private Function MeanLongitude() As Angle
            Return Angle.FromRadians(MeanAnomaly.Radians + LongitudeOfAscendingNode.Radians)
        End Function

        ''' <summary>
        ''' Radians moved in the orbit per day
        ''' </summary>
        Private Function MeanAngularMotion() As Double
            Return (2 * Math.PI) / Period.TotalDays
        End Function

        Private _eccentricAnomaly As Angle

        Private Function CalculateEccentricAnomaly() As Angle

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
            Dim E0 As Double = If(Eccentricity > 0.8, Math.PI, MeanAnomaly().Radians)

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
    End Class
End Namespace