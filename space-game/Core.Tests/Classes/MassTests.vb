Imports Core.Classes
Imports Core.Tests.Data
Imports NUnit.Framework

Namespace Classes
    <TestFixture>
    Public Class MassTests

        <TestCase(0.0)>
        <TestCase(56.0)>
        <TestCase(0.00000001)>
        <TestCase(25444588655.0)>
        Public Sub FromKilograms_WhenCalled_StoresCorrectValue(kilograms As Double)

            Dim m = Mass.FromKilograms(kilograms)

            Dim result = m.Kilograms

            Assert.AreEqual(kilograms, result)
        End Sub

        <TestCase(0.0)>
        <TestCase(56.0)>
        <TestCase(0.00000001)>
        <TestCase(25444588655.0)>
        Public Sub FromSolarMasses_WhenCalled_StoresCorrectValue(solarMasses As Double)
            Dim acceptableDelta = Mass.FromKilograms(1).Kilograms
            Dim m = Mass.FromSolarMasses(solarMasses)

            Dim result = m.SolarMasses

            Assert.AreEqual(solarMasses, result, acceptableDelta)
        End Sub

        <TestCase(0.0)>
        <TestCase(56.0)>
        <TestCase(0.00000001)>
        <TestCase(25444588655.0)>
        Public Sub FromEarthMasses_WhenCalled_StoresCorrectValue(earthMasses As Double)
            Dim acceptableDelta = Mass.FromKilograms(1).Kilograms
            Dim m = Mass.FromEarthMasses(earthMasses)

            Dim result = m.EarthMasses

            Assert.AreEqual(earthMasses, result, acceptableDelta)
        End Sub

        <TestCaseSource(GetType(MassTestsData), NameOf(MassTestsData.Equals_WhenComparing_ReturnsExpected_Data))>
        Public Sub Equals_WhenComparing_ReturnsExpected(this As Mass, that As Mass, expected As Boolean)
            Dim result = this.Equals(that)

            Assert.AreEqual(expected, result)
        End Sub

    End Class
End Namespace