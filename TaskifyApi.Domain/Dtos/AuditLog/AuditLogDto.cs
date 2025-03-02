using TaskifyApi.Domain.Models.Enum;
using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Dtos.AuditLog;

public class AuditLogDto : BaseModel
{
    public Guid Id { get; set; }
    public string? OrgId { get; set; }
    public ACTION? Action { get; set; }
    public string? EntityId { get; set; }
    public string? EntityTitle { get; set; }
    public ENTITY_TYPE? EntityType { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserImage { get; set; }
}