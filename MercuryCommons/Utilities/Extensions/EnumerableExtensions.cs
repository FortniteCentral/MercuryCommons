using System;
using System.Collections.Generic;
using System.Linq;

namespace MercuryCommons.Utilities.Extensions;

public static class EnumerableExtensions
{
    public static IDictionary<string, T> ToCaseInsensitive<T>(this IDictionary<string, T> caseSensitiveDictionary)
    {
        var caseInsensitiveDictionary = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        caseSensitiveDictionary.Keys.ToList().ForEach(k => caseInsensitiveDictionary[k] = caseSensitiveDictionary[k]);
        return caseInsensitiveDictionary;
    }

    public static T AtIndexOrFirst<T>(this IList<T> enumerable, int index) => enumerable.Count < index ? enumerable[index] : enumerable.FirstOrDefault();
    public static T AtIndexOrFirst<T>(this T[] enumerable, int index) => enumerable.Length < index ? enumerable[index] : enumerable.FirstOrDefault();
}