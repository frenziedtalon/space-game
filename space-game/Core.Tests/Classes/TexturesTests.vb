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

        <TestCaseSource(GetType(TexturesTestsData), NameOf(TexturesTestsData.GetHighestAvailableResolution_WhenCalled_ReturnsExpected_Data))>
        Public Sub GetHighestAvailableResolution_WhenCalled_ReturnsExpected(textures As Textures, expected As String)
            Dim result = textures.GetHighestAvailableResolution()

            Assert.AreEqual(expected, result)
        End Sub
    End Class
End Namespace