using System.Text.Json;
using System.Text.Json.Serialization;

namespace JSCalendar.Net.Converters;

/// <summary>
///     JSON converter for IJSCalendarObject interface to support polymorphic deserialization.
///     This converter reads the @type property to determine whether to deserialize an Event or Task.
/// </summary>
public class JSCalendarObjectConverter : JsonConverter<IJSCalendarObject>
{
    public override IJSCalendarObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read the JSON as a JsonDocument to inspect the @type property
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        // Check for @type property
        if (!root.TryGetProperty("@type", out var typeProperty))
        {
            throw new JsonException("Missing @type property in JSCalendar object");
        }

        var typeName = typeProperty.GetString();

        // Deserialize based on the @type value
        var json = root.GetRawText();
        return typeName switch
        {
            "Event" => JsonSerializer.Deserialize<Event>(json, options),
            "Task" => JsonSerializer.Deserialize<Task>(json, options),
            _ => throw new JsonException($"Unknown JSCalendar object type: {typeName}")
        };
    }

    public override void Write(Utf8JsonWriter writer, IJSCalendarObject value, JsonSerializerOptions options)
    {
        // Serialize the concrete type
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
