using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Free/busy status (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum FreeBusyStatus
{
    /// <summary>
    ///     The object should be ignored when calculating whether the user is busy.
    /// </summary>
    [EnumMember(Value = "free")]
    Free,

    /// <summary>
    ///     The object should be included when calculating whether the user is busy.
    /// </summary>
    [EnumMember(Value = "busy")]
    Busy
}