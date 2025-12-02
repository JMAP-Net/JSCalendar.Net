using System.Text.Json.Serialization;
using JSCalendar.Net.Enums;
using DayOfWeek = JSCalendar.Net.Enums.DayOfWeek;

namespace JSCalendar.Net;

/// <summary>
///     Represents a recurrence rule (RFC 8984 Section 4.3.3).
/// </summary>
public sealed class RecurrenceRule
{
    /// <summary>
    ///     Type identifier. MUST be "RecurrenceRule".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "RecurrenceRule";

    /// <summary>
    ///     Time span covered by each iteration of this recurrence rule.
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("frequency")]
    public required RecurrenceFrequency Frequency { get; init; }

    /// <summary>
    ///     Interval of iteration periods at which the recurrence repeats.
    ///     Default: 1
    ///     MUST be >= 1.
    /// </summary>
    [JsonPropertyName("interval")]
    public int Interval { get; init; } = 1;

    /// <summary>
    ///     Calendar system in which this recurrence rule operates.
    ///     Default: "gregorian"
    /// </summary>
    [JsonPropertyName("rscale")]
    public string Rscale { get; init; } = "gregorian";

    /// <summary>
    ///     Behavior when expansion produces invalid dates.
    ///     Default: Omit
    /// </summary>
    [JsonPropertyName("skip")]
    public RecurrenceSkip Skip { get; init; } = RecurrenceSkip.Omit;

    /// <summary>
    ///     Day on which the week is considered to start.
    ///     Default: Monday
    /// </summary>
    [JsonPropertyName("firstDayOfWeek")]
    public DayOfWeek FirstDayOfWeek { get; init; } = DayOfWeek.Monday;

    /// <summary>
    ///     Days of the week on which to repeat.
    /// </summary>
    [JsonPropertyName("byDay")]
    public List<NDay>? ByDay { get; init; }

    /// <summary>
    ///     Days of the month on which to repeat.
    ///     Valid values between 1 and max days in month, and negative values.
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("byMonthDay")]
    public List<int>? ByMonthDay { get; init; }

    /// <summary>
    ///     Months in which to repeat.
    ///     String representations of numbers starting from "1".
    ///     May include "L" suffix for leap months (e.g., "3L").
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("byMonth")]
    public List<string>? ByMonth { get; init; }

    /// <summary>
    ///     Days of the year on which to repeat.
    ///     Valid values between 1 and max days in year, and negative values.
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("byYearDay")]
    public List<int>? ByYearDay { get; init; }

    /// <summary>
    ///     Weeks of the year in which to repeat.
    ///     Valid values between 1 and max weeks in year, and negative values.
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("byWeekNo")]
    public List<int>? ByWeekNo { get; init; }

    /// <summary>
    ///     Hours of the day in which to repeat.
    ///     Valid values: 0 to 23.
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("byHour")]
    public List<int>? ByHour { get; init; }

    /// <summary>
    ///     Minutes of the hour in which to repeat.
    ///     Valid values: 0 to 59.
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("byMinute")]
    public List<int>? ByMinute { get; init; }

    /// <summary>
    ///     Seconds of the minute in which to repeat.
    ///     Valid values: 0 to 60.
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("bySecond")]
    public List<int>? BySecond { get; init; }

    /// <summary>
    ///     Occurrences within the recurrence interval to include.
    ///     Negative values offset from the end.
    ///     MUST have at least one entry if included.
    /// </summary>
    [JsonPropertyName("bySetPosition")]
    public List<int>? BySetPosition { get; init; }

    /// <summary>
    ///     Number of occurrences at which to range-bound the recurrence.
    ///     MUST NOT be included if 'until' is specified.
    /// </summary>
    [JsonPropertyName("count")]
    public int? Count { get; init; }

    /// <summary>
    ///     Date-time at which to finish recurring.
    ///     MUST NOT be included if 'count' is specified.
    /// </summary>
    [JsonPropertyName("until")]
    public LocalDateTime? Until { get; init; }
}

/// <summary>
///     Represents a day of the week with optional position (RFC 8984 Section 4.3.3).
/// </summary>
public sealed class NDay
{
    /// <summary>
    ///     Type identifier. MUST be "NDay".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "NDay";

    /// <summary>
    ///     Day of the week.
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("day")]
    public required DayOfWeek Day { get; init; }

    /// <summary>
    ///     Position of the day within the recurrence period.
    ///     If present, represents only a specific instance (not every occurrence).
    ///     Negative values mean nth-last occurrence (-1 = last, -2 = second from last, etc.).
    ///     MUST NOT be zero.
    /// </summary>
    [JsonPropertyName("nthOfPeriod")]
    public int? NthOfPeriod { get; init; }
}