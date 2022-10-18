using System;
using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects.GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class GraphQLService : BaseService
{
    public override string BaseUrl => "https://www.epicgames.com/graphql";
    public override string StageUrl => "https://www.epicgames.com/graphql";

    internal GraphQLService(FortniteApiClient client, EEnvironment environment) : base(client, environment) { }

    public async Task<GQLCatalogRelatedResponse> GetRelatedOfferIds(string nmspc, string[] countries, string country = "NZ", string locale = "en-US", bool codeRedemptionOnly = false, string category = "addons|digitalextras")
    {
        var request = new RestRequest();
        request.AddParameter("operationName", "getRelatedOfferIdsByCategory");
        request.AddParameter("variables", JsonConvert.SerializeObject(new
        {
            category,
            country,
            allowCountries = string.Join(',', countries ?? Array.Empty<string>()),
            locale,
            @namespace = nmspc,
            sortBy = "pcReleaseDate",
            sortDir = "DESC",
            codeRedemptionOnly
        }));

        request.AddParameter("extensions", JsonConvert.SerializeObject(new
        {
            persistedQuery = new
            {
                version = 1,
                sha256Hash = "ff4dea7ebf14b25dc1cbedffe1d90620318a7bffea481fea02ac6e87310326f4"
            }
        }));
        
        var response = await ExecuteAsync<JObject>(request);
        var data = response.Data?["data"]?["Catalog"]?["catalogOffers"];
        return data?.ToObject<GQLCatalogRelatedResponse>();
    }

    public async Task<GQLCatalogOfferResponse> GetCatalogOffer(string offerId, string sandboxId, string country = "NZ", string locale = "en-US")
    {
        var request = new RestRequest();
        request.AddParameter("operationName", "getCatalogOffer");
        request.AddParameter("variables", JsonConvert.SerializeObject(new
        {
            locale,
            country,
            offerId,
            sandboxId
        }));

        request.AddParameter("extensions", JsonConvert.SerializeObject(new
        {
            persistedQuery = new
            {
                version = 1,
                sha256Hash = "546b92042e42306dfe422cf844e02aa8fbc3aff2d949ddecff8248b39badc040"
            }
        }));
        
        var response = await ExecuteAsync<JObject>(request);
        var data = response.Data?["data"]?["Catalog"]?["catalogOffer"];
        return data?.ToObject<GQLCatalogOfferResponse>();
    }
}