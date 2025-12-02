using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class RecurrenceRuleTests
{
    private readonly JsonSerializerOptions _options = new() { WriteIndented = false };

    [Fact]
    public void RecurrenceRule_MinimalDaily()
    {
        // Arrange & Act
        var rule = new RecurrenceRule
        {
            Frequency = "daily"
        };

        // Assert
        Assert.Equal("daily", rule.Frequency);
        Assert.Equal(1, rule.Interval);
        Assert.Equal("gregorian", rule.Rscale);
        Assert.Equal("omit", rule.Skip);
        Assert.Equal("mo", rule.FirstDayOfWeek);
    }

    [Fact]
    public void RecurrenceRule_WeeklyWithByDay()
    {
        // Arrange & Act
        var rule = new RecurrenceRule
        {
            Frequency = "weekly",
            ByDay = new List<NDay>
            {
                new() { Day = "mo" },
                new() { Day = "we" },
                new() { Day = "fr" }
            }
        };

        // Assert
        Assert.Equal("weekly", rule.Frequency);
        Assert.NotNull(rule.ByDay);
        Assert.Equal(3, rule.ByDay.Count);
        Assert.Equal("mo", rule.ByDay[0].Day);
    }

    [Fact]
    public void RecurrenceRule_MonthlyWithCount()
    {
        // Arrange & Act
        var rule = new RecurrenceRule
        {
            Frequency = "monthly",
            Interval = 2,
            Count = 10
        };

        // Assert
        Assert.Equal("monthly", rule.Frequency);
        Assert.Equal(2, rule.Interval);
        Assert.Equal(10, rule.Count);
        Assert.Null(rule.Until);
    }

    [Fact]
    public void RecurrenceRule_YearlyWithUntil()
    {
        // Arrange
        var until = new LocalDateTime(new DateTime(2025, 12, 31, 23, 59, 59));

        // Act
        var rule = new RecurrenceRule
        {
            Frequency = "yearly",
            Until = until
        };

        // Assert
        Assert.Equal("yearly", rule.Frequency);
        Assert.Equal(until, rule.Until);
        Assert.Null(rule.Count);
    }

    [Fact]
    public void RecurrenceRule_Serialize()
    {
        // Arrange
        var rule = new RecurrenceRule
        {
            Frequency = "weekly",
            Interval = 2,
            ByDay = new List<NDay>
            {
                new() { Day = "mo" }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(rule, _options);

        // Assert
        Assert.Contains("\"frequency\":\"weekly\"", json);
        Assert.Contains("\"interval\":2", json);
        Assert.Contains("\"byDay\":", json);
    }

    [Fact]
    public void RecurrenceRule_Deserialize()
    {
        // Arrange
        var json = """
                   {
                       "@type": "RecurrenceRule",
                       "frequency": "daily",
                       "interval": 3
                   }
                   """;

        // Act
        var rule = JsonSerializer.Deserialize<RecurrenceRule>(json);

        // Assert
        Assert.NotNull(rule);
        Assert.Equal("daily", rule.Frequency);
        Assert.Equal(3, rule.Interval);
    }

    [Fact]
    public void NDay_Simple()
    {
        // Arrange & Act
        var nday = new NDay { Day = "tu" };

        // Assert
        Assert.Equal("NDay", nday.Type);
        Assert.Equal("tu", nday.Day);
        Assert.Null(nday.NthOfPeriod);
    }

    [Fact]
    public void NDay_WithNthOfPeriod()
    {
        // Arrange & Act
        var nday = new NDay
        {
            Day = "mo",
            NthOfPeriod = 1 // First Monday
        };

        // Assert
        Assert.Equal("mo", nday.Day);
        Assert.Equal(1, nday.NthOfPeriod);
    }

    [Fact]
    public void NDay_LastOccurrence()
    {
        // Arrange & Act
        var nday = new NDay
        {
            Day = "fr",
            NthOfPeriod = -1 // Last Friday
        };

        // Assert
        Assert.Equal("fr", nday.Day);
        Assert.Equal(-1, nday.NthOfPeriod);
    }

    [Fact]
    public void RecurrenceRule_ComplexRule()
    {
        // Arrange & Act
        var rule = new RecurrenceRule
        {
            Frequency = "monthly",
            Interval = 1,
            ByDay = new List<NDay>
            {
                new() { Day = "mo", NthOfPeriod = 1 } // First Monday of month
            },
            Count = 12
        };

        // Assert
        Assert.Equal("monthly", rule.Frequency);
        Assert.Single(rule.ByDay);
        Assert.Equal(1, rule.ByDay[0].NthOfPeriod);
        Assert.Equal(12, rule.Count);
    }
}