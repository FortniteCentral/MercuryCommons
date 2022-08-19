using System;
using System.IO;
using System.Net.Http;

namespace MercuryCommons.Utilities.Extensions;

public static class UriExtensions
{
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