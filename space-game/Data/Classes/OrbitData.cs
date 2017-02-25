using Core.Classes;

namespace Data.Classes
{
    public class OrbitData
    {
        public Distance SemiMajorAxis { get; set; }
        public double Eccentricity { get; set; }
        public Angle Inclination { get; set; }
        public Angle ArgumentOfPeriapsis { get; set; }
        public Angle LongitudeOfAscendingNode { get; set; }
        public Angle MeanAnomalyZero { get; set; }

        /// <summary>
        /// Distances and angles must be in the correct units, kilometers and degrees
        /// </summary>
        public OrbitData(Distance semiMajorAxis, double eccentricity, Angle inclination, Angle argumentOfPeriapsis, Angle longitudeOfAscendingNode, Angle meanAnomalyZero)
        {
            this.SemiMajorAxis = semiMajorAxis;
            this.Eccentricity = eccentricity;
            this.Inclination = inclination;
            this.ArgumentOfPeriapsis = argumentOfPeriapsis;
            this.LongitudeOfAscendingNode = longitudeOfAscendingNode;
            this.MeanAnomalyZero = meanAnomalyZero;
        }

        private OrbitData()
        {
            // For Mapster
        }
    }
}