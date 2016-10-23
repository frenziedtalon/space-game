Imports Core.Classes
Imports Core.Tests.Data
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

        <TestCaseSource(GetType(DistanceTestData), NameOf(DistanceTestData.Equals_WhenComparing_ReturnsExpected_Data))>
        Public Sub Equals_WhenComparing_ReturnsExpected(this As Distance, that As Distance, expected As Boolean)
            Dim result = this.Equals(that)

            Assert.AreEqual(expected, result)
        End Sub

    End Class
End Namespace