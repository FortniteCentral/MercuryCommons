using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MercuryCommons.Framework.Fortnite.API.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EPlatform
{
    Unknown = -1,

    [Description("Windows")]
    Windows,
    [Description("Android")]
    Android,
    [Description("MacOS")]
    Mac,
    [Description("iOS")]
    IOS,
    [Description("Xbox One")]
    XboxOneGDK,
    [Description("Xbox Series X|S")]
    XSX,
    [Description("PlayStation 4")]
    PS4,
    [Description("PlayStation 5")]
    PS5,
    [Description("GeForce NOW (Mobile)")]
    GFNMobile,
    [Description("GeForce NOW")]
    GFN,
    [Description("Xbox Cloud Gaming (Mobile)")]
    HeliosMobile,
    [Description("Xbox Cloud Gaming")]
    Helios,
    [Description("Amazon Luna")]
    Luna,
    [Description("Amazon Luna (Mobile)")]
    LunaMobile,
    [Description("Nintendo Switch")]
    Switch,

    [Description("UEFN")]
    UEFN
}