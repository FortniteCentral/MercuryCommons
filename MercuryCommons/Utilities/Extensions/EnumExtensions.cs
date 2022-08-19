using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MercuryCommons.Utilities.Extensions;

public static class EnumExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ToEnum<T>(this string value, T defaultValue)
    {
        if (!Enum.TryParse(typeof(T), value, true, out var ret))
            return defaultValue;

        return (T) ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetDescription(this Enum value, bool includeValue = false)
    {
        var fi = value.GetType().GetField(value.ToString());
        if (fi == null) return value + (includeValue ? $"{value:D}" : string.Empty);
        var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value + (includeValue ? $"{value:D}" : string.Empty);
    }
}