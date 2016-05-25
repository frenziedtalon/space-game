Imports System.Windows.Media.Media3D
Imports Core.Classes
Imports OrbitalMechanics.Classes


Public Interface IOrbit
    ReadOnly Property Position As Point3D
    ReadOnly Property Period As TimeSpan
    Sub Update()
    ReadOnly Property LongitudeOfAscendingNode As Angle
    ReadOnly Property Inclination As Angle
    ReadOnly Property ArgumentOfPeriapsis As Angle
    ''' <summary>
    ''' Mean distance to the primary.
    ''' </summary>
    ReadOnly Property SemiMajorAxis As Distance
    ''' <summary>
    ''' 0=circle, 0-1=ellipse, 1=parabola.
    ''' </summary>
    ''' <remarks>Units=AU</remarks>
    ReadOnly Property Eccentricity As Double
    ''' <summary>
    ''' Closest distance to the primary.
    ''' </summary>
    ''' <remarks>Units=AU</remarks>
    ReadOnly Property PeriapsisDistance As Distance
    ''' <summary>
    ''' Furthest distance from the primary.
    ''' </summary>
    ''' <remarks>Units=AU</remarks>
    ReadOnly Property ApapsisDistance As Distance
    ''' <summary>
    ''' Represents the angle at which the object lies within its orbit at zero time
    ''' </summary>
    ReadOnly Property MeanAnomalyZero As Angle
    ''' <summary>
    ''' List of points representing the full orbit path
    ''' </summary>
    ReadOnly Property OrbitPath As List(Of Point3D)
End Interface