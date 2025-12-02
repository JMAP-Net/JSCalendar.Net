using System.Text.Json;
using JSCalendar.Net.Enums;
using DayOfWeek = JSCalendar.Net.Enums.DayOfWeek;

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
            Frequency = RecurrenceFrequency.Daily
        };

        // Assert
        Assert.Equal(RecurrenceFrequency.Daily, rule.Frequency);
        Assert.Equal(1, rule.Interval);
        Assert.Equal("gregorian", rule.Rscale);
        Assert.Equal(RecurrenceSkip.Omit, rule.Skip);
        Assert.Equal(DayOfWeek.Monday, rule.FirstDayOfWeek);
    }

    [Fact]
    public void RecurrenceRule_WeeklyWithByDay()
    {
        // Arrange & Act
        var rule = new RecurrenceRule
        {
            Frequency = RecurrenceFrequency.Weekly,
            ByDay =
            [
                new NDay { Day = DayOfWeek.Monday },
                new NDay { Day = DayOfWeek.Wednesday },
                new NDay { Day = DayOfWeek.Friday }
            ]
        };

        // Assert
        Assert.Equal(RecurrenceFrequency.Weekly, rule.Frequency);
        Assert.NotNull(rule.ByDay);
        Assert.Equal(3, rule.ByDay.Count);
        Assert.Equal(DayOfWeek.Monday, rule.ByDay[0].Day);
    }

    [Fact]
    public void RecurrenceRule_MonthlyWithCount()
    {
        // Arrange & Act
        var rule = new RecurrenceRule
        {
            Frequency = RecurrenceFrequency.Monthly,
            Interval = 2,
            Count = 10
        };

        // Assert
        Assert.Equal(RecurrenceFrequency.Monthly, rule.Frequency);
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
            Frequency = RecurrenceFrequency.Yearly,
            Until = until
        };

        // Assert
        Assert.Equal(RecurrenceFrequency.Yearly, rule.Frequency);
        Assert.Equal(until, rule.Until);
        Assert.Null(rule.Count);
    }

    [Fact]
    public void RecurrenceRule_Serialize()
    {
        // Arrange
        var rule = new RecurrenceRule
        {
            Frequency = RecurrenceFrequency.Weekly,
            Interval = 2,
            ByDay = [new NDay { Day = DayOfWeek.Monday }]
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
        Assert.Equal(RecurrenceFrequency.Daily, rule.Frequency);
        Assert.Equal(3, rule.Interval);
    }

    [Fact]
    public void NDay_Simple()
    {
        // Arrange & Act
        var nday = new NDay { Day = DayOfWeek.Tuesday };

        // Assert
        Assert.Equal("NDay", nday.Type);
        Assert.Equal(DayOfWeek.Tuesday, nday.Day);
        Assert.Null(nday.NthOfPeriod);
    }

    [Fact]
    public void NDay_WithNthOfPeriod()
    {
        // Arrange & Act
        var nday = new NDay
        {
            Day = DayOfWeek.Monday,
            NthOfPeriod = 1 // First Monday
        };

        // Assert
        Assert.Equal(DayOfWeek.Monday, nday.Day);
        Assert.Equal(1, nday.NthOfPeriod);
    }

    [Fact]
    public void NDay_LastOccurrence()
    {
        // Arrange & Act
        var nday = new NDay
        {
            Day = DayOfWeek.Friday,
            NthOfPeriod = -1 // Last Friday
        };

        // Assert
        Assert.Equal(DayOfWeek.Friday, nday.Day);
        Assert.Equal(-1, nday.NthOfPeriod);
    }

    [Fact]
    public void RecurrenceRule_ComplexRule()
    {
        // Arrange & Act
        var rule = new RecurrenceRule
        {
            Frequency = RecurrenceFrequency.Monthly,
            Interval = 1,
            ByDay = [new NDay { Day = DayOfWeek.Monday, NthOfPeriod = 1 }],
            Count = 12
        };

        // Assert
        Assert.Equal(RecurrenceFrequency.Monthly, rule.Frequency);
        Assert.Single(rule.ByDay);
        Assert.Equal(1, rule.ByDay[0].NthOfPeriod);
        Assert.Equal(12, rule.Count);
    }
}