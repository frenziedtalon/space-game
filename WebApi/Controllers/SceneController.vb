Imports System.Net
Imports System.Web.Http
Imports OrbitalMechanics
Imports WebApi.Models

Namespace Controllers
    Public Class SceneController
        Inherits ApiController

        ' GET api/values
        Public Function GetSceneObjects() As SolarSystem
            Dim constructor = New SceneConstructor
            Return constructor.SolSystem()
        End Function

    End Class
End Namespace