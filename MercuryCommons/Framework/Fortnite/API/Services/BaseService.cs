using System.Threading;
using System.Threading.Tasks;
using MercuryCommons.Framework.Data.Remote;
using MercuryCommons.Framework.Fortnite.API.Exceptions;
using MercuryCommons.Framework.Fortnite.API.Objects;
using Newtonsoft.Json;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public abstract class BaseService
{
    public abstract string BaseUrl { get; }

    internal FortniteApiClient Client { get; set; }
    internal RestClient RestClient { get; set; }

    internal BaseService(FortniteApiClient client)
    {
        Client = client;
        RestClient = client.CreateRestClient(this);
    }

    internal virtual async Task<FortniteResponse> ExecuteAsync(
        RestRequest request,
        bool withAuth = false,
        CancellationToken token = default)
        => await ExecuteAsync<object>(request, withAuth, false, token: token).ConfigureAwait(false);

    internal virtual async Task<FortniteResponse<T>> ExecuteAsync<T>(
        RestRequest request,
        bool withAuth = false,
        bool withData = true,
        string accessToken = null,
        bool requiresLogin = true,
        CancellationToken token = default)
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

        var response = await RestClient.ExecuteAsync(request, token).ConfigureAwait(false);
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
                fortniteResponse.Error = JsonConvert.DeserializeObject<EpicError>(content, JsonNetSerializer.SerializerSettings);
                break;
        }

        return fortniteResponse;
    }

}