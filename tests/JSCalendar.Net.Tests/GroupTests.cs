using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class GroupTests
{
    private readonly JsonSerializerOptions _options = new() { WriteIndented = false };

    [Fact]
    public void Group_MinimalProperties_SerializesCorrectly()
    {
        // Arrange
        var group = new Group
        {
            Uid = "group-123",
            Updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            Entries = []
        };

        // Act
        var json = JsonSerializer.Serialize(group, _options);

        // Assert
        Assert.Contains("\"@type\":\"Group\"", json);
        Assert.Contains("\"uid\":\"group-123\"", json);
        Assert.Contains("\"updated\":\"2024-12-01T10:00:00+00:00\"", json);
        Assert.Contains("\"entries\":[]", json);
    }

    [Fact]
    public void Group_WithEventAndTask_SerializesCorrectly()
    {
        // Arrange
        var evt = new Event
        {
            Uid = "event-001",
            Updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            Start = new LocalDateTime(new DateTime(2024, 12, 15, 14, 0, 0)),
            Title = "Team Meeting"
        };

        var task = new Task
        {
            Uid = "task-001",
            Updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            Title = "Prepare slides"
        };

        var group = new Group
        {
            Uid = "group-123",
            Updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            Title = "Work Items",
            Entries = [evt, task]
        };

        // Act
        var json = JsonSerializer.Serialize(group, _options);

        // Assert
        Assert.Contains("\"@type\":\"Group\"", json);
        Assert.Contains("\"uid\":\"group-123\"", json);
        Assert.Contains("\"title\":\"Work Items\"", json);
        Assert.Contains("\"entries\":[", json);
        Assert.Contains("\"@type\":\"Event\"", json);
        Assert.Contains("\"@type\":\"Task\"", json);
        Assert.Contains("\"uid\":\"event-001\"", json);
        Assert.Contains("\"uid\":\"task-001\"", json);
    }

    [Fact]
    public void Group_WithAllProperties_SerializesCorrectly()
    {
        // Arrange
        var group = new Group
        {
            Uid = "group-456",
            Created = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            Updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero),
            ProdId = "//example.com//calendar//EN",
            Title = "Conference Events",
            Description = "All events related to the annual conference",
            DescriptionContentType = "text/plain",
            Locale = "en",
            Color = "blue",
            Keywords = new Dictionary<string, bool>
            {
                ["conference"] = true,
                ["annual"] = true
            },
            Categories = new Dictionary<string, bool>
            {
                ["http://example.com/categories/work"] = true
            },
            Links = new Dictionary<string, Link>
            {
                ["website"] = new Link
                {
                    Href = "https://conference.example.com"
                }
            },
            Source = "https://calendar.example.com/groups/456",
            Entries = []
        };

        // Act
        var json = JsonSerializer.Serialize(group, _options);

        // Assert
        Assert.Contains("\"@type\":\"Group\"", json);
        Assert.Contains("\"uid\":\"group-456\"", json);
        Assert.Contains("\"title\":\"Conference Events\"", json);
        Assert.Contains("\"description\":\"All events related to the annual conference\"", json);
        Assert.Contains("\"locale\":\"en\"", json);
        Assert.Contains("\"color\":\"blue\"", json);
        Assert.Contains("\"keywords\":{\"conference\":true,\"annual\":true}", json);
        Assert.Contains("\"source\":\"https://calendar.example.com/groups/456\"", json);
    }

    [Fact]
    public void Group_Deserialize_WorksCorrectly()
    {
        // Arrange
        var json = """
        {
            "@type": "Group",
            "uid": "group-789",
            "updated": "2024-12-01T10:00:00+00:00",
            "title": "My Group",
            "entries": [
                {
                    "@type": "Event",
                    "uid": "event-001",
                    "updated": "2024-12-01T10:00:00+00:00",
                    "start": "2024-12-01T10:00:00+00:00",
                    "title": "Event Title"
                },
                {
                    "@type": "Task",
                    "uid": "task-001",
                    "updated": "2024-12-02T11:00:00Z",
                    "title": "Task Title"
                }
            ]
        }
        """;

        // Act
        var group = JsonSerializer.Deserialize<Group>(json);

        // Assert
        Assert.NotNull(group);
        Assert.Equal("Group", group.Type);
        Assert.Equal("group-789", group.Uid);
        Assert.Equal("My Group", group.Title);
        Assert.NotNull(group.Entries);
        Assert.Equal(2, group.Entries.Count);
        
        // Check first entry is Event
        var firstEntry = group.Entries[0];
        Assert.Equal("Event", firstEntry.Type);
        Assert.Equal("event-001", firstEntry.Uid);
        
        // Check second entry is Task
        var secondEntry = group.Entries[1];
        Assert.Equal("Task", secondEntry.Type);
        Assert.Equal("task-001", secondEntry.Uid);
    }

    [Fact]
    public void Group_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var group = new Group
        {
            Uid = "test-group",
            Updated = DateTimeOffset.UtcNow,
            Entries = []
        };

        // Assert
        Assert.Equal("Group", group.Type);
        Assert.Equal("", group.Title);
        Assert.Equal("", group.Description);
        Assert.Equal("text/plain", group.DescriptionContentType);
    }
}
