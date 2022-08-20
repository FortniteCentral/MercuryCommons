using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Lightswitch;

public class LightswitchResponse
{
    [J] public string ServiceInstanceId { get; set; }
    [J] public string Status { get; set; }
    [J] public string Message { get; set; }
    [J] public string MaintenanceUri { get; set; }
    [J] public string[] OverrideCatalogIds { get; set; }
    [J] public string[] AllowedActions { get; set; }
    [J] public bool Banned { get; set; }
    [J] public LauncherInfo LauncherInfoDto { get; set; }
}