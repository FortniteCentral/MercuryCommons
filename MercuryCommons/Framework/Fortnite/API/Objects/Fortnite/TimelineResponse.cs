using System;
using System.Collections.Generic;

namespace MercuryCommons.Framework.Fortnite.API.Objects.Fortnite;

public class TimelineResponse
{
    [J] public Dictionary<string, Channel> Channels { get; set; }
    [J] public double EventsTimeOffsetHrs { get; set; }
    [J] public double CacheIntervalMins { get; set; }
    [J] public DateTime CurrentTime { get; set; }
}

public class Channel
{
    [J] public States[] States { get; set; }
    [J] public DateTime CacheExpire { get; set; }
}

public class States
{
    [J] public DateTime ValidFrom { get; set; }
    [J] public Event[] ActiveEvents { get; set; }
    [J] public State State { get; set; }
}

public class State
{
    // client-matchmaking
    [J] public Dictionary<string, Region> Region { get; set; }

    // tk
    [J] public string[] K { get; set; }

    // client-events
    [J] public object ActiveStorefronts { get; set; } // TODO
    [J] public object EventNamedWeights { get; set; } // TODO
    [J] public Event[] ActiveEvents { get; set; }
    [J] public int SeasonNumber { get; set; }
    [J] public string SeasonTemplateId { get; set; }
    [J] public int MatchXpBonusPoints { get; set; }
    [J] public string EventPunchCardTemplateId { get; set; }
    [J] public DateTime SeasonBegin { get; set; }
    [J] public DateTime SeasonEnd { get; set; }
    [J] public DateTime SeasonDisplayedEnd { get; set; }
    [J] public DateTime WeeklyStoreEnd { get; set; }
    [J] public DateTime StwEventStoreEnd { get; set; }
    [J] public DateTime StwWeeklyStoreEnd { get; set; }
    [J] public Dictionary<string, DateTime> SectionStoreEnds { get; set; }
    [J] public string RmtPromotion { get; set; }
    [J] public DateTime DailyStoreEnd { get; set; }
}

public class Region
{
    [J] public string[] EventFlagsForcedOn { get; set; }
    [J] public string[] EventFlagsForcedOff { get; set; }
}

public class Event
{
    [J] public string InstanceId { get; set; }
    [J] public string DevName { get; set; }
    [J] public string EventName { get; set; }
    [J] public DateTime EventStart { get; set; }
    [J] public DateTime EventEnd { get; set; }
    [J] public string EventType { get; set; }
    [J] public DateTime ActiveUntil { get; set; }
    [J] public DateTime ActiveSince { get; set; }
}