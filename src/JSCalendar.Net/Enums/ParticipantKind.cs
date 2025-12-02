using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Participant kind types (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum ParticipantKind
{
    /// <summary>
    ///     A single person.
    /// </summary>
    [EnumMember(Value = "individual")]
    Individual,

    /// <summary>
    ///     A collection of people invited as a whole.
    /// </summary>
    [EnumMember(Value = "group")]
    Group,

    /// <summary>
    ///     A non-human resource other than a location.
    /// </summary>
    [EnumMember(Value = "resource")]
    Resource,

    /// <summary>
    ///     A physical location that needs to be scheduled.
    /// </summary>
    [EnumMember(Value = "location")]
    Location
}