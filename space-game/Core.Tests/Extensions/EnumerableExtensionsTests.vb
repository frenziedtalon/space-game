Imports Core.Extensions
Imports Core.Tests.Data
Imports NUnit.Framework

Namespace Extensions
    <TestFixture>
    Public Class EnumerableExtensionsTests
        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.HasAny_WhenGivenAList_ReturnsExpected_Data))>
        Public Sub HasAny_WhenGivenAList_ReturnsExpected(list As List(of string), expected As Boolean)
            Dim result = list.HasAny()

            Assert.AreEqual(expected, result)
        End Sub
    End Class
End Namespace