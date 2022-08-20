using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Lightswitch;

public class LauncherInfo
{
    [J] public string AppName { get; set; }
    [J] public string CatalogItemId { get; set; }
    [J] public string Namespace { get; set; }
}