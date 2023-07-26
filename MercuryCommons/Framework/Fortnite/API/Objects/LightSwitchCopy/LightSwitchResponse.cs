using System;
using System.ComponentModel;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.LightSwitch;

public class LightSwitchResponse
{
    [J] public string ServiceInstanceId { get; set; }
    [J] public string Status { get; set; }
    [J] public string Message { get; set; }
    [J] public string MaintenanceUri { get; set; }
    [J] public string[] OverrideCatalogIds { get; set; }
    [J] public string[] AllowedActions { get; set; }
    [J] public bool Banned { get; set; }
    [J] public LauncherInfo LauncherInfoDto { get; set; }
    
    public LightSwitchStatus StatusEnum => Enum.Parse<LightSwitchStatus>(Status);
}

public enum LightSwitchStatus
{
    [Description("UP")]
    Up,
    [Description("DOWN")]
    Down,
    [Description("UNKNOWN")]
    Unknown,
    [Description("FAILED")]
    Failed,
    [Description("RESTRICTED")]
    Restricted
}