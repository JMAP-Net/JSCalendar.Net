using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace JSCalendar.Net.Converters;

/// <summary>
///     JSON converter for Duration type.
///     Handles serialization/deserialization of Duration to/from ISO 8601 duration strings.
/// </summary>
public partial class DurationJsonConverter : JsonConverter<Duration>
{
    private static readonly Regex DurationRegex = CompiledDurationRegex();

    public override Duration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var durationString = reader.GetString();

            return string.IsNullOrEmpty(durationString)
                ? throw new JsonException("Duration cannot be null or empty")
                : ParseDuration(durationString);
        }

        throw new JsonException($"Unexpected token type: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, Duration value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    private static Duration ParseDuration(string durationString)
    {
        var match = DurationRegex.Match(durationString);
        if (!match.Success) throw new JsonException($"Invalid duration format: {durationString}");

        return new Duration
        {
            Years = ParseIntGroup(match.Groups[1]),
            Months = ParseIntGroup(match.Groups[2]),
            Weeks = ParseIntGroup(match.Groups[3]),
            Days = ParseIntGroup(match.Groups[4]),
            Hours = ParseIntGroup(match.Groups[5]),
            Minutes = ParseIntGroup(match.Groups[6]),
            Seconds = ParseIntGroup(match.Groups[7])
        };
    }

    private static int? ParseIntGroup(System.Text.RegularExpressions.Group group)
    {
        if (group.Success && int.TryParse(group.Value, out var value)) return value;

        return null;
    }

    [GeneratedRegex(@"^P(?:(\d+)Y)?(?:(\d+)M)?(?:(\d+)W)?(?:(\d+)D)?(?:T(?:(\d+)H)?(?:(\d+)M)?(?:(\d+)S)?)?$",
        RegexOptions.Compiled)]
    private static partial Regex CompiledDurationRegex();
}