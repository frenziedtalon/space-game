Imports NUnit.Framework
Imports Core.Extensions.StringExtensions

Namespace Extensions

    <TestFixture>
    Public Class StringExtensionsTests

        <TestCase("One", TestEnum.One)>
        <TestCase("two", TestEnum.Two)>
        <TestCase("three", TestEnum.Three)>
        Public Sub ToEnum_WhenCalledWithValidValue_ReturnsCorrectValue(testValue As String, expectedValue As TestEnum)
            Dim result = testValue.ToEnum(Of TestEnum)

            Assert.AreEqual(expectedValue, result)
        End Sub

        <TestCase("Four")>
        <TestCase("")>
        Public Sub ToEnum_WhenCalledWithInvalidValue_ReturnsNothing(value As String)
            Dim result = value.ToEnum(Of TestEnum)

            Assert.IsNull(result)
        End Sub

    End Class

    Public Enum TestEnum
        One
        Two
        Three
    End Enum
End Namespace