using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class EventTests
{
    private readonly JsonSerializerOptions _options = new() { WriteIndented = false };

    [Fact]
    public void Event_RequiredProperties_MustBeSet()
    {
        // Arrange
        var now = DateTimeOffset.UtcNow;
        var start = new LocalDateTime(DateTime.Now);

        // Act
        var evt = new Event
        {
            Uid = "event123",
            Updated = now,
            Start = start
        };

        // Assert
        Assert.Equal("event123", evt.Uid);
        Assert.Equal(now, evt.Updated);
        Assert.Equal(start, evt.Start);
    }

    [Fact]
    public void Event_DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var evt = new Event
        {
            Uid = "event123",
            Updated = DateTimeOffset.UtcNow,
            Start = new LocalDateTime(DateTime.Now)
        };

        // Assert
        Assert.Equal("Event", evt.Type);
        Assert.Equal(0, evt.Sequence);
        Assert.Equal("", evt.Title);
        Assert.Equal("", evt.Description);
        Assert.Equal("text/plain", evt.DescriptionContentType);
        Assert.False(evt.ShowWithoutTime);
        Assert.Equal(0, evt.Priority);
        Assert.Equal("busy", evt.FreeBusyStatus);
        Assert.Equal("public", evt.Privacy);
        Assert.False(evt.UseDefaultAlerts);
        Assert.Equal("confirmed", evt.Status);
        Assert.False(evt.Excluded);
    }

    [Fact]
    public void Event_Serialize_MinimalEvent()
    {
        // Arrange
        var evt = new Event
        {
            Uid = "event123",
            Updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            Start = new LocalDateTime(new DateTime(2024, 12, 1, 14, 0, 0))
        };

        // Act
        var json = JsonSerializer.Serialize(evt, _options);

        // Assert
        Assert.Contains("\"@type\":\"Event\"", json);
        Assert.Contains("\"uid\":\"event123\"", json);
        Assert.Contains("\"updated\":\"2024-12-01T10:00:00+00:00\"", json);
        Assert.Contains("\"start\":\"2024-12-01T14:00:00\"", json);
    }

    [Fact]
    public void Event_Serialize_WithOptionalProperties()
    {
        // Arrange
        var evt = new Event
        {
            Uid = "event123",
            Updated = DateTimeOffset.UtcNow,
            Start = new LocalDateTime(DateTime.Now),
            Title = "Team Meeting",
            Description = "Weekly team sync",
            Priority = 5,
            Status = "confirmed"
        };

        // Act
        var json = JsonSerializer.Serialize(evt, _options);

        // Assert
        Assert.Contains("\"title\":\"Team Meeting\"", json);
        Assert.Contains("\"description\":\"Weekly team sync\"", json);
        Assert.Contains("\"priority\":5", json);
        Assert.Contains("\"status\":\"confirmed\"", json);
    }

    [Fact]
    public void Event_Serialize_WithRecurrenceOverrides()
    {
        // Arrange
        var overrideDate = new LocalDateTime(new DateTime(2024, 12, 15, 10, 0, 0));
        var evt = new Event
        {
            Uid = "event123",
            Updated = DateTimeOffset.UtcNow,
            Start = new LocalDateTime(new DateTime(2024, 12, 1, 10, 0, 0)),
            RecurrenceOverrides = new Dictionary<LocalDateTime, PatchObject>
            {
                [overrideDate] = new()
                {
                    ["title"] = "Modified Title",
                    ["duration"] = "PT2H"
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(evt, _options);

        // Assert
        Assert.Contains("\"recurrenceOverrides\":", json);
        Assert.Contains("\"2024-12-15T10:00:00\":", json); // LocalDateTime as property name
        Assert.Contains("\"title\":\"Modified Title\"", json);
        Assert.Contains("\"duration\":\"PT2H\"", json);
    }

    [Fact]
    public void Event_Serialize_WithLocalizations()
    {
        // Arrange
        var evt = new Event
        {
            Uid = "event123",
            Updated = DateTimeOffset.UtcNow,
            Start = new LocalDateTime(DateTime.Now),
            Title = "Meeting",
            Localizations = new Dictionary<string, PatchObject>
            {
                ["de"] = new()
                {
                    ["title"] = "Besprechung",
                    ["description"] = "Woechentliches Team-Meeting" // Avoid special chars in test
                },
                ["fr"] = new()
                {
                    ["title"] = "Reunion" // Avoid special chars in test
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(evt, _options);

        // Assert
        Assert.Contains("\"localizations\":", json);
        Assert.Contains("\"de\":", json);
        Assert.Contains("\"Besprechung\"", json);
        Assert.Contains("\"fr\":", json);
        Assert.Contains("\"Reunion\"", json);
    }

    [Fact]
    public void Event_Deserialize_MinimalEvent()
    {
        // Arrange
        var json = """
                   {
                       "@type": "Event",
                       "uid": "event123",
                       "updated": "2024-12-01T10:00:00Z",
                       "start": "2024-12-01T14:00:00"
                   }
                   """;

        // Act
        var evt = JsonSerializer.Deserialize<Event>(json);

        // Assert
        Assert.NotNull(evt);
        Assert.Equal("Event", evt.Type);
        Assert.Equal("event123", evt.Uid);
        Assert.Equal(new DateTime(2024, 12, 1, 10, 0, 0, DateTimeKind.Utc), evt.Updated.UtcDateTime);
    }

    [Fact]
    public void Event_Deserialize_WithOptionalProperties()
    {
        // Arrange
        var json = """
                   {
                       "@type": "Event",
                       "uid": "event123",
                       "updated": "2024-12-01T10:00:00Z",
                       "start": "2024-12-01T14:00:00",
                       "title": "Team Meeting",
                       "description": "Weekly sync",
                       "priority": 5
                   }
                   """;

        // Act
        var evt = JsonSerializer.Deserialize<Event>(json);

        // Assert
        Assert.NotNull(evt);
        Assert.Equal("Team Meeting", evt.Title);
        Assert.Equal("Weekly sync", evt.Description);
        Assert.Equal(5, evt.Priority);
    }

    [Fact]
    public void Event_Deserialize_WithRecurrenceOverrides()
    {
        // Arrange
        var json = """
                   {
                       "@type": "Event",
                       "uid": "event123",
                       "updated": "2024-12-01T10:00:00Z",
                       "start": "2024-12-01T14:00:00",
                       "recurrenceOverrides": {
                           "2024-12-15T14:00:00": {
                               "title": "Modified Title",
                               "duration": "PT2H"
                           }
                       }
                   }
                   """;

        // Act
        var evt = JsonSerializer.Deserialize<Event>(json);

        // Assert
        Assert.NotNull(evt);
        Assert.NotNull(evt.RecurrenceOverrides);
        Assert.Single(evt.RecurrenceOverrides);

        var key = new LocalDateTime(new DateTime(2024, 12, 15, 14, 0, 0));
        Assert.True(evt.RecurrenceOverrides.ContainsKey(key));
        Assert.Equal("Modified Title", evt.RecurrenceOverrides[key]["title"]);
    }

    [Fact]
    public void Event_RoundTrip_PreservesData()
    {
        // Arrange
        var original = new Event
        {
            Uid = "event123",
            Updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            Start = new LocalDateTime(new DateTime(2024, 12, 1, 14, 0, 0)),
            Title = "Important Meeting",
            Description = "Quarterly review",
            Priority = 9,
            Status = "confirmed"
        };

        // Act
        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<Event>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(original.Uid, deserialized.Uid);
        Assert.Equal(original.Title, deserialized.Title);
        Assert.Equal(original.Description, deserialized.Description);
        Assert.Equal(original.Priority, deserialized.Priority);
        Assert.Equal(original.Status, deserialized.Status);
    }

    [Fact]
    public void Event_WithLocation_SerializesCorrectly()
    {
        // Arrange
        var evt = new Event
        {
            Uid = "event123",
            Updated = DateTimeOffset.UtcNow,
            Start = new LocalDateTime(DateTime.Now),
            Locations = new Dictionary<string, Location>
            {
                ["main"] = new()
                {
                    Name = "Conference Room A",
                    Coordinates = "geo:52.520008,13.404954"
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(evt, _options);

        // Assert
        Assert.Contains("\"locations\":", json);
        Assert.Contains("\"main\":", json);
        Assert.Contains("\"Conference Room A\"", json);
    }

    [Fact]
    public void Event_WithParticipants_SerializesCorrectly()
    {
        // Arrange
        var evt = new Event
        {
            Uid = "event123",
            Updated = DateTimeOffset.UtcNow,
            Start = new LocalDateTime(DateTime.Now),
            Participants = new Dictionary<string, Participant>
            {
                ["p1"] = new()
                {
                    Name = "John Doe",
                    Email = "john@example.com",
                    Roles = new Dictionary<string, bool> { ["attendee"] = true }
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(evt, _options);

        // Assert
        Assert.Contains("\"participants\":", json);
        Assert.Contains("\"John Doe\"", json);
        Assert.Contains("\"john@example.com\"", json);
    }

    [Fact]
    public void Event_WithAlerts_SerializesCorrectly()
    {
        // Arrange
        var evt = new Event
        {
            Uid = "event123",
            Updated = DateTimeOffset.UtcNow,
            Start = new LocalDateTime(DateTime.Now),
            Alerts = new Dictionary<string, Alert>
            {
                ["alert1"] = new()
                {
                    Trigger = new OffsetTrigger
                    {
                        Offset = "-PT15M"
                    }
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(evt, _options);

        // Assert
        Assert.Contains("\"alerts\":", json);
        Assert.Contains("\"alert1\":", json);
    }
}