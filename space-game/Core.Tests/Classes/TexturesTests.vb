Imports Core.Classes
Imports Core.Tests.Data
Imports NUnit.Framework

Namespace Classes
    Public Class TexturesTests

         <TestCaseSource(GetType(TexturesTestsData), NameOf(TexturesTestsData.Equals_WhenComparing_ReturnsExpected_Data))>
        Public Sub Equals_WhenComparing_ReturnsExpected(this As Textures, that As Textures, expected As Boolean)
            Dim result = this.Equals(that)

            Assert.AreEqual(expected, result)
        End Sub
    End Class
End NameSpace