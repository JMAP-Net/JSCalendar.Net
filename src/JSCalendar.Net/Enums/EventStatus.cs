using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Event status types (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum EventStatus
{
    /// <summary>
    ///     Indicates the event is definitely happening.
    /// </summary>
    [EnumMember(Value = "confirmed")]
    Confirmed,

    /// <summary>
    ///     Indicates the event has been cancelled.
    /// </summary>
    [EnumMember(Value = "cancelled")]
    Cancelled,

    /// <summary>
    ///     Indicates the event may happen.
    /// </summary>
    [EnumMember(Value = "tentative")]
    Tentative
}