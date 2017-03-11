using System;

namespace Core.Classes
{
    public class Mass : IEquatable<Mass>
    {
        private static readonly double KilogramsInSolarMass = 1.98855 * Math.Pow(10, 30);

        private static readonly double KilogramsInEarthMass = 5973.6 * Math.Pow(10, 21);
        private Mass(double kilograms)
        {
            Kilograms = kilograms;
        }

        public double Kilograms { get; }

        public static Mass FromKilograms(double kilograms)
        {
            return new Mass(kilograms);
        }

        public double SolarMasses => Kilograms / KilogramsInSolarMass;

        public static Mass FromSolarMasses(double solarMasses)
        {
            return new Mass(solarMasses * KilogramsInSolarMass);
        }

        public double EarthMasses => Kilograms / KilogramsInEarthMass;

        public static Mass FromEarthMasses(double earthMasses)
        {
            return new Mass(earthMasses * KilogramsInEarthMass);
        }

        public virtual bool Equals(Mass other)
        {
            return other != null && Kilograms.Equals(other.Kilograms);
        }
    }
}
