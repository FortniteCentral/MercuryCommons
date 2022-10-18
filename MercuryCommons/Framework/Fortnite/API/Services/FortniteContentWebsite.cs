using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class FortniteContentWebsite : BaseService
{
    public override string BaseUrl => "https://fortnitecontent-website-prod07.ol.epicgames.com";
    public override string StageUrl => "https://fortnitecontent-website-prod07.ol.epicgames.com";

    internal FortniteContentWebsite(FortniteApiClient client, EEnvironment environment) : base(client, environment) { }

    public async Task<FortniteResponse<ContentResponse>> GetContentWebsiteAsync(string lang = "en")
    {
        var request = new RestRequest("/content/api/pages/fortnite-game");
        request.AddParameter("lang", lang);
        var response = await ExecuteAsync<ContentResponse>(request);
        return response;
    }

    public async Task<FortniteResponse<FortTournament[]>> GetContentTournamentsAsync(string lang = "en")
    {
        var request = new RestRequest("/content/api/pages/fortnite-game/tournamentinformation");
        request.AddParameter("lang", lang);
        var data = await ExecuteAsync<JObject>(request);
        if (!data.IsSuccessful)
        {
            return new FortniteResponse<FortTournament[]>
            {
                Data = null,
                Error = data.Error,
                HttpStatusCode = data.HttpStatusCode
            };
        }

        var response = data.Data["tournament_info"]?["tournaments"]?.ToObject<FortTournament[]>();
        return new FortniteResponse<FortTournament[]>
        {
            Data = response,
        };
    }
}