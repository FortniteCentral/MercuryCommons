using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Auth;

public class DeviceLocation
{
    [J] public string Location { get; set; }
    [J] public string IpAddress { get; set; }
    [J] public DateTime DateTime { get; set; }
}