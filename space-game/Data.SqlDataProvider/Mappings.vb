Imports Core.Extensions
Imports Core.Helpers
Imports Data.Classes
Imports Mapster
Imports CelestialObjectType = Data.SqlDataProvider.CelestialObjectType

Public Class Mappings
    Implements IRegister

    Public Sub Register(config As TypeAdapterConfig) Implements IRegister.Register

        config.ForType(Of CelestialObject, OrbitData).
            Map(Function(dest) dest.SemiMajorAxis, Function(src) src.SemiMajorAxis).
            Map(Function(dest) dest.Eccentricity, Function(src) src.Eccentricity).
            Map(Function(dest) dest.Inclination, Function(src) src.Inclination).
            Map(Function(dest) dest.ArgumentOfPeriapsis, Function(src) src.ArgumentOfPeriapsis).
            Map(Function(dest) dest.LongitudeOfAscendingNode, Function(src) src.LongitudeOfAscendingNode).
            Map(Function(dest) dest.MeanAnomalyZero, Function(src) src.MeanAnomalyZero)

        config.ForType(Of CelestialObject, PhysicalData).
            Map(Function(dest) dest.Name, Function(src) src.Name).
            Map(Function(dest) dest.Radius, Function(src) src.Radius).
            Map(Function(dest) dest.Mass, Function(src) src.Mass).
            Map(Function(dest) dest.Type, Function(src) src.CelestialObjectType).
            Map(Function(dest) dest.Textures, Function(src) src.TextureGroup).
            Map(Function(dest) dest.Rings, function(src) src.RingSystem)

        config.ForType(Of CelestialObjectType, Global.Data.Classes.CelestialObjectType).
            MapWith(Function(src) src.Name.ToEnum(Of Global.Data.Classes.CelestialObjectType).Value)

        config.ForType(Of TextureGroup, List(Of Core.Classes.Texture)).
            MapWith(Function(src) MapTextureGroupToListOfTexture(src, config))

        config.ForType(Of TextureQuality, Core.Enums.TextureQuality).
            MapWith(Function(src) src.Quality.ToEnum(Of Core.Enums.TextureQuality).Value)

        config.ForType(Of TextureType, Core.Enums.TextureType).
            MapWith(Function(src) src.Type.ToEnum(Of Core.Enums.TextureType).Value)

        config.ForType(Of CelestialObjectType, CelestialObjectType)()

        config.ForType(Of CelestialObject, CelestialObject)().
            ShallowCopyForSameType(True)

        config.ForType(Of SolarSystem, SolarSystem)()

        config.ForType(Of Universe, Universe)()

        config.ForType(Of User, User)()

        config.ForType(Of TextureGroup, TextureGroup)()

        config.ForType(Of TextureGroupToTexture, TextureGroupToTexture)()

        config.ForType(Of Texture, Texture)()

        config.ForType(Of TexturePath, TexturePath)()

        config.ForType(Of TextureType, TextureType)()

        config.ForType(Of TextureQuality, TextureQuality)()

        config.ForType(Of RingSystem, RingData).
            Map(Function(dest) dest.InnerRadius, function(src) src.InnerRadius).
            Map(Function(dest) dest.OuterRadius, function(src) src.OuterRadius).
            Map(Function(dest) dest.Textures, function(src) src.TextureGroup)

        config.ForType(Of RingSystem, RingSystem)
    End Sub

    Private Function MapTextureGroupToListOfTexture(input As TextureGroup, config As TypeAdapterConfig) As List(Of Core.Classes.Texture)
        Dim result As New List(Of Core.Classes.Texture)
        If input IsNot Nothing Then
            For Each t As TextureGroupToTexture In input.TextureGroupToTextures
                Dim texture As New Core.Classes.Texture
                texture.Path = PathHelper.SitePathCombine(t.Texture.TexturePath.Path, t.Texture.Name)
                texture.QualityEnum = t.Texture.TextureQuality.Adapt(Of Core.Enums.TextureQuality)(config)
                texture.TypeEnum = t.Texture.TextureType.Adapt(Of Core.Enums.TextureType)(config)
                result.Add(texture)
            Next
        End If
        Return result
    End Function
End Class
