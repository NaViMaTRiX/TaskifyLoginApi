using TaskifyApi.Domain.Models;

namespace TaskifyApi.Domain.Interface;

public interface ICardRepository
{
    Task<List<Cards>> GetAllAsync(CancellationToken token);
    
    /// <summary>
    /// Пока что не совсем понимаю зачем этот метод, но по нему можно найти все карточки по доске
    /// </summary>
    /// <param name="boardId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<Cards>> GetAllByBoardAsync(Guid boardId, CancellationToken token);
    /// <summary>
    /// Метод для получения всех карточек по доске
    /// </summary>
    /// <param name="listId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<Cards>> GetAllByListAsync(Guid listId, CancellationToken token);
    Task<Cards?> GetByIdAsync(Guid cardId, CancellationToken token);
    Task<Cards?> CreateAsync(Cards cardModel, Guid listId, CancellationToken token);
    Task<Cards?> UpdateAsync(Guid cardId, Cards cardModel, CancellationToken token);
    Task<Cards?> DeleteAsync(Guid cardId, CancellationToken token);
    Task<bool> ExistAsync(Guid cardId, CancellationToken token);
}