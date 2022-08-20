using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using E = Newtonsoft.Json.JsonExtensionDataAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Catalog;

public class ItemIdResponse
{
    [J, E] public Dictionary<string, object> OfferIds { get; set; }
}

public class ItemIdInfo
{
    [J] public string Id { get; set; }
    [J] public string Title { get; set; }
    [J] public string Description { get; set; }
    [J] public string LastModifiedDate { get; set; }
    [J] public Dictionary<string, CustomAttribute> CustomAttributes { get; set; }
}

public class CustomAttribute
{
    [J] public string Type { get; set; }
    [J] public string Value { get; set; }
}