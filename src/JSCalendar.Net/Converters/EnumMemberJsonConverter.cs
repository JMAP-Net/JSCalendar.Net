using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JSCalendar.Net.Converters;

/// <summary>
///     Generic JSON converter for enums that uses EnumMember attribute values for serialization.
///     This allows enums to be serialized with custom string values like "needs-action" instead of integer values.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert.</typeparam>
public class EnumMemberJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    private readonly Dictionary<TEnum, string> _enumToString = new();
    private readonly Dictionary<string, TEnum> _stringToEnum = new();

    public EnumMemberJsonConverter()
    {
        var enumType = typeof(TEnum);
        var enumValues = Enum.GetValues<TEnum>();

        foreach (var value in enumValues)
        {
            var memberInfo = enumType.GetMember(value.ToString())[0];
            var enumMemberAttribute = memberInfo.GetCustomAttribute<EnumMemberAttribute>();

            var stringValue = enumMemberAttribute?.Value ?? value.ToString().ToLowerInvariant();

            _enumToString[value] = stringValue;
            _stringToEnum[stringValue] = value;
        }
    }

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected string value for enum {typeof(TEnum).Name}");
        }

        var stringValue = reader.GetString();
        if (string.IsNullOrEmpty(stringValue))
        {
            throw new JsonException($"Null or empty string for enum {typeof(TEnum).Name}");
        }

        if (_stringToEnum.TryGetValue(stringValue, out var enumValue))
        {
            return enumValue;
        }

        throw new JsonException($"Unknown enum value '{stringValue}' for enum {typeof(TEnum).Name}");
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (_enumToString.TryGetValue(value, out var stringValue))
        {
            writer.WriteStringValue(stringValue);
        }
        else
        {
            writer.WriteStringValue(value.ToString().ToLowerInvariant());
        }
    }

    public override TEnum ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        if (string.IsNullOrEmpty(stringValue))
        {
            throw new JsonException($"Null or empty string for enum property name {typeof(TEnum).Name}");
        }

        if (_stringToEnum.TryGetValue(stringValue, out var enumValue))
        {
            return enumValue;
        }

        throw new JsonException($"Unknown enum value '{stringValue}' for enum property name {typeof(TEnum).Name}");
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (_enumToString.TryGetValue(value, out var stringValue))
        {
            writer.WritePropertyName(stringValue);
        }
        else
        {
            writer.WritePropertyName(value.ToString().ToLowerInvariant());
        }
    }
}

/// <summary>
///     Factory for creating EnumMemberJsonConverter instances.
/// </summary>
public class EnumMemberJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(EnumMemberJsonConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter?)Activator.CreateInstance(converterType);
    }
}
