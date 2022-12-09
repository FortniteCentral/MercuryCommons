using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace MercuryCommons.Utilities.Extensions;

public static class UriExtensions
{
    public static Uri Combine(this Uri uri, params string[] paths) => new(paths.Aggregate(uri.AbsoluteUri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"));

    public static string CombineToString(this string uri, params string[] paths) => new Uri(paths.Aggregate(uri, (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}")).AbsoluteUri;

    public static bool TryGetBytes(this Uri link, out byte[] data)
    {
        data = new HttpClient().GetByteArrayAsync(link).GetAwaiter().GetResult();
        return data != null;
    }

    public static bool TryGetStream(this Uri link, out Stream stream)
    {
        stream = new HttpClient().GetStreamAsync(link).GetAwaiter().GetResult();
        return stream != null;
    }
}