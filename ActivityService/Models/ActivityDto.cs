using System.ComponentModel.DataAnnotations;

namespace ActivityService.Models;

public sealed class ActivityDto
{
    [Required] public string UserId { get; set; } = string.Empty;
    [Required] public string Action { get; set; } = string.Empty;
    public string? Source { get; set; } = "web";
    public long? TimestampUnix { get; set; }
}
