using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Relation types (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum RelationType
{
    /// <summary>
    ///     The linked object is the first in a series the linking object is part of.
    /// </summary>
    [EnumMember(Value = "first")]
    First,

    /// <summary>
    ///     The linked object is next in a series the linking object is part of.
    /// </summary>
    [EnumMember(Value = "next")]
    Next,

    /// <summary>
    ///     The linked object is a subpart of the linking object.
    /// </summary>
    [EnumMember(Value = "child")]
    Child,

    /// <summary>
    ///     The linking object is a subpart of the linked object.
    /// </summary>
    [EnumMember(Value = "parent")]
    Parent
}