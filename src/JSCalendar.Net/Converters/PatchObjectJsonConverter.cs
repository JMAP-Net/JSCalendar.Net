using System.Text.Json;
using System.Text.Json.Serialization;

namespace JSCalendar.Net.Converters;

/// <summary>
///     JSON converter for PatchObject that serializes/deserializes as a flat object.
///     Handles the String[*] format from RFC 8984 Section 1.4.9.
/// </summary>
public class PatchObjectJsonConverter : JsonConverter<PatchObject>
{
    public override PatchObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException($"Expected StartObject token, got {reader.TokenType}");

        var patchObject = new PatchObject();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException($"Expected PropertyName token, got {reader.TokenType}");

            var propertyName = reader.GetString();
            if (string.IsNullOrEmpty(propertyName))
                throw new JsonException("Property name cannot be null or empty");

            // Validate pointer format
            if (!PatchObject.IsValidPointer(propertyName))
                throw new JsonException(
                    $"Invalid patch pointer: '{propertyName}'. Pointer must not reference array indices.");

            reader.Read();

            // Read the value
            var value = ReadValue(ref reader, options);
            patchObject[propertyName] = value;
        }

        // Validate the entire PatchObject
        return !patchObject.Validate()
            ? throw new JsonException("Invalid PatchObject: contains prefix conflicts or invalid pointers")
            : patchObject;
    }

    public override void Write(Utf8JsonWriter writer, PatchObject value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        // Validate before serialization
        if (!value.Validate())
            throw new JsonException(
                "Cannot serialize invalid PatchObject: contains prefix conflicts or invalid pointers");

        writer.WriteStartObject();

        foreach (var kvp in value)
        {
            writer.WritePropertyName(kvp.Key);
            WriteValue(writer, kvp.Value, options);
        }

        writer.WriteEndObject();
    }

    private static object? ReadValue(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Number => reader.TryGetInt64(out var longValue)
                ? longValue
                : reader.GetDouble(),
            JsonTokenType.String => reader.GetString(),
            JsonTokenType.StartObject => ReadObject(ref reader, options),
            JsonTokenType.StartArray => ReadArray(ref reader, options),
            _ => throw new JsonException($"Unexpected token type: {reader.TokenType}")
        };
    }

    private static Dictionary<string, object?> ReadObject(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        var obj = new Dictionary<string, object?>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException($"Expected PropertyName, got {reader.TokenType}");

            var propertyName = reader.GetString()!;
            reader.Read();
            obj[propertyName] = ReadValue(ref reader, options);
        }

        return obj;
    }

    private static List<object?> ReadArray(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        var array = new List<object?>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                break;

            array.Add(ReadValue(ref reader, options));
        }

        return array;
    }

    private static void WriteValue(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case null:
                writer.WriteNullValue();
                break;
            case bool boolValue:
                writer.WriteBooleanValue(boolValue);
                break;
            case int intValue:
                writer.WriteNumberValue(intValue);
                break;
            case long longValue:
                writer.WriteNumberValue(longValue);
                break;
            case double doubleValue:
                writer.WriteNumberValue(doubleValue);
                break;
            case float floatValue:
                writer.WriteNumberValue(floatValue);
                break;
            case decimal decimalValue:
                writer.WriteNumberValue(decimalValue);
                break;
            case string stringValue:
                writer.WriteStringValue(stringValue);
                break;
            case Dictionary<string, object?> dictValue:
                writer.WriteStartObject();
                foreach (var kvp in dictValue)
                {
                    writer.WritePropertyName(kvp.Key);
                    WriteValue(writer, kvp.Value, options);
                }

                writer.WriteEndObject();
                break;
            case IEnumerable<object?> arrayValue:
                writer.WriteStartArray();
                foreach (var item in arrayValue) WriteValue(writer, item, options);
                writer.WriteEndArray();
                break;
            default:
                // Fallback to System.Text.Json serialization
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
                break;
        }
    }
}