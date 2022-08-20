using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;

public class DiscoveryResponse
{
    [J] public FortniteDiscoveryPanel[] Panels;
}

public class FortniteDiscoveryPanel
{
    [J] public string PanelName;
    [J] public FortniteDiscoveryPage[] Pages;
}

public class FortniteDiscoveryPage
{
    [J] public FortniteDiscoveryPageResult[] Results;
}

public class FortniteDiscoveryPageResult
{
    [J] public FortniteDiscoveryPageLinkData LinkData;
}

public class FortniteDiscoveryPageLinkData
{
    [J] public string Mnemonic;
    [J] public string LinkType;
    [J] public FortniteDiscoveryPageMetadata Metadata;
}

public class FortniteDiscoveryPageMetadata
{
    [J("image_url")] public string ImageUrl;
}