using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net;

/// <summary>
///     Marker interface for JSCalendar objects that can be entries in a Group.
///     This interface is implemented by Event and Task objects.
/// </summary>
[JsonConverter(typeof(JSCalendarObjectConverter))]
public interface IJSCalendarObject
{
    /// <summary>
    ///     Type identifier for this JSCalendar object.
    /// </summary>
    string Type { get; }

    /// <summary>
    ///     Globally unique identifier for this object.
    /// </summary>
    string Uid { get; }

    /// <summary>
    ///     Date and time this object was last updated.
    /// </summary>
    DateTimeOffset Updated { get; }
}
