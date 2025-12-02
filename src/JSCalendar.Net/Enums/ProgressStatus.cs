using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JSCalendar.Net.Converters;

namespace JSCalendar.Net.Enums;

/// <summary>
///     Progress status for tasks (RFC 8984 Section 8.4.3).
/// </summary>
[JsonConverter(typeof(EnumMemberJsonConverterFactory))]
public enum ProgressStatus
{
    /// <summary>
    ///     Indicates the task needs action.
    /// </summary>
    [EnumMember(Value = "needs-action")]
    NeedsAction,

    /// <summary>
    ///     Indicates the task is in process.
    /// </summary>
    [EnumMember(Value = "in-process")]
    InProcess,

    /// <summary>
    ///     Indicates the task is completed.
    /// </summary>
    [EnumMember(Value = "completed")]
    Completed,

    /// <summary>
    ///     Indicates the task failed.
    /// </summary>
    [EnumMember(Value = "failed")]
    Failed,

    /// <summary>
    ///     Indicates the task was cancelled.
    /// </summary>
    [EnumMember(Value = "cancelled")]
    Cancelled
}