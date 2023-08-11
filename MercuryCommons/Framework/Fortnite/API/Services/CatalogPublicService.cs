using System.Collections.Generic;
using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Catalog;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class CatalogPublicService(FortniteApiClient client, EEnvironment environment) : BaseService(client, environment)
{
    public override string BaseUrl => "https://catalog-public-service-prod06.ol.epicgames.com";
    public override string StageUrl => "https://catalogv2-public-service-stage.ol.epicgames.com";

    public async Task<FortniteResponse<Dictionary<string, ItemIdInfo>>> GetItemIdInfo(IEnumerable<string> itemIds, bool returnItemDetails = false, string country = "NZ", string locale = "en")
    {
        var request = new RestRequest("/catalog/api/shared/bulk/items");
        foreach (var offerId in itemIds)
        {
            request.AddParameter("id", offerId);
        }
        request.AddParameter("returnItemDetails", returnItemDetails);
        request.AddParameter("country", country);
        request.AddParameter("locale", locale);
        var response = await ExecuteAsync<Dictionary<string, ItemIdInfo>>(request, true);
        return response;
    }

    public async Task<FortniteResponse<Dictionary<string, OfferIdInfo>>> GetOfferIdInfo(IEnumerable<string> offerIds, bool returnItemDetails = false, string country = "NZ", string locale = "en")
    {
        var request = new RestRequest("/catalog/api/shared/bulk/offers");
        foreach (var offerId in offerIds)
        {
            request.AddParameter("id", offerId);
        }
        request.AddParameter("returnItemDetails", returnItemDetails);
        request.AddParameter("country", country);
        request.AddParameter("locale", locale);
        var response = await ExecuteAsync<Dictionary<string, OfferIdInfo>>(request, true);
        return response;
    }
}