Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SpaceGame.Controllers

Namespace Controllers

    <TestClass()> Public Class SceneApiControllerTest

        <TestMethod()> Public Sub GetSceneObjects()

            'Arrange
            Dim controller As New SceneApiController()

            'Act
            Dim result As IEnumerable(Of String) = controller.GetSceneObjects()

            'Assert
            Assert.IsNotNull(result)
            Assert.AreNotEqual(0, result.Count())

        End Sub

    End Class
End Namespace