using System;

namespace Core.Classes
{
    public class Distance : IEquatable<Distance>
    {
        private const double KilometersInAstronomicalUnit = 149597870.691;

        private Distance(double kilometers)
        {
            Kilometers = kilometers;
        }

        public double AstronomicalUnits => Kilometers / KilometersInAstronomicalUnit;

        public static Distance FromAstronomicalUnits(double astronomicalUnits)
        {
            return new Distance(astronomicalUnits * KilometersInAstronomicalUnit);
        }

        public double Kilometers { get; }

        public static Distance FromKilometers(double kilometers)
        {
            return new Distance(kilometers);
        }

        public virtual bool Equals(Distance other)
        {
            return other != null && Kilometers.Equals(other.Kilometers);
        }
    }
}
