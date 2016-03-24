Imports NSubstitute
Imports NUnit.Framework

<TestFixture>
Public Class BaseGameEntityTests

    <Test()>
    Public Sub ctor_WhenArgumentIsNull_ThrowsArgumentNullException()
        Dim entityManager As IEntityManager = Nothing

        Dim ex = Assert.Catch(Of Exception)(Function() Substitute.For(Of BaseGameEntity)(entityManager))

        Dim expectedType As Type = GetType(ArgumentNullException)

        Assert.AreEqual(ex.InnerException.GetType(), expectedType)

    End Sub

End Class
