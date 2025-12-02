using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class AlertTests
{
    private readonly JsonSerializerOptions _options = new() { WriteIndented = false };

    [Fact]
    public void Alert_WithOffsetTrigger()
    {
        // Arrange & Act
        var alert = new Alert
        {
            Trigger = new OffsetTrigger
            {
                Offset = "-PT15M"
            }
        };

        // Assert
        Assert.Equal("Alert", alert.Type);
        Assert.Equal("display", alert.Action);
        Assert.IsType<OffsetTrigger>(alert.Trigger);
    }

    [Fact]
    public void Alert_WithAbsoluteTrigger()
    {
        // Arrange
        var when = new DateTimeOffset(2024, 12, 1, 9, 0, 0, TimeSpan.Zero);

        // Act
        var alert = new Alert
        {
            Trigger = new AbsoluteTrigger
            {
                When = when
            },
            Action = "email"
        };

        // Assert
        Assert.Equal("email", alert.Action);
        Assert.IsType<AbsoluteTrigger>(alert.Trigger);
        var trigger = (AbsoluteTrigger)alert.Trigger;
        Assert.Equal(when, trigger.When);
    }

    [Fact]
    public void OffsetTrigger_Defaults()
    {
        // Arrange & Act
        var trigger = new OffsetTrigger
        {
            Offset = "-PT30M"
        };

        // Assert
        Assert.Equal("OffsetTrigger", trigger.Type);
        Assert.Equal("-PT30M", trigger.Offset);
        Assert.Equal("start", trigger.RelativeTo);
    }

    [Fact]
    public void OffsetTrigger_RelativeToEnd()
    {
        // Arrange & Act
        var trigger = new OffsetTrigger
        {
            Offset = "-PT5M",
            RelativeTo = "end"
        };

        // Assert
        Assert.Equal("end", trigger.RelativeTo);
    }

    [Fact]
    public void AbsoluteTrigger_Basic()
    {
        // Arrange
        var when = DateTimeOffset.UtcNow;

        // Act
        var trigger = new AbsoluteTrigger
        {
            When = when
        };

        // Assert
        Assert.Equal("AbsoluteTrigger", trigger.Type);
        Assert.Equal(when, trigger.When);
    }

    [Fact]
    public void Alert_Serialize_WithOffsetTrigger()
    {
        // Arrange
        var alert = new Alert
        {
            Trigger = new OffsetTrigger
            {
                Offset = "-PT15M"
            },
            Action = "display"
        };

        // Act
        var json = JsonSerializer.Serialize(alert, _options);

        // Assert
        Assert.Contains("\"@type\":\"Alert\"", json);
        Assert.Contains("\"action\":\"display\"", json);
        Assert.Contains("\"trigger\":", json);
    }

    [Fact]
    public void Alert_WithAcknowledged()
    {
        // Arrange
        var acknowledged = new DateTimeOffset(2024, 12, 1, 10, 30, 0, TimeSpan.Zero);

        // Act
        var alert = new Alert
        {
            Trigger = new OffsetTrigger { Offset = "-PT15M" },
            Acknowledged = acknowledged
        };

        // Assert
        Assert.Equal(acknowledged, alert.Acknowledged);
    }

    [Fact]
    public void UnknownTrigger_ForwardCompatibility()
    {
        // Arrange & Act
        var trigger = new UnknownTrigger
        {
            Type = "FutureTrigger"
        };

        // Assert
        Assert.Equal("FutureTrigger", trigger.Type);
    }

    [Fact]
    public void Alert_Deserialize()
    {
        // Arrange
        var json = """
                   {
                       "@type": "Alert",
                       "trigger": {
                           "@type": "OffsetTrigger",
                           "offset": "-PT10M"
                       },
                       "action": "display"
                   }
                   """;

        // Act
        var alert = JsonSerializer.Deserialize<Alert>(json);

        // Assert
        Assert.NotNull(alert);
        Assert.Equal("display", alert.Action);
    }
}