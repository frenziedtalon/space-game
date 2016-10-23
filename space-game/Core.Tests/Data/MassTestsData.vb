Imports Core.Classes
Imports NUnit.Framework

Namespace Data
    Public Class MassTestsData
        Public Shared Function Equals_WhenComparing_ReturnsExpected_Data() As List(Of TestCaseData)
            Dim nullMass As Mass = Nothing
            Dim m = Mass.FromKilograms(12345)
            Dim copyOfM = m
            Dim notSame = Mass.FromKilograms(123456)

            Return New List(Of TestCaseData) From {
                New TestCaseData(m, nullMass, False),
                New TestCaseData(m, copyOfM, True),
                New TestCaseData(m, notSame, False)
            }
        End Function

    End Class
End Namespace