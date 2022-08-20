using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Auth;

public class ExchangeCode
{
    [J] public int ExpiresInSeconds { get; set; }
    [J] public string Code { get; set; }
    [J] public string CreatingClientId { get; set; }
}