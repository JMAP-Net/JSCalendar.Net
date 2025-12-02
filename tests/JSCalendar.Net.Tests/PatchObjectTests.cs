namespace JSCalendar.Net.Tests;

public class PatchObjectTests
{
    [Fact]
    public void PatchObject_CanBeCreated_Empty()
    {
        // Arrange & Act
        var patch = new PatchObject();

        // Assert
        Assert.NotNull(patch);
        Assert.Empty(patch);
    }

    [Fact]
    public void PatchObject_CanBeCreated_WithInitialValues()
    {
        // Arrange & Act
        var patch = new PatchObject
        {
            ["title"] = "Test Title",
            ["description"] = "Test Description"
        };

        // Assert
        Assert.Equal(2, patch.Count);
        Assert.Equal("Test Title", patch["title"]);
        Assert.Equal("Test Description", patch["description"]);
    }

    [Fact]
    public void PatchObject_CanBeCreated_FromDictionary()
    {
        // Arrange
        var dict = new Dictionary<string, object?>
        {
            ["title"] = "Test",
            ["duration"] = "PT1H"
        };

        // Act
        var patch = new PatchObject(dict);

        // Assert
        Assert.Equal(2, patch.Count);
        Assert.Equal("Test", patch["title"]);
    }

    [Theory]
    [InlineData("title", true)]
    [InlineData("locations/loc1/name", true)]
    [InlineData("participants/p1/email", true)]
    [InlineData("locations/0/name", false)] // Array index
    [InlineData("items/123/title", false)] // Numeric segment
    [InlineData("", false)] // Empty
    public void IsValidPointer_ValidatesCorrectly(string pointer, bool expected)
    {
        // Act
        var result = PatchObject.IsValidPointer(pointer);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("title", "title", true)]
    [InlineData("title", "description", false)]
    [InlineData("locations/loc1", "locations/loc1/name", true)]
    [InlineData("locations/loc1/name", "locations/loc1", true)]
    [InlineData("locations/loc1", "locations/loc2", false)]
    public void IsPrefixConflict_DetectsConflicts(string pointer1, string pointer2, bool expected)
    {
        // Act
        var result = PatchObject.IsPrefixConflict(pointer1, pointer2);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Validate_ReturnsTrueForValidPatch()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["title"] = "Test",
            ["description"] = "Description",
            ["locations/loc1/name"] = "Location Name"
        };

        // Act
        var result = patch.Validate();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_ReturnsFalseForInvalidPointer()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["title"] = "Test",
            ["items/0/name"] = "Invalid" // Array index
        };

        // Act
        var result = patch.Validate();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validate_ReturnsFalseForPrefixConflict()
    {
        // Arrange
        var patch = new PatchObject
        {
            ["locations/loc1"] = new { name = "Test" },
            ["locations/loc1/name"] = "Conflict" // Prefix conflict!
        };

        // Act
        var result = patch.Validate();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("@type", true)]
    [InlineData("uid", true)]
    [InlineData("recurrenceRules", true)]
    [InlineData("recurrenceOverrides", true)]
    [InlineData("method", true)]
    [InlineData("privacy", true)]
    [InlineData("title", false)]
    [InlineData("description", false)]
    [InlineData("start", false)]
    public void IsForbiddenForRecurrence_ChecksForbiddenPrefixes(string pointer, bool expected)
    {
        // Act
        var result = PatchObject.IsForbiddenForRecurrence(pointer);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsForbiddenForRecurrence_ChecksNestedPaths()
    {
        // Arrange & Act
        var result1 = PatchObject.IsForbiddenForRecurrence("uid/nested");
        var result2 = PatchObject.IsForbiddenForRecurrence("title/nested");

        // Assert
        Assert.True(result1); // uid prefix is forbidden
        Assert.False(result2); // title prefix is allowed
    }

    [Fact]
    public void PatchObject_SupportsNullValues()
    {
        // Arrange & Act
        var patch = new PatchObject
        {
            ["title"] = "Test",
            ["alerts"] = null // Remove property
        };

        // Assert
        Assert.Equal(2, patch.Count);
        Assert.Null(patch["alerts"]);
    }

    [Fact]
    public void PatchObject_SupportsNestedObjects()
    {
        // Arrange & Act
        var patch = new PatchObject
        {
            ["locations/loc1/name"] = "Conference Room",
            ["locations/loc1/coordinates"] = "geo:52.520008,13.404954"
        };

        // Assert
        Assert.Equal(2, patch.Count);
        Assert.True(patch.Validate());
    }

    [Fact]
    public void ForbiddenPrefixesForRecurrence_ContainsExpectedValues()
    {
        // Arrange
        var expectedPrefixes = new[]
        {
            "@type",
            "excludedRecurrenceRules",
            "method",
            "privacy",
            "prodId",
            "recurrenceId",
            "recurrenceIdTimeZone",
            "recurrenceOverrides",
            "recurrenceRules",
            "relatedTo",
            "replyTo",
            "sentBy",
            "timeZones",
            "uid"
        };

        // Act & Assert
        foreach (var prefix in expectedPrefixes) Assert.Contains(prefix, PatchObject.ForbiddenPrefixesForRecurrence);
    }
}