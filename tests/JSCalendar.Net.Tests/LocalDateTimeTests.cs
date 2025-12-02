using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class LocalDateTimeTests
{
    [Fact]
    public void LocalDateTime_Constructor()
    {
        // Arrange
        var dt = new DateTime(2024, 12, 1, 14, 30, 45);

        // Act
        var ldt = new LocalDateTime(dt);

        // Assert
        Assert.Equal(dt, ldt.Value);
    }

    [Fact]
    public void LocalDateTime_ImplicitConversion_FromDateTime()
    {
        // Arrange
        var dt = new DateTime(2024, 12, 1, 14, 30, 0);

        // Act
        LocalDateTime ldt = dt;

        // Assert
        Assert.Equal(dt, ldt.Value);
    }

    [Fact]
    public void LocalDateTime_ImplicitConversion_ToDateTime()
    {
        // Arrange
        var ldt = new LocalDateTime(new DateTime(2024, 12, 1, 14, 30, 0));

        // Act
        DateTime dt = ldt;

        // Assert
        Assert.Equal(ldt.Value, dt);
    }

    [Fact]
    public void LocalDateTime_ToString()
    {
        // Arrange
        var ldt = new LocalDateTime(new DateTime(2024, 12, 1, 14, 30, 45));

        // Act
        var result = ldt.ToString();

        // Assert
        Assert.Equal("2024-12-01T14:30:45", result);
    }

    [Fact]
    public void LocalDateTime_Serialize()
    {
        // Arrange
        var ldt = new LocalDateTime(new DateTime(2024, 12, 1, 14, 30, 0));

        // Act
        var json = JsonSerializer.Serialize(ldt);

        // Assert
        Assert.Equal("\"2024-12-01T14:30:00\"", json);
    }

    [Fact]
    public void LocalDateTime_Deserialize()
    {
        // Arrange
        var json = "\"2024-12-01T14:30:00\"";

        // Act
        var ldt = JsonSerializer.Deserialize<LocalDateTime>(json);

        // Assert
        Assert.NotNull(ldt);
        Assert.Equal(new DateTime(2024, 12, 1, 14, 30, 0), ldt.Value);
    }

    [Fact]
    public void LocalDateTime_RoundTrip()
    {
        // Arrange
        var original = new LocalDateTime(new DateTime(2024, 12, 1, 14, 30, 45));

        // Act
        var json = JsonSerializer.Serialize(original);
        var deserialized = JsonSerializer.Deserialize<LocalDateTime>(json);

        // Assert
        Assert.Equal(original.Value, deserialized.Value);
    }
}