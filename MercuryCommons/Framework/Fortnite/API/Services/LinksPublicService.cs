using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Links;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class LinksPublicService : BaseService
{
    public override string BaseUrl => "https://links-public-service-live.ol.epicgames.com";

    internal LinksPublicService(FortniteApiClient client) : base(client) { }

    public async Task<FortniteResponse<MnemonicResponse>> GetMnemonicAsync(
        string mnemonic,
        string linkType = "")
    {
        var request = new RestRequest($"/links/api/fn/mnemonic/{mnemonic.ToLower()}{(!string.IsNullOrWhiteSpace(linkType) ? $"?type={linkType}" : "")}");
        var response = await ExecuteAsync<MnemonicResponse>(request, true);
        return response;
    }
}