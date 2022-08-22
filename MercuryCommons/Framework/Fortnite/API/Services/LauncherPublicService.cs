using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Unreal.Manifests.Objects;
using Newtonsoft.Json;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class LauncherPublicService : BaseService
{
    public override string BaseUrl => "https://launcher-public-service-prod06.ol.epicgames.com";

    public LauncherPublicService(FortniteApiClient client) : base(client) { }

    // Uses default Label
    public async Task<ManifestInfo> GetGameManifestAsync((string, string, string, string) items)
        => await GetGameManifestAsync(items.Item1, items.Item2, items.Item3, items.Item4);

    /// <summary>
    /// Gets the manifest information for the specified game
    /// </summary>
    /// <param name="appId">App ID of game</param>
    /// <param name="catalogId">Catalog ID of game</param>
    /// <param name="namespace">EGS Namespace of game</param>
    /// <param name="platform">Platform of game</param>
    /// <param name="label">Version label of game</param>
    /// <returns>Manifest info</returns>
    public async Task<ManifestInfo> GetGameManifestAsync(string appId, string catalogId, string @namespace, string platform = "Windows", string label = "Live")
    {
        var launcher = await Client.AccountPublicService.GetAccessTokenAsync(GrantType.ClientCredentials, ClientToken.LauncherAppClient2);
        if (!launcher.IsSuccessful) return null;
        var request = new RestRequest($"/launcher/api/public/assets/v2/platform/{platform}/namespace/{@namespace}/catalogItem/{catalogId}/app/{appId}/label/{label}", Method.Post);
        if (@namespace == "fn" && platform == "Android" && catalogId == "4fe75bbc5a674f4f9b356b5c90567da5") request.AddJsonBody(new { abis = new[] { "arm64-v8a" } }); // Manual
        var response = await ExecuteAsync<object>(request, true, accessToken: launcher.Data.AccessToken);
        return response.IsSuccessful && response.Data != null ? new ManifestInfo(JsonConvert.SerializeObject(response.Data), 1) : null;
    }

    /// <summary>
    /// Gets the manifest information for the current build of the Epic Games Launcher
    /// </summary>
    /// <returns>Manifest info</returns>
    public async Task<ManifestInfo> GetLauncherManifestDataAsync()
    {
        var launcher = await Client.AccountPublicService.GetAccessTokenAsync(GrantType.ClientCredentials, ClientToken.LauncherAppClient2);
        if (!launcher.IsSuccessful) return null;
        var request = new RestRequest("/launcher/api/public/assets/v2/platform/Windows/launcher/label/Live-Firebrand", Method.Post);
        var response = await ExecuteAsync<object>(request, true, accessToken: launcher.Data.AccessToken);
        return response.IsSuccessful && response.Data != null ? new ManifestInfo(JsonConvert.SerializeObject(response.Data), 1) : null;
    }
}