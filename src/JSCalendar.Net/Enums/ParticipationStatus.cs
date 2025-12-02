using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Participation status (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum ParticipationStatus
{
    /// <summary>
    ///     No status has yet been set by the participant.
    /// </summary>
    [EnumMember(Value = "needs-action")]
    NeedsAction,

    /// <summary>
    ///     The invited participant will participate.
    /// </summary>
    [EnumMember(Value = "accepted")]
    Accepted,

    /// <summary>
    ///     The invited participant will not participate.
    /// </summary>
    [EnumMember(Value = "declined")]
    Declined,

    /// <summary>
    ///     The invited participant may participate.
    /// </summary>
    [EnumMember(Value = "tentative")]
    Tentative,

    /// <summary>
    ///     The invited participant has delegated their attendance to another participant.
    /// </summary>
    [EnumMember(Value = "delegated")]
    Delegated
}