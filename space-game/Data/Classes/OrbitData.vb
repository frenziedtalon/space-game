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
        ''' Distances and angles must be in the correct units, kilometers and degrees
        ''' </summary>
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

        Private Sub New ()
            ' For Mapster
        End Sub
    End Class
End Namespace