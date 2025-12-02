using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net;

/// <summary>
///     Represents a local date-time without timezone information (RFC 8984).
///     Format: YYYY-MM-DDTHH:MM:SS
/// </summary>
[JsonConverter(typeof(LocalDateTimeJsonConverter))]
public sealed record LocalDateTime(DateTime Value)
{
    public static implicit operator DateTime(LocalDateTime ldt)
    {
        return ldt.Value;
    }

    public static implicit operator LocalDateTime(DateTime dt)
    {
        return new LocalDateTime(dt);
    }

    public override string ToString()
    {
        return Value.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}