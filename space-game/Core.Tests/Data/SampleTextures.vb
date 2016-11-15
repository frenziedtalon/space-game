Imports Core.Classes
Imports Core.Enums

Namespace Data
    Public Class SampleTextures
        Public ReadOnly High As Texture
        Public ReadOnly Medium As Texture
        Public ReadOnly Low As Texture
        Public ReadOnly EmptyHigh As Texture
        Public ReadOnly EmptyMedium As Texture
        Public ReadOnly EmptyLow As Texture
        Public ReadOnly Type As TextureType

        Public Sub New(type As TextureType)
            Type = type

            Low = New Texture() With {.Quality = TextureQuality.Low,
                                            .Type = Type,
                                            .Path = "low texture path"}

            Medium = New Texture() With {.Quality = TextureQuality.Medium,
                                            .Type = Type,
                                            .Path = "medium texture path"}

            High = New Texture() With {.Quality = TextureQuality.High,
                                            .Type = Type,
                                            .Path = "high texture path"}


            EmptyLow = New Texture() With {.Quality = TextureQuality.Low,
                                            .Type = Type,
                                            .Path = " "}

            EmptyMedium = New Texture() With {.Quality = TextureQuality.Medium,
                                            .Type = Type,
                                            .Path = " "}

            EmptyHigh = New Texture() With {.Quality = TextureQuality.High,
                                            .Type = Type,
                                            .Path = " "}
        End Sub
    End Class
End Namespace