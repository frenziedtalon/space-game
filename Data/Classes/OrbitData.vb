Imports Core.Classes

Namespace Classes
    Public Class OrbitData
        Public Property SemiMajorAxis As Distance
        Public Property Eccentricity As Double
        Public Property Inclination As Angle
        Public Property ArgumentOfPeriapsis As Angle
        Public Property LongitudeOfAscendingNode As Angle
        Public Property MeanAnomalyZero As Angle

        ''' <summary>
        ''' Distances and angles must be in the correct units, astronomical units and radians
        ''' </summary>
        ''' <param name="semiMajorAxis">astronomical units</param>
        ''' <param name="eccentricity">double</param>
        ''' <param name="inclination">radians</param>
        ''' <param name="argumentOfPeriapsis">radians</param>
        ''' <param name="longitudeOfAscendingNode">radians</param>
        ''' <param name="meanAnomalyZero">radians</param>
        Public Sub New(semiMajorAxis As Distance,
                        eccentricity As Double,
                        inclination As Angle,
                        argumentOfPeriapsis As Angle,
                        longitudeOfAscendingNode As Angle,
                        meanAnomalyZero As Angle)
            Me.SemiMajorAxis = semiMajorAxis
            Me.Eccentricity = eccentricity
            Me.Inclination = inclination
            Me.ArgumentOfPeriapsis = argumentOfPeriapsis
            Me.LongitudeOfAscendingNode = longitudeOfAscendingNode
            Me.MeanAnomalyZero = meanAnomalyZero
        End Sub

    End Class
End Namespace