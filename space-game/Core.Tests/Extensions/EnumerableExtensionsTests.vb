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

        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_High))>
        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Medium))>
        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Low))>
        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Mix))>
        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.GetHighestAvailableResolutionForEachType_Empty))>
        Public Sub GetHighestAvailableResolutionForEachType_WhenGivenAList_ReturnsExpected(textures As List(Of Texture), expected As List(Of Texture))
            Dim result = textures.GetHighestAvailableResolutionForEachType()

            Assert.AreEqual(expected, result)
        End Sub

        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.IsEquivalent_WhenGivenAList_ReturnsExpected_NullAndEmpty))>
        <TestCaseSource(GetType(EnumerableExtensionsTestsData), NameOf(EnumerableExtensionsTestsData.IsEquivalent_WhenGivenAList_ReturnsExpected_Contents))>
        Public Sub IsEquivalent_WhenGivenAList_ReturnsExpected(one As List(Of Texture), two As List(Of Texture), expected As Boolean)
            Dim result = one.IsEquivalent(two)

            Assert.AreEqual(expected, result)
        End Sub
    End Class
End Namespace