﻿using System.Threading;
using System.Threading.Tasks;
using MercuryCommons.Framework.Data.Remote;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Exceptions;
using MercuryCommons.Framework.Fortnite.API.Objects;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public abstract class BaseService
{
    public string UrlToUse { get; protected set; }
    public abstract string BaseUrl { get; }
    public abstract string StageUrl { get; }

    public FortniteApiClient Client { get; internal set; }
    public RestClient RestClient { get; internal set; }
    internal EEnvironment Environment { get; set; }

    internal BaseService(FortniteApiClient client, EEnvironment environment)
    {
        Client = client;
        Environment = environment;
        UrlToUse = Environment switch
        {
            EEnvironment.Prod => BaseUrl,
            EEnvironment.Stage => StageUrl,
            _ => UrlToUse
        };
        RestClient = client.CreateRestClient(this);
    }

    public virtual async Task<byte[]> DownloadFile(
        RestRequest request,
        bool withAuth = false,
        string accessToken = null,
        bool requiresLogin = false,
        RestClient client = null)
    {
        if (requiresLogin)
        {
            var isLoggedIn = await Client.VerifyTokenAsync().ConfigureAwait(false);
            if (!isLoggedIn)
            {
                throw new FortniteException("You need to be logged in to use this!");
            }
        }
        
        if (withAuth) request.AddHeader("Authorization", $"bearer {accessToken ?? Client.CurrentLogin.AccessToken}");
        var response = (client ?? RestClient).Execute(request);
        byte[] ret = null;
        if (response.IsSuccessful) ret = response.RawBytes;
        return ret;
    }

    internal virtual async Task<FortniteResponse> ExecuteAsync(
        RestRequest request,
        bool withAuth = false,
        CancellationToken token = default)
        => await ExecuteAsync<object>(request, withAuth, false, token: token).ConfigureAwait(false);

    public virtual async Task<FortniteResponse<T>> ExecuteAsync<T>(
        RestRequest request,
        bool withAuth = false,
        bool withData = true,
        string accessToken = null,
        bool requiresLogin = true,
        CancellationToken token = default,
        RestClient client = null)
    {
        if (requiresLogin)
        {
            var isLoggedIn = await Client.VerifyTokenAsync().ConfigureAwait(false);
            if (!isLoggedIn)
            {
                throw new FortniteException("You need to be logged in to use this!");
            }
        }

        if (withAuth) request.AddHeader("Authorization", $"bearer {accessToken ?? Client.CurrentLogin.AccessToken}");

        client ??= RestClient;
        var response = await client.ExecuteAsync(request, token).ConfigureAwait(false);
        var content = response.Content ?? "{}";
        var fortniteResponse = new FortniteResponse<T>
        {
            HttpStatusCode = response.StatusCode
        };

        switch (response.IsSuccessful)
        {
            case true when withData:
                fortniteResponse.Data = JsonConvert.DeserializeObject<T>(content, JsonNetSerializer.SerializerSettings);
                break;
            case false:
                try
                {
                    fortniteResponse.Error = JsonConvert.DeserializeObject<EpicError>(content, JsonNetSerializer.SerializerSettings);
                }
                catch (JsonReaderException e)
                {
                    fortniteResponse.Error = new EpicError
                    {
                        ErrorMessage = e.ToString(),
                        ErrorCode = $"{(int) response.StatusCode}",
                        OriginatingService = "any"
                    };
                }
                break;
        }

        Log.Information("[{Method}] [{Status}({StatusCode})] '{Resource}'", request.Method, response.StatusDescription, (int) response.StatusCode, response.ResponseUri?.OriginalString);
        return fortniteResponse;
    }
}