using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Launcher;

public class AppManifestItem
{
    [J] public string Signature { get; set; }
    [J] public string Distribution { get; set; }
    [J] public string Path { get; set; }
    [J] public string Hash { get; set; }
    [J] public List<string> AdditionalDistributions { get; set; }
}