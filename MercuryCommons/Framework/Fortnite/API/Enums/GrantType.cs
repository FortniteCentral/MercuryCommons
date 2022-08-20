using System.ComponentModel;

namespace MercuryCommons.Framework.Fortnite.API.Enums;

public enum GrantType
{
    [Description("authorization_code")]
    AuthorizationCode,
    [Description("client_credentials")]
    ClientCredentials,
    [Description("device_code")]
    DeviceCode,
    [Description("device_auth")]
    DeviceAuth,
    [Description("exchange_code")]
    ExchangeCode,
    [Description("external_auth")]
    ExternalAuth,
    [Description("opt")]
    Opt,
    [Description("password")]
    Password,
    [Description("refresh_token")]
    RefreshToken,
    [Description("token_to_token")]
    TokenToToken
}