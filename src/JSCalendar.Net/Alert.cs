using System.Text.Json.Serialization;

namespace JSCalendar.Net;

/// <summary>
///     Represents an alert/reminder for a calendar event (RFC 8984 Section 4.5.2).
/// </summary>
public sealed class Alert
{
    /// <summary>
    ///     Type identifier. MUST be "Alert".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Alert";

    /// <summary>
    ///     Trigger defining when to alert (Section 4.5.2).
    ///     REQUIRED property.
    ///     Can be OffsetTrigger, AbsoluteTrigger, or UnknownTrigger.
    /// </summary>
    [JsonPropertyName("trigger")]
    public required object Trigger { get; init; }

    /// <summary>
    ///     When the alert was last acknowledged (Section 4.5.2).
    /// </summary>
    [JsonPropertyName("acknowledged")]
    public DateTimeOffset? Acknowledged { get; init; }

    /// <summary>
    ///     Relates this alert to other alerts (Section 4.5.2).
    /// </summary>
    [JsonPropertyName("relatedTo")]
    public Dictionary<string, Relation>? RelatedTo { get; init; }

    /// <summary>
    ///     How to alert the user (Section 4.5.2).
    ///     Default: "display"
    /// </summary>
    [JsonPropertyName("action")]
    public string Action { get; init; } = "display";
}

/// <summary>
///     Represents an offset trigger for alerts (RFC 8984 Section 4.5.2).
/// </summary>
public sealed class OffsetTrigger
{
    /// <summary>
    ///     Type identifier. MUST be "OffsetTrigger".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "OffsetTrigger";

    /// <summary>
    ///     Offset duration from the related time.
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("offset")]
    public required string Offset { get; init; }

    /// <summary>
    ///     Relation to the event time (start or end).
    ///     Default: "start"
    /// </summary>
    [JsonPropertyName("relativeTo")]
    public string RelativeTo { get; init; } = "start";
}

/// <summary>
///     Represents an absolute time trigger for alerts (RFC 8984 Section 4.5.2).
/// </summary>
public sealed class AbsoluteTrigger
{
    /// <summary>
    ///     Type identifier. MUST be "AbsoluteTrigger".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "AbsoluteTrigger";

    /// <summary>
    ///     Absolute UTC date-time when to trigger the alert.
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("when")]
    public required DateTimeOffset When { get; init; }
}

/// <summary>
///     Represents an unknown trigger type (RFC 8984 Section 4.5.2).
///     Used for forward compatibility with future extensions.
/// </summary>
public sealed class UnknownTrigger
{
    /// <summary>
    ///     Type identifier. Unknown type not recognized.
    /// </summary>
    [JsonPropertyName("@type")]
    public required string Type { get; init; }
}