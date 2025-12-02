using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class PatchObjectJsonConverterTests
{
    private readonly JsonSerializerOptions _options = new() { WriteIndented = false };

    [Fact]
    public void Serialize_EmptyPatchObject_ReturnsEmptyObject()
    {
        // Arrange
        var patch = new PatchObject();

        // Act
        var json = JsonSerializer.Serialize(patch, _options);

        // Assert
        Assert.Equal("{}", json);
    }

    [Fact]
    public void Serialize_SimplePatchObject_ReturnsCorrectJson()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["title"] = "Test Title",
            ["description"] = "Test Description"
        };

        // Act
        var json = JsonSerializer.Serialize(patch, _options);

        // Assert
        Assert.Contains("\"title\":\"Test Title\"", json);
        Assert.Contains("\"description\":\"Test Description\"", json);
    }

    [Fact]
    public void Serialize_WithNullValue_SerializesNull()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["title"] = "Test",
            ["alerts"] = null
        };

        // Act
        var json = JsonSerializer.Serialize(patch, _options);

        // Assert
        Assert.Contains("\"alerts\":null", json);
    }

    [Fact]
    public void Serialize_WithNumericValues_SerializesCorrectly()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["priority"] = 5,
            ["percentComplete"] = 75,
            ["rating"] = 4.5
        };

        // Act
        var json = JsonSerializer.Serialize(patch, _options);

        // Assert
        Assert.Contains("\"priority\":5", json);
        Assert.Contains("\"percentComplete\":75", json);
        Assert.Contains("\"rating\":4.5", json);
    }

    [Fact]
    public void Serialize_WithBooleanValues_SerializesCorrectly()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["excluded"] = true,
            ["showWithoutTime"] = false
        };

        // Act
        var json = JsonSerializer.Serialize(patch, _options);

        // Assert
        Assert.Contains("\"excluded\":true", json);
        Assert.Contains("\"showWithoutTime\":false", json);
    }

    [Fact]
    public void Serialize_WithNestedObject_SerializesCorrectly()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["location"] = new Dictionary<string, object?>
            {
                ["name"] = "Conference Room",
                ["floor"] = 3
            }
        };

        // Act
        var json = JsonSerializer.Serialize(patch, _options);

        // Assert
        Assert.Contains("\"location\":{", json);
        Assert.Contains("\"name\":\"Conference Room\"", json);
        Assert.Contains("\"floor\":3", json);
    }

    [Fact]
    public void Serialize_WithArray_SerializesCorrectly()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["tags"] = new List<object?> { "work", "meeting", "important" }
        };

        // Act
        var json = JsonSerializer.Serialize(patch, _options);

        // Assert
        Assert.Contains("\"tags\":[\"work\",\"meeting\",\"important\"]", json);
    }

    [Fact]
    public void Serialize_InvalidPatchObject_ThrowsJsonException()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["items/0/name"] = "Invalid" // Array index
        };

        // Act & Assert
        var exception = Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize(patch, _options));
        Assert.Contains("invalid", exception.Message.ToLower());
    }

    [Fact]
    public void Deserialize_EmptyObject_ReturnsEmptyPatchObject()
    {
        // Arrange
        var json = "{}";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(patch);
        Assert.Empty(patch);
    }

    [Fact]
    public void Deserialize_SimplePatchObject_ReturnsCorrectObject()
    {
        // Arrange
        var json = """{"title":"Test Title","description":"Test Description"}""";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(patch);
        Assert.Equal(2, patch.Count);
        Assert.Equal("Test Title", patch["title"]);
        Assert.Equal("Test Description", patch["description"]);
    }

    [Fact]
    public void Deserialize_WithNullValue_ParsesCorrectly()
    {
        // Arrange
        var json = """{"title":"Test","alerts":null}""";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(patch);
        Assert.Equal(2, patch.Count);
        Assert.Null(patch["alerts"]);
    }

    [Fact]
    public void Deserialize_WithNumericValues_ParsesCorrectly()
    {
        // Arrange
        var json = """{"priority":5,"percentComplete":75,"rating":4.5}""";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(patch);

        Assert.Equal(5d, patch["priority"]);
        Assert.Equal(75d, patch["percentComplete"]);
        Assert.Equal(4.5d, patch["rating"]);
    }

    [Fact]
    public void Deserialize_WithBooleanValues_ParsesCorrectly()
    {
        // Arrange
        var json = """{"excluded":true,"showWithoutTime":false}""";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(patch);
        Assert.True((bool)patch["excluded"]!);
        Assert.False((bool)patch["showWithoutTime"]!);
    }

    [Fact]
    public void Deserialize_WithNestedObject_ParsesCorrectly()
    {
        // Arrange
        var json = """{"location":{"name":"Conference Room","floor":3}}""";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(patch);
        var location = Assert.IsType<Dictionary<string, object?>>(patch["location"]);
        Assert.Equal("Conference Room", location["name"]);

        Assert.Equal(3d, location["floor"]);
    }

    [Fact]
    public void Deserialize_WithArray_ParsesCorrectly()
    {
        // Arrange
        var json = """{"tags":["work","meeting","important"]}""";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(patch);
        var tags = Assert.IsType<List<object?>>(patch["tags"]);
        Assert.Equal(3, tags.Count);
        Assert.Equal("work", tags[0]);
    }

    [Fact]
    public void Deserialize_WithInvalidPointer_ThrowsJsonException()
    {
        // Arrange
        var json = """{"items/0/name":"Invalid"}""";

        // Act & Assert
        var exception = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<PatchObject>(json));
        Assert.Contains("invalid", exception.Message.ToLower());
    }

    [Fact]
    public void Deserialize_WithPrefixConflict_ThrowsJsonException()
    {
        // Arrange
        var json = """{"locations/loc1":{"name":"Test"},"locations/loc1/name":"Conflict"}""";

        // Act & Assert
        var exception = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<PatchObject>(json));
        Assert.Contains("prefix conflicts", exception.Message.ToLower());
    }

    [Fact]
    public void Deserialize_Null_ReturnsNull()
    {
        // Arrange
        var json = "null";

        // Act
        var patch = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.Null(patch);
    }

    [Fact]
    public void RoundTrip_SimplePatchObject_PreservesData()
    {
        // Arrange
        var original = new PatchObject
        {
            ["title"] = "Test Title",
            ["duration"] = "PT1H",
            ["priority"] = 5
        };

        // Act
        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Count, deserialized.Count);
        Assert.Equal(original["title"], deserialized["title"]);
        Assert.Equal(original["duration"], deserialized["duration"]);
    }

    [Fact]
    public void RoundTrip_ComplexPatchObject_PreservesData()
    {
        // Arrange
        var original = new PatchObject
        {
            ["title"] = "Complex Test",
            ["excluded"] = true,
            ["priority"] = 3,
            ["location"] = new Dictionary<string, object?>
            {
                ["name"] = "Room A",
                ["capacity"] = 50
            },
            ["tags"] = new List<object?> { "important", "urgent" },
            ["alerts"] = null
        };

        // Act
        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<PatchObject>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Count, deserialized.Count);
        Assert.Equal(original["title"], deserialized["title"]);
        Assert.True((bool)deserialized["excluded"]!);
        Assert.Null(deserialized["alerts"]);
    }
}