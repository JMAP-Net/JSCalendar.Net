using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Schedule agent types (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum ScheduleAgent
{
    /// <summary>
    ///     The calendar server will send the scheduling messages.
    /// </summary>
    [EnumMember(Value = "server")]
    Server,

    /// <summary>
    ///     The calendar client will send the scheduling messages.
    /// </summary>
    [EnumMember(Value = "client")]
    Client,

    /// <summary>
    ///     No scheduling messages are to be sent to this participant.
    /// </summary>
    [EnumMember(Value = "none")]
    None
}