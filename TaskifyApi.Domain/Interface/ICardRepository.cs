using TaskifyApi.Domain.Models;

namespace TaskifyApi.Domain.Interface;

public interface ICardRepository
{
    Task<List<Cards>> GetAllAsync(CancellationToken token);
    Task<Cards?> GetByIdAsync(Guid id, CancellationToken token);
    Task<Cards?> CreateAsync(Cards cardModel, CancellationToken token);
    Task<Cards?> UpdateAsync(Guid id, Cards cardModel, CancellationToken token);
    Task<Cards?> DeleteAsync(Guid id, CancellationToken token);
    Task<bool> ExistAsync(Guid id, CancellationToken token);
}