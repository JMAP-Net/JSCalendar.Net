using System.Text.Json.Serialization;

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
    ///     Keys MUST be one of: "first", "next", "child", "parent", or vendor-specific.
    ///     Values MUST be true.
    /// </summary>
    [JsonPropertyName("relation")]
    public Dictionary<string, bool> RelationTypes { get; init; } = new();
}