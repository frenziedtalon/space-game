Imports NUnit.Framework
Imports Core.Classes

Namespace Classes
    <TestFixture>
    Public Class RangeTests
        <Test>
        Public Sub ctor_WithInvalidDefaultValues_ThrowsArgumentOutOfRangeException()
            Const defaultLowerBound = 10.4544
            Const defaultUpperBound = -50.4534

            Dim expectedExceptionType As Type = GetType(ArgumentOutOfRangeException)

            Dim ex = Assert.Catch(Of Exception)(Function() New Range(defaultLowerBound, defaultUpperBound))

            Assert.AreEqual(ex.GetType(), expectedExceptionType)
        End Sub

        <Test>
        Public Sub LowerBound_WithDefaultValue_ReturnsDefaultValue()
            Const defaultLowerBound = -10.3434
            Const defaultUpperBound = 50.5465
            Dim r = New Range(defaultLowerBound, defaultUpperBound)

            Dim result = r.LowerBound

            Assert.AreEqual(defaultLowerBound, result)
        End Sub

        <Test>
        Public Sub UpperBound_WithDefaultValue_ReturnsDefaultValue()
            Const defaultLowerBound = -10.3434
            Const defaultUpperBound = 50.7676
            Dim r = New Range(defaultLowerBound, defaultUpperBound)

            Dim result = r.UpperBound

            Assert.AreEqual(defaultUpperBound, result)
        End Sub

        <Test>
        Public Sub LowerBound_WithValues_ReturnsCorrectValue()
            Const defaultLowerBound = -10.7767
            Const defaultUpperBound = 50.3434
            Dim r = New Range(defaultLowerBound, defaultUpperBound)

            Const expectedValue = -20.2323
            r.AddValue(50.767)
            r.AddValue(-12.5656)
            r.AddValue(17.6565)
            r.AddValue(expectedValue)
            r.AddValue(0)
            r.AddValue(13)

            Dim result = r.LowerBound

            Assert.AreEqual(expectedValue, result)
        End Sub

        <Test>
        Public Sub UpperBound_WithValues_ReturnsCorrectValue()
            Const defaultLowerBound = -10
            Const defaultUpperBound = 50.656
            Dim r = New Range(defaultLowerBound, defaultUpperBound)

            Const expectedValue = 200.878
            r.AddValue(50)
            r.AddValue(-12)
            r.AddValue(17.878)
            r.AddValue(expectedValue)
            r.AddValue(0)
            r.AddValue(13)

            Dim result = r.UpperBound

            Assert.AreEqual(expectedValue, result)
        End Sub
    End Class
End Namespace