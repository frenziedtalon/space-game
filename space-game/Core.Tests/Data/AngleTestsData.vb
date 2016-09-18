Imports Core.Classes
Imports NUnit.Framework

Namespace Data
    Public Class AngleTestsData

        Public Shared Function Equals_WhenComparing_ReturnsExpected_Data() As List(Of TestCaseData)
            Dim nullAngle As Angle = Nothing
            Dim a = Angle.FromDegrees(123.456)
            Dim copyOfA = a
            Dim notSame = Angle.FromDegrees(12)

            Return New List(Of TestCaseData) From {
                New TestCaseData(a, nullAngle, False),
                New TestCaseData(a, copyOfA, True),
                New TestCaseData(a, notSame, False)
            }
        End Function

    End Class
End Namespace