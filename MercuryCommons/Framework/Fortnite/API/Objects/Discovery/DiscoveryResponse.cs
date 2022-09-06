using System;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Discovery;

public class DiscoveryResponse
{
    [J] public FortniteDiscoveryPanel[] Panels { get; set; }
}

public class FortniteDiscoveryPanel
{
    [J] public string PanelName { get; set; }
    [J] public FortniteDiscoveryPage[] Pages { get; set; }
}

public class FortniteDiscoveryPage
{
    [J] public FortniteDiscoveryPageResult[] Results { get; set; }
    [J] public bool HasMore { get; set; }
}

public class FortniteDiscoveryPageResult
{
    [J] public DateTime LastVisited { get; set; }
    [J] public string LinkCode { get; set; }
    [J] public bool IsFavorite { get; set; }
}