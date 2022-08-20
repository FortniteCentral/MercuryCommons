using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Unreal.Manifests.Objects;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class LauncherPublicService : BaseService
{
    public override string BaseUrl => "https://launcher-public-service-prod06.ol.epicgames.com";

    public LauncherPublicService(FortniteApiClient client) : base(client) { }

    public async Task<ManifestInfo> GetLauncherManifestAsync()
    {
        var launcher = await Client.AccountPublicService.GetAccessTokenAsync(GrantType.ClientCredentials, ClientToken.LauncherAppClient2);
        if (!launcher.IsSuccessful) return null;
        var request = new RestRequest("/launcher/api/public/assets/v2/platform/Windows/launcher");
        request.AddParameter("label", "Live-Firebrand");
        var response = await ExecuteAsync<string>(request, true, accessToken: launcher.Data.AccessToken);
        return response.IsSuccessful && !(response.Data.Length <= 0) ? new ManifestInfo(response.Data, 1) : null;
    }
}