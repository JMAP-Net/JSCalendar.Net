using System.Text.Json;
using System.Text.Json.Serialization;

namespace JSCalendar.Net.Converters;

/// <summary>
///     JSON converter for LocalDateTime type.
///     Handles serialization/deserialization of LocalDateTime to/from ISO 8601 strings.
/// </summary>
public class LocalDateTimeJsonConverter : JsonConverter<LocalDateTime>
{
    public override LocalDateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var dateString = reader.GetString();
            if (string.IsNullOrEmpty(dateString)) throw new JsonException("LocalDateTime cannot be null or empty");

            // Parse ISO 8601 date/time string
            return DateTime.TryParse(dateString, out var dateTime)
                ? new LocalDateTime(dateTime)
                : throw new JsonException($"Unable to parse '{dateString}' as LocalDateTime");
        }

        throw new JsonException($"Unexpected token type: {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, LocalDateTime value, JsonSerializerOptions options)
    {
        // Write as ISO 8601 string
        writer.WriteStringValue(value.ToString());
    }

    /// <summary>
    ///     Reads LocalDateTime as a dictionary key (property name).
    ///     Required for Dictionary&lt;LocalDateTime, T&gt; support.
    /// </summary>
    public override LocalDateTime ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.PropertyName)
            throw new JsonException($"Expected PropertyName token, got {reader.TokenType}");

        var propertyName = reader.GetString();
        if (string.IsNullOrEmpty(propertyName))
            throw new JsonException("LocalDateTime property name cannot be null or empty");

        // Parse ISO 8601 date/time string
        return DateTime.TryParse(propertyName, out var dateTime)
            ? new LocalDateTime(dateTime)
            : throw new JsonException($"Unable to parse '{propertyName}' as LocalDateTime property name");
    }

    /// <summary>
    ///     Writes LocalDateTime as a dictionary key (property name).
    ///     Required for Dictionary&lt;LocalDateTime, T&gt; support.
    /// </summary>
    public override void WriteAsPropertyName(Utf8JsonWriter writer, LocalDateTime value, JsonSerializerOptions options)
    {
        // Write as ISO 8601 string property name
        writer.WritePropertyName(value.ToString());
    }
}