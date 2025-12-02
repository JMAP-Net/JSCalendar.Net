using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Virtual location features (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum VirtualLocationFeature
{
    /// <summary>
    ///     Audio conferencing.
    /// </summary>
    [EnumMember(Value = "audio")]
    Audio,

    /// <summary>
    ///     Chat or instant messaging.
    /// </summary>
    [EnumMember(Value = "chat")]
    Chat,

    /// <summary>
    ///     Blog or atom feed.
    /// </summary>
    [EnumMember(Value = "feed")]
    Feed,

    /// <summary>
    ///     Provides moderator-specific features.
    /// </summary>
    [EnumMember(Value = "moderator")]
    Moderator,

    /// <summary>
    ///     Phone conferencing.
    /// </summary>
    [EnumMember(Value = "phone")]
    Phone,

    /// <summary>
    ///     Screen sharing.
    /// </summary>
    [EnumMember(Value = "screen")]
    Screen,

    /// <summary>
    ///     Video conferencing.
    /// </summary>
    [EnumMember(Value = "video")]
    Video
}