using TaskifyApi.Domain.Models;

namespace TaskifyApi.Domain.Interface;

public interface IOrgLimitRepository
{
    Task<List<OrgLimits>> GetAllAsync(CancellationToken token);
    Task<OrgLimits?> GetByIdAsync(Guid id, CancellationToken token);
    Task<OrgLimits?> CreateAsync(OrgLimits listModel, CancellationToken token); 
    Task<OrgLimits?> UpdateAsync(Guid id, OrgLimits listModel, CancellationToken token);
    Task<OrgLimits?> DeleteAsync(Guid id, CancellationToken token);
    Task<bool> ExistAsync(Guid id, CancellationToken token);
}