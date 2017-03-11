using System;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static TEnum? ToEnum<TEnum>(this string value) where TEnum : struct
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                TEnum e = new TEnum();
                if (Enum.TryParse(value, true, out e))
                {
                    return e;
                }
            }
            return null;
        }

    }
}
