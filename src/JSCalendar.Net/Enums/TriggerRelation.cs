using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Offset trigger relative-to values (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum TriggerRelation
{
    /// <summary>
    ///     Triggers the alert relative to the start of the calendar object.
    /// </summary>
    [EnumMember(Value = "start")]
    Start,

    /// <summary>
    ///     Triggers the alert relative to the end/due time of the calendar object.
    /// </summary>
    [EnumMember(Value = "end")]
    End
}