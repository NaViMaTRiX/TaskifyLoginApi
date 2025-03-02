using TaskifyApi.Domain.Models.Enum;
using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Models;
public class AuditLogs : BaseModel
{
    public Guid Id { get; init; } 
    public string? OrgId { get; init; }
    public ACTION? Action { get; init; }
    public string? EntityId { get; init; } 
    public string? EntityTitle { get; init; }
    public ENTITY_TYPE? EntityType { get; init; }
    public string? UserId { get; init; }
    public string? UserName { get; init; } 
    public string? UserImage { get; init; }
}