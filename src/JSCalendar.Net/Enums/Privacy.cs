using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Privacy classification (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum Privacy
{
    /// <summary>
    ///     The full details of the object are visible.
    /// </summary>
    [EnumMember(Value = "public")]
    Public,

    /// <summary>
    ///     The details of the object are hidden; only the basic time and metadata are shared.
    /// </summary>
    [EnumMember(Value = "private")]
    Private,

    /// <summary>
    ///     The object is hidden completely when the calendar is shared.
    /// </summary>
    [EnumMember(Value = "secret")]
    Secret
}