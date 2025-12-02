using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net;

/// <summary>
///     Represents a set of patches to apply to a JSCalendar object (RFC 8984 Section 1.4.9).
///     Used for recurrenceOverrides and localizations.
///     A PatchObject is of type String[*] and represents an unordered set of patches on a JSON object.
///     Each key is a path in JSON Pointer format (RFC 6901) with an implicit leading '/'.
///     Patch validation rules:
///     1. The pointer MUST NOT reference inside an array
///     2. All parts prior to the last MUST already exist on the object being patched
///     3. There MUST NOT be two patches where one pointer is a prefix of another
///     4. The value for the patch MUST be valid for the property being set
///     Patch application:
///     - If value is null: remove the property (no-op if not present)
///     - If value is non-null: set the value (replacement or addition)
/// </summary>
/// <remarks>
///     This type is typically represented as Dictionary&lt;string, object?&gt; in C#.
///     The keys are JSON Pointer paths (without leading '/').
///     Example usage in recurrenceOverrides:
///     {
///     "title": "Modified Title",           // Patches the title property
///     "locations/loc1/name": "New Place",  // Patches nested location name
///     "alerts": null                       // Removes the alerts property
///     }
///     A PatchObject does not define its own @type property.
///     Implementations MUST reject invalid PatchObjects entirely (no partial application).
/// </remarks>
[JsonConverter(typeof(PatchObjectJsonConverter))]
public sealed class PatchObject : Dictionary<string, object?>
{
    /// <summary>
    ///     Gets the list of forbidden pointer prefixes for recurrence overrides.
    ///     According to RFC 8984 Section 4.3.5, these MUST be ignored.
    /// </summary>
    public static readonly string[] ForbiddenPrefixesForRecurrence =
    [
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
    ];

    /// <summary>
    ///     Creates an empty PatchObject.
    /// </summary>
    public PatchObject()
    {
    }

    /// <summary>
    ///     Creates a PatchObject with initial patches.
    /// </summary>
    /// <param name="patches">Initial patches to apply.</param>
    public PatchObject(IDictionary<string, object?> patches) : base(patches)
    {
    }

    /// <summary>
    ///     Validates a patch pointer according to RFC 8984 Section 1.4.9.
    /// </summary>
    /// <param name="pointer">The JSON Pointer path (without leading '/').</param>
    /// <returns>True if valid, false otherwise.</returns>
    public static bool IsValidPointer(string pointer)
    {
        if (string.IsNullOrEmpty(pointer))
            return false;

        // Must not reference inside an array (contains numeric segment like /0/ or /123/)
        var segments = pointer.Split('/');
        return segments.All(segment => !int.TryParse(segment, out _));
    }

    /// <summary>
    ///     Checks if one pointer is a prefix of another.
    /// </summary>
    /// <param name="pointer1">First pointer.</param>
    /// <param name="pointer2">Second pointer.</param>
    /// <returns>True if one is a prefix of the other.</returns>
    public static bool IsPrefixConflict(string pointer1, string pointer2)
    {
        if (pointer1 == pointer2)
            return true;

        return pointer1.StartsWith(pointer2 + "/") || pointer2.StartsWith(pointer1 + "/");
    }

    /// <summary>
    ///     Validates this PatchObject according to RFC 8984 rules.
    /// </summary>
    /// <returns>True if all patches are valid.</returns>
    public bool Validate()
    {
        var pointers = Keys.ToList();

        // Check each pointer is valid
        if (pointers.Any(pointer => !IsValidPointer(pointer))) return false;

        // Check for prefix conflicts (rule 3)
        for (var i = 0; i < pointers.Count; i++)
        for (var j = i + 1; j < pointers.Count; j++)
            if (IsPrefixConflict(pointers[i], pointers[j]))
                return false;

        return true;
    }

    /// <summary>
    ///     Checks if a pointer is forbidden for recurrence overrides.
    /// </summary>
    /// <param name="pointer">The JSON Pointer path.</param>
    /// <returns>True if the pointer should be ignored.</returns>
    public static bool IsForbiddenForRecurrence(string pointer)
    {
        return ForbiddenPrefixesForRecurrence
            .Any(prefix => pointer == prefix || pointer.StartsWith(prefix + "/"));
    }
}