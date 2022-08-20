using System;
using RestSharp;

namespace MercuryCommons.Framework.Data.Remote;

public class MercuryRequest : RestRequest
{
    private const int _timeout = 3 * 1000;

    public MercuryRequest(string url, Method method = Method.Get) : base(url, method)
    {
        Timeout = _timeout;
    }

    public MercuryRequest(Uri uri, Method method = Method.Get) : base(uri, method)
    {
        Timeout = _timeout;
    }
}