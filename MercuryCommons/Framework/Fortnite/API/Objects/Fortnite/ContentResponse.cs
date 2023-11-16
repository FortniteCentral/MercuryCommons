using System;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;

public class ContentResponse
{
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
    [J] public FortSubGameInfo SubGameInfo { get; set; }
    [J] public FortAthenaMessage AthenaMessage { get; set; }
    [J] public FortNewsBase BattleRoyaleNews { get; set; }
    [J] public FortLobby Lobby { get; set; }
    [J] public FortSurvivalMessage SurvivalMessage { get; set; }
    [J] public FortNewsBase CreativeNews { get; set; }
    [J] public FortNewsBase SaveTheWorldNews { get; set; }
    [J] public FortEmergencyNotice EmergencyNotice { get; set; }
    [J] public FortKoreanCafe KoreanCafe { get; set; }
    [J] public FortBattlePassAboutMessages BattlePassAboutMessages { get; set; }
    [J] public FortLoginMessage LoginMessage { get; set; }
    [J] public FortDynamicBackgrounds DynamicBackgrounds { get; set; }
    [J] public FortSubGameSelectData SubGameSelectData { get; set; }
    [J] public FortPlaylistInformation PlaylistInformation { get; set; }
    [J] public FortCreativeAdFeatures CreativeFeatures { get; set; }
    [J] public FortCreativeAdFeatures CreativeAds { get; set; }

    [J] public FortTournamentInformation TournamentInformation { get; set; }

    // public FortComics Comics { get; set; }
    [J] public FortShopSections ShopSections { get; set; }
    [J] public FortNewsBaseV2 CreativeNewsV2 { get; set; }
    [J] public FortNewsBaseV2 BattleRoyaleNewsV2 { get; set; }
    [J] public FortRadioStations RadioStations { get; set; }
    [J] public FortSubscription Subscription { get; set; }
    [J] public FortEmergencyNoticeV2 EmergencyNoticeV2 { get; set; }
    [J] public FortShopCarousel ShopCarousel { get; set; }
    [J] public FortMPItemShop MPItemShop { get; set; }
}

public class FortSubGameInfo
{
    [J] public FortSubGame BattleRoyale { get; set; }
    [J] public FortSubGame SaveTheWorld { get; set; }
    [J] public FortSubGame Creative { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortSubGame
{
    [J] public string Image { get; set; }
    [J] public string Color { get; set; }
    [J] public string SpecialMessage { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string Description { get; set; }
    [J] public string Subgame { get; set; }
    [J] public string StandardMessageLine2 { get; set; }
    [J] public string Title { get; set; }
    [J] public string StandardMessageLine1 { get; set; }
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

public class FortNewsBaseV2
{
    [J] public FortNewsV2 News { get; set; }
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortNewsV2
{
    [J] public FortMotdV2[] Motds { get; set; }
    [J("_type")] public string Type { get; set; }
}

public class FortMotdV2
{
    [J] public string EntryType { get; set; }
    [J] public string Image { get; set; }
    [J] public string TileImage { get; set; }
    [J] public bool VideoMute { get; set; }
    [J] public bool Hidden { get; set; }
    [J] public string TabTitleOverride { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string Title { get; set; }
    [J] public string Body { get; set; }
    [J] public string OfferAction { get; set; }
    [J] public bool VideoLoop { get; set; }
    [J] public bool VideoStreamingEnabled { get; set; }
    [J] public int SortingPriority { get; set; }
    [J] public string ButtonTextOverride { get; set; }
    [J] public string OfferId { get; set; }
    [J] public string Id { get; set; }
    [J] public bool VideoAutoplay { get; set; }
    [J] public bool VideoFullscreen { get; set; }
    [J] public bool Spotlight { get; set; }
}

public class FortShopSections
{
    [J("_title")] public string Title { get; set; }
    [J] public FortSectionList SectionList { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortSectionList
{
    [J("_type")] public string Type { get; set; }
    [J] public FortSection[] Sections { get; set; }
}

public class FortSection
{
    [J("bSortOffersByOwnership")] public bool SortOffersByOwnership { get; set; }
    [J("bShowIneligibleOffersIfGiftable")] public bool ShowIneligibleOffersIfGiftable { get; set; }
    [J("bEnableToastNotification")] public bool EnableToastNotification { get; set; }
    [J] public FortBackground Background { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public int LandingPriority { get; set; }
    [J("bHidden")] public bool Hidden { get; set; }
    [J] public string SectionId { get; set; }
    [J("bShowTimer")] public bool ShowTimer { get; set; }
    [J] public string SectionDisplayName { get; set; }
    [J("bShowIneligibleOffers")] public bool ShowIneligibleOffers { get; set; }
    [I] public bool HasSentAdded { get; set; }
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

public class FortSubGameSelectData
{
    [J] public FortSubGameSelect SaveTheWorldUnowned { get; set; }
    [J("_title")] public string Title { get; set; }
    [J] public FortSubGameSelect BattleRoyale { get; set; }
    [J] public FortSubGameSelect Creative { get; set; }
    [J] public FortSubGameSelect SaveTheWorld { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }

    public class FortSubGameSelect
    {
        [J("_title")] public string Title { get; set; }
        [J] public FortMessage Message { get; set; }
    }
}

public class FortPlaylistInformation
{
    [J("is_tile_hidden")] public bool TileHidden { get; set; }
    [J("frontend_matchmaking_header_style")]
    public string FrontendMatchmakingHeaderStyle { get; set; }
    [J("conversion_config")] public FortConversionConfig ConversionConfig { get; set; }
    [J("show_ad_violator")] public bool ShowAdViolator;
    [J("_title")] public string Title { get; set; }
    [J("frontend_matchmaking_header_text_description")]
    public string FrontendMatchmakingHeaderTextDescription { get; set; }
    [J("frontend_matchmaking_header_text")]
    public string FrontendMatchmakingHeaderText { get; set; }
    [J("playlist_info")] public FortPlaylistInfo PlaylistInfo { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
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

public class FortPlaylistInfo
{
    [J("_type")] public string Type { get; set; }
    [J] public FortPlaylist[] Playlists { get; set; }
}

public class FortPlaylist
{
    [J] public string Image { get; set; }
    [J("playlist_name")] public string PlaylistName { get; set; }
    [J] public bool Hidden { get; set; }
    [J] public string Violator { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string Description { get; set; }
    [J("display_subname")] public string DisplaySubname { get; set; }
    [J("display_name")] public string DisplayName { get; set; }
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

public class FortBattlePassAboutMessages
{
    [J] public FortNews News { get; set; }
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortEmergencyNotice
{
    [J] public FortNews News { get; set; }
    [J("_title")] public string Title { get; set; }
    [J] public bool AlwaysShow { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortEmergencyNoticeV2
{
    [J("_title")] public string Title { get; set; }
    [J] public FortEmergencyNotices EmergencyNotices { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }

    public class FortEmergencyNotices
    {
        [J("_type")] public string Type { get; set; }
        [J] public FortMessage[] EmergencyNotices { get; set; }
    }
}

public class FortKoreanCafe
{
    [J("_title")] public string Title { get; set; }
    [J("cafe_info")] public FortCafeInfo CafeInfo { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortCafeInfo
{
    [J] public FortCafe[] Cafes { get; set; }
    [J("_type")] public string Type { get; set; }
}

public class FortCafe
{
    [J("korean_cafe")] public string KoreanCafe { get; set; }
    [J("korean_cafe_description")] public string KoreanCafeDescription { get; set; }
    [J("_type")] public string Type { get; set; }
    [J("korean_cafe_header")] public string KoreanCafeHeader { get; set; }
}

public class FortSurvivalMessage
{
    [J] public FortOverrideableMessage OverrideableMessage { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortAthenaMessage
{
    [J] public FortOverrideableMessage OverrideableMessage { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortOverrideableMessage
{
    [J] public FortMessage Message { get; set; }
    [J("_title")] public string Title { get; set; }
    [J] public string Header { get; set; }
    [J] public string Style { get; set; }
    [J] public bool AlwaysShow { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortLobby
{
    [J] public string BackgroundImage { get; set; }
    [J] public string Stage { get; set; }
    [J("_title")] public string Title { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortNews
{
    [J] public FortMessage[] Messages { get; set; }
    [J("platform_messages")] public FortMessage[] PlatformMessages { get; set; } // TODO Double Check
    [J("platform_motds")] public FortPlatformMotd[] PlatformMotds { get; set; }
}

public class FortPlatformMotd
{
    [J] public bool Hidden { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public FortMessage Message { get; set; }
    [J] public string Platform { get; set; }
}

public class FortMessage
{
    [J] public string Layout { get; set; }
    [J] public string EntryType { get; set; }
    [J] public string Image { get; set; }
    [J] public string TileImage { get; set; }
    [J] public bool Hidden { get; set; }
    [J] public bool VideoMute { get; set; }
    [J] public string MessageType { get; set; }
    [J("_type")] public string Type { get; set; }
    [J] public string Title { get; set; }
    [J] public string Body { get; set; }
    [J] public bool VideoLoop { get; set; }
    [J] public bool VideoStreamingEnabled { get; set; }
    [J] public string Id { get; set; }
    [J] public bool VideoAutoplay { get; set; }
    [J] public bool VideoFullscreen { get; set; }
    [J] public bool Spotlight { get; set; }
}

public class FortNewsBase
{
    [J] public FortNews News { get; set; }
}

public class FortLoginMessage
{
    [J("_title")] public string Title { get; set; }
    [J] public FortOverrideableMessage LoginMessage { get; set; }
    [J("_activeDate")] public DateTime ActiveDate { get; set; }
    [J] public DateTime LastModified { get; set; }
    [J("_locale")] public string Locale { get; set; }
}

public class FortMPItemShop
{
    [J] public FortMPShopData ShopData { get; set; }
    [J("_title")] public string Title { get; set; }
    [J] public FortOverrideableMessage LoginMessage { get; set; }
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
    [J] public FortMPBackground Background { get; set; }
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