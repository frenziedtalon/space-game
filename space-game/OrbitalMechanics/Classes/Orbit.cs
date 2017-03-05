using Core.Classes;
using Data.Classes;
using Newtonsoft.Json;
using OrbitalMechanics.Helpers;
using OrbitalMechanics.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using TurnTracker;

namespace OrbitalMechanics.Classes
{
    public class Orbit : IOrbit
    {
        private readonly ITurnTracker _turnTracker;

        public Orbit(ITurnTracker turnTracker, Distance semiMajorAxis, double eccentricity, Angle inclination, Angle argumentOfPeriapsis, Angle longitudeOfAscendingNode, Angle meanAnomalyZero, bool shouldDisplayOrbit = false)
        {
            _turnTracker = turnTracker;
            SemiMajorAxis = semiMajorAxis;
            Eccentricity = eccentricity;
            Inclination = inclination;
            ArgumentOfPeriapsis = argumentOfPeriapsis;
            LongitudeOfAscendingNode = longitudeOfAscendingNode;
            MeanAnomalyZero = meanAnomalyZero;
            _shouldDisplayOrbit = shouldDisplayOrbit;
        }

        public Orbit(ITurnTracker turnTracker, OrbitData data, bool shouldDisplayOrbit = false) : this(turnTracker: turnTracker, semiMajorAxis: data.SemiMajorAxis, eccentricity: data.Eccentricity, inclination: data.Inclination, argumentOfPeriapsis: data.ArgumentOfPeriapsis, longitudeOfAscendingNode: data.LongitudeOfAscendingNode, meanAnomalyZero: data.MeanAnomalyZero, shouldDisplayOrbit: shouldDisplayOrbit)
        {
        }

        public void Update()
        {
            _recalculatePosition = true;
        }

        private TimeSpan _period = TimeSpan.Zero;

        [JsonIgnore]
        public TimeSpan Period
        {
            get
            {
                if (_period == TimeSpan.Zero)
                {
                    OrbitHelper orbitHelper = new OrbitHelper();
                    _period = orbitHelper.CalculatePeriod(orbitHelper.CalculateTotalMass(MassOfPrimary, MassOfSatellite), SemiMajorAxis);
                }
                return _period;
            }
        }

        public double PeriodDays => Period.TotalDays;

        public Angle LongitudeOfAscendingNode { get; }

        public Angle Inclination { get; }

        public Angle ArgumentOfPeriapsis { get; }

        /// <summary>
        /// One half of the major axis, represents the mean distance from the primary 
        /// </summary>
        public Distance SemiMajorAxis { get; }

        public double Eccentricity { get; }

        private Distance _periapsisDistance;
        [JsonIgnore]
        public Distance PeriapsisDistance
        {
            get
            {
                if (_periapsisDistance == null)
                {
                    _periapsisDistance = Distance.FromAstronomicalUnits(SemiMajorAxis.AstronomicalUnits * (1 - Eccentricity));
                }
                return _periapsisDistance;
            }
        }

        private Distance _apapsisDistance;
        [JsonIgnore]
        public Distance ApapsisDistance
        {
            get
            {
                if (_apapsisDistance == null)
                {
                    _apapsisDistance = Distance.FromAstronomicalUnits(SemiMajorAxis.AstronomicalUnits * (1 + Eccentricity));
                }
                return _apapsisDistance;
            }
        }

        public Angle MeanAnomalyZero { get; }

        private double TurnsPerOrbit()
        {
            return Period.Ticks / _turnTracker.TurnLength.Ticks;
        }

        private Angle LongitudeOfPeriapsis()
        {
            return Angle.FromRadians(LongitudeOfAscendingNode.Radians + ArgumentOfPeriapsis.Radians);
        }

        private Angle MeanLongitude(Angle meanAnomaly)
        {
            return Angle.FromRadians(meanAnomaly.Radians + LongitudeOfPeriapsis().Radians);
        }

        // TODO: If parent / satellite mass can ever be changed in the future this needs to change
        public Mass MassOfPrimary { get; set; }

        public Mass MassOfSatellite { get; set; }

        private Angle _meanAngularMotion;
        /// <summary>
        /// Radians moved in the orbit per day
        /// </summary>
        private Angle MeanAngularMotion => _meanAngularMotion ?? (_meanAngularMotion = Angle.FromRadians((2*Math.PI)/Period.TotalDays));

        bool _recalculatePosition = true;
        Point3D _position;
        public Point3D Position
        {
            get
            {
                if (_recalculatePosition)
                {
                    _position = CalculatePosition(_turnTracker.TimeSinceStart.TotalDays);
                    _recalculatePosition = false;
                }
                return _position;
            }
        }

        private bool _shouldDisplayOrbit;
        private bool ShouldDisplayOrbit => _shouldDisplayOrbit;

        private List<Point3D> _orbitPath;
        [JsonIgnore]
        public List<Point3D> OrbitPath
        {
            get
            {
                if (_orbitPath == null)
                {
                    _orbitPath = ShouldDisplayOrbit ? GenerateOrbitPath() : new List<Point3D>();
                }
                return _orbitPath;
            }
        }

        public void StartDisplayingOrbitPath()
        {
            _shouldDisplayOrbit = true;
            _orbitPath = null;
        }

        public void StopDisplayingOrbitPath()
        {
            _shouldDisplayOrbit = false;
            _orbitPath = null;
        }

        private Point3D CalculatePosition(double days)
        {
            if (Eccentricity > 1)
            {
                return CalculatePositionForHyperbolicOrbit(days);
            }
            if (double.Equals(Eccentricity, 1.0))
            {
                return CalculatePositionForParabolicOrbit(days);
            }
            return CalculatePositionForEllipticalOrbit(days);
        }

        private Point3D CalculatePositionForEllipticalOrbit(double days)
        {
            Angle meanAnomaly = CalculateMeanAnomaly(days);
            Angle eccentricAnomaly = CalculateEccentricAnomaly(meanAnomaly);
            Angle trueAnomaly = CalculateTrueAnomaly(eccentricAnomaly);
            Distance distance = CalculateDistance(trueAnomaly);

            Distance x = CalculateX(distance, trueAnomaly);
            Distance y = CalculateY(distance, trueAnomaly);
            Distance z = CalculateZ(distance, trueAnomaly);

            return new Point3D(x.Kilometers, y.Kilometers, z.Kilometers);
        }

        private Point3D CalculatePositionForParabolicOrbit(double days)
        {
            throw new NotImplementedException();
        }

        private Point3D CalculatePositionForHyperbolicOrbit(double days)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Angle of average orbital motion.
        /// </summary>
        /// <remarks>0 at periapsis. Increases uniformly with time.</remarks>
        private Angle CalculateMeanAnomaly(double days)
        {
            double radians = MeanAnomalyZero.Radians + (MeanAngularMotion.Radians * days);
            return Angle.FromRadians(radians);
        }

        private Angle CalculateEccentricAnomaly(Angle meanAnomaly)
        {

            double threshold = Angle.FromDegrees(0.001).Radians;
            const int maxIterations = 50;
            int iterations = 0;

            // initial guess
            double En = Eccentricity > 0.8 ? Math.PI : meanAnomaly.Radians;
            double En1 = 0.0;

            while (!(iterations >= maxIterations))
            {
                // E1 = E0 - ( E0 - e * sin(E0) - M ) / ( 1 - e * cos(E0) )
                En1 = En - ((En - meanAnomaly.Radians - (Eccentricity * Math.Sin(En))) / (1 - (Eccentricity * Math.Cos(En))));

                if (En1 - En < threshold)
                {
                    break;
                }

                iterations += 1;
                En = En1;
            }

            return Angle.FromRadians(En1);
        }

        /// <summary>
        /// Angle between planet and perihelion.
        /// </summary>
        /// <remarks>0 at perihelion. Unit is radians. Changes most rapidly at perihelion.</remarks>
        private Angle CalculateTrueAnomaly(Angle eccentricAnomaly)
        {
            double x = Math.Sqrt(1 - Eccentricity) * Math.Cos(eccentricAnomaly.Radians / 2);
            double y = Math.Sqrt(1 + Eccentricity) * Math.Sin(eccentricAnomaly.Radians / 2);
            double radians = 2 * Math.Atan2(y, x);

            return Angle.FromRadians(radians);
        }

        private Distance CalculateDistance(Angle trueAnomaly)
        {
            double aus = (SemiMajorAxis.AstronomicalUnits * (1 - (Math.Pow(Eccentricity, 2)))) / (1 + (Eccentricity * (Math.Cos(trueAnomaly.Radians))));
            return Distance.FromAstronomicalUnits(aus);
        }

        //R,X,Y,Z-Heliocentric Distances
        //TA - True Anomaly
        //N - Longitude of the Ascending Node
        //w - Argument of the Perihelion
        //i - inclination

        //X = R * (Cos(N) * Cos(TA + w) - Sin(N) * Sin(TA+w)*Cos(i)
        private Distance CalculateX(Distance distance, Angle trueAnomaly)
        {
            double kilometers = distance.Kilometers * ((Math.Cos(LongitudeOfAscendingNode.Radians) * Math.Cos(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) - (Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians))) * Math.Cos(Inclination.Radians);
            return Distance.FromKilometers(kilometers);
        }

        //Y = R * (Sin(N) * Cos(TA+w) + Cos(N) * Sin(TA+w)) * Cos(i))
        private Distance CalculateY(Distance distance, Angle trueAnomaly)
        {
            double kilometers = distance.Kilometers * ((Math.Sin(LongitudeOfAscendingNode.Radians) * Math.Cos(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) + (Math.Cos(LongitudeOfAscendingNode.Radians)) * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians)) * Math.Cos(Inclination.Radians);
            return Distance.FromKilometers(kilometers);
        }

        //Z = R * Sin(TA+w) * Sin(i)
        private Distance CalculateZ(Distance distance, Angle trueAnomaly)
        {
            double kilometers = distance.Kilometers * Math.Sin(trueAnomaly.Radians + ArgumentOfPeriapsis.Radians) * Math.Sin(Inclination.Radians);
            return Distance.FromKilometers(kilometers);
        }

        private List<Point3D> GenerateOrbitPath()
        {
            List<Point3D> result = new List<Point3D>();
            double stepSize = CalculateOrbitPathStep();

            for (double i = 0; i <= Period.Days; i += stepSize)
            {
                result.Add(CalculatePosition(i));
            }

            if (Eccentricity < 1)
            {
                // Add the first point at the end to complete the ellipse
                result.Add(result[0]);
            }

            return result;
        }

        private double CalculateOrbitPathStep()
        {
            // speed around periapsis can be very high and so we need greater plot points to maintain the illusion of a continuous ellipse in certain circumstances

            // lower the eccentricity, higher the step size
            // higher the semi major axis, higher the step size

            return 1;
        }
    }
}
