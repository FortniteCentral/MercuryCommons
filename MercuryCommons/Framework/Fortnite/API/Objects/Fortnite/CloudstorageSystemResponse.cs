using System;
using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;

public class CloudstorageSystemResponse
{
    [J] public string UniqueFilename { get; set; }
    [J] public string Filename { get; set; }
    [J] public string Hash { get; set; }
    [J] public string Hash256 { get; set; }
    [J] public int Length { get; set; }
    [J] public string ContentType { get; set; }
    [J] public DateTime Uploaded { get; set; }
    [J] public string StorageType { get; set; }
    [J] public IDictionary<string, string> StorageIds { get; set; }
    [J] public bool DoNotCache { get; set; }
}