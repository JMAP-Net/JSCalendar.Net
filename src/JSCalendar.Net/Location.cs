using System.Text.Json.Serialization;

namespace JSCalendar.Net;

/// <summary>
///     Represents a physical location (RFC 8984 Section 4.2.5).
///     MUST have at least one property other than relativeTo.
/// </summary>
public sealed class Location
{
    /// <summary>
    ///     Type identifier. MUST be "Location".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "Location";

    /// <summary>
    ///     Human-readable name of the location.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    ///     Human-readable instructions for accessing this location.
    ///     May be an address, directions, door code, etc.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    ///     Location types from the Location Types Registry (RFC 4589).
    /// </summary>
    [JsonPropertyName("locationTypes")]
    public Dictionary<string, bool>? LocationTypes { get; init; }

    /// <summary>
    ///     Relation between this location and the time of the event.
    ///     Values: "start" or "end"
    /// </summary>
    [JsonPropertyName("relativeTo")]
    public string? RelativeTo { get; init; }

    /// <summary>
    ///     Time zone for this location.
    /// </summary>
    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; init; }

    /// <summary>
    ///     Coordinates as a "geo:" URI (RFC 5870).
    /// </summary>
    [JsonPropertyName("coordinates")]
    public string? Coordinates { get; init; }

    /// <summary>
    ///     Links to external resources associated with this location.
    /// </summary>
    [JsonPropertyName("links")]
    public Dictionary<string, Link>? Links { get; init; }
}