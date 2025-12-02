using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class ParticipantTests
{
    private readonly JsonSerializerOptions _options = new() { WriteIndented = false };

    [Fact]
    public void Participant_Minimal()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Roles = new Dictionary<string, bool>
            {
                ["attendee"] = true
            }
        };

        // Assert
        Assert.Equal("Participant", participant.Type);
        Assert.Single(participant.Roles);
        Assert.Equal("needs-action", participant.ParticipationStatus);
        Assert.False(participant.ExpectReply);
        Assert.Equal("server", participant.ScheduleAgent);
        Assert.Equal(0, participant.ScheduleSequence);
    }

    [Fact]
    public void Participant_WithNameAndEmail()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Name = "John Doe",
            Email = "john@example.com",
            Roles = new Dictionary<string, bool>
            {
                ["attendee"] = true
            }
        };

        // Assert
        Assert.Equal("John Doe", participant.Name);
        Assert.Equal("john@example.com", participant.Email);
    }

    [Fact]
    public void Participant_Organizer()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Name = "Meeting Organizer",
            Email = "organizer@example.com",
            Roles = new Dictionary<string, bool>
            {
                ["owner"] = true,
                ["chair"] = true
            },
            ParticipationStatus = "accepted"
        };

        // Assert
        Assert.Equal(2, participant.Roles.Count);
        Assert.True(participant.Roles["owner"]);
        Assert.True(participant.Roles["chair"]);
        Assert.Equal("accepted", participant.ParticipationStatus);
    }

    [Fact]
    public void Participant_WithMultipleRoles()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Roles = new Dictionary<string, bool>
            {
                ["attendee"] = true,
                ["optional"] = true
            }
        };

        // Assert
        Assert.Equal(2, participant.Roles.Count);
    }

    [Fact]
    public void Participant_Declined()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Email = "user@example.com",
            Roles = new Dictionary<string, bool> { ["attendee"] = true },
            ParticipationStatus = "declined",
            ParticipationComment = "Not available"
        };

        // Assert
        Assert.Equal("declined", participant.ParticipationStatus);
        Assert.Equal("Not available", participant.ParticipationComment);
    }

    [Fact]
    public void Participant_Delegated()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Email = "delegate@example.com",
            Roles = new Dictionary<string, bool> { ["attendee"] = true },
            DelegatedFrom = new Dictionary<string, bool>
            {
                ["original-participant"] = true
            }
        };

        // Assert
        Assert.NotNull(participant.DelegatedFrom);
        Assert.Single(participant.DelegatedFrom);
    }

    [Fact]
    public void Participant_WithSendTo()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Name = "John Doe",
            Roles = new Dictionary<string, bool> { ["attendee"] = true },
            SendTo = new Dictionary<string, string>
            {
                ["imip"] = "mailto:john@example.com",
                ["other"] = "https://calendar.example.com/notify/john"
            }
        };

        // Assert
        Assert.NotNull(participant.SendTo);
        Assert.Equal(2, participant.SendTo.Count);
        Assert.Equal("mailto:john@example.com", participant.SendTo["imip"]);
    }

    [Fact]
    public void Participant_Serialize()
    {
        // Arrange
        var participant = new Participant
        {
            Name = "Jane Smith",
            Email = "jane@example.com",
            Roles = new Dictionary<string, bool>
            {
                ["attendee"] = true
            },
            ParticipationStatus = "accepted"
        };

        // Act
        var json = JsonSerializer.Serialize(participant, _options);

        // Assert
        Assert.Contains("\"@type\":\"Participant\"", json);
        Assert.Contains("\"name\":\"Jane Smith\"", json);
        Assert.Contains("\"email\":\"jane@example.com\"", json);
        Assert.Contains("\"participationStatus\":\"accepted\"", json);
    }

    [Fact]
    public void Participant_Deserialize()
    {
        // Arrange
        var json = """
                   {
                       "@type": "Participant",
                       "name": "Bob Johnson",
                       "email": "bob@example.com",
                       "roles": {
                           "attendee": true
                       },
                       "participationStatus": "tentative"
                   }
                   """;

        // Act
        var participant = JsonSerializer.Deserialize<Participant>(json);

        // Assert
        Assert.NotNull(participant);
        Assert.Equal("Bob Johnson", participant.Name);
        Assert.Equal("bob@example.com", participant.Email);
        Assert.Equal("tentative", participant.ParticipationStatus);
    }

    [Fact]
    public void Participant_WithScheduleUpdated()
    {
        // Arrange
        var updated = new DateTimeOffset(2024, 12, 1, 10, 0, 0, TimeSpan.Zero);

        // Act
        var participant = new Participant
        {
            Roles = new Dictionary<string, bool> { ["attendee"] = true },
            ScheduleSequence = 5,
            ScheduleUpdated = updated
        };

        // Assert
        Assert.Equal(5, participant.ScheduleSequence);
        Assert.Equal(updated, participant.ScheduleUpdated);
    }

    [Fact]
    public void Participant_TaskProgress()
    {
        // Arrange & Act
        var participant = new Participant
        {
            Roles = new Dictionary<string, bool> { ["attendee"] = true },
            Progress = "in-process",
            PercentComplete = 50,
            ProgressUpdated = DateTimeOffset.UtcNow
        };

        // Assert
        Assert.Equal("in-process", participant.Progress);
        Assert.Equal(50, participant.PercentComplete);
        Assert.NotNull(participant.ProgressUpdated);
    }
}