using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Events;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class EventsPublicService(FortniteApiClient client, EEnvironment environment) : BaseService(client, environment)
{
    public override string BaseUrl => "https://events-public-service-prod.ol.epicgames.com";
    public override string StageUrl => "https://events-public-service-stage.ol.epicgames.com";

    public async Task<FortniteResponse<EventsResponse>> GetEventsList(
        string gameId = "Fortnite",
        string region = "NAE",
        string platform = "Windows")
    {
        var accountId = Client.CurrentLogin.AccountId;
        var request = new RestRequest($"/api/v1/events/{gameId}/download/{accountId}?region={region}&platform={platform}&teamAccountIds={accountId}");
        var response = await ExecuteAsync<EventsResponse>(request, true);
        return response;
    }
}
