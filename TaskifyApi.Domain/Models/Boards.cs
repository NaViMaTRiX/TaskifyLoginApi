using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Models;

public class Boards : BaseModel
{
    public Guid Id { get; init; }
    public string? OrgId { get; set; }
    public string? Title { get; set; } 
    public string? ImageId { get; set; } 
    public string? ImageThumbUrl { get; set; } 
    public string? ImageFullUrl { get; set; } 
    public string? ImageUserName { get; set; } 
    public string? ImageLinkHTML { get; set; }
    
    public List<Lists>? Lists { get; init; }
}