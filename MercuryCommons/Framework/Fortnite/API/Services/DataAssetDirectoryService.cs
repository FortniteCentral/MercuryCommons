using System.Collections.Generic;
using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.DataAssetDirectory;
using Newtonsoft.Json;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class DataAssetDirectoryService : BaseService
{
    public override string BaseUrl => "https://data-asset-directory-public-service-prod.ol.epicgames.com";
    public override string StageUrl => "https://data-asset-directory-public-service-stage.ol.epicgames.com";

    public DataAssetDirectoryService(FortniteApiClient client, EEnvironment environment) : base(client, environment) { }
    
    public async Task<FortniteResponse<Dictionary<string, DataAssetResponse>>> RetrieveItemsFromDirectory(string branch, string cl, IDictionary<string, int> types)
    {
        var request = new RestRequest($"/api/v1/assets/Fortnite/++Fortnite+{branch}/{cl}?appId=Fortnite", Method.Post);
        request.AddJsonBody(JsonConvert.SerializeObject(types));
        var response = await ExecuteAsync<Dictionary<string, DataAssetResponse>>(request, true);
        return response;
    }
}