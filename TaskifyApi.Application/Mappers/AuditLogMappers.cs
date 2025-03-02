using TaskifyApi.Domain.Dtos.AuditLog;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.Application.Mappers;

public static class AuditLogMappers
{
    public static AuditLogDto ToAuditLogDto(this AuditLogs auditLog)
    {
        return new AuditLogDto
        {
            Id = auditLog.Id,
            OrgId = auditLog.OrgId,
            UserId = auditLog.UserId,
            UserName = auditLog.UserName,
            UserImage = auditLog.UserImage,
            Action = auditLog.Action,
            EntityId = auditLog.EntityId,
            EntityTitle = auditLog.EntityTitle,
            EntityType = auditLog.EntityType,
            CreatedTime = auditLog.CreatedTime,
            LastModifyTime = auditLog.LastModifyTime,
            CreatedUser = auditLog.CreatedUser,
            LastModifyUser = auditLog.LastModifyUser,
        };
    }

    public static AuditLogs ToAuditLogsFromCreate(this CreateAuditLogDto createAuditLog)
    {
        return new AuditLogs
        {
            Id = Guid.NewGuid(),
            OrgId = createAuditLog.OrgId,
            UserId = createAuditLog.UserId,
            UserName = createAuditLog.UserName,
            UserImage = createAuditLog.UserImage,
            Action = createAuditLog.Action,
            EntityId = createAuditLog.EntityId,
            EntityTitle = createAuditLog.EntityTitle,
            EntityType = createAuditLog.EntityType,
        };
    }
}