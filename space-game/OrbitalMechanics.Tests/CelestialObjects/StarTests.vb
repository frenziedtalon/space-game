Imports Core.Classes
Imports Core.Enums
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

        Dim textures = New List(Of Texture) From {
            New Texture() With {.QualityEnum = TextureQuality.Low,
                                            .TypeEnum = TextureType.Diffuse,
                                            .Path = "low texture path"}
            }


        Dim star As New Star("test", Mass.FromSolarMasses(1), temperature, textures, Distance.FromKilometers(1), entityManager)

        Assert.AreEqual(expected, star.Classification)

    End Sub

End Class
