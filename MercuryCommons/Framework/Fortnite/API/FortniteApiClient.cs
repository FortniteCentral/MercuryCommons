using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MercuryCommons.Framework.Data.Remote;
using MercuryCommons.Framework.Fortnite.API.Config;
using MercuryCommons.Framework.Fortnite.API.Enums;
using MercuryCommons.Framework.Fortnite.API.Exceptions;
using MercuryCommons.Framework.Fortnite.API.Jobs;
using MercuryCommons.Framework.Fortnite.API.Objects.Auth;
using MercuryCommons.Framework.Fortnite.API.Services;
using Quartz;
using Quartz.Impl;
using RestSharp;

namespace MercuryCommons.Framework.Fortnite.API;

/// <summary>
/// The Fortnite Api Client
/// It's recommended to dispose the client when you are done since it kills the current session.
/// If this is not disposed, you may have too many access tokens active and you will need to wait a few hours
/// to use the api again.
/// </summary>
public class FortniteApiClient : IAsyncDisposable
{
    public AuthConfig AuthConfig { get; set; }
    internal ClientToken DefaultClientToken { get; set; }

    /// <summary>
    /// Fired on Authentication. (This will not fire on refresh)
    /// </summary>
    public event Func<AuthResponse, Task> Login;

    /// <summary>
    /// Fired on refresh.
    /// </summary>
    public event Func<AuthResponse, Task> Refresh;

    /// <summary>
    /// The current authentication session
    /// </summary>
    public AuthResponse CurrentLogin { get; set; }

    /// <summary>
    /// If the user is logged in
    /// </summary>
    public bool IsLoggedIn { get; set; }

    /// <summary>
    /// Contains some of the links endpoints.
    /// </summary>
    private readonly LinksPublicService _linksPublicService;
    public LinksPublicService LinksPublicService
    {
        get
        {
            VerifyLogin();
            return _linksPublicService;
        }
        private init => _linksPublicService = value;
    }

    /// <summary>
    /// Contains all of the lightswitch endpoints.
    /// </summary>
    private readonly LightSwitchPublicService _lightSwitchPublicService;
    public LightSwitchPublicService LightSwitchPublicService
    {
        get
        {
            VerifyLogin();
            return _lightSwitchPublicService;
        }
        private init => _lightSwitchPublicService = value;
    }

    /// <summary>
    /// Contains most/all of the main fortnite endpoints.
    /// </summary>
    private readonly FortnitePublicService _fortnitePublicService;
    public FortnitePublicService FortnitePublicService
    {
        get
        {
            VerifyLogin();
            return _fortnitePublicService;
        }
        private init => _fortnitePublicService = value;
    }

    /// <summary>
    /// Contains most/all of the catalog endpoints.
    /// </summary>
    private readonly CatalogPublicService _catalogPublicService;
    public CatalogPublicService CatalogPublicService
    {
        get
        {
            VerifyLogin();
            return _catalogPublicService;
        }
        private init => _catalogPublicService = value;
    }

    /// <summary>
    /// Contains most/all of the launcher endpoints.
    /// </summary>
    private readonly LauncherPublicService _launcherPublicService;
    public LauncherPublicService LauncherPublicService
    {
        get
        {
            VerifyLogin();
            return _launcherPublicService;
        }
        private init => _launcherPublicService = value;
    }

    private DiscoveryService _discoveryService;
    public DiscoveryService DiscoveryService
    {
        get
        {
            VerifyLogin();
            return _discoveryService;
        }
        set => _discoveryService = value;
    }

    private GraphQLService _graphQlService;
    public GraphQLService GraphQlService
    {
        get => _graphQlService;
        set => _graphQlService = value;
    }

    private DataAssetDirectoryService _dataAssetDirectoryService;
    public DataAssetDirectoryService DataAssetDirectoryService
    {
        get
        {
            VerifyLogin();
            return _dataAssetDirectoryService;
        }
        set => _dataAssetDirectoryService = value;
    }

    private EventsPublicService _eventsPublicService;

    public EventsPublicService EventsPublicService
    {
        get
        {
            VerifyLogin();
            return _eventsPublicService;
        }
        set => _eventsPublicService = value;
    }
    
    /// <summary>
    /// Contains most/all of the account endpoints.
    /// </summary>
    public AccountPublicService AccountPublicService { get; }

    public FortniteContentWebsite ContentWebsite { get; }

    private readonly Action<RestClient> _restClientAction;
    public readonly string _userAgent;

    private IScheduler _scheduler;

    internal FortniteApiClient(
        AuthConfig authConfig,
        Action<RestClient> restClientActions,
        string userAgent,
        ClientToken defaultClientToken,
        IScheduler scheduler)
    {
        _restClientAction = restClientActions;
        _userAgent = userAgent;
        _scheduler = scheduler;

        AuthConfig = authConfig;
        DefaultClientToken = defaultClientToken;
        LinksPublicService = new LinksPublicService(this, defaultClientToken.Environment);
        LightSwitchPublicService = new LightSwitchPublicService(this, defaultClientToken.Environment);
        FortnitePublicService = new FortnitePublicService(this, defaultClientToken.Environment);
        CatalogPublicService = new CatalogPublicService(this, defaultClientToken.Environment);
        LauncherPublicService = new LauncherPublicService(this, defaultClientToken.Environment);
        DiscoveryService = new DiscoveryService(this, defaultClientToken.Environment);
        AccountPublicService = new AccountPublicService(this, defaultClientToken.Environment);
        ContentWebsite = new FortniteContentWebsite(this, defaultClientToken.Environment);
        GraphQlService = new GraphQLService(this, defaultClientToken.Environment);
        DataAssetDirectoryService = new DataAssetDirectoryService(this, defaultClientToken.Environment);
        EventsPublicService = new EventsPublicService(this, defaultClientToken.Environment);
    }

    /// <summary>
    /// This starts the scheduler to refresh the access token.
    /// </summary>
    /// <returns>The next trigger</returns>
    public async Task<DateTimeOffset> StartRefreshScheduler()
    {
        VerifyLogin();

        if (AuthConfig.RefreshType == RefreshType.Scheduler)
        {
            if (_scheduler == null)
            {
                var schedulerFactory = new StdSchedulerFactory();
                _scheduler = await schedulerFactory.GetScheduler().ConfigureAwait(false);
            }

            IDictionary<string, object> jobDataDictionary = new Dictionary<string, object>
            {
                { "client", this }
            };
            var jobData = new JobDataMap(jobDataDictionary);
            var job = JobBuilder.Create<RefreshAccountJob>()
                .WithIdentity($"RefreshAccountJob{DefaultClientToken.Environment}{DefaultClientToken.ClientId}")
                .SetJobData(jobData)
                .WithDescription("Job to refresh the current session.")
                .Build();

            var interval = CurrentLogin.ExpiresIn;
            var trigger = TriggerBuilder.Create()
                .WithDescription("Trigger to refresh the current session.")
                .StartAt(DateTimeOffset.Now.AddSeconds(interval))
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromSeconds(interval))
                    .RepeatForever())
                .Build();

            await _scheduler.ScheduleJob(job, trigger).ConfigureAwait(false);
            if (!_scheduler.IsStarted)
            {
                await _scheduler.Start().ConfigureAwait(false);
            }

            return trigger.GetNextFireTimeUtc().GetValueOrDefault();
        }

        return default;
    }

    /// <summary>
    /// Logs in with an authorization code, will authenticate with device auth.
    /// </summary>
    /// <param name="code">The authorization code, if null it will get the code given in the api client builder.</param>
    /// <param name="clientToken">WebsocketClient token used for the authorization code, must be the same as the client for the code.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The device that has been created.</returns>
    public async Task<Device> LoginWithAuthorizationCodeAsync(
        string code = null,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        var codeUsed = code ?? AuthConfig.AuthorizationCode;
        if (codeUsed == null)
        {
            throw new ArgumentException(
                "Tried to login with an authorization code, but there was no code specified in the call or the client builder.");
        }

        var authorizationAuth = await AccountPublicService.AuthWithAuthorizationCodeAsync(codeUsed, clientToken, cancellationToken).ConfigureAwait(false);
        if (!authorizationAuth.IsSuccessful)
        {
            throw new FortniteException("Failed to authenticate with the authorization code",
                authorizationAuth.Error);
        }

        var exchangeCode = await AccountPublicService
            .GetExchangeAsync(authorizationAuth.Data, cancellationToken)
            .ConfigureAwait(false);
        if (!exchangeCode.IsSuccessful)
        {
            throw new FortniteException("Failed to get the exchange code.", exchangeCode.Error);
        }

        var exchangeAuth =
            await LoginWithExchangeAsync(exchangeCode.Data, cancellationToken: cancellationToken, fireLogin: false);

        var deviceResponse = await AccountPublicService
            .CreateDeviceAsync(exchangeAuth, cancellationToken)
            .ConfigureAwait(false);
        if (!deviceResponse.IsSuccessful)
        {
            throw new FortniteException("Failed to create a device.", deviceResponse.Error);
        }

        await LoginWithDeviceAsync(deviceResponse.Data, cancellationToken: cancellationToken);
        return deviceResponse.Data;
    }

    /// <summary>
    /// Logs in with the exchange code.
    /// </summary>
    /// <param name="exchangeCode">Exchange code</param>
    /// <param name="clientToken">WebsocketClient token, default is the client token specified in the client builder/</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="fireLogin">If true, it will fire the <see cref="Login"/> event.</param>
    /// <returns>Authentication response</returns>
    public async Task<AuthResponse> LoginWithExchangeAsync(
        ExchangeCode exchangeCode,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default,
        bool fireLogin = false)
    {
        if (exchangeCode == null)
        {
            throw new ArgumentException(
                "Tried to login with exchange code, but there was no exchange specified in the call.");
        }

        return await LoginWithExchangeAsync(exchangeCode.Code, clientToken, cancellationToken, fireLogin);
    }

    /// <inheritdoc cref="LoginWithExchangeAsync(ExchangeCode,ClientToken,System.Threading.CancellationToken,bool)"/>
    public async Task<AuthResponse> LoginWithExchangeAsync(
        string exchangeCode = null,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default,
        bool fireLogin = false)
    {
        var exchangeCodeUsed = exchangeCode ?? AuthConfig.ExchangeCode;
        if (exchangeCodeUsed == null)
        {
            throw new ArgumentException(
                "Tried to login with exchange code, but there was no exchange specified in the call or the client builder.");
        }

        var response = await AccountPublicService
            .AuthWithExchangeAsync(exchangeCodeUsed, clientToken, cancellationToken)
            .ConfigureAwait(false);
        if (!response.IsSuccessful)
        {
            throw new FortniteException("Failed to authenticate with the exchange code.", response.Error);
        }

        var responseData = response.Data;
        if (fireLogin)
        {
            await OnLoginAsync(responseData);
        }

        return responseData;
    }

    /// <summary>
    /// Logs in with the device.
    /// </summary>
    /// <param name="device">Device</param>
    /// <param name="clientToken">WebsocketClient token, default is the one specified in the client builder.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<Device> LoginWithDeviceAsync(
        Device device = null,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        var deviceUsed = device ?? AuthConfig.Device;
        if (deviceUsed == null)
        {
            throw new ArgumentException(
                "Tried to login with device, but there was no device specified in the call or the client builder.");
        }

        await LoginWithDeviceAsync(deviceUsed.AccountId, deviceUsed.DeviceId, deviceUsed.Secret, clientToken,
                cancellationToken)
            .ConfigureAwait(false);

        return device;
    }

    /// <inheritdoc cref="LoginWithDeviceAsync(Device,ClientToken,System.Threading.CancellationToken)"/>
    /// <param name="accountId">Id of the account</param>
    /// <param name="deviceId">Id of the device</param>
    /// <param name="secret">Secret</param>
    /// <param name="clientToken">WebsocketClient token, default is the one specified in the client builder.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<AuthResponse> LoginWithDeviceAsync(
        string accountId,
        string deviceId,
        string secret,
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        var response = await AccountPublicService
            .AuthWithDeviceAsync(accountId, deviceId, secret, clientToken, cancellationToken)
            .ConfigureAwait(false);
        if (!response.IsSuccessful)
        {
            throw new FortniteException("Failed to authenticate with device.", response.Error);
        }

        var responseData = response.Data;
        await OnLoginAsync(responseData);

        return responseData;
    }

    public async Task<AuthResponse> LoginWithPassword(
        string username = "",
        string password = "",
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        var response = await AccountPublicService
            .AuthWithPasswordAsync(string.IsNullOrEmpty(username) ? AuthConfig.UserPassword.Item1 : username, string.IsNullOrEmpty(password) ? AuthConfig.UserPassword.Item2 : password, clientToken, cancellationToken)
            .ConfigureAwait(false);
        if (!response.IsSuccessful)
        {
            throw new FortniteException("Failed to authenticate with password.", response.Error);
        }

        var responseData = response.Data;
        await OnLoginAsync(responseData);

        return responseData;
    }

    public async Task<AuthResponse> LoginAsClientCredentials(
        ClientToken clientToken = null,
        CancellationToken cancellationToken = default)
    {
        var response = await AccountPublicService.GetAccessTokenAsync(GrantType.ClientCredentials, clientToken, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessful) throw new FortniteException("Somehow Epic failed to provide a client credentials response", response.Error);

        var responseData = response.Data;
        await OnLoginAsync(responseData);

        return responseData;
    }

    internal RestClient CreateRestClient(BaseService service)
    {
        var restClient = new RestClient(new RestClientOptions(service.UrlToUse) { UserAgent = _userAgent }, configureSerialization: s => s.UseSerializer<JsonNetSerializer>());
        _restClientAction?.Invoke(restClient);

        return restClient;
    }

    internal void VerifyLogin()
    {
        if (!IsLoggedIn)
        {
            throw new FortniteException("You must be logged in to use this.");
        }
    }

    internal async Task<bool> VerifyTokenAsync()
    {
        if (AuthConfig.RefreshType != RefreshType.OnCall)
        {
            return true;
        }

        if (CurrentLogin == null)
        {
            return false;
        }

        if (CurrentLogin.ExpiresAt > DateTime.UtcNow)
        {
            if (CurrentLogin.RefreshExpiresAt < DateTime.UtcNow)
            {
                throw new FortniteException("Couldn't refresh token because the refresh token was expired.");
            }

            var response = await AccountPublicService.RefreshAccessTokenAsync()
                .ConfigureAwait(false);
            if (response.IsSuccessful)
            {
                CurrentLogin = response.Data;
            }

            return response.IsSuccessful;
        }

        return true;
    }

    internal async Task OnLoginAsync(AuthResponse authResponse)
    {
        CurrentLogin = authResponse;
        IsLoggedIn = true;
        if (Login != null)
        {
            await Login.Invoke(authResponse);
        }
    }

    internal async Task OnRefreshAsync(AuthResponse authResponse)
    {
        CurrentLogin = authResponse;
        if (Refresh != null)
        {
            await Refresh.Invoke(authResponse);
        }
    }

    private bool _disposed;

    /// <summary>
    /// Kills the scheduler for refresh and the current session.
    /// If there are too many sessions active on your account you might
    /// not be able to use Epic Games' API for a while.
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        try
        {
            if (_disposed)
            {
                return;
            }

            await AccountPublicService.KillCurrentSessionAsync();
            _disposed = true;
        }
        catch
        {
            // ignored
        }
    }
}