using System;
using System.Collections.Generic;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Links;

public class MnemonicResponse
{
    [J] public string Namespace { get; set; }
    [J] public string AccountId { get; set; }
    [J] public string CreatorName { get; set; }
    [J] public string Mnemonic { get; set; }
    [J] public string LinkType { get; set; }
    [J] public MnemonicMetadata Metadata { get; set; }
    [J] public int Version { get; set; }
    [J] public bool Active { get; set; }
    [J] public bool Disabled { get; set; }
    [J] public DateTime Created { get; set; }
    [J] public DateTime Published { get; set; }
    [J] public string[] DescriptionTags { get; set; }
    [J] public DateTime LastActivatedDate { get; set; }
}

public class MnemonicMetadata
{
    [J] public string DisplayDataId { get; set; }
    [J("alt_image_urls")] public Dictionary<string, Dictionary<string, string>> AltImageUrls { get; set; } = new();
    [J("alt_image_url")] public Dictionary<string, string> AltImageUrl { get; set; } = new();
    [J("image_url")] public string ImageUrl { get; set; }
    [J("image_urls")] public Dictionary<string, string> ImageUrls { get; set; } = new();
    [J] public string Title { get; set; }
    [J("sub_link_codes")] public string[] SubLinkCodes { get; set; }
    [J("default_sub_link_code")] public string DefaultSubLinkCode { get; set; }
    [J] public MnemonicMatchmaking Matchmaking { get; set; }
    [J] public string QuestContextTag { get; set; } // Important for leaked creative challenges
    [J] public string Mode { get; set; } // Valkyrie
    [J] public string Tagline { get; set; }
    [J] public string IslandType { get; set; }
    [J] public Dictionary<string, dynamic> DynamicXp { get; set; } = new(); // CalibrationPhase is what we need
    [J] public string SupportCode { get; set; }
    [J] public string Introduction { get; set; }
}

public class MnemonicMatchmaking
{
    [J] public string Name { get; set; }
    [J] public string[] Playlists { get; set; }
}