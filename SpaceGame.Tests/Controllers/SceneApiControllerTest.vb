Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports OrbitalMechanics
Imports SpaceGame.Controllers

Namespace Controllers

    <TestClass()> Public Class SceneApiControllerTest

        <TestMethod()> Public Sub GetSceneObjects()

            'Arrange
            Dim controller As New SceneApiController()

            'Act
            Dim result As SolarSystem = controller.GetSceneObjects()

            'Assert
            Assert.IsNotNull(result)
            Assert.AreNotEqual(0, result.Objects)

        End Sub

    End Class
End Namespace