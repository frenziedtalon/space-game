Imports Core.Extensions
Imports NUnit.Framework

Namespace Extensions
    <TestFixture>
    Public Class GuidExtensionsTests

        <Test()>
        Public Sub IsEmpty_WhenEmpty_ReturnsTrue()
            Dim test As Guid

            Dim result = test.IsEmpty()

            Assert.AreEqual(True, result)
        End Sub

        <Test()>
        Public Sub IsEmpty_WhenNotEmpty_ReturnsFalse()
            Dim test = Guid.NewGuid()

            Dim result = test.IsEmpty()

            Assert.AreEqual(False, result)
        End Sub

    End Class
End Namespace