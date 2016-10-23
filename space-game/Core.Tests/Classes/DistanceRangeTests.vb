Imports NUnit.Framework
Imports Core.Classes

Namespace Classes
    <TestFixture>
    Public Class DistanceRangeTests

        <Test>
        Public Sub ctor_WithInvalidDefaultValues_ThrowsArgumentOutOfRangeException()
            Dim defaultLowerBound = Distance.FromAstronomicalUnits(10.4544)
            Dim defaultUpperBound = Distance.FromAstronomicalUnits(-50.4534)

            Dim expectedExceptionType As Type = GetType(ArgumentOutOfRangeException)

            Dim ex = Assert.Catch(Of Exception)(Function() New DistanceRange(defaultLowerBound, defaultUpperBound))

            Assert.AreEqual(ex.GetType(), expectedExceptionType)
        End Sub

        <Test>
        Public Sub LowerBound_WithDefaultValue_ReturnsDefaultValue()
            Dim defaultLowerBound = Distance.FromAstronomicalUnits(-10.3434)
            Dim defaultUpperBound = Distance.FromAstronomicalUnits(50.5465)
            Dim r = New DistanceRange(defaultLowerBound, defaultUpperBound)

            Dim result = r.LowerBound

            Assert.AreEqual(defaultLowerBound.Kilometers, result.Kilometers)
        End Sub

        <Test>
        Public Sub UpperBound_WithDefaultValue_ReturnsDefaultValue()
            Dim defaultLowerBound = Distance.FromAstronomicalUnits(-10.3434)
            Dim defaultUpperBound = Distance.FromAstronomicalUnits(50.7676)
            Dim r = New DistanceRange(defaultLowerBound, defaultUpperBound)

            Dim result = r.UpperBound

            Assert.AreEqual(defaultUpperBound.Kilometers, result.Kilometers)
        End Sub

        <Test>
        Public Sub LowerBound_WithValues_ReturnsCorrectValue()
            Dim defaultLowerBound = Distance.FromAstronomicalUnits(-10.7767)
            Dim defaultUpperBound = Distance.FromAstronomicalUnits(50.3434)
            Dim r = New DistanceRange(defaultLowerBound, defaultUpperBound)

            Dim expectedValue = Distance.FromAstronomicalUnits(-20.2323)
            r.AddValue(Distance.FromAstronomicalUnits(50.767))
            r.AddValue(Distance.FromAstronomicalUnits(-12.5656))
            r.AddValue(Distance.FromAstronomicalUnits(17.6565))
            r.AddValue(expectedValue)
            r.AddValue(Distance.FromAstronomicalUnits(0))
            r.AddValue(Distance.FromAstronomicalUnits(13))

            Dim result = r.LowerBound

            Assert.AreEqual(expectedValue.Kilometers, result.Kilometers)
        End Sub

        <Test>
        Public Sub UpperBound_WithValues_ReturnsCorrectValue()
            Dim defaultLowerBound = Distance.FromAstronomicalUnits(-10)
            Dim defaultUpperBound = Distance.FromAstronomicalUnits(50.656)
            Dim r = New DistanceRange(defaultLowerBound, defaultUpperBound)

            Dim expectedValue = Distance.FromAstronomicalUnits(200.878)
            r.AddValue(Distance.FromAstronomicalUnits(50))
            r.AddValue(Distance.FromAstronomicalUnits(-12))
            r.AddValue(Distance.FromAstronomicalUnits(17.878))
            r.AddValue(expectedValue)
            r.AddValue(Distance.FromAstronomicalUnits(0))
            r.AddValue(Distance.FromAstronomicalUnits(13))

            Dim result = r.UpperBound

            Assert.AreEqual(expectedValue.Kilometers, result.Kilometers)
        End Sub

    End Class
End Namespace