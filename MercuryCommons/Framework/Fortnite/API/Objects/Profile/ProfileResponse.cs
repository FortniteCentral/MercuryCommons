using System.Collections.Generic;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Profile;

public class ProfileResponse
{
    public string Version { get; set; }
    public Dictionary<string, ProfileItem> Items { get; set; }
}

public class ProfileItem
{
    public string TemplateId { get; set; }
    public Dictionary<string, object> Attributes { get; set; }
}