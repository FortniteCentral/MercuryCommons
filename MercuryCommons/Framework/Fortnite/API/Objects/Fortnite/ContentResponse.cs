using System;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;

public class ContentResponse
{
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
    [J] public FortDynamicBackgrounds DynamicBackgrounds { get; set; }
    [J] public FortCreativeAdFeatures CreativeFeatures { get; set; }
    [J] public FortCreativeAdFeatures CreativeAds { get; set; }
    [J] public FortTournamentInformation TournamentInformation { get; set; }
    [J] public FortRadioStations RadioStations { get; set; }
    [J] public FortSubscription Subscription { get; set; }
    [J] public FortShopCarousel ShopCarousel { get; set; }
    [J] public FortEventScreens EventScreens { get; set; }
    [J] public FortMPItemShop MPItemShop { get; set; }
}

public class FortShopCarousel
{
    [J("_title")] public string Title { get; set; }
    [J] public FortCarouselItemList ItemsList { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortCarouselItemList
{
    [J("_type")] public string Type { get; set; }
    [J] public FortCarouselItem[] Items { get; set; }
}

public class FortCarouselItem
{
    [J] public string TileImage { get; set; }
    [J] public string FullTitle { get; set; }
    [J] public bool Hidden { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public int LandingPriority { get; set; }
    [J] public string Action { get; set; }
    [J] public string OfferId { get; set; }
    [J] public string Title { get; set; }
}

public class FortSubscription
{
    [J] public string SubscriptionDisclaimer { get; set; }
    [J] public string BlockedBenefitsNotice { get; set; }
    [J] public FortPurchaseDetails PurchaseSubscriptionDetails { get; set; }
    [J("_title")] public string Title { get; set; }
    [J] public FortSubscriptionRewards CurrentRewards { get; set; }
    [J] public FortSubscriptionRewards NextRewards { get; set; }
    [J] public FortSubscriptionModals SubModals { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortSubscriptionModals
{
    [J("_type")] public string Type { get; set; }
    [J] public FortSubscriptionModal[] Modals { get; set; }
}

public class FortSubscriptionModal
{
    [J] public string[] Entries { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string ModalId { get; set; }
    [J] public string PlatformId { get; set; }
}

public class FortSubscriptionRewards
{
    [J] public string ColorA { get; set; }
    [J] public string ColorB { get; set; }
    [J] public string ItemShopTileViolatorText { get; set; }
    [J] public string BattlepassDescription { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string ItemShopTileViolatorIntensity { get; set; }
    [J] public string SkinTitle { get; set; }
    [J] public string BattlepassImageUrl { get; set; }
    [J] public string MtxTitle { get; set; }
    [J] public FortItemStack[] CrewPackItems { get; set; }
    [J] public string BattlepassTitle { get; set; }
    [J] public string ItemShopTileImageUrl { get; set; }
    [J] public string SkinImageUrl { get; set; }
    [J] public string ColorC { get; set; }
    [J] public DateTime CrewPackDateOverride { get; set; }
}

public class FortPurchaseDetails
{
    [J] public int BattlepassRefund { get; set; }
    [J] public string BattlepassDescription { get; set; }
    [J] public AdditionalPoints AdditionalBulletPoints { get; set; }
    [J] public string BattlepassTitle { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string MtxDescription { get; set; }
    [J] public string SkinDescription { get; set; }
    [J] public string SkinTitle { get; set; }
    [J] public string MtxTitle { get; set; }

    public class AdditionalPoints
    {
        [J("_type")] public string Type { get; set; }
        [J] public string Description { get; set; }
        [J] public string Title { get; set; }
        [J] public string IncludedCountries { get; set; }
    }
}

public class FortRadioStations
{
    [J("_title")] public string Title { get; set; }
    [J] public FortRadioStationList RadioStationList { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortRadioStationList
{
    [J("_type")] public string Type { get; set; }
    [J] public FortRadioStation[] Stations { get; set; }
}

public class FortRadioStation
{
    [J] public string ResourceId { get; set; }
    [J] public string StationImage { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string Title { get; set; }
}

public class FortCreativeAdFeatures
{
    [J("ad_info")] public FortAdInfo AdInfo { get; set; }
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }

    public class FortAdInfo
    {
        // ???
        [J("_type")] public string Type { get; set; }
    }
}

public class FortTournamentInformation
{
    [J] public FortConversionConfig ConversionConfig { get; set; }
    [J("tournament_info")] public FortTournamentInfo TournamentInfo { get; set; }
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortTournamentInfo
{
    [J] public FortTournament[] Tournaments { get; set; }
    [J("_type")] public string Type { get; set; }
}

public class FortTournament
{
    [J("title_color")] public string TitleColor { get; set; }
    [J("loading_screen_image")] public string LoadingScreenImage { get; set; }
    [J("background_text_color")] public string BackgroundTextColor { get; set; }
    [J("background_right_color")] public string BackgroundRightColor { get; set; }
    [J("poster_back_image")] public string PosterBackImage { get; set; }
    [J("_type")] public string Type { get; set; }
    [J("pin_earned_text")] public string PinEarnedText { get; set; }
    [J("tournament_display_id")] public string TournamentDisplayId { get; set; }
    [J("schedule_info")] public string ScheduleInfo { get; set; }
    [J("highlight_color")] public string HighlightColor { get; set; }
    [J("primary_color")] public string PrimaryColor { get; set; }
    [J("flavor_description")] public string FlavorDescription { get; set; }
    [J("poster_front_image")] public string PosterFrontImage { get; set; }
    [J("short_format_title")] public string ShortFormatTitle { get; set; }
    [J("title_line_2")] public string TitleLine2 { get; set; }
    [J("title_line_1")] public string TitleLine1 { get; set; }
    [J("background_title")] public string BackgroundTitle { get; set; }
    [J("shadow_color")] public string ShadowColor { get; set; }
    [J("details_description")] public string DetailsDescription { get; set; }
    [J("background_left_color")] public string BackgroundLeftColor { get; set; }
    [J("long_format_title")] public string LongFormatTitle { get; set; }
    [J("poster_fade_color")] public string PosterFadeColor { get; set; }
    [J("secondary_color")] public string SecondaryColor { get; set; }
    [J("playlist_tile_image")] public string PlaylistTileImage { get; set; }
    [J("base_color")] public string BaseColor { get; set; }
}

public class FortConversionConfig
{
    [J] public string ContainerName { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public bool EnableReferences { get; set; }
    [J] public string ContentName { get; set; }
}

public class FortDynamicBackgrounds
{
    [J] public FortBackgrounds Backgrounds { get; set; }
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortBackgrounds
{
    [J] public FortBackground[] Backgrounds { get; set; }
    [J("_type")] public string Type { get; set; }
}

public class FortBackground
{
    [J] public string Stage { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string Key { get; set; }
}

public class FortEventScreens
{
    [J("_title")] public string Title { get; set; }
    [J] public FortEventScreenGroup EventScreenGroup { get; set; }
}

public class FortEventScreenGroup
{
    [J("_type")] public string Type { get; set; }
    [J] public FortEventScreenData[] EventScreens { get; set; }
}

public class FortEventScreenData
{
    [J] public string EventCMSId { get; set; }
    [J] public string EventName { get; set; }
    [J] public string EventDescription { get; set; }
    [J] public string ResourceHeader { get; set; }
    [J] public string StarterHeader { get; set; }
    [J] public string CompletionHeader { get; set; }
    [J] public string EventCTA { get; set; }
    [J] public string EventCTACompleted { get; set; }
    [J] public string HeaderCTA { get; set; }
    [J] public string ItemShopCallout { get; set; }
    [J] public string CTAIconURL { get; set; }
    [J] public string KeyArtURL { get; set; }
    [J] public string MoreInfoHeader { get; set; }
    [J] public string MoreInfoSubHeader { get; set; }
    [J] public string MoreInfoLegal { get; set; }
    [J] public EventScreenCMSMoreInfoGroup[] MoreInfoGroups { get; set; }
    [J] public string PurchaseLegal { get; set; }
    [J] public string RewardTrackLegal { get; set; }
    [J] public string ItemShopOfferId { get; set; }
    [J] public string PremiumUpsellUnownedHeader { get; set; }
    [J] public string PremiumUpsellUnownedBody { get; set; }
    [J] public string PremiumUpsellOwnedHeader { get; set; }
    [J] public string PremiumUpsellOwnedBody { get; set; }
    [J] public string PremiumUpsellIconURL { get; set; }
    [J] public string PurchasePremiumTrackHeader { get; set; }
    [J] public string[] PurchasePremiumTrackBodyList { get; set; }
    [J] public string InspectSpecialItemUnowned { get; set; }
    [J] public string InspectSpecialItemOwned { get; set; }
    [J] public string InspectSpecialPremiumItemUnowned { get; set; }
    [J] public string InspectSpecialPremiumItemOwned { get; set; }
    [J] public EventScreenResourceGroupOverrides[] ResourceGroupOverrides { get; set; }
}

public class EventScreenCMSMoreInfoGroup
{
    [J] public string Header { get; set; }
    [J] public string Body { get; set; }
    [J] public string IconURL { get; set; }
}

public class EventScreenResourceGroupOverrides
{
    [J] public int ResourceValue { get; set; }
    [J] public string KeyArtOverrideURL { get; set; }
}

public class FortMPItemShop
{
    [J] public FortMPShopData ShopData { get; set; }
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortMPShopData
{
    [J("_type")] public string Type { get; set; }
    [J] public FortMPItemShopSection[] Sections { get; set; }
}

public class FortMPItemShopSection
{
    [J] public string DisplayName { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string SectionId { get; set; }
    [J] public FortMPMetadata Metadata { get; set; }
}

public class FortMPMetadata
{
    [J] public object[] OfferGroups { get; set; } // Only need length
    [J] public string ShowIneligibleOffers { get; set; }
    [J] public dynamic Background { get; set; } // FortMPBackground or FortMPBackground[]
    [J] public FortMPStackRank[] StackRanks { get; set; }
    [J] public FortMPItemShopSubsection[] Subsections { get; set; }
}

public class FortMPStackRank
{
    [J] public string Context { get; set; }
    [J] public DateTime StartDate { get; set; }

    [I] public DateTime ActualStartDate => StartDate - TimeSpan.FromDays(1);
}

public class FortMPItemShopSubsection
{
    [J] public string DisplayName { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string SectionId { get; set; }
    [J] public FortMPSubsectionMetadata Metadata { get; set; }
}

public class FortMPSubsectionMetadata
{
    [J] public object[] OfferGroups { get; set; } // Only need length
    [J] public string ShowIneligibleOffers { get; set; }
}

public class FortMPBackground
{
    [J] public string CookedAssetKey { get; set; }
    [J] public string CustomTexture { get; set; }
}