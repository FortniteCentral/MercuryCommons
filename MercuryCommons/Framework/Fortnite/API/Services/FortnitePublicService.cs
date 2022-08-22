using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Catalog;
using MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class FortnitePublicService : BaseService
{
    public override string BaseUrl => "https://fortnite-public-service-prod11.ol.epicgames.com";

    internal FortnitePublicService(FortniteApiClient client) : base(client) { }

    public async Task<FortniteResponse<DiscoveryResponse>> GetDiscoveryAsync()
    {
        var request = new RestRequest($"/fortnite/api/game/v2/creative/discovery/surface/{Client.CurrentLogin.AccountId}", Method.Post);
        request.AddJsonBody(new
        {
            surfaceName = "CreativeDiscoverySurface_Frontend",
            partyMemberIds = new[] { Client.CurrentLogin.AccountId }
        });
        var response = await ExecuteAsync<DiscoveryResponse>(request, true);
        return response;
    }

    public async Task<FortniteResponse<CloudstorageSystemResponse[]>> GetCloudstorageSystemAsync()
    {
        var request = new RestRequest("/fortnite/api/cloudstorage/system");
        var response = await ExecuteAsync<CloudstorageSystemResponse[]>(request, true);
        return response;
    }

    public byte[] GetCloudstorageFile(string uniqueFilename)
    {
        var request = new RestRequest($"/fortnite/api/cloudstorage/system/{uniqueFilename}");
        var response = DownloadFile(request, true);
        return response;
    }

    public async Task<FortniteResponse<string[]>> GetKeychainAsync()
    {
        var request = new RestRequest("/fortnite/api/storefront/v2/keychain");
        var response = await ExecuteAsync<string[]>(request, true);
        return response;
    }

    public async Task<FortniteResponse<CatalogResponse>> GetCatalogAsync()
    {
        var request = new RestRequest("/fortnite/api/storefront/v2/catalog");
        var response = await ExecuteAsync<CatalogResponse>(request, true);
        return response;
    }

    public async Task<FortniteResponse<TimelineResponse>> GetTimelineAsync()
    {
        var request = new RestRequest("/fortnite/api/calendar/v1/timeline");
        var response = await ExecuteAsync<TimelineResponse>(request, true);
        return response;
    }
}