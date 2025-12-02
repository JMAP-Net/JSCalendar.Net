# JSCalendar.Net

[![.NET](https://img.shields.io/badge/.NET-10.0-purple)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A modern .NET implementation of [RFC 8984 (JSCalendar)](https://datatracker.ietf.org/doc/html/rfc8984) - a JSON
representation of calendar data.

## Features

✅ **Full RFC 8984 Compliance** - Complete implementation of the JSCalendar specification  
✅ **Native .NET 10** - Built with the latest .NET features  
✅ **Type-Safe** - Strongly-typed models with comprehensive validation  
✅ **JSON Serialization** - Seamless integration with System.Text.Json  
✅ **Recurrence Support** - Full support for recurring events with overrides  
✅ **Localization** - Multi-language support via localizations  
✅ **Time Zones** - Complete time zone handling

## Installation

```bash
dotnet add package JSCalendar.Net
```

Or via NuGet Package Manager:

```
Install-Package JSCalendar.Net
```

## Quick Start

### Creating a Simple Event

```csharp
using JSCalendar.Net;
using System.Text.Json;

var meeting = new Event
{
    Uid = "meeting-001",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(new DateTime(2024, 12, 15, 14, 0, 0)),
    Duration = new Duration { Hours = 1, Minutes = 30 },
    Title = "Team Meeting",
    Description = "Weekly team sync",
    Status = "confirmed"
};

// Serialize to JSON
var json = JsonSerializer.Serialize(meeting, new JsonSerializerOptions 
{ 
    WriteIndented = true 
});

Console.WriteLine(json);
```

**Output:**

```json
{
  "@type": "Event",
  "uid": "meeting-001",
  "updated": "2024-12-02T10:00:00Z",
  "start": "2024-12-15T14:00:00",
  "duration": "PT1H30M",
  "title": "Team Meeting",
  "description": "Weekly team sync",
  "status": "confirmed"
}
```

### Deserializing Events

```csharp
var json = """
{
    "@type": "Event",
    "uid": "event-123",
    "updated": "2024-12-02T10:00:00Z",
    "start": "2024-12-15T14:00:00",
    "title": "Conference Call"
}
""";

var evt = JsonSerializer.Deserialize<Event>(json);
Console.WriteLine($"Event: {evt.Title} at {evt.Start}");
```

## Core Concepts

### Events

Events represent scheduled calendar items with a specific start time.

```csharp
var conference = new Event
{
    Uid = "conf-2024",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(new DateTime(2024, 12, 15, 9, 0, 0)),
    Duration = new Duration { Hours = 8 },
    Title = "Tech Conference 2024",
    Locations = new Dictionary<string, Location>
    {
        ["venue"] = new Location
        {
            Name = "Convention Center",
            Coordinates = "geo:52.520008,13.404954"
        }
    },
    VirtualLocations = new Dictionary<string, VirtualLocation>
    {
        ["stream"] = new VirtualLocation
        {
            Name = "Live Stream",
            Uri = "https://stream.example.com/conf2024",
            Features = new Dictionary<string, bool>
            {
                ["video"] = true,
                ["chat"] = true
            }
        }
    }
};
```

### Tasks

Tasks represent action items with optional due dates.

```csharp
var task = new Task
{
    Uid = "task-001",
    Updated = DateTimeOffset.UtcNow,
    Title = "Prepare presentation",
    Due = new LocalDateTime(new DateTime(2024, 12, 14, 17, 0, 0)),
    Status = "in-process",
    PercentComplete = 60,
    Priority = 1
};
```

### Recurring Events

Create events that repeat with flexible recurrence rules.

```csharp
var weeklyMeeting = new Event
{
    Uid = "weekly-standup",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(new DateTime(2024, 12, 2, 10, 0, 0)),
    Duration = new Duration { Minutes = 30 },
    Title = "Daily Standup",
    RecurrenceRules = new List<RecurrenceRule>
    {
        new RecurrenceRule
        {
            Frequency = "weekly",
            ByDay = new List<NDay>
            {
                new NDay { Day = "mo" },
                new NDay { Day = "tu" },
                new NDay { Day = "we" },
                new NDay { Day = "th" },
                new NDay { Day = "fr" }
            },
            Until = new LocalDateTime(new DateTime(2024, 12, 31, 23, 59, 59))
        }
    }
};
```

### Recurrence Overrides

Override specific instances of recurring events.

```csharp
var recurringEvent = new Event
{
    Uid = "meeting-recurring",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(new DateTime(2024, 12, 1, 10, 0, 0)),
    Title = "Weekly Meeting",
    RecurrenceRules = new List<RecurrenceRule>
    {
        new RecurrenceRule { Frequency = "weekly" }
    },
    RecurrenceOverrides = new Dictionary<LocalDateTime, PatchObject>
    {
        // Special meeting on Dec 15
        [new LocalDateTime(new DateTime(2024, 12, 15, 10, 0, 0))] = new PatchObject
        {
            ["title"] = "Special Year-End Meeting",
            ["duration"] = "PT2H",  // 2 hours instead of default
            ["locations/main/name"] = "Large Conference Room"
        },
        // Cancel meeting on Dec 25 (Christmas)
        [new LocalDateTime(new DateTime(2024, 12, 25, 10, 0, 0))] = new PatchObject
        {
            ["excluded"] = true
        }
    }
};
```

### Participants

Add participants with roles and participation status.

```csharp
var meeting = new Event
{
    Uid = "team-meeting",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(DateTime.Now),
    Participants = new Dictionary<string, Participant>
    {
        ["organizer"] = new Participant
        {
            Name = "Alice Smith",
            Email = "alice@example.com",
            Roles = new Dictionary<string, bool>
            {
                ["owner"] = true,
                ["chair"] = true
            },
            ParticipationStatus = "accepted"
        },
        ["attendee1"] = new Participant
        {
            Name = "Bob Johnson",
            Email = "bob@example.com",
            Roles = new Dictionary<string, bool>
            {
                ["attendee"] = true
            },
            ParticipationStatus = "tentative",
            ExpectReply = true
        }
    }
};
```

### Alerts

Set up reminders and notifications.

```csharp
var eventWithAlerts = new Event
{
    Uid = "important-meeting",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(new DateTime(2024, 12, 15, 14, 0, 0)),
    Alerts = new Dictionary<string, Alert>
    {
        ["reminder15"] = new Alert
        {
            Trigger = new OffsetTrigger
            {
                Offset = "-PT15M",  // 15 minutes before
                RelativeTo = "start"
            },
            Action = "display"
        },
        ["reminder1day"] = new Alert
        {
            Trigger = new OffsetTrigger
            {
                Offset = "-P1D",  // 1 day before
                RelativeTo = "start"
            },
            Action = "email"
        }
    }
};
```

### Localizations

Support multiple languages.

```csharp
var internationalEvent = new Event
{
    Uid = "global-event",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(DateTime.Now),
    Title = "International Conference",
    Description = "Annual technology conference",
    Locale = "en",
    Localizations = new Dictionary<string, PatchObject>
    {
        ["de"] = new PatchObject
        {
            ["title"] = "Internationale Konferenz",
            ["description"] = "Jährliche Technologiekonferenz"
        },
        ["fr"] = new PatchObject
        {
            ["title"] = "Conférence Internationale",
            ["description"] = "Conférence technologique annuelle"
        },
        ["es"] = new PatchObject
        {
            ["title"] = "Conferencia Internacional",
            ["description"] = "Conferencia tecnológica anual"
        }
    }
};
```

## Advanced Features

### Duration Format

JSCalendar uses ISO 8601 duration format:

```csharp
// Various duration examples
var durations = new[]
{
    new Duration { Hours = 1 },                    // PT1H
    new Duration { Minutes = 30 },                 // PT30M
    new Duration { Hours = 2, Minutes = 15 },      // PT2H15M
    new Duration { Days = 1 },                     // P1D
    new Duration { Weeks = 2 },                    // P2W
    new Duration { Days = 1, Hours = 3 }           // P1DT3H
};

// From TimeSpan
var duration = Duration.FromTimeSpan(TimeSpan.FromHours(1.5));
Console.WriteLine(duration.ToString());  // PT1H30M
```

### PatchObject for Dynamic Updates

PatchObjects allow you to modify properties without redefining entire objects.

```csharp
var patch = new PatchObject
{
    // Update top-level property
    ["title"] = "Updated Title",
    
    // Update nested property using JSON Pointer notation
    ["locations/venue/name"] = "New Venue Name",
    
    // Remove a property
    ["alerts"] = null,
    
    // Add complex nested data
    ["participants/newPerson/email"] = "new@example.com"
};

// Validation
if (patch.Validate())
{
    Console.WriteLine("Patch is valid!");
}
```

### Time Zones

Handle time zones properly:

```csharp
var zonedEvent = new Event
{
    Uid = "zoned-event",
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(new DateTime(2024, 12, 15, 14, 0, 0)),
    TimeZone = "Europe/Berlin",
    TimeZones = new Dictionary<string, TimeZone>
    {
        ["Europe/Berlin"] = new TimeZone
        {
            TzId = "Europe/Berlin",
            Updated = DateTimeOffset.UtcNow
        }
    }
};
```

## Validation

JSCalendar.Net includes built-in validation:

```csharp
// PatchObject validation
var patch = new PatchObject
{
    ["title"] = "Valid",
    ["items/0/name"] = "Invalid"  // Array indices not allowed
};

if (!patch.Validate())
{
    Console.WriteLine("Invalid patch!");
}

// Check forbidden properties for recurrence overrides
if (PatchObject.IsForbiddenForRecurrence("uid"))
{
    Console.WriteLine("Cannot patch 'uid' in recurrence overrides");
}
```

## Serialization Options

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};

var json = JsonSerializer.Serialize(myEvent, options);
```

## Best Practices

### 1. Always Set Required Properties

```csharp
// ✅ Good
var evt = new Event
{
    Uid = Guid.NewGuid().ToString(),
    Updated = DateTimeOffset.UtcNow,
    Start = new LocalDateTime(DateTime.Now)
};

// ❌ Bad - Missing required properties will cause errors
var evt = new Event();
```

### 2. Use Meaningful UIDs

```csharp
// ✅ Good - Descriptive and unique
Uid = $"meeting-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N}"

// ❌ Bad - Not unique
Uid = "meeting"
```

### 3. Validate Before Serialization

```csharp
if (myPatchObject.Validate())
{
    var json = JsonSerializer.Serialize(myPatchObject);
}
```

### 4. Handle Time Zones Properly

```csharp
// For floating time (no time zone)
Start = new LocalDateTime(new DateTime(2024, 12, 15, 14, 0, 0))

// For zoned time
Start = new LocalDateTime(new DateTime(2024, 12, 15, 14, 0, 0)),
TimeZone = "Europe/Berlin"
```

## Testing

Run the test suite:

```bash
cd tests/JSCalendar.Net.Tests
dotnet test
```

## Contributing

Contributions are welcome! Please:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Setup

```bash
git clone https://github.com/JMAP-Net/JSCalendar.Net
cd JSCalendar.Net
dotnet restore
dotnet build
dotnet test
```

## RFC 8984 Compliance

This library implements the complete JSCalendar specification as defined
in [RFC 8984](https://datatracker.ietf.org/doc/html/rfc8984).

**Supported Sections:**

- ✅ Section 2: Object Model (Event, Task, Group)
- ✅ Section 4: Common Properties
- ✅ Section 5: Type-Specific Properties
- ✅ Recurrence Rules (Section 4.3)
- ✅ PatchObject (Section 1.4.9)
- ✅ Time Zones (Section 4.7)
- ✅ Participants (Section 4.4)
- ✅ Alerts (Section 4.5)
- ✅ Localization (Section 4.6)

## Resources

- [RFC 8984 Specification](https://datatracker.ietf.org/doc/html/rfc8984)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Built with ❤️ for the .NET community
- Uses System.Text.Json for high-performance JSON serialization

---

Made with ☕ and .NET
