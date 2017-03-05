using Core.Classes;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace OrbitalMechanics.Interfaces
{
    public interface IOrbit
    {
        Point3D Position { get; }
        TimeSpan Period { get; }
        void Update();
        Angle LongitudeOfAscendingNode { get; }
        Angle Inclination { get; }
        Angle ArgumentOfPeriapsis { get; }
        /// <summary>
        /// Mean distance to the primary.
        /// </summary>
        Distance SemiMajorAxis { get; }
        /// <summary>
        /// 0=circle, 0-1=ellipse, 1=parabola.
        /// </summary>
        /// <remarks>Units=AU</remarks>
        double Eccentricity { get; }
        /// <summary>
        /// Closest distance to the primary.
        /// </summary>
        /// <remarks>Units=AU</remarks>
        Distance PeriapsisDistance { get; }
        /// <summary>
        /// Furthest distance from the primary.
        /// </summary>
        /// <remarks>Units=AU</remarks>
        Distance ApapsisDistance { get; }
        /// <summary>
        /// Represents the angle at which the object lies within its orbit at zero time
        /// </summary>
        Angle MeanAnomalyZero { get; }
        /// <summary>
        /// List of points representing the full orbit path
        /// </summary>
        List<Point3D> OrbitPath { get; }
        Mass MassOfPrimary { get; set; }
        Mass MassOfSatellite { get; set; }
    }
}
