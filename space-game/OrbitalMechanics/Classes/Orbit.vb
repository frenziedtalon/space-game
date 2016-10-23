Imports System.Windows.Media.Media3D
Imports Core.Classes
Imports Data.Classes
Imports Newtonsoft.Json
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
                        meanAnomalyZero As Angle,
                        Optional shouldDisplayOrbit As Boolean = False)
            _turnTracker = turnTracker
            _semiMajorAxis = semiMajorAxis
            _eccentricity = eccentricity
            _inclination = inclination
            _argumentOfPeriapsis = argumentOfPeriapsis
            _longitudeOfAscendingNode = longitudeOfAscendingNode
            _meanAnomalyZero = meanAnomalyZero
            _shouldDisplayOrbit = shouldDisplayOrbit
        End Sub

        Public Sub New(turnTracker As ITurnTracker,
                       data As OrbitData,
                       Optional shouldDisplayOrbit As Boolean = False)

            Me.New(turnTracker:=turnTracker,
                                   semiMajorAxis:=data.SemiMajorAxis,
                                   eccentricity:=data.Eccentricity,
                                   inclination:=data.Inclination,
                                   argumentOfPeriapsis:=data.ArgumentOfPeriapsis,
                                   longitudeOfAscendingNode:=data.LongitudeOfAscendingNode,
                                   meanAnomalyZero:=data.MeanAnomalyZero,
                                   shouldDisplayOrbit:=shouldDisplayOrbit)
        End Sub

        Public Sub Update() Implements IOrbit.Update
            _recalculatePosition = True
        End Sub

        Private _period As TimeSpan = TimeSpan.Zero
        <JsonIgnore()>
        Public ReadOnly Property Period As TimeSpan Implements IOrbit.Period
            Get
                If _period = TimeSpan.Zero Then
                    Dim orbitHelper = New OrbitHelper
                    _period = orbitHelper.CalculatePeriod(orbitHelper.CalculateTotalMass(MassOfPrimary, MassOfSatellite), SemiMajorAxis)
                End If
                Return _period
            End Get
        End Property

        Public ReadOnly Property PeriodDays As Double
            Get
                Return Period.TotalDays
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
        <JsonIgnore()>
        Public ReadOnly Property PeriapsisDistance As Distance Implements IOrbit.PeriapsisDistance
            Get
                If _periapsisDistance Is Nothing Then
                    _periapsisDistance = Distance.FromAstronomicalUnits(SemiMajorAxis.AstronomicalUnits * (1 - Eccentricity))
                End If
                Return _periapsisDistance
            End Get
        End Property

        Private _apapsisDistance As Distance
        <JsonIgnore()>
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
            Return Angle.FromRadians(meanAnomaly.Radians + LongitudeOfPeriapsis.Radians)
        End Function

        ' TODO: If parent / satellite mass can ever be changed in the future this needs to change
        Public Property MassOfPrimary As Mass Implements IOrbit.MassOfPrimary

        Public Property MassOfSatellite As Mass Implements IOrbit.MassOfSatellite

        Private _meanAngularMotion As Angle
        ''' <summary>
        ''' Radians moved in the orbit per day
        ''' </summary>
        Private ReadOnly Property MeanAngularMotion As Angle
            Get
                If _meanAngularMotion Is Nothing Then
                    _meanAngularMotion = Angle.FromRadians((2 * Math.PI) / Period.TotalDays)
                End If
                Return _meanAngularMotion
            End Get
        End Property

        Dim _recalculatePosition As Boolean = True
        Dim _position As Point3D
        Public ReadOnly Property Position As Point3D Implements IOrbit.Position
            Get
                If _recalculatePosition Then
                    _position = CalculatePosition(_turnTracker.TimeSinceStart.TotalDays)
                    _recalculatePosition = False
                End If
                Return _position
            End Get
        End Property

        Private _shouldDisplayOrbit As Boolean
        Private ReadOnly Property ShouldDisplayOrbit As Boolean
            Get
                Return _shouldDisplayOrbit
            End Get
        End Property

        Private _orbitPath As List(Of Point3D)
        <JsonIgnore()>
        Public ReadOnly Property OrbitPath As List(Of Point3D) Implements IOrbit.OrbitPath
            Get
                If _orbitPath Is Nothing Then
                    If ShouldDisplayOrbit Then
                        _orbitPath = GenerateOrbitPath()
                    Else
                        _orbitPath = New List(Of Point3D)
                    End If
                End If
                Return _orbitPath
            End Get
        End Property

        Public Sub StartDisplayingOrbitPath()
            _shouldDisplayOrbit = True
            _orbitPath = Nothing
        End Sub

        Public Sub StopDisplayingOrbitPath()
            _shouldDisplayOrbit = False
            _orbitPath = Nothing
        End Sub

        Private Function CalculatePosition(days As Double) As Point3D
            Select Case Eccentricity
                Case > 1
                    Return CalculatePositionForHyperbolicOrbit(days)
                Case 1
                    Return CalculatePositionForParabolicOrbit(days)
                Case Else
                    Return CalculatePositionForEllipticalOrbit(days)
            End Select
        End Function

        Private Function CalculatePositionForEllipticalOrbit(days As Double) As Point3D
            Dim meanAnomaly As Angle = CalculateMeanAnomaly(days)
            Dim eccentricAnomaly As Angle = CalculateEccentricAnomaly(meanAnomaly)
            Dim trueAnomaly As Angle = CalculateTrueAnomaly(eccentricAnomaly)
            Dim distance As Distance = CalculateDistance(trueAnomaly)

            Dim x = CalculateX(distance, trueAnomaly)
            Dim y = CalculateY(distance, trueAnomaly)
            Dim z = CalculateZ(distance, trueAnomaly)

            Return New Point3D(x.Kilometers, y.Kilometers, z.Kilometers)
        End Function

        Private Function CalculatePositionForParabolicOrbit(days As Double) As Point3D
            Throw New NotImplementedException
        End Function

        Private Function CalculatePositionForHyperbolicOrbit(days As Double) As Point3D
            Throw New NotImplementedException
        End Function

        ''' <summary>
        ''' Angle of average orbital motion.
        ''' </summary>
        ''' <remarks>0 at periapsis. Increases uniformly with time.</remarks>
        Private Function CalculateMeanAnomaly(days As Double) As Angle
            Dim radians = MeanAnomalyZero.Radians + (MeanAngularMotion.Radians * days)
            Return Angle.FromRadians(radians)
        End Function

        Private Function CalculateEccentricAnomaly(meanAnomaly As Angle) As Angle

            Dim threshold = Angle.FromDegrees(0.001).Radians
            Dim maxIterations = 50
            Dim iterations = 0

            ' initial guess
            Dim En As Double = If(Eccentricity > 0.8, Math.PI, meanAnomaly.Radians)
            Dim En1 As Double = 0.0

            Do Until iterations >= maxIterations
                ' E1 = E0 - ( E0 - e * sin(E0) - M ) / ( 1 - e * cos(E0) )
                En1 = En - ((En - meanAnomaly.Radians - (Eccentricity * Math.Sin(En))) / (1 - (Eccentricity * Math.Cos(En))))

                If En1 - En < threshold Then
                    Exit Do
                End If

                iterations += 1
                En = En1
            Loop

            Return Angle.FromRadians(En1)
        End Function

        ''' <summary>
        ''' Angle between planet and perihelion.
        ''' </summary>
        ''' <remarks>0 at perihelion. Unit is radians. Changes most rapidly at perihelion.</remarks>
        Private Function CalculateTrueAnomaly(eccentricAnomaly As Angle) As Angle

            Dim x = Math.Sqrt(1 - Eccentricity) * Math.Cos(eccentricAnomaly.Radians / 2)

            Dim y = Math.Sqrt(1 + Eccentricity) * Math.Sin(eccentricAnomaly.Radians / 2)

            Dim radians = 2 * Math.Atan2(y, x)

            Return Angle.FromRadians(radians)
        End Function

        Private Function CalculateDistance(trueAnomaly As Angle) As Distance
            Dim aus = (SemiMajorAxis.AstronomicalUnits * (1 - (Math.Pow(Eccentricity, 2)))) / (1 + (Eccentricity * (Math.Cos(trueAnomaly.Radians))))
            Return Distance.FromAstronomicalUnits(aus)
        End Function

        'R,X,Y,Z-Heliocentric Distances
        'TA - True Anomaly
        'N - Longitude of the Ascending Node
        'w - Argument of the Perihelion
        'i - inclination

        'X = R * (Cos(N) * Cos(TA + w) - Sin(N) * Sin(TA+w)*Cos(i)
        Private Function CalculateX(distance As Distance, trueAnomaly As Angle) As Distance
            Dim kilometers = distance.Kilometers * ((Math.Cos(LongitudeOfAscendingNode.Radians) * Math.Cos(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) - (Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians))) * Math.Cos(Inclination.Radians)
            Return Distance.FromKilometers(kilometers)
        End Function

        'Y = R * (Sin(N) * Cos(TA+w) + Cos(N) * Sin(TA+w)) * Cos(i))
        Private Function CalculateY(distance As Distance, trueAnomaly As Angle) As Distance
            Dim kilometers = distance.Kilometers * ((Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Cos(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) + (Math.Cos(LongitudeOfAscendingNode.Radians)) * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) * Math.Cos(Inclination.Radians)
            Return Distance.FromKilometers(kilometers)
        End Function

        'Z = R * Sin(TA+w) * Sin(i)
        Private Function CalculateZ(distance As Distance, trueAnomaly As Angle) As Distance
            Dim kilometers = distance.Kilometers * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians) * Math.Sin(Inclination.Radians)
            Return Distance.FromKilometers(kilometers)
        End Function

        Private Function GenerateOrbitPath() As List(Of Point3D)
            Dim result As New List(Of Point3D)
            Dim stepSize = CalculateOrbitPathStep()

            For i = 0 To Period.Days Step stepSize
                result.Add(CalculatePosition(i))
            Next

            If Eccentricity < 1 Then
                ' Add the first point at the end to complete the ellipse
                result.Add(result(0))
            End If

            Return result
        End Function

        Private Function CalculateOrbitPathStep() As Double
            ' speed around periapsis can be very high and so we need greater plot points to maintain the illusion of a continuous ellipse in certain circumstances

            ' lower the eccentricity, higher the step size
            ' higher the semi major axis, higher the step size

            Return 1
        End Function

    End Class
End Namespace