Imports System.Runtime.CompilerServices
Imports Core.Classes
Imports Core.Enums

Namespace Extensions
    Public Module EnumerableExtensions

        <Extension>
        Public Function HasAny(Of T)(this As IEnumerable(Of T)) As Boolean
            Return this IsNot Nothing AndAlso this.Any()
        End Function

        <Extension>
        Public Function GetHighestAvailableResolution(this As IEnumerable(Of Texture), type As TextureType) As String
            Dim result = ""

            Dim r = GetTexture(this, type).
                    OrderByDescending(Function(t) t.Quality).
                    FirstOrDefault()

            If r IsNot Nothing Then
                result = r.Path
            End If

            Return result
        End Function

        <Extension>
        Public Function GetLowestAvailableResolution(this As IEnumerable(Of Texture), type As TextureType) As String
            Dim result = ""

            Dim r = GetTexture(this, type).
                    OrderBy(Function(t) t.Quality).
                    FirstOrDefault()

            If r IsNot Nothing Then
                result = r.Path
            End If

            Return result
        End Function

        Private Function GetTexture(textures As IEnumerable(Of Texture), type As TextureType) As IEnumerable(Of Texture)
            Return textures.Where(Function(t) t.Type.Equals(type) AndAlso Not String.IsNullOrWhiteSpace(t.Path))
        End Function
    End Module
End Namespace