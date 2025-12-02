using System.Text.Json.Serialization;
using JSCalendar.Net.Enums;

namespace JSCalendar.Net;

/// <summary>
///     Represents a task/todo item in JSCalendar format (RFC 8984 Section 2.2).
/// </summary>
public sealed class Task : IJSCalendarObject
{
    // Metadata Properties (Section 4.1)

    /// <summary>
    ///     Type identifier. MUST be "Task".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Task";

    /// <summary>
    ///     Globally unique identifier for this task (Section 4.1.2).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("uid")]
    public required string Uid { get; init; }

    /// <summary>
    ///     Relates this task to other objects (Section 4.1.3).
    /// </summary>
    [JsonPropertyName("relatedTo")]
    public Dictionary<string, Relation>? RelatedTo { get; init; }

    /// <summary>
    ///     Producer identifier (Section 4.1.4).
    /// </summary>
    [JsonPropertyName("prodId")]
    public string? ProdId { get; init; }

    /// <summary>
    ///     Creation timestamp (Section 4.1.5).
    /// </summary>
    [JsonPropertyName("created")]
    public DateTimeOffset? Created { get; init; }

    /// <summary>
    ///     Last update timestamp (Section 4.1.6).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("updated")]
    public required DateTimeOffset Updated { get; init; }

    /// <summary>
    ///     Sequence number (Section 4.1.7).
    ///     Default: 0
    /// </summary>
    [JsonPropertyName("sequence")]
    public int Sequence { get; init; } = 0;

    /// <summary>
    ///     iTIP method (Section 4.1.8).
    /// </summary>
    [JsonPropertyName("method")]
    public string? Method { get; init; }

    // What and Where Properties (Section 4.2)

    /// <summary>
    ///     Title/summary of the task (Section 4.2.1).
    ///     Default: empty String
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = "";

    /// <summary>
    ///     Description of the task (Section 4.2.2).
    ///     Default: empty String
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = "";

    /// <summary>
    ///     Content type of description (Section 4.2.3).
    ///     Default: "text/plain"
    /// </summary>
    [JsonPropertyName("descriptionContentType")]
    public string DescriptionContentType { get; init; } = "text/plain";

    /// <summary>
    ///     Show without time (Section 4.2.4).
    ///     Default: false
    /// </summary>
    [JsonPropertyName("showWithoutTime")]
    public bool ShowWithoutTime { get; init; } = false;

    /// <summary>
    ///     Physical locations (Section 4.2.5).
    /// </summary>
    [JsonPropertyName("locations")]
    public Dictionary<string, Location>? Locations { get; init; }

    /// <summary>
    ///     Virtual locations (Section 4.2.6).
    /// </summary>
    [JsonPropertyName("virtualLocations")]
    public Dictionary<string, VirtualLocation>? VirtualLocations { get; init; }

    /// <summary>
    ///     Links to resources (Section 4.2.7).
    /// </summary>
    [JsonPropertyName("links")]
    public Dictionary<string, Link>? Links { get; init; }

    /// <summary>
    ///     Language tag (Section 4.2.8).
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    /// <summary>
    ///     Keywords/tags (Section 4.2.9).
    /// </summary>
    [JsonPropertyName("keywords")]
    public Dictionary<string, bool>? Keywords { get; init; }

    /// <summary>
    ///     Categories (Section 4.2.10).
    /// </summary>
    [JsonPropertyName("categories")]
    public Dictionary<string, bool>? Categories { get; init; }

    /// <summary>
    ///     Display color (Section 4.2.11).
    /// </summary>
    [JsonPropertyName("color")]
    public string? Color { get; init; }

    // Recurrence Properties (Section 4.3)

    /// <summary>
    ///     Recurrence ID (Section 4.3.1).
    /// </summary>
    [JsonPropertyName("recurrenceId")]
    public LocalDateTime? RecurrenceId { get; init; }

    /// <summary>
    ///     Recurrence ID time zone (Section 4.3.2).
    /// </summary>
    [JsonPropertyName("recurrenceIdTimeZone")]
    public string? RecurrenceIdTimeZone { get; init; }

    /// <summary>
    ///     Recurrence rules (Section 4.3.3).
    /// </summary>
    [JsonPropertyName("recurrenceRules")]
    public List<RecurrenceRule>? RecurrenceRules { get; init; }

    /// <summary>
    ///     Excluded recurrence rules (Section 4.3.4).
    /// </summary>
    [JsonPropertyName("excludedRecurrenceRules")]
    public List<RecurrenceRule>? ExcludedRecurrenceRules { get; init; }

    /// <summary>
    ///     Recurrence overrides (Section 4.3.5).
    ///     Keys are LocalDateTime instances, values are PatchObjects.
    /// </summary>
    [JsonPropertyName("recurrenceOverrides")]
    public Dictionary<LocalDateTime, PatchObject>? RecurrenceOverrides { get; init; }

    /// <summary>
    ///     Whether excluded (Section 4.3.6).
    ///     Default: false
    /// </summary>
    [JsonPropertyName("excluded")]
    public bool Excluded { get; init; } = false;

    // Sharing and Scheduling Properties (Section 4.4)

    /// <summary>
    ///     Priority (Section 4.4.1).
    ///     Default: 0
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
    ///     Privacy (Section 4.4.3).
    ///     Default: Public
    /// </summary>
    [JsonPropertyName("privacy")]
    public Privacy Privacy { get; init; } = Privacy.Public;

    /// <summary>
    ///     Reply methods (Section 4.4.4).
    /// </summary>
    [JsonPropertyName("replyTo")]
    public Dictionary<string, string>? ReplyTo { get; init; }

    /// <summary>
    ///     Sent by (Section 4.4.5).
    /// </summary>
    [JsonPropertyName("sentBy")]
    public string? SentBy { get; init; }

    /// <summary>
    ///     Participants (Section 4.4.6).
    /// </summary>
    [JsonPropertyName("participants")]
    public Dictionary<string, Participant>? Participants { get; init; }

    /// <summary>
    ///     Request status (Section 4.4.7).
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
    ///     Alerts (Section 4.5.2).
    /// </summary>
    [JsonPropertyName("alerts")]
    public Dictionary<string, Alert>? Alerts { get; init; }

    // Multilingual Properties (Section 4.6)

    /// <summary>
    ///     Localizations (Section 4.6.1).
    ///     Keys are language tags (RFC 5646), values are PatchObjects.
    /// </summary>
    [JsonPropertyName("localizations")]
    public Dictionary<string, PatchObject>? Localizations { get; init; }

    // Time Zone Properties (Section 4.7)

    /// <summary>
    ///     Time zone (Section 4.7.1).
    ///     Default: null
    /// </summary>
    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; init; }

    /// <summary>
    ///     Time zone definitions (Section 4.7.2).
    /// </summary>
    [JsonPropertyName("timeZones")]
    public Dictionary<string, TimeZone>? TimeZones { get; init; }

    // Task-specific Properties (Section 5.2)

    /// <summary>
    ///     Due date/time (Section 5.2.1).
    /// </summary>
    [JsonPropertyName("due")]
    public LocalDateTime? Due { get; init; }

    /// <summary>
    ///     Start date/time (Section 5.2.2).
    /// </summary>
    [JsonPropertyName("start")]
    public LocalDateTime? Start { get; init; }

    /// <summary>
    ///     Estimated duration (Section 5.2.3).
    /// </summary>
    [JsonPropertyName("estimatedDuration")]
    public Duration? EstimatedDuration { get; init; }

    /// <summary>
    ///     Percentage complete (Section 5.2.4).
    ///     Value between 0 and 100.
    /// </summary>
    [JsonPropertyName("percentComplete")]
    public int? PercentComplete { get; init; }

    /// <summary>
    ///     Progress status (Section 5.2.5).
    /// </summary>
    [JsonPropertyName("progress")]
    public ProgressStatus? Progress { get; init; }

    /// <summary>
    ///     Progress updated timestamp (Section 5.2.6).
    /// </summary>
    [JsonPropertyName("progressUpdated")]
    public DateTimeOffset? ProgressUpdated { get; init; }
}