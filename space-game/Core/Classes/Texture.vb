Imports Core.Enums

Namespace Classes
    Public Class Texture
        Implements IEquatable(Of Texture)

        Public Type As TextureType
        Public Path As String
        Public Quality As TextureQuality

        Public Shadows Function Equals(other As Texture) As Boolean Implements IEquatable(Of Texture).Equals
            If other IsNot Nothing Then
                Return Type.Equals(other.Type) AndAlso 
                        Path.Equals(other.Path) AndAlso 
                        Quality.Equals(other.Quality)
            End If
            Return False
        End Function
    End Class
End Namespace