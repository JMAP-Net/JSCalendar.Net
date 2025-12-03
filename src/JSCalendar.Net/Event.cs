using System.Text.Json.Serialization;
using JSCalendar.Net.Enums;

namespace JSCalendar.Net;

/// <summary>
///     Represents a calendar event in JSCalendar format (RFC 8984).
///     This is the main type for calendar events.
/// </summary>
public class Event : IJSCalendarObject
{
    // Metadata Properties (Section 4.1)

    /// <summary>
    ///     Type identifier. MUST be "Event".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Event";

    /// <summary>
    ///     Globally unique identifier for this event (Section 4.1.2).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("uid")]
    public required string Uid { get; init; }

    /// <summary>
    ///     Relates this event to other events/objects (Section 4.1.3).
    /// </summary>
    [JsonPropertyName("relatedTo")]
    public Dictionary<string, Relation>? RelatedTo { get; init; }

    /// <summary>
    ///     Producer identifier that created this event (Section 4.1.4).
    /// </summary>
    [JsonPropertyName("prodId")]
    public string? ProdId { get; init; }

    /// <summary>
    ///     Date and time this event was created (Section 4.1.5).
    /// </summary>
    [JsonPropertyName("created")]
    public DateTimeOffset? Created { get; init; }

    /// <summary>
    ///     Date and time this event was last updated (Section 4.1.6).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("updated")]
    public required DateTimeOffset Updated { get; init; }

    /// <summary>
    ///     Sequence number for updates (Section 4.1.7).
    ///     Default: 0
    /// </summary>
    [JsonPropertyName("sequence")]
    public int Sequence { get; init; } = 0;

    /// <summary>
    ///     The iTIP (RFC5546) method (Section 4.1.8).
    /// </summary>
    [JsonPropertyName("method")]
    public string? Method { get; init; }

    // What and Where Properties (Section 4.2)

    /// <summary>
    ///     Short summary of the event (Section 4.2.1).
    ///     Default: empty String
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = "";

    /// <summary>
    ///     Detailed description of the event (Section 4.2.2).
    ///     Default: empty String
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = "";

    /// <summary>
    ///     Content type of the description (Section 4.2.3).
    ///     Default: "text/plain"
    /// </summary>
    [JsonPropertyName("descriptionContentType")]
    public string DescriptionContentType { get; init; } = "text/plain";

    /// <summary>
    ///     Indicates that the time is not important to display (Section 4.2.4).
    ///     Default: false
    /// </summary>
    [JsonPropertyName("showWithoutTime")]
    public bool ShowWithoutTime { get; init; } = false;

    /// <summary>
    ///     Physical locations associated with this event (Section 4.2.5).
    /// </summary>
    [JsonPropertyName("locations")]
    public Dictionary<string, Location>? Locations { get; init; }

    /// <summary>
    ///     Virtual locations (video conferences, etc.) for this event (Section 4.2.6).
    /// </summary>
    [JsonPropertyName("virtualLocations")]
    public Dictionary<string, VirtualLocation>? VirtualLocations { get; init; }

    /// <summary>
    ///     Links to additional resources (Section 4.2.7).
    /// </summary>
    [JsonPropertyName("links")]
    public Dictionary<string, Link>? Links { get; init; }

    /// <summary>
    ///     Language tag for this event (Section 4.2.8).
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    /// <summary>
    ///     Keywords or tags for this event (Section 4.2.9).
    /// </summary>
    [JsonPropertyName("keywords")]
    public Dictionary<string, bool>? Keywords { get; init; }

    /// <summary>
    ///     Categories for this event (Section 4.2.10).
    /// </summary>
    [JsonPropertyName("categories")]
    public Dictionary<string, bool>? Categories { get; init; }

    /// <summary>
    ///     Color to use when displaying this event (Section 4.2.11).
    /// </summary>
    [JsonPropertyName("color")]
    public string? Color { get; init; }

    // Recurrence Properties (Section 4.3)

    /// <summary>
    ///     If present, this is an occurrence of a recurring event (Section 4.3.1).
    /// </summary>
    [JsonPropertyName("recurrenceId")]
    public LocalDateTime? RecurrenceId { get; init; }

    /// <summary>
    ///     Time zone of the main event (Section 4.3.2).
    /// </summary>
    [JsonPropertyName("recurrenceIdTimeZone")]
    public string? RecurrenceIdTimeZone { get; init; }

    /// <summary>
    ///     Recurrence rules for this event (Section 4.3.3).
    /// </summary>
    [JsonPropertyName("recurrenceRules")]
    public List<RecurrenceRule>? RecurrenceRules { get; init; }

    /// <summary>
    ///     Excluded recurrence rules (Section 4.3.4).
    /// </summary>
    [JsonPropertyName("excludedRecurrenceRules")]
    public List<RecurrenceRule>? ExcludedRecurrenceRules { get; init; }

    /// <summary>
    ///     Overrides for specific recurrence instances (Section 4.3.5).
    ///     Keys are LocalDateTime instances, values are PatchObjects.
    /// </summary>
    [JsonPropertyName("recurrenceOverrides")]
    public Dictionary<LocalDateTime, PatchObject>? RecurrenceOverrides { get; init; }

    /// <summary>
    ///     Whether this instance is excluded (Section 4.3.6).
    ///     Default: false
    /// </summary>
    [JsonPropertyName("excluded")]
    public bool Excluded { get; init; } = false;

    // Sharing and Scheduling Properties (Section 4.4)

    /// <summary>
    ///     Priority of the event (Section 4.4.1).
    ///     Default: 0 (undefined)
    /// </summary>
    [JsonPropertyName("priority")]
    public int Priority { get; init; } = 0;

    /// <summary>
    ///     Free/busy status (Section 4.4.2).
    ///     Default: Busy
    /// </summary>
    [JsonPropertyName("freeBusyStatus")]
    public FreeBusyStatus FreeBusyStatus { get; init; } = FreeBusyStatus.Busy;

    /// <summary>
    ///     Privacy classification (Section 4.4.3).
    ///     Default: Public
    /// </summary>
    [JsonPropertyName("privacy")]
    public Privacy Privacy { get; init; } = Privacy.Public;

    /// <summary>
    ///     Methods by which participants may submit responses (Section 4.4.4).
    /// </summary>
    [JsonPropertyName("replyTo")]
    public Dictionary<string, string>? ReplyTo { get; init; }

    /// <summary>
    ///     Email address in the "From" header (Section 4.4.5).
    /// </summary>
    [JsonPropertyName("sentBy")]
    public string? SentBy { get; init; }

    /// <summary>
    ///     Participants in this event (Section 4.4.6).
    /// </summary>
    [JsonPropertyName("participants")]
    public Dictionary<string, Participant>? Participants { get; init; }

    /// <summary>
    ///     Request status from scheduling (Section 4.4.7).
    /// </summary>
    [JsonPropertyName("requestStatus")]
    public string? RequestStatus { get; init; }

    // Alerts Properties (Section 4.5)

    /// <summary>
    ///     Use default alerts (Section 4.5.1).
    ///     Default: false
    /// </summary>
    [JsonPropertyName("useDefaultAlerts")]
    public bool UseDefaultAlerts { get; init; } = false;

    /// <summary>
    ///     Alerts/reminders for this event (Section 4.5.2).
    /// </summary>
    [JsonPropertyName("alerts")]
    public Dictionary<string, Alert>? Alerts { get; init; }

    // Multilingual Properties (Section 4.6)

    /// <summary>
    ///     Localizations of the event (Section 4.6.1).
    ///     Keys are language tags (RFC 5646), values are PatchObjects.
    /// </summary>
    [JsonPropertyName("localizations")]
    public Dictionary<string, PatchObject>? Localizations { get; init; }

    // Time Zone Properties (Section 4.7)

    /// <summary>
    ///     Time zone for the event (Section 4.7.1).
    ///     Default: null (floating time)
    /// </summary>
    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; init; }

    /// <summary>
    ///     Time zone definitions referenced by this event (Section 4.7.2).
    /// </summary>
    [JsonPropertyName("timeZones")]
    public Dictionary<string, TimeZone>? TimeZones { get; init; }

    // Event-specific Properties (Section 5.1)

    /// <summary>
    ///     Start date/time of the event (Section 5.1.1).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("start")]
    public required LocalDateTime Start { get; init; }

    /// <summary>
    ///     Duration of the event (Section 5.1.2).
    ///     Default: "PT0S"
    /// </summary>
    [JsonPropertyName("duration")]
    public Duration Duration { get; init; } = new() { Seconds = 0 };

    /// <summary>
    ///     Scheduling status of the event (Section 5.1.3).
    ///     Default: Confirmed
    /// </summary>
    [JsonPropertyName("status")]
    public EventStatus Status { get; init; } = EventStatus.Confirmed;
}