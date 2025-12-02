using System.Text.Json.Serialization;
using JSCalendar.Net.Enums;

namespace JSCalendar.Net;

/// <summary>
///     Represents a relation between calendar objects (RFC 8984 Section 1.4.10).
///     Defines the relation to other objects using a set of relation types.
/// </summary>
public sealed class Relation
{
    /// <summary>
    ///     Type identifier. MUST be "Relation".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Relation";

    /// <summary>
    ///     Describes how the linked object is related to the linking object.
    ///     Default: empty Object (unspecified relationship)
    ///     Values MUST be true.
    /// </summary>
    [JsonPropertyName("relation")]
    public Dictionary<RelationType, bool> RelationTypes { get; init; } = new();
}