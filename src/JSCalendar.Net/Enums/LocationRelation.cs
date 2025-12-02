using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Location relative-to values (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum LocationRelation
{
    /// <summary>
    ///     The event/task occurs at this location at the start time.
    /// </summary>
    [EnumMember(Value = "start")]
    Start,

    /// <summary>
    ///     The event/task occurs at this location at the end time.
    /// </summary>
    [EnumMember(Value = "end")]
    End
}