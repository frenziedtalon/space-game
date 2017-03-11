using System;

namespace Core.Extensions
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid @this)
        {
            return @this.Equals(Guid.Empty);
        }
    }
}
