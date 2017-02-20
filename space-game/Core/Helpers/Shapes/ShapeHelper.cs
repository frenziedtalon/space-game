using System;

namespace Core.Helpers.Shapes
{
    public class ShapeHelper
    {
        public static double VolumeOfASphere(double radius)
        {
            return (4 / 3) * Math.PI * radius * radius;
        }

    }
}
