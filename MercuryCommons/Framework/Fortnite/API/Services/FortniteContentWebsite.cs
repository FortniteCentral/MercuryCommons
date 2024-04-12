using System.Collections.Generic;
using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class FortniteContentWebsite(FortniteApiClient client, EEnvironment environment) : BaseService(client, environment)
{
    public override string BaseUrl => "https://fortnitecontent-website-prod07.ol.epicgames.com";
    public override string StageUrl => "https://fortnitecontent-website-prod07.ol.epicgames.com";

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
            Data = response
        };
    }

    public async Task<FortniteResponse<FortMPItemShopSection[]>> GetContentMPShopSectionsAsync(string lang = "en")
    {
        var request = new RestRequest("/content/api/pages/fortnite-game/mp-item-shop");
        request.AddParameter("lang", lang);
        var data = await ExecuteAsync<JObject>(request);
        if (!data.IsSuccessful)
        {
            return new FortniteResponse<FortMPItemShopSection[]>
            {
                Data = null,
                Error = data.Error,
                HttpStatusCode = data.HttpStatusCode
            };
        }

        var response = data.Data["shopData"]?["sections"]?.ToObject<FortMPItemShopSection[]>();
        return new FortniteResponse<FortMPItemShopSection[]>
        {
            Data = response
        };
    }

    public async Task<FortniteResponse<List<FortEventScreenData>>> GetContentEventScreenDataAsync(string lang = "en")
    {
        var request = new RestRequest("/content/api/pages/fortnite-game/eventscreens");
        request.AddParameter("lang", lang);
        var data = await ExecuteAsync<JObject>(request);
        if (!data.IsSuccessful)
        {
            return new FortniteResponse<List<FortEventScreenData>>
            {
                Data = null,
                Error = data.Error,
                HttpStatusCode = data.HttpStatusCode
            };
        }

        var response = data.Data["eventScreenGroup"]?["eventScreens"]?.ToObject<List<FortEventScreenData>>() ?? [];

        foreach (var prop in data.Data.Properties())
        {
            try
            {
                var screen = prop.Value.ToObject<JObject>();
                if (!screen.ContainsKey("eventScreenData")) continue;
                var screenData = screen["eventScreenData"]?.ToObject<FortEventScreenData>();
                if (screenData?.EventCMSId == null) continue;
                response.Add(screenData);
            }
            catch
            {
                // ignore
            }
        }
        
        return new FortniteResponse<List<FortEventScreenData>>
        {
            Data = response
        };
    }
}