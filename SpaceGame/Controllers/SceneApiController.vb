Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class SceneApiController
        Inherits ApiController

        ' GET api/values
        Public Function GetSceneObjects() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

    End Class
End Namespace