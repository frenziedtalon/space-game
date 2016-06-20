Imports Core.Classes

Public Class OrbitHelper

    Public Function CalculateTotalMass(massOfPrimary As Mass, massOfSatellite As Mass) As Mass
        Return Mass.FromKilograms(massOfPrimary.Kilograms + massOfSatellite.Kilograms)
    End Function

End Class
