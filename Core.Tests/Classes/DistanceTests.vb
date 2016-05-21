Imports Core.Classes
Imports NUnit.Framework

Namespace Classes
    <TestFixture>
    Public Class DistanceTests

        <TestCase(0.0)>
        <TestCase(56.0)>
        <TestCase(0.00000001)>
        <TestCase(25444588655.0)>
        Public Sub FromKilometers_WhenCalled_StoresCorrectValue(kilometers As Double)

            Dim d = Distance.FromKilometers(kilometers)

            Dim result = d.Kilometers

            Assert.AreEqual(kilometers, result)
        End Sub

        <TestCase(0.0)>
        <TestCase(56.0)>
        <TestCase(0.00000001)>
        <TestCase(300.0)>
        Public Sub FromAstronomicalUnits_WhenCalled_StoresCorrectValue(aus As Double)

            Dim d = Distance.FromAstronomicalUnits(aus)

            Dim result = d.AstronomicalUnits

            Assert.AreEqual(aus, result)
        End Sub

        <Test()>
        Public Sub Distance_WhenLessThanZero_ThrowsArgumentOutOfRangeException()

            Dim d As Double = -10000
            Dim expected = GetType(ArgumentOutOfRangeException)

            Dim ex = Assert.Catch(Of Exception)(Function() Distance.FromKilometers(d))

            Assert.IsInstanceOf(expected, ex)
        End Sub


    End Class
End Namespace