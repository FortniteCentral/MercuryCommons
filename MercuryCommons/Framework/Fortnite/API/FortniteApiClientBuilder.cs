using System;
using MercuryCommons.Framework.Fortnite.API.Config;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Objects.Auth;
using Quartz;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API;

/// <summary>
/// Used to create the fortnite api client.
/// </summary>
public class FortniteApiClientBuilder
{
    private readonly AuthConfig _authConfig = new();

    private Action<RestClient> _restClientAction;
    private string _userAgent;
    private ClientToken _clientToken;
    private IScheduler _scheduler;

    /// <summary>
    /// Sets the default user agent of the http client.
    /// </summary>
    /// <param name="userAgent">User agent</param>
    /// <returns></returns>
    public FortniteApiClientBuilder WithUserAgent(string userAgent)
    {
        _userAgent = userAgent;
        return this;
    }

    /// <summary>
    /// Sets the authorization code.
    /// </summary>
    /// <param name="authorizationCode">Authorization code</param>
    /// <returns>Client builder</returns>
    public FortniteApiClientBuilder WithAuthorizationCode(string authorizationCode)
    {
        _authConfig.AuthorizationCode = authorizationCode;
        return this;
    }

    /// <summary>
    /// Configures the rest client.
    /// </summary>
    /// <param name="restClientAction">Action for the rest client</param>
    /// <returns>Client builder</returns>
    public FortniteApiClientBuilder ConfigureRestClient(Action<RestClient> restClientAction)
    {
        _restClientAction = restClientAction;
        return this;
    }

    /// <summary>
    /// Sets the client token for the fortnite client. (The default client token is <see cref="ClientToken.FortniteAndroidGameClient"/>
    /// </summary>
    /// <param name="clientToken">WebsocketClient token which you can get from <seealso cref="ClientToken"/> or your own. A client token is 'ClientId:Secret' encoded in base64.</param>
    /// <returns>Client builder</returns>
    public FortniteApiClientBuilder WithDefaultClientToken(ClientToken clientToken)
    {
        _clientToken = clientToken;
        return this;
    }

    /// <summary>
    /// Configures the username and password
    /// </summary>
    /// <param name="username">Username to login with</param>
    /// <param name="password">Password to login with</param>
    /// <returns>Client builder</returns>
    public FortniteApiClientBuilder WithUserPassword(string username, string password)
    {
        _authConfig.UserPassword = (username, password);
        return this;
    }
    
    /// <summary>
    /// Sets the device.
    /// </summary>
    /// <param name="accountId">Id of the account</param>
    /// <param name="deviceId">Id of the device</param>
    /// <param name="secret">Secret</param>
    /// <returns>Client builder</returns>
    public FortniteApiClientBuilder WithDevice(
        string accountId,
        string deviceId,
        string secret)
    {
        return WithDevice(new Device(accountId, deviceId, secret));
    }

    /// <inheritdoc cref="WithDevice(string,string,string)"/>
    /// <param name="device">Device</param>
    public FortniteApiClientBuilder WithDevice(Device device)
    {
        _authConfig.Device = device;
        return this;
    }

    /// <summary>
    /// The way the client should handel refreshes.
    /// </summary>
    /// <param name="refreshType">The refresh type</param>
    /// <returns></returns>
    public FortniteApiClientBuilder WithRefresh(RefreshType refreshType)
    {
        _authConfig.RefreshType = refreshType;
        return this;
    }

    /// <summary>
    /// Scheduler for the access token refreshes
    /// </summary>
    /// <param name="scheduler">Scheduler</param>
    /// <returns></returns>
    public FortniteApiClientBuilder WithScheduler(IScheduler scheduler)
    {
        _scheduler = scheduler;
        return this;
    }

    /// <summary>
    /// Creates the api client.
    /// </summary>
    /// <returns>Api client</returns>
    public FortniteApiClient Create()
    {
        return new FortniteApiClient(
            _authConfig, _restClientAction,
            _userAgent, _clientToken ?? ClientToken.FortniteAndroidGameClient,
            _scheduler);
    }
}