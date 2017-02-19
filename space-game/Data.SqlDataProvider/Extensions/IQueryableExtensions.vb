Imports System.Runtime.CompilerServices
Imports System.Data.Entity

Namespace Extensions
    Public Module QueryableExtensions

        <Extension>
        Public Function IncludeAllTables(this As IQueryable(Of CelestialObject)) As IQueryable(Of CelestialObject)
            Return this.
                IncludeType().
                IncludeRingSystem().
                IncludeTextures()

        End Function

        <Extension>
        Private Function IncludeTextures(this As IQueryable(Of CelestialObject)) As IQueryable(Of CelestialObject)
            Return this.Include(Function(s) s.TextureGroup).
                                Include(Function(s) s.TextureGroup).
                                Include(Function(s) s.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TextureType)).
                                Include(Function(s) s.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TexturePath)).
                                Include(Function(s) s.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TextureQuality))
        End Function

        <Extension>
        Private Function IncludeType(this As IQueryable(Of CelestialObject)) As IQueryable(Of CelestialObject)
            Return this.Include(Function(s) s.CelestialObjectType)
        End Function

        <Extension>
        Private Function IncludeRingSystem(this As IQueryable(Of CelestialObject)) As IQueryable(Of CelestialObject)
            Return this.Include(Function(s) s.RingSystem).
                                Include(Function(s) s.RingSystem.TextureGroup).
                                Include(Function(s) s.RingSystem.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TextureType)).
                                Include(Function(s) s.RingSystem.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TexturePath)).
                                Include(Function(s) s.RingSystem.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TextureQuality))
        End Function

    End Module
End Namespace