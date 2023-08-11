using System;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Auth;

public class BaseOAuth
{
    [J("account_id")] public string AccountId { get; set; }
    [J("client_id")] public string ClientId { get; set; }
    [J("client_service")] public string ClientService { get; set; }

    // Non existent if X-Epic-Device-Id header was not provided
    [J("device_id")] public string DeviceId { get; set; }
    [J("expires_at")] public DateTime ExpiresAt { get; set; }
    [J("expires_in")] public long ExpiresIn { get; set; }
    [J("in_app_id")] public string InAppId { get; set; }
    [J("internal_client")] public bool InternalClient { get; set; }
    [J("lastPasswordValidation")] public DateTime LastPasswordValidation { get; set; }
    [J("token_type")] public string TokenType { get; set; }
    [J] public string DisplayName { get; set; }
}