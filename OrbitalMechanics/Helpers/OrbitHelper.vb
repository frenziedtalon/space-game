Imports Core.Classes

Public Class OrbitHelper

    Public Function CalculateTotalMass(massOfPrimary As Mass, massOfSatellite As Mass) As Mass
        Return Mass.FromKilograms(massOfPrimary.Kilograms + massOfSatellite.Kilograms)
    End Function

    Public Function CalculatePeriod(totalMass As Mass, semiMajorAxis As Distance) As TimeSpan
        Dim daysSquared = ((4 * Math.PI * Math.PI * Math.Pow(semiMajorAxis.AstronomicalUnits, 3)) / (Math.Pow(Constants.GaussianGravitationalConstant, 2) * totalMass.SolarMasses))

        Return TimeSpan.FromDays(Math.Pow(daysSquared, 0.5))
    End Function

End Class
