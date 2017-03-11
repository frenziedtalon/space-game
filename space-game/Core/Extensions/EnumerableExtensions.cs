using Core.Classes;
using Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HasAny<T>(this IEnumerable<T> @this)
        {
            return @this != null && @this.Any();
        }

        public static string GetHighestAvailableResolution(this IEnumerable<Texture> @this, TextureType type)
        {
            string result = "";

            Texture r = GetTexture(@this, type).OrderByDescending(t => t.QualityEnum).FirstOrDefault();

            if (r != null)
            {
                result = r.Path;
            }

            return result;
        }

        public static List<Texture> GetHighestAvailableResolutionForEachType(this IEnumerable<Texture> @this)
        {
            Dictionary<TextureType, Texture> result = new Dictionary<TextureType, Texture>();

            List<Texture> textures = @this.OrderByDescending(t => t.QualityEnum).ToList();

            for (int i = 0; i <= textures.Count() - 1; i += 1)
            {
                Texture t = textures[i];
                if (!string.IsNullOrWhiteSpace(t.Path) && !result.ContainsKey(t.TypeEnum))
                {
                    result.Add(t.TypeEnum, t);
                }
            }

            return result.Values.ToList();
        }

        public static string GetLowestAvailableResolution(this IEnumerable<Texture> @this, TextureType type)
        {
            string result = "";

            Texture r = GetTexture(@this, type).OrderBy(t => t.QualityEnum).FirstOrDefault();

            if (r != null)
            {
                result = r.Path;
            }

            return result;
        }

        private static IEnumerable<Texture> GetTexture(IEnumerable<Texture> textures, TextureType type)
        {
            return textures.Where(t => t.TypeEnum.Equals(type) && !string.IsNullOrWhiteSpace(t.Path));
        }

        public static bool IsEquivalent(this IEnumerable<Texture> @this, IEnumerable<Texture> other)
        {
            if ((@this == null & other == null) || (@this != null & other != null))
            {

                if (@this.Count().Equals(other.Count()))
                {
                    List<Texture> sortedThis = @this.OrderBy(l => l.Path).ThenBy(l => l.Quality).ThenBy(l => l.Type).ToList();

                    List<Texture> sortedOther = other.OrderBy(l => l.Path).ThenBy(l => l.Quality).ThenBy(l => l.Type).ToList();

                    return sortedThis.SequenceEqual(sortedOther);
                }
            }

            return false;
        }
    }
}
