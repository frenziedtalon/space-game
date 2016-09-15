Imports Core.Classes
Imports NUnit.Framework

Namespace Data
    Public Class DistanceTestData

        Public Shared Function Equals_WhenComparing_ReturnsExpected_Data() As List(Of TestCaseData)
            Dim nullDistance As Distance = Nothing
            Dim d = Distance.FromKilometers(12345)
            Dim copyOfD = d
            Dim notSame = Distance.FromKilometers(123456)

            Return New List(Of TestCaseData) From {
                New TestCaseData(d, nullDistance, False),
                New TestCaseData(d, copyOfD, True),
                New TestCaseData(d, notSame, False)
            }
        End Function

    End Class
End Namespace