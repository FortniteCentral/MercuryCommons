using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Links;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class LinksPublicService(FortniteApiClient client, EEnvironment environment) : BaseService(client, environment)
{
    public override string BaseUrl => "https://links-public-service-live.ol.epicgames.com";
    public override string StageUrl => "https://links-public-service-stage.ol.epicgames.com";

    public async Task<FortniteResponse<MnemonicResponse>> GetMnemonicAsync(
        string mnemonic,
        string linkType = "")
    {
        var request = new RestRequest($"/links/api/fn/mnemonic/{mnemonic.ToLower()}{(!string.IsNullOrWhiteSpace(linkType) ? $"?type={linkType}" : "")}");
        var response = await ExecuteAsync<MnemonicResponse>(request, true);
        return response;
    }
}