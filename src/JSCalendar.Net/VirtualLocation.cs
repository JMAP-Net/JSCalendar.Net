using System.Text.Json.Serialization;
using JSCalendar.Net.Enums;

namespace JSCalendar.Net;

/// <summary>
///     Represents a virtual location like a video conference (RFC 8984 Section 4.2.6).
/// </summary>
public sealed class VirtualLocation
{
    /// <summary>
    ///     Type identifier. MUST be "VirtualLocation".
    /// </summary>
    [JsonPropertyName("@type")]
    public string Type { get; init; } = "VirtualLocation";

    /// <summary>
    ///     Human-readable name of the virtual location.
    ///     Default: empty String
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = "";

    /// <summary>
    ///     Human-readable instructions for accessing this virtual location.
    ///     May be a conference access code, etc.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    ///     URI that represents how to connect to this virtual location.
    ///     REQUIRED property.
    ///     May be a telephone number (tel:), web address, or custom URI.
    /// </summary>
    [JsonPropertyName("uri")]
    public required string Uri { get; init; }

    /// <summary>
    ///     Features supported by this virtual location.
    /// </summary>
    [JsonPropertyName("features")]
    public Dictionary<VirtualLocationFeature, bool>? Features { get; init; }
}