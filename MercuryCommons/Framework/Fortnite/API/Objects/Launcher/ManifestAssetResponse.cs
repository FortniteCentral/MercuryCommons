using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Launcher;

public class ManifestAssetResponse
{
    [J] public string AppName { get; set; }
    [J] public string LabelName { get; set; }
    [J] public string BuildVersion { get; set; }
    [J] public string CatalogItemId { get; set; }
    [J] public string Expires { get; set; }
    [J] public string AssetId { get; set; }
    [J] public Dictionary<string, AppManifestItem> Items { get; set; }
}