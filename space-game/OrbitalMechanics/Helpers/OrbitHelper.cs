using Core.Classes;
using System;

namespace OrbitalMechanics.Helpers
{
    public class OrbitHelper
    {
        public Mass CalculateTotalMass(Mass massOfPrimary, Mass massOfSatellite)
        {
            return Mass.FromKilograms(massOfPrimary.Kilograms + massOfSatellite.Kilograms);
        }

        public TimeSpan CalculatePeriod(Mass totalMass, Distance semiMajorAxis)
        {
            double daysSquared = ((4 * Math.PI * Math.PI * Math.Pow(semiMajorAxis.AstronomicalUnits, 3)) / (Math.Pow(Constants.GaussianGravitationalConstant, 2) * totalMass.SolarMasses));

            return TimeSpan.FromDays(Math.Pow(daysSquared, 0.5));
        }
    }
}
