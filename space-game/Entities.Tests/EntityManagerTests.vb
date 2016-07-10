Imports NSubstitute
Imports NUnit.Framework

<TestFixture>
Public Class EntityManagerTests

    <TestCase(1, 1)>
    <TestCase(5, 5)>
    <TestCase(20, 20)>
    Public Sub RegisterEntity_WhenCalled_StoresEntity(count As Integer, expected As Integer)

        Dim entityManager = New EntityManager()

        For i = 1 To count
            Dim entity = Substitute.For(Of BaseGameEntity)(entityManager)
        Next

        Assert.AreEqual(expected, entityManager.Count)

    End Sub

    <Test>
    Public Sub RegisterEntity_WhenCalledWithDuplicate_Throws()

        Dim entityManager = New EntityManager()
        Dim expected = GetType(ArgumentException)

        Dim entity = Substitute.For(Of BaseGameEntity)(entityManager)
        Dim ex = Assert.Catch(Of Exception)(Sub() entityManager.RegisterEntity(entity))

        Assert.IsInstanceOf(expected, ex)

    End Sub

    <TestCase(Nothing)>
    Public Sub GetEntityFromId_WhenIdIsNothing_ReturnsNothing(id As Guid)

        Dim entityManager = New EntityManager()

        Dim entity = entityManager.GetEntityFromId(id)

        Assert.AreSame(entity, Nothing)

    End Sub

    <Test>
    Public Sub GetEntityFromId_WhenNoEntityForId_ReturnsNothing()

        Dim entityManager = New EntityManager()
        Dim id As Guid = New Guid()

        Dim entity = entityManager.GetEntityFromId(id)

        Assert.AreSame(entity, Nothing)

    End Sub

    <Test>
    Public Sub GetEntityFromId_WhenIdValid_ReturnsEntity()

        Dim entityManager = New EntityManager()

        Dim entity = Substitute.For(Of BaseGameEntity)(entityManager)
        Dim copyEntity = entityManager.GetEntityFromId(entity.Id)

        Assert.AreSame(entity, copyEntity)

    End Sub


End Class