using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Catalog;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class CatalogPublicService : BaseService
{
    public override string BaseUrl => "https://catalog-public-service-prod06.ol.epicgames.com";

    public CatalogPublicService(FortniteApiClient client) : base(client) { }

    public async Task<FortniteResponse<ItemIdResponse>> GetItemIdInfo(string itemId)
    {
        var request = new RestRequest($"/catalog/api/shared/bulk/items?id={itemId}&returnItemDetails=false&country=NZ&locale=en");
        var response = await ExecuteAsync<ItemIdResponse>(request, true);
        return response;
    }
}