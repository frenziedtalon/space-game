Imports System.Text

Namespace Helpers
    Public Class PathHelper

        Public Shared Function SitePathCombine(path1 As String, path2 As String) As String

            If {path1, path2}.Any(Function(p) String.IsNullOrWhiteSpace(p)) Then
                Throw New ArgumentNullException()
            End If

            dim result as New StringBuilder

            result.Append(path1.TrimEnd("/"c))
            result.Append("/")
            result.Append(path2.TrimStart("/"c))

            Return result.ToString()
        End Function

    End Class
End Namespace