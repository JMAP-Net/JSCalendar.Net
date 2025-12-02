using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Participant roles (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum ParticipantRole
{
    /// <summary>
    ///     The participant is an owner of the object.
    /// </summary>
    [EnumMember(Value = "owner")]
    Owner,

    /// <summary>
    ///     The participant is expected to be present at the event.
    /// </summary>
    [EnumMember(Value = "attendee")]
    Attendee,

    /// <summary>
    ///     The participant's involvement with the event is optional.
    /// </summary>
    [EnumMember(Value = "optional")]
    Optional,

    /// <summary>
    ///     The participant is copied for informational reasons.
    /// </summary>
    [EnumMember(Value = "informational")]
    Informational,

    /// <summary>
    ///     The participant is in charge of the event/task when it occurs.
    /// </summary>
    [EnumMember(Value = "chair")]
    Chair,

    /// <summary>
    ///     The participant is someone that may be contacted for information.
    /// </summary>
    [EnumMember(Value = "contact")]
    Contact
}