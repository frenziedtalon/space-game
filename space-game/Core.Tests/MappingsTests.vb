Imports Core.Classes
Imports Mapster
Imports NUnit.Framework

<TestFixture>
Public Class MappingsTests
    <OneTimeSetUp>
    Public Sub SetupMappings()
        Dim mappings = New Mappings
        mappings.Register(TypeAdapterConfig.GlobalSettings)
        TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = True
        TypeAdapterConfig.GlobalSettings.Compile()
    End Sub

    <OneTimeTearDown>
    Public Sub TearDown()
        TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = False
        TypeAdapterConfig.GlobalSettings.Rules.Clear()
        TypeAdapterConfig.GlobalSettings.RuleMap.Clear()
    End Sub

    <Test>
    Public Sub Integer_MapTo_Mass()
        Dim source = 1234567890
        Dim expected = Mass.FromKilograms(source)

        Dim result = source.Adapt(Of Mass)()

        Assert.AreEqual(expected.Kilograms, result.Kilograms)
        Assert.AreEqual(expected.EarthMasses, result.EarthMasses)
        Assert.AreEqual(expected.SolarMasses, result.SolarMasses)
    End Sub

    <Test>
    Public Sub Double_MapTo_Mass()
        Dim source = 1234567890.12
        Dim expected = Mass.FromKilograms(source)

        Dim result = source.Adapt(Of Mass)()

        Assert.AreEqual(expected.Kilograms, result.Kilograms)
        Assert.AreEqual(expected.EarthMasses, result.EarthMasses)
        Assert.AreEqual(expected.SolarMasses, result.SolarMasses)
    End Sub

    <Test>
    Public Sub Integer_MapTo_Distance()
        Dim source = 1234567890
        Dim expected = Distance.FromKilometers(source)

        Dim result = source.Adapt(Of Distance)()

        Assert.AreEqual(expected.Kilometers, result.Kilometers)
        Assert.AreEqual(expected.AstronomicalUnits, result.AstronomicalUnits)
    End Sub

    <Test>
    Public Sub NullableInteger_WithoutValue_MapTo_Distance()
        Dim source As Integer?
        Dim expected = Nothing

        Dim result = source.Adapt(Of Distance)()

        Assert.AreEqual(expected, result)
    End Sub

    <Test>
    Public Sub NullableInteger_WithValue_MapTo_Distance()
        Dim source As Integer? = 45
        Dim expected = Distance.FromKilometers(45)

        Dim result = source.Adapt(Of Distance)()

        Assert.AreEqual(expected.Kilometers, result.Kilometers)
    End Sub

    <Test>
    Public Sub Double_MapTo_Distance()
        Dim source = 1234567890.12
        Dim expected = Distance.FromKilometers(source)

        Dim result = source.Adapt(Of Distance)()

        Assert.AreEqual(expected.Kilometers, result.Kilometers)
        Assert.AreEqual(expected.AstronomicalUnits, result.AstronomicalUnits)
    End Sub

    <Test>
    Public Sub NullableDouble_WithoutValue_MapTo_Distance()
        Dim source As Double?
        Dim expected = Nothing

        Dim result = source.Adapt(Of Distance)()

        Assert.AreEqual(expected, result)
    End Sub

    <Test>
    Public Sub NullableDouble_WithValue_MapTo_Distance()
        Dim source As Double? = 1.11
        Dim expected = Distance.FromKilometers(1.11)

        Dim result = source.Adapt(Of Distance)()

        Assert.AreEqual(expected.Kilometers, result.Kilometers)
    End Sub
End Class
