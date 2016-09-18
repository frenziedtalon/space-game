Imports Core.Classes
Imports Core.Tests.Data
Imports NUnit.Framework

Namespace Classes
    <TestFixture>
    Public Class AngleTests

        <TestCase(0)>
        <TestCase(90.102)>
        <TestCase(180)>
        <TestCase(270.8782737)>
        Public Sub FromDegrees_WhenWithinLimits_ReturnsSameValue(input As Double)

            Dim a = Angle.FromDegrees(input)

            Dim result = a.Degrees

            Assert.AreEqual(input, result)
        End Sub

        <TestCase(-1000, 80)>
        <TestCase(-30, 330)>
        <TestCase(390, 30)>
        <TestCase(4678, 358)>
        <TestCase(360, 0)>
        Public Sub FromDegrees_WhenOutsideLimits_ReturnsCorrectedValue(input As Double, expected As Double)

            Dim a = Angle.FromDegrees(input)

            Dim result = a.Degrees

            Assert.AreEqual(expected, result)
        End Sub

        <TestCase(0)>
        <TestCase(Math.PI / 400)>
        <TestCase(Math.PI / 4)>
        <TestCase(Math.PI / 2)>
        <TestCase(Math.PI)>
        <TestCase(3 * (Math.PI / 2))>
        Public Sub FromRadians_WhenWithinLimits_ReturnsSameValue(input As Double)

            Dim a = Angle.FromRadians(input)

            Dim result = a.Radians

            Dim roundedExpected = Math.Round(input, a.DecimalPlaces, MidpointRounding.AwayFromZero)

            Assert.AreEqual(roundedExpected, result)
        End Sub

        <TestCase(-Math.PI, Math.PI)>
        <TestCase(3 * Math.PI, Math.PI)>
        <TestCase(15 * (Math.PI / 4), 7 * (Math.PI / 4))>
        Public Sub FromRadians_WhenOutsideLimits_ReturnsCorrectedValue(input As Double, expected As Double)

            Dim a = Angle.FromRadians(input)

            Dim result = a.Radians

            Dim roundedExpected = Math.Round(expected, a.DecimalPlaces, MidpointRounding.AwayFromZero)

            Assert.AreEqual(roundedExpected, result)
        End Sub

        <TestCaseSource(GetType(AngleTestsData), NameOf(AngleTestsData.Equals_WhenComparing_ReturnsExpected_Data))>
        Public Sub Equals_WhenComparing_ReturnsExpected(this As Angle, that As Angle, expected As Boolean)
            Dim result = this.Equals(that)

            Assert.AreEqual(expected, result)
        End Sub

    End Class
End Namespace