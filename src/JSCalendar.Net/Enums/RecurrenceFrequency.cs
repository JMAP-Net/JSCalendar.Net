using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Recurrence rule frequency values (RFC 8984 Section 4.3.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum RecurrenceFrequency
{
    /// <summary>
    ///     Yearly recurrence.
    /// </summary>
    [EnumMember(Value = "yearly")]
    Yearly,

    /// <summary>
    ///     Monthly recurrence.
    /// </summary>
    [EnumMember(Value = "monthly")]
    Monthly,

    /// <summary>
    ///     Weekly recurrence.
    /// </summary>
    [EnumMember(Value = "weekly")]
    Weekly,

    /// <summary>
    ///     Daily recurrence.
    /// </summary>
    [EnumMember(Value = "daily")]
    Daily,

    /// <summary>
    ///     Hourly recurrence.
    /// </summary>
    [EnumMember(Value = "hourly")]
    Hourly,

    /// <summary>
    ///     Minutely recurrence.
    /// </summary>
    [EnumMember(Value = "minutely")]
    Minutely,

    /// <summary>
    ///     Secondly recurrence.
    /// </summary>
    [EnumMember(Value = "secondly")]
    Secondly
}