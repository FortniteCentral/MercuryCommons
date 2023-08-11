using System;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;

public class CatalogResponse
{
    [J] public int RefreshIntervalHrs { get; set; }
    [J] public int DailyPurchaseHrs { get; set; }
    [J] public DateTime Expiration { get; set; }
    [J] public StoreFront[] StoreFronts { get; set; }
}

public class StoreFront
{
    [J] public string Name { get; set; }
    [J] public CatalogEntry[] CatalogEntries { get; set; }
}

public class CatalogEntry
{
    [J] public string OfferId { get; set; }
    [J] public string DevName { get; set; }
    [J] public string OfferType { get; set; }
    [J] public CatalogEntryPrice[] Prices { get; set; }
    [J] public CatalogEntryBundleInfo DynamicBundleInfo { get; set; }
    [J] public string[] Categories { get; set; }
    [J] public int DailyLimit { get; set; }
    [J] public int WeeklyLimit { get; set; }
    [J] public int MonthlyLimit { get; set; }
    [J] public string[] AppStoreId { get; set; }
    [J] public CatalogEntryRequirement[] Requirements { get; set; }
    [J] public CatalogEntryMeta[] MetaInfo { get; set; }
    [J] public string CatalogGroup { get; set; }
    [J] public int CatalogGroupPriority { get; set; }
    [J] public int SortPriority { get; set; }
    [J] public string Title { get; set; }
    [J] public string ShortDescription { get; set; }
    [J] public string Description { get; set; }
    [J] public string DisplayAssetPath { get; set; }
    [J] public FortItemStack[] ItemGrants { get; set; }
    [J] public string[] FulfillmentIds { get; set; }
    [J] public CatalogMeta Meta { get; set; }
    [J] public string MatchFilter { get; set; }
    [J] public double FilterWeight { get; set; }
    [J] public GiftInfo GiftInfo { get; set; }
    [J] public bool Refundable { get; set; }
}

public class CatalogEntryRequirement
{
    [J] public string RequirementType { get; set; }
    [J] public string RequiredId { get; set; }
    [J] public int MinQuantity { get; set; }
}

public class CatalogEntryPrice
{
    [J] public string CurrencyType { get; set; }
    [J] public string CurrencySubType { get; set; }
    [J] public int RegularPrice { get; set; }
    [J] public int DynamicRegularPrice { get; set; }
    [J] public int FinalPrice { get; set; }
    [J] public DateTime SaleExpiration { get; set; }
    [J] public int BasePrice { get; set; }
}

public class CatalogEntryBundleInfo
{
    [J] public int DiscountedBasePrice { get; set; }
    [J] public CatalogEntryBundleItem[] BundleItems { get; set; }
}

public class CatalogEntryBundleItem
{
    [J] public int RegularPrice { get; set; }
    [J] public FortItemStack Item { get; set; }
}

public class CatalogEntryMeta
{
    [J] public string Key { get; set; }
    [J] public string Value { get; set; }
}

public class CatalogMeta
{
    [J] public string NewDisplayAssetPath { get; set; }
    [J] public string SectionId { get; set; }
    [J] public string TileSize { get; set; }
    [J] public string AnalyticOfferGroupId { get; set; }
}

public class FortItemStack
{
    [J] public int Quantity { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string TemplateId { get; set; }
}

public class GiftInfo
{
    [J("bIsEnabled")] public bool IsEnabled { get; set; }
    [J] public string ForcedGiftBoxTemplateId { get; set; }
    [J] public string[] PurchaseRequirements { get; set; }
    [J] public string[] GiftRecordIds { get; set; }
}