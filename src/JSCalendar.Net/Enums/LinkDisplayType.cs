using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Link display types for icons (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum LinkDisplayType
{
    /// <summary>
    ///     An image meant to be displayed alongside the title.
    /// </summary>
    [EnumMember(Value = "badge")]
    Badge,

    /// <summary>
    ///     A full image replacement for the object itself.
    /// </summary>
    [EnumMember(Value = "graphic")]
    Graphic,

    /// <summary>
    ///     An image that is used to enhance the object.
    /// </summary>
    [EnumMember(Value = "fullsize")]
    Fullsize,

    /// <summary>
    ///     A smaller variant of fullsize to be used when space for the image is constrained.
    /// </summary>
    [EnumMember(Value = "thumbnail")]
    Thumbnail
}