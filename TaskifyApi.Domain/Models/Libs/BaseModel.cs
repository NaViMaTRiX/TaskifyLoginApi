using System.ComponentModel.DataAnnotations;

namespace TaskifyApi.Domain.Models.Libs;

public abstract class BaseModel
{
    [Required]
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime? LastModifyTime { get; set; } = DateTime.UtcNow;
    public string CreatedUser { get; set; } = string.Empty;
    public string? LastModifyUser { get; set; } = string.Empty;
}