using Data.SqlDataProvider.Model;
using System.Data.Entity;
using System.Linq;

namespace Data.SqlDataProvider.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IQueryableExtensions
    {
        public static IQueryable<CelestialObject> IncludeAllTables(this IQueryable<CelestialObject> @this)
        {
            return @this.IncludeType()
                        .IncludeRingSystem()
                        .IncludeTextures();
        }
        
        private static IQueryable<CelestialObject> IncludeTextures(this IQueryable<CelestialObject> @this)
        {
            return @this.Include(s => s.TextureGroup)
                        .Include(s => s.TextureGroup)
                        .Include(s => s.TextureGroup.TextureGroupToTextures.Select(t => t.Texture.TextureType))
                        .Include(s => s.TextureGroup.TextureGroupToTextures.Select(t => t.Texture.TexturePath))
                        .Include(s => s.TextureGroup.TextureGroupToTextures.Select(t => t.Texture.TextureQuality));
        }
        
        private static IQueryable<CelestialObject> IncludeType(this IQueryable<CelestialObject> @this)
        {
            return @this.Include(s => s.CelestialObjectType);
        }
        
        private static IQueryable<CelestialObject> IncludeRingSystem(this IQueryable<CelestialObject> @this)
        {
            return @this.Include(s => s.RingSystem)
                        .Include(s => s.RingSystem.TextureGroup)
                        .Include(s => s.RingSystem.TextureGroup.TextureGroupToTextures.Select(t => t.Texture.TextureType))
                        .Include(s => s.RingSystem.TextureGroup.TextureGroupToTextures.Select(t => t.Texture.TexturePath))
                        .Include(s => s.RingSystem.TextureGroup.TextureGroupToTextures.Select(t => t.Texture.TextureQuality));
        }

    }
}
