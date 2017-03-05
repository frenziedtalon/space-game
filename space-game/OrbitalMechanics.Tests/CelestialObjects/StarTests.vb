Imports Core.Classes
Imports Core.Enums
Imports Entities
Imports NSubstitute
Imports NUnit.Framework
Imports OrbitalMechanics.CelestialObjects

<TestFixture>
Public Class StarTests
    <TestCase(33001, StarClassification.O)>
    <TestCase(33000, StarClassification.O)>
    <TestCase(10501, StarClassification.B)>
    <TestCase(10500, StarClassification.B)>
    <TestCase(7501, StarClassification.A)>
    <TestCase(7500, StarClassification.A)>
    <TestCase(6001, StarClassification.F)>
    <TestCase(6000, StarClassification.F)>
    <TestCase(5501, StarClassification.G)>
    <TestCase(5500, StarClassification.G)>
    <TestCase(4001, StarClassification.K)>
    <TestCase(4000, StarClassification.K)>
    <TestCase(1, StarClassification.M)>
    <TestCase(0, StarClassification.M)>
    Public Sub Classification_WhenCalled_ReturnsCorrectValue(temperature As Integer, expected As StarClassification)

        Dim entityManager As IEntityManager = Substitute.For(Of IEntityManager)

        Dim textures = New List(Of Texture) From {
            New Texture() With {.QualityEnum = TextureQuality.Low,
                                            .TypeEnum = TextureType.Diffuse,
                                            .Path = "low texture path"}
            }


        Dim star As New Star("test", Mass.FromSolarMasses(1), temperature, textures, Distance.FromKilometers(1), entityManager, Nothing)

        Assert.AreEqual(expected, star.Classification)

    End Sub

End Class
