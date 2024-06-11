using System;
using RestSharp;

namespace MercuryCommons.Framework.Data.Remote;

public class MercuryRequest : RestRequest
{
    private const int TimeoutSeconds = 5;

    public MercuryRequest(string url, Method method = Method.Get) : base(url, method)
    {
        Timeout = TimeSpan.FromSeconds(TimeoutSeconds);
    }

    public MercuryRequest(Uri uri, Method method = Method.Get) : base(uri, method)
    {
        Timeout = TimeSpan.FromSeconds(TimeoutSeconds);
    }
}