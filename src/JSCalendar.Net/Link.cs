using System.Text.Json.Serialization;

namespace JSCalendar.Net;

/// <summary>
///     Represents a link to external resources (RFC 8984 Section 1.4.11).
/// </summary>
public sealed class Link
{
    /// <summary>
    ///     Type identifier. MUST be "Link".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Link";

    /// <summary>
    ///     URI from which the resource may be fetched.
    ///     REQUIRED property.
    ///     MAY be a data: URL, but hosting on a server is recommended.
    /// </summary>
    [JsonPropertyName("href")]
    public required string Href { get; init; }

    /// <summary>
    ///     Valid content-id value according to RFC 2392.
    ///     MUST be unique within this Link object.
    ///     MAY differ from the link id for this Link object.
    /// </summary>
    [JsonPropertyName("cid")]
    public string? Cid { get; init; }

    /// <summary>
    ///     Media type (RFC 6838) of the resource, if known.
    /// </summary>
    [JsonPropertyName("contentType")]
    public string? ContentType { get; init; }

    /// <summary>
    ///     Size in octets of the resource when fully decoded, if known.
    ///     This is an informational estimate.
    /// </summary>
    [JsonPropertyName("size")]
    public long? Size { get; init; }

    /// <summary>
    ///     Relation of the linked resource to the object.
    ///     If set, MUST be a relation type from the IANA "Link Relations" registry (RFC 8288).
    /// </summary>
    [JsonPropertyName("rel")]
    public string? Rel { get; init; }

    /// <summary>
    ///     Intended purpose of a link to an image.
    ///     If set, the 'rel' property MUST be set to "icon".
    ///     Values: "badge", "graphic", "fullsize", "thumbnail"
    /// </summary>
    [JsonPropertyName("display")]
    public string? Display { get; init; }

    /// <summary>
    ///     Human-readable, plain-text description of the resource.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }
}