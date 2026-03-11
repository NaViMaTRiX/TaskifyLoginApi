using Microsoft.EntityFrameworkCore;
using TaskifyApi.DAL.Data;
using TaskifyApi.Domain.Interface;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

public class ListRepository(AppDbContext context) : IListRepository
{
    public async Task<List<Lists>> GetAllAsync(CancellationToken token)
    {
        var lists = await context.List
            .AsNoTracking()
            .Include(p => p.Cards).ToListAsync(token);
        
        if(lists is null)
            throw new ArgumentNullException($"{nameof(lists)} is null");
        
        return lists;
    }

    public async Task<List<Lists>> GetAllByBoardAsync(Guid boardId, CancellationToken token)
    {
        if(!await context.Board.AnyAsync(x => x.Id == boardId, token))
            throw new ArgumentNullException($"{nameof(boardId)} is null, Id: {boardId}");
        
        var lists = await context.List.Where(x => x.BoardId == boardId)
            .AsNoTracking()
            .ToListAsync(token);
        
        if(lists is null)
            throw new ArgumentNullException($"{nameof(lists)} is null");
        
        return lists;
    }

    public async Task<Lists?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var list = await context.List
            .AsNoTracking()
            .Include(x => x.Cards)
            .SingleOrDefaultAsync(x => x.Id == id, token);
        
        if(list is null)
            throw new ArgumentNullException($"{nameof(list)} is null");
        
        return list;
    }

    public async Task<Lists?> CreateAsync(Lists listModel, CancellationToken token)
    {
        await context.List.AddAsync(listModel, token);
        await context.SaveChangesAsync(token);
        return listModel;
    }

    public async Task<Lists?> UpdateAsync(Guid id, Lists listModel, CancellationToken token)
    {
        var list = await context.List.SingleOrDefaultAsync(x => x.Id == id, token);
        
        if (list is null) 
            throw new ArgumentNullException($"{nameof(list)} is null, id: {id}");
        
        list.Title = listModel.Title;
        list.Order = listModel.Order;
        list.LastModifyTime = listModel.LastModifyTime;
        
        await context.SaveChangesAsync(token);
        return list;
    }

    public async Task<Lists?> DeleteAsync(Guid id, CancellationToken token)
    {
        var list = await context.List.SingleOrDefaultAsync(x => x.Id == id, token);
        
        if (list is null) 
            throw new ArgumentNullException($"{nameof(list)} is null, id: {id}");
        
        context.List.Remove(list);
        await context.SaveChangesAsync(token);
        return list;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken token)
    {
        return await context.List.AnyAsync(x => x.Id == id, token);
    }
}