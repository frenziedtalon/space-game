Imports Core.Classes
Imports Core.Enums
Imports Core.Extensions
Imports Core.Tests.Data
Imports NUnit.Framework

Namespace Extensions
    <TestFixture>
    Public Class EnumerableExtensionsTests
        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.HasAny_WhenGivenAList_ReturnsExpected_Data))>
        Public Sub HasAny_WhenGivenAList_ReturnsExpected(list As List(Of String), expected As Boolean)
            Dim result = list.HasAny()

            Assert.AreEqual(expected, result)
        End Sub

        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.GetHighestAvailableResolution_WhenGivenAList_ReturnsExpected_Data))>
        Public Sub GetHighestAvailableResolution_WhenGivenAList_ReturnsExpected(textures As List(Of Texture), type As TextureType, expected As String)
            Dim result = textures.GetHighestAvailableResolution(type)

            Assert.AreEqual(expected, result)
        End Sub

        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.GetLowestAvailableResolution_WhenGivenAList_ReturnsExpected_Data))>
        Public Sub GetLowestAvailableResolution_WhenGivenAList_ReturnsExpected(textures As List(Of Texture), type As TextureType, expected As String)
            Dim result = textures.GetLowestAvailableResolution(type)

            Assert.AreEqual(expected, result)
        End Sub
    End Class
End Namespace