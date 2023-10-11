using System;
using System.Collections.Generic;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Events;

public class EventsResponse
{
    [J] public IEnumerable<Event> Events { get; set; }
    [J] public IEnumerable<Template> Templates { get; set; }
}

public class Event
{
    [J] public string EventId { get; set; }
    [J] public IEnumerable<string> Regions { get; set; }
    [J] public IDictionary<string, string> RegionMappings { get; set; }
    [J] public IEnumerable<string> Platforms { get; set; }
    [J] public IDictionary<string, string> PlatformMappings { get; set; }
    [J] public string DisplayDataId { get; set; }
    [J] public DateTime AnnouncementTime { get; set; }
    [J] public IDictionary<string, dynamic> Metadata { get; set; }
    [J] public IEnumerable<EventWindow> EventWindows { get; set; }
    [J] public DateTime BeginTime { get; set; }
    [J] public DateTime EndTime { get; set; }
}

public class EventWindow
{
    [J] public string EventWindowId { get; set; }
    [J] public string EventTemplateId { get; set; }
    [J] public DateTime CountdownBeginTime { get; set; }
    [J] public DateTime BeginTime { get; set; }
    [J] public DateTime EndTime { get; set; }
    [J] public int Round { get; set; }
    [J] public int PayoutDelay { get; set; }
    [J] public bool IsTBD { get; set; }
    [J] public bool CanLiveSpectate { get; set; }
    [J] public string Visibility { get; set; }
    [J] public IEnumerable<string> RequireAllTokens { get; set; }
    [J] public IEnumerable<string> RequireAnyTokens { get; set; }
    [J] public IEnumerable<string> RequireNoneTokensCaller { get; set; }
    [J] public IEnumerable<string> RequireAllTokensCaller { get; set; }
    [J] public IEnumerable<string> RequireAnyTokensCaller { get; set; }
    [J] public IEnumerable<string> AdditionalRequirements { get; set; }
    [J] public string TeammateEligibility { get; set; }
    [J] public IDictionary<string, string> RegionMappings { get; set; }
    [J] public IDictionary<string, dynamic> Metadata { get; set; }
}

public class Template
{
    [J] public string EventTemplateId { get; set; }
    [J] public string PlaylistId { get; set; }
    [J] public int MatchCap { get; set; }
    [J] public IEnumerable<TemplatePayout> PayoutTable { get; set; }
}

public class TemplatePayout
{
    [J] public string ScoreId { get; set; }
    [J] public string ScoringType { get; set; }
    [J] public IEnumerable<PayoutRank> Ranks { get; set; }
}

public class PayoutRank
{
    [J] public double Threshold { get; set; }
    [J] public IEnumerable<Payout> Payouts { get; set; }
}

public class Payout
{
    [J] public string RewardType { get; set; }
    [J] public string RewardMode { get; set; }
    [J] public string Value { get; set; }
    [J] public int Quantity { get; set; }
}