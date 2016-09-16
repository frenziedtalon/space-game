Imports Core.Classes
Imports Core.Extensions
Imports Core.Helpers
Imports Data.Classes
Imports Data.Data
Imports Mapster

Public Class Mappings
    Implements IRegister

    Public Sub Register(config As TypeAdapterConfig) Implements IRegister.Register

        config.ForType(Of CelestialObject, OrbitData).
            Map(Function(dest) dest.SemiMajorAxis, Function(src) src.SemiMajorAxis.Adapt(Of Distance)()).
            Map(Function(dest) dest.Eccentricity, Function(src) src.Eccentricity).
            Map(Function(dest) dest.Inclination, Function(src) src.Inclination.Adapt(Of Angle)()).
            Map(Function(dest) dest.ArgumentOfPeriapsis, Function(src) src.ArgumentOfPeriapsis.Adapt(Of Angle)()).
            Map(Function(dest) dest.LongitudeOfAscendingNode, Function(src) src.LongitudeOfAscendingNode.Adapt(Of Angle)()).
            Map(Function(dest) dest.MeanAnomalyZero, Function(src) src.MeanAnomalyZero.Adapt(Of Angle)())

        config.ForType(Of CelestialObject, PhysicalData).
            Map(Function(dest) dest.Name, Function(src) src.Name).
            Map(Function(dest) dest.Radius, Function(src) src.Radius.Adapt(Of Distance)()).
            Map(Function(dest) dest.Mass, Function(src) src.Mass.Adapt(Of Mass)()).
            Map(Function(dest) dest.Type, Function(src) src.CelestialObjectType.Adapt(Of CelestialObjectType)()).
            Map(Function(dest) dest.Texture, Function(src) src.TextureGroup.Adapt(Of Textures)())

        config.ForType(Of Global.Data.SqlDataProvider.CelestialObjectType, Global.Data.Classes.CelestialObjectType).
            MapWith(Function(src) src.Name.ToEnum(Of Global.Data.Classes.CelestialObjectType).Value)

        config.ForType(Of TextureGroup, Textures).
            Map(Function(dest) dest.Low, Function(src) CreateTexturePath("Low", src.TextureGroupToTextures)).
            Map(Function(dest) dest.Medium, Function(src) CreateTexturePath("Medium", src.TextureGroupToTextures)).
            Map(Function(dest) dest.High, Function(src) CreateTexturePath("High", src.TextureGroupToTextures))

        config.ForType(Of CelestialObjectType, CelestialObjectType)()

        config.ForType(Of CelestialObject, CelestialObject)()

        config.ForType(Of SolarSystem, SolarSystem)()

        config.ForType(Of Universe, Universe)()

        config.ForType(Of User, User)()

        config.ForType(Of TextureGroup, TextureGroup)()

        config.ForType(Of TextureGroupToTexture, TextureGroupToTexture)()

        config.ForType(Of Texture, Texture)()

        config.ForType(Of TexturePath, TexturePath)()

        config.ForType(Of TextureType, TextureType)()
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
