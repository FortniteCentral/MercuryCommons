using System.Linq;
using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Lightswitch;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class LightswitchPublicService : BaseService
{
    public override string BaseUrl => "https://lightswitch-public-service-prod06.ol.epicgames.com";

    internal LightswitchPublicService(FortniteApiClient client) : base(client) { }

    public async Task<FortniteResponse<LightswitchResponse>> GetStatusAsync(string service)
    {
        if (string.IsNullOrWhiteSpace(service)) return null;
        var request = new RestRequest($"/lightswitch/api/service/{service}/status");
        var response = await ExecuteAsync<LightswitchResponse>(request, true);
        return response;
    }

    public async Task<FortniteResponse<LightswitchResponse[]>> GetBulkStatusAsync(params string[] services)
    {
        if (services.Length == 0) return null;
        var request = new RestRequest("/lightswitch/api/service/bulk/status");
        request.AddOrUpdateParameters(services.Select(service => new QueryParameter("serviceId", service)));
        var response = await ExecuteAsync<LightswitchResponse[]>(request, true);
        return response;
    }
}