using System;

namespace MercuryCommons.Framework.Fortnite.API.Objects.GraphQL;

public class GQLCatalogOfferResponse
{
    [J] public string Title { get; set; }
    [J] public string Id { get; set; }
    [J] public string Namespace { get; set; }
    [J] public string Description { get; set; }
    [J] public DateTime EffectiveDate { get; set; }
    [J] public DateTime ExpiryDate { get; set; }
    [J] public DateTime ViewableDate { get; set; }
    [J] public bool AllowPurchaseForPartialOwned { get; set; }
    [J] public string OfferType { get; set; }
    [J] public bool IsCodeRedemptionOnly { get; set; }
}