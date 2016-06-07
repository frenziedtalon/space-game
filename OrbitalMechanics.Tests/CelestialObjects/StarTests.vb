Imports Core.Classes
Imports Entities
Imports NSubstitute
Imports NUnit.Framework
Imports OrbitalMechanics.CelestialObjects

<TestFixture>
Public Class StarTests

    <TestCase(33000, StarClassification.O)>
    <TestCase(10500, StarClassification.B)>
    <TestCase(7500, StarClassification.A)>
    <TestCase(6000, StarClassification.F)>
    <TestCase(5500, StarClassification.G)>
    <TestCase(4000, StarClassification.K)>
    <TestCase(0, StarClassification.M)>
    Public Sub Classification_WhenCalled_ReturnsCorrectValue(temperature As Integer, expected As StarClassification)

        Dim entityManager As IEntityManager = Substitute.For(Of IEntityManager)

        Dim star As New Star("test", 1, temperature, "none", Distance.FromKilometers(1), entityManager)

        Assert.AreEqual(expected, star.Classification)

    End Sub

End Class
