using System.Text.Json.Serialization;

namespace JSCalendar.Net;

/// <summary>
///     Represents a collection of events and/or tasks in JSCalendar format (RFC 8984 Section 2.3).
///     A Group is used to organize related calendar objects together.
/// </summary>
public sealed class Group
{
    // Metadata Properties (Section 4.1)

    /// <summary>
    ///     Type identifier. MUST be "Group".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Group";

    /// <summary>
    ///     Globally unique identifier for this group (Section 4.1.2).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("uid")]
    public required string Uid { get; init; }

    /// <summary>
    ///     Producer identifier that created this group (Section 4.1.4).
    /// </summary>
    [JsonPropertyName("prodId")]
    public string? ProdId { get; init; }

    /// <summary>
    ///     Date and time this group was created (Section 4.1.5).
    /// </summary>
    [JsonPropertyName("created")]
    public DateTimeOffset? Created { get; init; }

    /// <summary>
    ///     Date and time this group was last updated (Section 4.1.6).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("updated")]
    public required DateTimeOffset Updated { get; init; }

    // What and Where Properties (Section 4.2)

    /// <summary>
    ///     Short summary or name of the group (Section 4.2.1).
    ///     Default: empty String
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; } = "";

    /// <summary>
    ///     Detailed description of the group (Section 4.2.2).
    ///     Default: empty String
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; } = "";

    /// <summary>
    ///     Content type of the description (Section 4.2.3).
    ///     Default: "text/plain"
    /// </summary>
    [JsonPropertyName("descriptionContentType")]
    public string DescriptionContentType { get; init; } = "text/plain";

    /// <summary>
    ///     Links to external resources associated with this group (Section 4.2.7).
    /// </summary>
    [JsonPropertyName("links")]
    public Dictionary<string, Link>? Links { get; init; }

    /// <summary>
    ///     Language tag for this group (Section 4.2.8).
    /// </summary>
    [JsonPropertyName("locale")]
    public string? Locale { get; init; }

    /// <summary>
    ///     Keywords or tags for this group (Section 4.2.9).
    /// </summary>
    [JsonPropertyName("keywords")]
    public Dictionary<string, bool>? Keywords { get; init; }

    /// <summary>
    ///     Categories for this group (Section 4.2.10).
    /// </summary>
    [JsonPropertyName("categories")]
    public Dictionary<string, bool>? Categories { get; init; }

    /// <summary>
    ///     Color to use when displaying this group (Section 4.2.11).
    /// </summary>
    [JsonPropertyName("color")]
    public string? Color { get; init; }

    // Time Zone Properties (Section 4.7)

    /// <summary>
    ///     Time zone definitions referenced by entries in this group (Section 4.7.2).
    /// </summary>
    [JsonPropertyName("timeZones")]
    public Dictionary<string, TimeZone>? TimeZones { get; init; }

    // Group-specific Properties (Section 5.3)

    /// <summary>
    ///     Collection of Event and/or Task objects in this group (Section 5.3.1).
    ///     REQUIRED property.
    /// </summary>
    [JsonPropertyName("entries")]
    public required List<IJSCalendarObject> Entries { get; init; }

    /// <summary>
    ///     Source URI from which updated versions of this group may be retrieved (Section 5.3.2).
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; init; }
}
