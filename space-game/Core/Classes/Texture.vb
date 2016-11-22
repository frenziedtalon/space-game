Imports Core.Enums
Imports Newtonsoft.Json

Namespace Classes
    Public Class Texture
        Implements IEquatable(Of Texture)

        <JsonIgnore>
        Public TypeEnum As TextureType
        Public Path As String
        <JsonIgnore>
        Public QualityEnum As TextureQuality

        Public ReadOnly Property Type As String
            Get
                Return TypeEnum.ToString
            End Get
        End Property

        Public ReadOnly Property Quality As String
            Get
                Return QualityEnum.ToString
            End Get
        End Property

        Public Shadows Function Equals(other As Texture) As Boolean Implements IEquatable(Of Texture).Equals
            If other IsNot Nothing Then
                Return TypeEnum.Equals(other.TypeEnum) AndAlso
                        Path.Equals(other.Path, StringComparison.OrdinalIgnoreCase) AndAlso
                        QualityEnum.Equals(other.QualityEnum)
            End If
            Return False
        End Function
    End Class
End Namespace