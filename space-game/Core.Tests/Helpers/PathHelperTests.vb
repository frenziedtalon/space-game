Imports Core.Helpers
Imports NUnit.Framework

Namespace Helpers
    <TestFixture>
    Public Class PathHelperTests

        <TestCase("low/path/", "/Low.jpg", "low/path/Low.jpg")>
        <TestCase("low/path/", "Low.jpg", "low/path/Low.jpg")>
        <TestCase("low/path//", "Low.jpg", "low/path/Low.jpg")>
        Public Sub PathCombine_WithValidInput_ReturnsExpected(path1 As String, path2 As String, expected As String)
            Dim result = PathHelper.SitePathCombine(path1, path2)

            Assert.AreEqual(expected, result)
        End Sub

        <TestCase(Nothing, Nothing)>
        <TestCase(Nothing, "")>
        <TestCase("", Nothing)>
        <TestCase("", "12345")>
        <TestCase("12345", "")>
        <TestCase(Nothing, "12345")>
        <TestCase("12345", Nothing)>
        Public Sub PathCombine_WithInvalidInput_ThrowsArgumentNullException(path1 As String, path2 As String)
            Dim expected = GetType(ArgumentNullException)

            Dim ex = Assert.Catch(Of ArgumentNullException)(Function() PathHelper.SitePathCombine(path1, path2))
            
            Assert.AreEqual(expected, ex.GetType())
        End Sub
    End Class
End Namespace