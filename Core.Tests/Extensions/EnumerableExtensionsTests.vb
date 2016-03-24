Imports Core.Extensions
Imports NUnit.Framework

Namespace Extensions
    <TestFixture>
    Public Class EnumerableExtensionsTests

        <Test()>
        Public Sub HasAny_WhenListIsNull_ReturnsFalse()
            Dim testList As List(Of String) = Nothing

            Dim result = testList.HasAny()

            Assert.AreEqual(False, result)
        End Sub

        <Test()>
        Public Sub HasAny_WhenListIsEmpty_ReturnsFalse()
            Dim testList As New List(Of String)

            Dim result = testList.HasAny()

            Assert.AreEqual(False, result)
        End Sub

        <Test()>
        Public Sub HasAny_WhenListHasMembers_ReturnsTrue()
            Dim testList As New List(Of String)
            testList.Add("a")
            testList.Add("b")

            Dim result = testList.HasAny()

            Assert.AreEqual(True, result)
        End Sub

    End Class
End Namespace