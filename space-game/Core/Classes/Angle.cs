using Newtonsoft.Json;
using System;

namespace Core.Classes
{
    /// <summary>
    /// Represents the concept of an angle and allows easily switching between the different unit types.
    /// </summary>
    /// <remarks>
    /// Any angle less than zero or greater than (or equal to) 360 (or 2*Pi) will be corrected until it is within these limits
    /// Using degrees as the internal value. Found that this results in fewer rounding errors than operating with Pi repeatedly.
    /// </remarks>
    public class Angle : IEquatable<Angle>
    {

        private readonly double _degrees;
        private const int DefaultDecimalPlaces = 14;
        private const MidpointRounding RoundingMethod = MidpointRounding.AwayFromZero;

        private Angle(double degrees, int decimalPlaces = DefaultDecimalPlaces)
        {
            _degrees = CorrectDegrees(degrees);

            if (decimalPlaces > DefaultDecimalPlaces)
            {
                decimalPlaces = DefaultDecimalPlaces;
            }

            DecimalPlaces = decimalPlaces;
        }

        public double Radians => Math.Round(ConvertDegreesToRadians(_degrees), DecimalPlaces, RoundingMethod);

        [JsonIgnore()]
        public double Degrees => Math.Round(_degrees, DecimalPlaces, RoundingMethod);

        public static Angle FromRadians(double radians)
        {
            return new Angle(ConvertRadiansToDegrees(radians));
        }

        public static Angle FromDegrees(double degrees)
        {
            return new Angle(degrees);
        }

        private double CorrectDegrees(double angle)
        {
            if (angle < 0)
            {
                while (!(angle > 0))
                {
                    angle += 360;
                }
            }
            else if (angle >= 360)
            {
                while (!(angle < 360))
                {
                    angle -= 360;
                }
            }

            return angle;
        }

        private static double ConvertRadiansToDegrees(double radians)
        {
            return radians / (Math.PI / 180);
        }

        private static double ConvertDegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        /// <summary>
        /// The number of decimal places to which all results will be rounded
        /// </summary>
        [JsonIgnore()]
        public int DecimalPlaces { get; }

        public virtual bool Equals(Angle other)
        {
            return other != null && Degrees.Equals(other.Degrees);
        }
    }
}
