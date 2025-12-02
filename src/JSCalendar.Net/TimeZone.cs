using System.Text.Json.Serialization;

namespace JSCalendar.Net;

/// <summary>
///     Represents a time zone definition (RFC 8984 Section 4.7.2).
///     Maps a VTIMEZONE component from iCalendar.
///     A valid time zone MUST define at least one transition rule in standard or daylight.
/// </summary>
public sealed class TimeZone
{
    /// <summary>
    ///     Type identifier. MUST be "TimeZone".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "TimeZone";

    /// <summary>
    ///     IANA time zone identifier (TZID from iCalendar).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("tzId")]
    public required string TzId { get; init; }

    /// <summary>
    ///     Last modification time (LAST-MODIFIED from iCalendar).
    /// </summary>
    [JsonPropertyName("updated")]
    public DateTimeOffset? Updated { get; init; }

    /// <summary>
    ///     URL to obtain the full time zone data (TZURL from iCalendar).
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    ///     Validity period end (TZUNTIL from iCalendar RFC 7808).
    /// </summary>
    [JsonPropertyName("validUntil")]
    public DateTimeOffset? ValidUntil { get; init; }

    /// <summary>
    ///     Aliases for this time zone (TZID-ALIAS-OF from iCalendar RFC 7808).
    /// </summary>
    [JsonPropertyName("aliases")]
    public Dictionary<string, bool>? Aliases { get; init; }

    /// <summary>
    ///     Standard time components (STANDARD from iCalendar).
    ///     Order MUST be preserved.
    /// </summary>
    [JsonPropertyName("standard")]
    public List<TimeZoneRule>? Standard { get; init; }

    /// <summary>
    ///     Daylight saving time components (DAYLIGHT from iCalendar).
    ///     Order MUST be preserved.
    /// </summary>
    [JsonPropertyName("daylight")]
    public List<TimeZoneRule>? Daylight { get; init; }
}

/// <summary>
///     Represents a time zone rule (RFC 8984 Section 4.7.2).
///     Maps a STANDARD or DAYLIGHT sub-component from iCalendar.
///     At most one recurrence rule is allowed per rule.
/// </summary>
public sealed class TimeZoneRule
{
    /// <summary>
    ///     Type identifier. MUST be "TimeZoneRule".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "TimeZoneRule";

    /// <summary>
    ///     Start date/time of the rule (DTSTART from iCalendar).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("start")]
    public required LocalDateTime Start { get; init; }

    /// <summary>
    ///     UTC offset before this rule (TZOFFSETFROM from iCalendar).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("offsetFrom")]
    public required string OffsetFrom { get; init; }

    /// <summary>
    ///     UTC offset after this rule applies (TZOFFSETTO from iCalendar).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("offsetTo")]
    public required string OffsetTo { get; init; }

    /// <summary>
    ///     Recurrence rules (RRULE from iCalendar).
    /// </summary>
    [JsonPropertyName("recurrenceRules")]
    public List<RecurrenceRule>? RecurrenceRules { get; init; }

    /// <summary>
    ///     Recurrence dates (RDATE from iCalendar).
    ///     Keys are recurrence dates, values MUST be empty PatchObjects.
    /// </summary>
    [JsonPropertyName("recurrenceOverrides")]
    public Dictionary<LocalDateTime, PatchObject>? RecurrenceOverrides { get; init; }

    /// <summary>
    ///     Names for this time zone (TZNAME from iCalendar).
    /// </summary>
    [JsonPropertyName("names")]
    public Dictionary<string, bool>? Names { get; init; }

    /// <summary>
    ///     Comments (COMMENT from iCalendar).
    ///     Order MUST be preserved.
    /// </summary>
    [JsonPropertyName("comments")]
    public List<string>? Comments { get; init; }
}