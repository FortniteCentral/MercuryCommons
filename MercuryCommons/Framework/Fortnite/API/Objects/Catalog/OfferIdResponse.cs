using System;
using System.Collections.Generic;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using E = Newtonsoft.Json.JsonExtensionDataAttribute;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Catalog;

public class OfferIdInfo
{
    [J] public string Id { get; set; }
    [J] public string Title { get; set; }
    [J] public string Description { get; set; }
    [J] public string LongDescription { get; set; }
    [J] public OfferKeyImage[] KeyImages { get; set; }
    [J] public Dictionary<string, string>[] Categories { get; set; }
    [J] public string Namespace { get; set; }
    [J] public string Status { get; set; }
    [J] public DateTime CreationDate { get; set; }
    [J] public DateTime LastModifiedDate { get; set; }
    [J] public Dictionary<string, CustomAttribute> CustomAttributes { get; set; }
    [J] public string InternalName { get; set; }
    [J] public string Recurrence { get; set; }
    [J] public ItemIdInfo[] Items { get; set; }
    [J] public string CurrencyCode { get; set; }
    [J] public int CurrentPrice { get; set; }
    [J] public int Price { get; set; }
    [J] public int BasePrice { get; set; }
    [J] public string BasePriceCurrencyCode { get; set; }
    [J] public int RecurringPrice { get; set; }
    [J] public int FreeDays { get; set; }
    [J] public int MaxBillingCycles { get; set; }
    // TODO Seller info
    [J] public DateTime ViewableDate { get; set; }
    [J] public DateTime EffectiveDate { get; set; }
    [J] public DateTime ExpiryDate { get; set; }
    [J] public bool VATIncluded { get; set; }
    [J] public bool IsCodeRedemptionOnly { get; set; }
    [J] public DateTime ReleaseDate { get; set; }
}

public class OfferKeyImage
{
    [J] public string Type { get; set; }
    [J] public string Url { get; set; }
    [J] public string MD5 { get; set; }
    [J] public int Width { get; set; }
    [J] public int Height { get; set; }
    [J] public long Size { get; set; }
    [J] public DateTime UploadedDate { get; set; }
}

