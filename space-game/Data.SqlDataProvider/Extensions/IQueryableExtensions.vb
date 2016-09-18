Imports System.Runtime.CompilerServices
Imports System.Data.Entity

Namespace Extensions
    Public Module QueryableExtensions

        <Extension>
        Public Function IncludeAllTables(this As IQueryable(Of CelestialObject)) As IQueryable(Of CelestialObject)
            Return this.
                IncludeTextures().
                IncludeType()
        End Function

        <Extension>
        Private Function IncludeTextures(this As IQueryable(Of CelestialObject)) As IQueryable(Of CelestialObject)
            Return this.Include(Function(s) s.TextureGroup).
                                Include(Function(s) s.TextureGroup).
                                Include(Function(s) s.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TextureType)).
                                Include(Function(s) s.TextureGroup.TextureGroupToTextures.Select(Function(t) t.Texture.TexturePath))
        End Function

        <Extension>
        Private Function IncludeType(this As IQueryable(Of CelestialObject)) As IQueryable(Of CelestialObject)
            Return this.Include(Function(s) s.CelestialObjectType)
        End Function

    End Module
End Namespace