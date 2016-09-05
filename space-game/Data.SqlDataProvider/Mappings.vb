Imports Core.Classes
Imports Core.Helpers
Imports Mapster

Public Class Mappings
    Implements IRegister

    Public Sub Register(config As TypeAdapterConfig) Implements IRegister.Register

        config.ForType(Of TextureGroup, Textures).
            Map(Function(dest) dest.Low, Function(src) CreateTexturePath("Low", src.TextureGroupToTextures)).
            Map(Function(dest) dest.Medium, Function(src) CreateTexturePath("Medium", src.TextureGroupToTextures)).
            Map(Function(dest) dest.High, Function(src) CreateTexturePath("High", src.TextureGroupToTextures))

    End Sub

    Private Function CreateTexturePath(typeName As String, group As ICollection(Of TextureGroupToTexture)) As String

        Dim result As String = String.Empty
        Dim target = group.FirstOrDefault(Function(t) t.Texture.TextureType.Type.Equals(typeName, StringComparison.OrdinalIgnoreCase))

        If target IsNot Nothing Then
            result = PathHelper.SitePathCombine(target.Texture.TexturePath.Path, target.Texture.Name)
        End If
        Return result
    End Function

End Class
