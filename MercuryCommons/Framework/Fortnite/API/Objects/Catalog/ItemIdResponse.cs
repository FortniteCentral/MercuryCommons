using System.Collections.Generic;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Catalog;

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