using System.Threading.Tasks;
using CUE4Parse.Utils;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Discovery;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class DiscoveryService(FortniteApiClient client, EEnvironment environment) : BaseService(client, environment)
{
    public override string BaseUrl => "https://fn-service-discovery-live-public.ogs.live.on.epicgames.com";
    public override string StageUrl => "https://fn-service-discovery-stage-public.ogs.dev.on.epicgames.com";

    public async Task<FortniteResponse<DiscoveryResponse>> GetDiscoverySurfaceAsync(string fortniteVersion)
    {
        var request = new RestRequest($"/api/v1/discovery/surface/{Client.CurrentLogin.AccountId}?appId=Fortnite", Method.Post);
        request.AddHeader("User-Agent", fortniteVersion.Replace("-Windows", ""));
        request.AddBody(new
        {
            surfaceName = "CreativeDiscoverySurface_Frontend",
            revision = -1,
            partyMemberIds = new[]
            {
                Client.CurrentLogin.AccountId
            },
            matchmakingRegion = "US"
        }, "application/json");
        var response = await ExecuteAsync<DiscoveryResponse>(request, true);
        return response;
    }
}