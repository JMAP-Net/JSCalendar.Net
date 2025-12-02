using System.Text.Json.Serialization;

namespace JSCalendar.Net;

/// <summary>
///     Represents a participant in a calendar event (RFC 8984 Section 4.4.6).
/// </summary>
public sealed class Participant
{
    /// <summary>
    ///     Type identifier. MUST be "Participant".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Participant";

    /// <summary>
    ///     Display name of the participant.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    ///     Email address of the participant.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>
    ///     Plain-text description of this participant.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    ///     Methods by which the participant may receive invitations.
    ///     Keys: method names (e.g., "imip", "other")
    ///     Values: URIs for the method
    /// </summary>
    [JsonPropertyName("sendTo")]
    public Dictionary<string, string>? SendTo { get; init; }

    /// <summary>
    ///     What kind of entity this participant is.
    ///     Values: "individual", "group", "location", "resource"
    /// </summary>
    [JsonPropertyName("kind")]
    public string? Kind { get; init; }

    /// <summary>
    ///     Roles that this participant fulfills.
    ///     REQUIRED property. At least one role MUST be specified.
    ///     Values: "owner", "attendee", "optional", "informational", "chair", "contact"
    /// </summary>
    [JsonPropertyName("roles")]
    public required Dictionary<string, bool> Roles { get; init; }

    /// <summary>
    ///     Location ID where this participant is expected to be.
    /// </summary>
    [JsonPropertyName("locationId")]
    public string? LocationId { get; init; }

    /// <summary>
    ///     Language tag (RFC 5646) for participant's preferred language.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; init; }

    /// <summary>
    ///     Participation status of this participant.
    ///     Default: "needs-action"
    ///     Values: "needs-action", "accepted", "declined", "tentative", "delegated"
    /// </summary>
    [JsonPropertyName("participationStatus")]
    public string ParticipationStatus { get; init; } = "needs-action";

    /// <summary>
    ///     Note from the participant to explain their participation status.
    /// </summary>
    [JsonPropertyName("participationComment")]
    public string? ParticipationComment { get; init; }

    /// <summary>
    ///     Whether the organizer expects the participant to notify them.
    ///     Default: false
    /// </summary>
    [JsonPropertyName("expectReply")]
    public bool ExpectReply { get; init; } = false;

    /// <summary>
    ///     Who is responsible for sending scheduling messages.
    ///     Default: "server"
    ///     Values: "server", "client", "none"
    /// </summary>
    [JsonPropertyName("scheduleAgent")]
    public string ScheduleAgent { get; init; } = "server";

    /// <summary>
    ///     Request the server to send a scheduling message.
    ///     Default: false
    ///     MUST NOT be stored on the server.
    /// </summary>
    [JsonPropertyName("scheduleForceSend")]
    public bool ScheduleForceSend { get; init; } = false;

    /// <summary>
    ///     Sequence number of the last response from the participant.
    ///     Default: 0
    /// </summary>
    [JsonPropertyName("scheduleSequence")]
    public int ScheduleSequence { get; init; } = 0;

    /// <summary>
    ///     Status codes from processing the most recent scheduling message.
    /// </summary>
    [JsonPropertyName("scheduleStatus")]
    public List<string>? ScheduleStatus { get; init; }

    /// <summary>
    ///     Timestamp for the most recent response from this participant.
    /// </summary>
    [JsonPropertyName("scheduleUpdated")]
    public DateTimeOffset? ScheduleUpdated { get; init; }

    /// <summary>
    ///     Email address in the "From" header of the email.
    /// </summary>
    [JsonPropertyName("sentBy")]
    public string? SentBy { get; init; }

    /// <summary>
    ///     ID of the participant who added this participant.
    /// </summary>
    [JsonPropertyName("invitedBy")]
    public string? InvitedBy { get; init; }

    /// <summary>
    ///     Participant IDs that this participant has delegated to.
    /// </summary>
    [JsonPropertyName("delegatedTo")]
    public Dictionary<string, bool>? DelegatedTo { get; init; }

    /// <summary>
    ///     Participant IDs that this participant is acting as delegate for.
    /// </summary>
    [JsonPropertyName("delegatedFrom")]
    public Dictionary<string, bool>? DelegatedFrom { get; init; }

    /// <summary>
    ///     Group participant IDs that caused this participant to be invited.
    /// </summary>
    [JsonPropertyName("memberOf")]
    public Dictionary<string, bool>? MemberOf { get; init; }

    /// <summary>
    ///     Links to external resources associated with this participant.
    /// </summary>
    [JsonPropertyName("links")]
    public Dictionary<string, Link>? Links { get; init; }

    /// <summary>
    ///     Progress of the participant for a task.
    ///     Only allowed for Task participants.
    /// </summary>
    [JsonPropertyName("progress")]
    public string? Progress { get; init; }

    /// <summary>
    ///     Date-time the progress property was last set.
    ///     Only allowed for Task participants.
    /// </summary>
    [JsonPropertyName("progressUpdated")]
    public DateTimeOffset? ProgressUpdated { get; init; }

    /// <summary>
    ///     Percent completion of the participant for a task.
    ///     Only allowed for Task participants.
    ///     Value between 0 and 100.
    /// </summary>
    [JsonPropertyName("percentComplete")]
    public int? PercentComplete { get; init; }
}