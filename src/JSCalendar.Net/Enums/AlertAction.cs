using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Alert action types (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum AlertAction
{
    /// <summary>
    ///     Display the alert.
    /// </summary>
    [EnumMember(Value = "display")]
    Display,

    /// <summary>
    ///     Send an email alert.
    /// </summary>
    [EnumMember(Value = "email")]
    Email
}