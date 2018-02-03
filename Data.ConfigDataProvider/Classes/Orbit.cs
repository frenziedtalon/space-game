namespace Data.ConfigDataProvider.Classes
{
    public class Orbit
    {
        public Distance SemiMajorAxis { get; set; }
        public double Eccentricity { get; set; }
        public Angle Inclination { get; set; }
        public Angle ArgumentOfPeriapsis { get; set; }
        public Angle LongitudeOfAscendingNode { get; set; }
        public Angle MeanAnomalyZero { get; set; }
    }
}