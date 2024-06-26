﻿using System.Threading.Tasks;
using EpicManifestParser.Api;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects.Auth;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class LauncherPublicService(FortniteApiClient client, EEnvironment environment) : BaseService(client, environment)
{
    public override string BaseUrl => "https://launcher-public-service-prod06.ol.epicgames.com";
    public override string StageUrl => "https://launcher-public-service-stage.ol.epicgames.com";

    public async Task<ManifestInfo> GetGameManifestAsync((string, string, string, string) items, string label = "Live", AuthResponse auth = null)
        => await GetGameManifestAsync(items.Item1, items.Item2, items.Item3, items.Item4, label, auth);

    /// <summary>
    /// Gets the manifest information for the specified game
    /// </summary>
    /// <param name="appId">App ID of game</param>
    /// <param name="catalogId">Catalog ID of game</param>
    /// <param name="namespace">EGS Namespace of game</param>
    /// <param name="platform">Platform of game</param>
    /// <param name="label">Version label of game</param>
    /// <param name="auth">Auth response for entitlements</param>
    /// <returns>Manifest info</returns>
    public async Task<ManifestInfo> GetGameManifestAsync(string appId, string catalogId, string @namespace, string platform = "Windows", string label = "Live", AuthResponse auth = null)
    {
        var authResponse = auth ?? (await Client.AccountPublicService.GetAccessTokenAsync(GrantType.ClientCredentials, ClientToken.LauncherAppClient2)).Data;
        var request = new RestRequest($"/launcher/api/public/assets/v2/platform/{platform}/namespace/{@namespace}/catalogItem/{catalogId}/app/{appId}/label/{label}", Method.Post);
        if (@namespace == "fn" && platform is "Android" or "IOS" && catalogId == "4fe75bbc5a674f4f9b356b5c90567da5") request.AddJsonBody(new { abis = new[] { "arm64-v8a" } }); // Manual
        var response = await DownloadFile(request, true, accessToken: authResponse.AccessToken, requiresLogin: false);
        return response != null ? ManifestInfo.Deserialize(response) : null;
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
        var response = await DownloadFile(request, true, accessToken: launcher.Data.AccessToken, requiresLogin: false);
        return response != null ? ManifestInfo.Deserialize(response) : null;
    }
}