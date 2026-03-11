using System.ComponentModel.DataAnnotations;

namespace TaskifyApi.Domain.Models.Libs;

public abstract class BaseModel
{
    [Required]
    public DateTime CreatedTime { get; init; } = DateTime.UtcNow;
    public DateTime? LastModifyTime { get; set; } = DateTime.UtcNow;
    public string CreatedUser { get; init; } = string.Empty;
    public string? LastModifyUser { get; init; } = string.Empty;
}