using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using TimeZoneConverter;

namespace MercuryCommons.Utilities.Extensions;

public static class DateExtensions
{
    public static DateTime CurrentTime() => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("America/New_York"));
    public static DateTime ToEastern(this DateTime time) => TimeZoneInfo.ConvertTime(time, TZConvert.GetTimeZoneInfo("America/New_York"));
    public static DateTime GetDateTimeNoFormat(string date) => DateTime.TryParse(date, out var dateTime) ? dateTime : default;
    public static DateTime GetDateTimeNoFormat(JValue date) => GetDateTimeNoFormat(date.ToObject<string>());
    public static DateTime GetDateTimeFromApi(string date) => GetDateTimeNoFormat(date.Replace("+00:00", string.Empty));
    public static DateTime ParseDateNoTime(string date) => DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    public static string DiscordFormattedUnixTimestamp(this DateTime date, string format) => $"<t:{ToUnixTimestamp(date)}:{format}>";
    public static string DiscordFormattedUnixTimestamp(this DateTime date, char format) => $"<t:{ToUnixTimestamp(date)}:{format}>";

    public static long ToUnixTimestamp(this DateTime target)
    {
        var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
        var unixTimestamp = Convert.ToInt64((target - date).TotalSeconds);
        return unixTimestamp;
    }

    public static string ToFormatted(this TimeSpan span)
    {
        var formatted = $"{(span.Duration().Days > 0 ?
            $"{span.Days:0} day{(span.Days == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Hours > 0 ?
            $"{span.Hours:0} hour{(span.Hours == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Minutes > 0 ?
            $"{span.Minutes:0} minute{(span.Minutes == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Seconds > 0 ?
            $"{span.Seconds:0} second{(span.Seconds == 1 ? string.Empty : "s")}" : string.Empty)}";

        if (formatted.EndsWith(", ")) formatted = formatted[..^2];
        if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

        return formatted;
    }
}