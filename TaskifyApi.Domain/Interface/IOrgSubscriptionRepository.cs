using TaskifyApi.Domain.Models;

namespace TaskifyApi.Domain.Interface;

public interface IOrgSubscriptionRepository
{
    Task<List<OrgSubscriptions>> GetAllAsync(int page, int pageSize, CancellationToken token);
    Task<OrgSubscriptions?> GetByIdAsync(Guid id, CancellationToken token);
    Task<OrgSubscriptions?> CreateAsync(OrgSubscriptions listModel, CancellationToken token); 
    Task<OrgSubscriptions?> DeleteAsync(Guid id, CancellationToken token);
    Task<bool> ExistAsync(Guid id, CancellationToken token);
}