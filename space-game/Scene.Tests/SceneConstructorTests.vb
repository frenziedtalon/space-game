Imports Entities
Imports NSubstitute
Imports NUnit.Framework
Imports OrbitalMechanics.CelestialObjects
Imports TurnTracker

<TestFixture>
Public Class SceneConstructorTests
    <Test()>
    Public Sub SolSystem_WhenCalledWithInMemoryData_ReturnsSystem()
        Dim entityManager = Substitute.For(Of IEntityManager)()
        Dim turnTracker = Substitute.For(Of ITurnTracker)()
        Dim dataProvider = New Data.InMemoryData.DataProvider

        Dim sceneConstructor = New SceneConstructor(entityManager, turnTracker, dataProvider)

        Dim result = sceneConstructor.SolSystem()

        Assert.AreNotEqual(Nothing, result)
        Assert.AreEqual(1, result.Count)
        Assert.AreEqual(9, CType(result(0), BaseCelestialObject).Satellites.Count)
        Assert.AreEqual(1, CType(result(0), BaseCelestialObject).Satellites(2).Satellites.Count)
    End Sub
End Class
