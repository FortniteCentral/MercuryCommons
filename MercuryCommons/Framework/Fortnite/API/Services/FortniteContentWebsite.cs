using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class FortniteContentWebsite : BaseService
{
    public override string BaseUrl => "https://fortnitecontent-website-prod07.ol.epicgames.com";

    internal FortniteContentWebsite(FortniteApiClient client) : base(client) { }

    public async Task<FortniteResponse<ContentResponse>> GetContentWebsiteAsync(string lang = "en")
    {
        var request = new RestRequest("/content/api/pages/fortnite-game");
        request.AddParameter("lang", lang);
        var response = await ExecuteAsync<ContentResponse>(request);
        return response;
    }
}