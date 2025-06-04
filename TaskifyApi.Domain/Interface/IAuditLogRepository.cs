using TaskifyApi.Domain.Models;

namespace TaskifyApi.Domain.Interface;

public interface IAuditLogRepository
{
    Task<List<AuditLogs>> GetAllAsync(int page, int pageSize, CancellationToken token);
    Task<AuditLogs?> GetByIdAsync(Guid id, CancellationToken token);
    Task<AuditLogs?> CreateAsync(AuditLogs boardsModel, CancellationToken token);
    Task<AuditLogs?> DeleteAsync(Guid id, CancellationToken token);
    Task<bool> ExistAsync(Guid id, CancellationToken token);
}