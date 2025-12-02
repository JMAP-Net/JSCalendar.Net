using System.Text.Json;

namespace JSCalendar.Net.Tests;

public class LocationAndVirtualLocationTests
{
    private readonly JsonSerializerOptions _options = new() { WriteIndented = false };

    [Fact]
    public void Location_Minimal()
    {
        // Arrange & Act
        var location = new Location
        {
            Name = "Conference Room A"
        };

        // Assert
        Assert.Equal("Location", location.Type);
        Assert.Equal("Conference Room A", location.Name);
    }

    [Fact]
    public void Location_WithCoordinates()
    {
        // Arrange & Act
        var location = new Location
        {
            Name = "Berlin Office",
            Coordinates = "geo:52.520008,13.404954"
        };

        // Assert
        Assert.Equal("Berlin Office", location.Name);
        Assert.Equal("geo:52.520008,13.404954", location.Coordinates);
    }

    [Fact]
    public void Location_Serialize()
    {
        // Arrange
        var location = new Location
        {
            Name = "Meeting Room",
            Description = "2nd floor, next to elevator",
            TimeZone = "Europe/Berlin"
        };

        // Act
        var json = JsonSerializer.Serialize(location, _options);

        // Assert
        Assert.Contains("\"@type\":\"Location\"", json);
        Assert.Contains("\"name\":\"Meeting Room\"", json);
        Assert.Contains("\"description\":\"2nd floor, next to elevator\"", json);
        Assert.Contains("\"timeZone\":\"Europe/Berlin\"", json);
    }

    [Fact]
    public void Location_Deserialize()
    {
        // Arrange
        var json = """
                   {
                       "@type": "Location",
                       "name": "Office",
                       "coordinates": "geo:52.52,13.40"
                   }
                   """;

        // Act
        var location = JsonSerializer.Deserialize<Location>(json);

        // Assert
        Assert.NotNull(location);
        Assert.Equal("Office", location.Name);
        Assert.Equal("geo:52.52,13.40", location.Coordinates);
    }

    [Fact]
    public void VirtualLocation_Minimal()
    {
        // Arrange & Act
        var vLoc = new VirtualLocation
        {
            Uri = "https://meet.example.com/abc123"
        };

        // Assert
        Assert.Equal("VirtualLocation", vLoc.Type);
        Assert.Equal("", vLoc.Name);
        Assert.Equal("https://meet.example.com/abc123", vLoc.Uri);
    }

    [Fact]
    public void VirtualLocation_WithFeatures()
    {
        // Arrange & Act
        var vLoc = new VirtualLocation
        {
            Name = "Zoom Meeting",
            Uri = "https://zoom.us/j/123456789",
            Features = new Dictionary<string, bool>
            {
                ["audio"] = true,
                ["video"] = true,
                ["chat"] = true,
                ["screen"] = true
            }
        };

        // Assert
        Assert.Equal("Zoom Meeting", vLoc.Name);
        Assert.NotNull(vLoc.Features);
        Assert.Equal(4, vLoc.Features.Count);
        Assert.True(vLoc.Features["audio"]);
    }

    [Fact]
    public void VirtualLocation_PhoneNumber()
    {
        // Arrange & Act
        var vLoc = new VirtualLocation
        {
            Name = "Conference Call",
            Uri = "tel:+1-555-0123",
            Description = "Access code: 123456"
        };

        // Assert
        Assert.Equal("tel:+1-555-0123", vLoc.Uri);
        Assert.Equal("Access code: 123456", vLoc.Description);
    }

    [Fact]
    public void VirtualLocation_Serialize()
    {
        // Arrange
        var vLoc = new VirtualLocation
        {
            Name = "Teams Meeting",
            Uri = "https://teams.microsoft.com/l/meetup/...",
            Features = new Dictionary<string, bool>
            {
                ["video"] = true,
                ["screen"] = true
            }
        };

        // Act
        var json = JsonSerializer.Serialize(vLoc, _options);

        // Assert
        Assert.Contains("\"@type\":\"VirtualLocation\"", json);
        Assert.Contains("\"name\":\"Teams Meeting\"", json);
        Assert.Contains("\"uri\":\"https://teams.microsoft.com", json);
        Assert.Contains("\"features\":", json);
    }

    [Fact]
    public void VirtualLocation_Deserialize()
    {
        // Arrange
        var json = """
                   {
                       "@type": "VirtualLocation",
                       "name": "Video Call",
                       "uri": "https://meet.example.com/room123"
                   }
                   """;

        // Act
        var vLoc = JsonSerializer.Deserialize<VirtualLocation>(json);

        // Assert
        Assert.NotNull(vLoc);
        Assert.Equal("Video Call", vLoc.Name);
        Assert.Equal("https://meet.example.com/room123", vLoc.Uri);
    }
}