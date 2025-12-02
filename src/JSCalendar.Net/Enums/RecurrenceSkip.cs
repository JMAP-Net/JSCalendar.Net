using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Recurrence rule skip behavior (RFC 8984 Section 4.3.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum RecurrenceSkip
{
    /// <summary>
    ///     Omit invalid dates.
    /// </summary>
    [EnumMember(Value = "omit")]
    Omit,

    /// <summary>
    ///     Move invalid dates backward.
    /// </summary>
    [EnumMember(Value = "backward")]
    Backward,

    /// <summary>
    ///     Move invalid dates forward.
    /// </summary>
    [EnumMember(Value = "forward")]
    Forward
}