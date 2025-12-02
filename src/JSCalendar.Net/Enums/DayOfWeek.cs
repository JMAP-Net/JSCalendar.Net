using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Day of week values (RFC 8984 Section 4.3.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum DayOfWeek
{
    /// <summary>
    ///     Monday.
    /// </summary>
    [EnumMember(Value = "mo")]
    Monday,

    /// <summary>
    ///     Tuesday.
    /// </summary>
    [EnumMember(Value = "tu")]
    Tuesday,

    /// <summary>
    ///     Wednesday.
    /// </summary>
    [EnumMember(Value = "we")]
    Wednesday,

    /// <summary>
    ///     Thursday.
    /// </summary>
    [EnumMember(Value = "th")]
    Thursday,

    /// <summary>
    ///     Friday.
    /// </summary>
    [EnumMember(Value = "fr")]
    Friday,

    /// <summary>
    ///     Saturday.
    /// </summary>
    [EnumMember(Value = "sa")]
    Saturday,

    /// <summary>
    ///     Sunday.
    /// </summary>
    [EnumMember(Value = "su")]
    Sunday
}
