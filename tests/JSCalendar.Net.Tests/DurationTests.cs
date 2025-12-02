using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class DurationTests
{
    [Fact]
    public void Duration_ToString_Days()
    {
        // Arrange
        var duration = new Duration { Days = 5 };

        // Act
        var result = duration.ToString();

        // Assert
        Assert.Equal("P5D", result);
    }

    [Fact]
    public void Duration_ToString_Time()
    {
        // Arrange
        var duration = new Duration { Hours = 2, Minutes = 30 };

        // Act
        var result = duration.ToString();

        // Assert
        Assert.Equal("PT2H30M", result);
    }

    [Fact]
    public void Duration_ToString_Weeks()
    {
        // Arrange
        var duration = new Duration { Weeks = 3 };

        // Act
        var result = duration.ToString();

        // Assert
        Assert.Equal("P3W", result);
    }

    [Fact]
    public void Duration_ToString_Complex()
    {
        // Arrange
        var duration = new Duration
        {
            Years = 1,
            Months = 2,
            Days = 3,
            Hours = 4,
            Minutes = 5,
            Seconds = 6
        };

        // Act
        var result = duration.ToString();

        // Assert
        Assert.Equal("P1Y2M3DT4H5M6S", result);
    }

    [Fact]
    public void Duration_FromTimeSpan()
    {
        // Arrange
        var timeSpan = new TimeSpan(2, 3, 30, 45);

        // Act
        var duration = Duration.FromTimeSpan(timeSpan);

        // Assert
        Assert.Equal(2, duration.Days);
        Assert.Equal(3, duration.Hours);
        Assert.Equal(30, duration.Minutes);
        Assert.Equal(45, duration.Seconds);
    }

    [Fact]
    public void Duration_Serialize()
    {
        // Arrange
        var duration = new Duration { Hours = 1, Minutes = 30 };

        // Act
        var json = JsonSerializer.Serialize(duration);

        // Assert
        Assert.Equal("\"PT1H30M\"", json);
    }

    [Fact]
    public void Duration_Deserialize()
    {
        // Arrange
        var json = "\"PT2H15M\"";

        // Act
        var duration = JsonSerializer.Deserialize<Duration>(json);

        // Assert
        Assert.NotNull(duration);
        Assert.Equal(2, duration.Hours);
        Assert.Equal(15, duration.Minutes);
    }
}