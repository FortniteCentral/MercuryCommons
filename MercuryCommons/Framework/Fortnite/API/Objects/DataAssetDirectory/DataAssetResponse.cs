using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.DataAssetDirectory;

public class DataAssetResponse
{
    [J] public DataAssetMeta Meta { get; set; }
    [J] public Dictionary<string, JToken> AssetData { get; set; }
}

public class DataAssetMeta
{
    [J] public int Revision { get; set; }
    [J] public int HeadRevision { get; set; }
    [J] public DateTime RevisedAt { get; set; }
    [J] public int Promotion { get; set; }
    [J] public DateTime PromotedAt { get; set; }
}