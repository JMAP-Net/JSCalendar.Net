using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net;

/// <summary>
///     Represents a duration in JSCalendar format (RFC 8984).
///     Format: PTnYnMnDTnHnMnS or PnW
/// </summary>
[JsonConverter(typeof(DurationJsonConverter))]
public sealed record Duration
{
    public int? Years { get; init; }
    public int? Months { get; init; }
    public int? Weeks { get; init; }
    public int? Days { get; init; }
    public int? Hours { get; init; }
    public int? Minutes { get; init; }
    public int? Seconds { get; init; }

    public static Duration FromTimeSpan(TimeSpan timeSpan)
    {
        return new Duration
        {
            Days = timeSpan.Days,
            Hours = timeSpan.Hours,
            Minutes = timeSpan.Minutes,
            Seconds = timeSpan.Seconds
        };
    }

    public override string ToString()
    {
        if (Weeks.HasValue)
            return $"P{Weeks}W";

        var result = "P";
        if (Years.HasValue) result += $"{Years}Y";
        if (Months.HasValue) result += $"{Months}M";
        if (Days.HasValue) result += $"{Days}D";

        var hasTime = Hours.HasValue || Minutes.HasValue || Seconds.HasValue;
        if (hasTime)
        {
            result += "T";
            if (Hours.HasValue) result += $"{Hours}H";
            if (Minutes.HasValue) result += $"{Minutes}M";
            if (Seconds.HasValue) result += $"{Seconds}S";
        }

        return result;
    }
}