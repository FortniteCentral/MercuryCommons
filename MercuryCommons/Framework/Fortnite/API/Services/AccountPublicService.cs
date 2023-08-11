using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Exceptions;
using MercuryCommons.Framework.Fortnite.API.Objects;
using MercuryCommons.Framework.Fortnite.API.Objects.Auth;
using MercuryCommons.Utilities.Extensions;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API.Services;

public class AccountPublicService(FortniteApiClient client, EEnvironment environment) : BaseService(client, environment)
{
    public override string BaseUrl => "https://account-public-service-prod.ol.epicgames.com";
    public override string StageUrl => "https://account-public-service-stage.ol.epicgames.com";

    /// <summary>
    /// Authenticates with <paramref name="grantType"/>
    /// </summary>
    /// <param name="grantType"><see cref="GrantType"/></param>
    /// <param name="token">Client token, if null it will use the one provided in the client builder.</param>
    /// <param name="fields">The fields for the request</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse<AuthResponse>> GetAccessTokenAsync(
        GrantType grantType,
        ClientToken token = null,
        params (string Key, string value)[] fields)
    {
        var response = await GetAccessTokenAsync(grantType, token, default, fields).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Authenticates with <paramref name="grantType"/>
    /// </summary>
    /// <param name="grantType"><see cref="GrantType"/></param>
    /// <param name="token">Client token, if null it will use the one provided in the client builder.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="fields">The fields for the request</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse<AuthResponse>> GetAccessTokenAsync(
        GrantType grantType,
        ClientToken token = null,
        CancellationToken cancellationToken = default,
        params (string Key, string value)[] fields)
    {
        var request = new RestRequest("/account/api/oauth/token", Method.Post);
        request.AddHeader("Authorization", $"basic {token?.Base64 ?? Client.DefaultClientToken.Base64}");
        request.AddParameter("grant_type", grantType.GetDescription());

        foreach (var (k, v) in fields)
        {
            request.AddParameter(k, v);
        }

        var response = await ExecuteAsync<AuthResponse>(request, token: cancellationToken, requiresLogin: false).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Refreshes the access token
    /// </summary>
    /// <param name="refreshToken">Refresh token</param>
    /// <param name="clientToken">Client token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<FortniteResponse<AuthResponse>> RefreshAccessTokenAsync(
        string refreshToken,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        refreshToken.NotNullOrEmpty(nameof(refreshToken));
        var response = await GetAccessTokenAsync(GrantType.RefreshToken, clientToken, cancellationToken, ("refresh_token", refreshToken)).ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc cref="RefreshAccessTokenAsync(string,ClientToken,System.Threading.CancellationToken)"/>
    public async Task<FortniteResponse<AuthResponse>> RefreshAccessTokenAsync(
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        var response = await RefreshAccessTokenAsync(Client.CurrentLogin.RefreshToken, clientToken, cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Authenticates with an authorization code.
    /// </summary>
    /// <param name="code">Authorization code</param>
    /// <param name="clientToken">The client token, this needs to be the same client for the code.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication response</returns>
    public async Task<FortniteResponse<AuthResponse>> AuthWithAuthorizationCodeAsync(
        string code = null,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        code.NotNullOrEmpty(nameof(code));
        var response = await GetAccessTokenAsync(GrantType.AuthorizationCode, clientToken, cancellationToken, ("code", code)).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Gets an exchange code
    /// </summary>
    /// <param name="authResponse">Authentication response</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Exchange code response</returns>
    public async Task<FortniteResponse<ExchangeCode>> GetExchangeAsync(
        AuthResponse authResponse,
        CancellationToken cancellationToken = default)
        => await GetExchangeAsync(authResponse.AccessToken, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc cref="GetExchangeAsync(AuthResponse,System.Threading.CancellationToken)"/>
    public async Task<FortniteResponse<ExchangeCode>> GetExchangeAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        accessToken.NotNullOrEmpty(nameof(accessToken));

        var request = new RestRequest("/account/api/oauth/exchange");
        request.AddHeader("Authorization", $"bearer {accessToken}");
        var response = await ExecuteAsync<ExchangeCode>(request, token: cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Authenticates with an exchange code
    /// </summary>
    /// <param name="exchangeCode">Exchange code</param>
    /// <param name="clientToken">WebsocketClient token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication response</returns>
    public async Task<FortniteResponse<AuthResponse>> AuthWithExchangeAsync(
        ExchangeCode exchangeCode,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
        => await AuthWithExchangeAsync(exchangeCode.Code, clientToken, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc cref="AuthWithExchangeAsync(ExchangeCode,ClientToken,System.Threading.CancellationToken)"/>
    public async Task<FortniteResponse<AuthResponse>> AuthWithExchangeAsync(
        string exchangeCode,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        exchangeCode.NotNullOrEmpty(nameof(exchangeCode));
        var response = await GetAccessTokenAsync(GrantType.ExchangeCode, clientToken, cancellationToken, ("exchange_code", exchangeCode)).ConfigureAwait(false);
        return response;
    }

    public async Task<FortniteResponse<AuthResponse>> AuthWithPasswordAsync(
        string username,
        string password,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        username.NotNullOrEmpty(nameof(username));
        password.NotNullOrEmpty(nameof(password));
        var response = await GetAccessTokenAsync(GrantType.Password, clientToken, cancellationToken, ("username", username), ("password", password)).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Creates a device.
    /// </summary>
    /// <param name="authResponse">Authentication response</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The device that has been created.</returns>
    public async Task<FortniteResponse<Device>> CreateDeviceAsync(
        AuthResponse authResponse,
        CancellationToken cancellationToken = default) => await CreateDeviceAsync(authResponse.AccessToken, authResponse.AccountId, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc cref="CreateDeviceAsync(AuthResponse,System.Threading.CancellationToken)" />
    /// <param name="accessToken">Access token</param>
    /// <param name="accountId">Id of the account</param>
    /// <param name="cancellationToken"></param>
    public async Task<FortniteResponse<Device>> CreateDeviceAsync(
        string accessToken,
        string accountId,
        CancellationToken cancellationToken = default)
    {
        accessToken.NotNullOrEmpty(nameof(accessToken));
        accountId.NotNullOrEmpty(nameof(accountId));

        var request = new RestRequest($"/account/api/public/account/{accountId}/deviceAuth", Method.Post);
        request.AddHeader("Authorization", $"bearer {accessToken}");
        var response = await ExecuteAsync<Device>(request, token: cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Authenticates with the device provided.
    /// </summary>
    /// <param name="device">Device</param>
    /// <param name="clientToken">WebsocketClient token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication response</returns>
    public async Task<FortniteResponse<AuthResponse>> AuthWithDeviceAsync(
        Device device,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default) => await AuthWithDeviceAsync(device.AccountId, device.DeviceId, device.Secret, clientToken, cancellationToken).ConfigureAwait(false);

    /// <inheritdoc cref="AuthWithDeviceAsync(Device,ClientToken,System.Threading.CancellationToken)"/>
    /// <param name="accountId"></param>
    /// <param name="deviceId"></param>
    /// <param name="secret"></param>
    /// <param name="clientToken">WebsocketClient token</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task<FortniteResponse<AuthResponse>> AuthWithDeviceAsync(
        string accountId,
        string deviceId,
        string secret,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        accountId.NotNullOrEmpty(nameof(accountId));
        deviceId.NotNullOrEmpty(nameof(deviceId));
        secret.NotNullOrEmpty(nameof(secret));

        var response = await GetAccessTokenAsync(GrantType.DeviceAuth, clientToken, cancellationToken, ("account_id", accountId), ("device_id", deviceId), ("secret", secret)).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Gets all the device auths
    /// </summary>
    /// <param name="device">Device</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse<Device[]>> GetDeviceAuthsAsync(
        Device device,
        CancellationToken cancellationToken = default)
    {
        device.NotNull(nameof(device));
        var response = await GetDeviceAuthsAsync(device.AccountId, cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc cref="GetDeviceAuthsAsync(Device,System.Threading.CancellationToken)"/>
    /// <param name="accountId">Id of the account</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task<FortniteResponse<Device[]>> GetDeviceAuthsAsync(
        string accountId,
        CancellationToken cancellationToken = default)
    {
        accountId.NotNullOrEmpty(nameof(accountId));
        var request = new RestRequest($"/account/api/public/account/{accountId}/deviceAuth");
        var response = await ExecuteAsync<Device[]>(request, true, token: cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc cref="GetDeviceAuthsAsync(Device,System.Threading.CancellationToken)"/>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task<FortniteResponse<Device[]>> GetDeviceAuthsAsync(
        CancellationToken cancellationToken = default)
    {
        if (!Client.IsLoggedIn || Client.CurrentLogin == null) throw new FortniteException("You need to be logged in to use this.");
        var response = await GetDeviceAuthsAsync(Client.CurrentLogin.AccountId, cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Gets the device auth
    /// </summary>
    /// <param name="device">The device</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse<Device>> GetDeviceAuthAsync(
        Device device,
        CancellationToken cancellationToken = default)
    {
        device.NotNull(nameof(device));
        var response = await GetDeviceAuthAsync(device.AccountId, device.DeviceId, cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc cref="GetDeviceAuthAsync(Device,System.Threading.CancellationToken)"/>
    /// <param name="accountId">The account id</param>
    /// <param name="deviceId">The device id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    public async Task<FortniteResponse<Device>> GetDeviceAuthAsync(
        string accountId,
        string deviceId,
        CancellationToken cancellationToken = default)
    {
        accountId.NotNull(nameof(accountId));
        deviceId.NotNull(nameof(deviceId));

        var request = new RestRequest($"/account/api/public/account/{accountId}/deviceAuth/{deviceId}");
        var response = await ExecuteAsync<Device>(request, true, token: cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Deletes the device auth
    /// </summary>
    /// <param name="device">The device</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse<Device>> DeleteDeviceAuthAsync(
        Device device,
        CancellationToken cancellationToken = default)
    {
        device.NotNull(nameof(device));
        var response = await DeleteDeviceAuthAsync(device.AccountId, device.DeviceId, cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <inheritdoc cref="DeleteDeviceAuthAsync(Device,System.Threading.CancellationToken)"/>
    /// <param name="accountId">The account id</param>
    /// <param name="deviceId">The device id</param>
    /// <param name="cancellationToken">The cancellation token</param>
    public async Task<FortniteResponse<Device>> DeleteDeviceAuthAsync(
        string accountId,
        string deviceId,
        CancellationToken cancellationToken = default)
    {
        accountId.NotNull(nameof(accountId));
        deviceId.NotNull(nameof(deviceId));

        var request = new RestRequest($"/account/api/public/account/{accountId}/deviceAuth/{deviceId}", Method.Delete);
        var response = await ExecuteAsync<Device>(request, true, token: cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Kills sessions
    /// </summary>
    /// <param name="killType">The kill type</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse> KillSessionsAsync(
        SessionKillType killType,
        CancellationToken cancellationToken = default)
    {
        var request = new RestRequest($"/account/api/oauth/sessions/kill?killType={killType}", Method.Delete);
        var response = await ExecuteAsync(request, true, cancellationToken).ConfigureAwait(false);

        if (killType != SessionKillType.ALL) return response;
        Client.IsLoggedIn = false;
        Client.CurrentLogin = null;
        return response;
    }

    /// <summary>
    /// Kills a session
    /// </summary>
    /// <param name="accessToken">The AccessToken from the session to kill</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse> KillSessionAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        accessToken.NotNullOrEmpty(nameof(accessToken));

        var request = new RestRequest($"/account/api/oauth/sessions/kill/{accessToken}", Method.Delete);
        var response = await ExecuteAsync(request, true, cancellationToken).ConfigureAwait(false);
        return response;
    }

    /// <summary>
    /// Kills the current session
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse> KillCurrentSessionAsync(CancellationToken cancellationToken = default)
    {
        if (!Client.IsLoggedIn || Client.CurrentLogin == null) throw new InvalidOperationException("Tried killing the current session but the client was not logged in.");
        var response = await KillSessionAsync(Client.CurrentLogin.AccessToken, cancellationToken).ConfigureAwait(false);
        Client.IsLoggedIn = false;
        Client.CurrentLogin = null;
        return response;
    }

    /// <summary>
    /// Gets all the SSO domains
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Fortnite response</returns>
    public async Task<FortniteResponse<List<string>>> QuerySsoDomainsAsync(CancellationToken cancellationToken = default)
    {
        var request = new RestRequest("/account/api/epicdomains/ssodomains");
        var response = await ExecuteAsync<List<string>>(request, token: cancellationToken).ConfigureAwait(false);
        return response;
    }
}