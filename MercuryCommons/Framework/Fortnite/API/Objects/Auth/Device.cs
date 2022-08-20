using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Auth;

public class Device
{
    [J] public string DeviceId { get; set; }
    [J] public string AccountId { get; set; }
    [J] public string Secret { get; set; }
    [J] public string UserAgent { get; set; }
    [J] public DeviceLocation Created { get; set; }
    [J] public DeviceLocation LastAccess { get; set; }

    public Device(string deviceId, string accountId, string secret)
    {
        DeviceId = deviceId;
        AccountId = accountId;
        Secret = secret;
    }
}