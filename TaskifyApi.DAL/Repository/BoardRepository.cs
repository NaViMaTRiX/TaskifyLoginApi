using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

using Data;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;

public class BoardRepository(AppDbContext context) : IBoardRepository
{
    public async Task<List<Boards>> GetAllAsync(int page, int pageSize, CancellationToken token)
    {
        return await context.Board
            .AsNoTracking()
            .Include(x => x.Lists)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
    }

    public async Task<Boards?> GetByIdAsync(Guid id, CancellationToken token)
    {
         var board =  await context.Board
             .AsNoTracking()
            .Include(x => x.Lists)
            .FirstOrDefaultAsync(x => x.Id == id, token);
         
         if (board is null)
             return null;
         
         return board;
    }

    public Task<Boards?> GetAllByOrgIdAsync(string orgId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Boards?> CreateAsync(string orgId, Boards boardModel, CancellationToken token)
    {
        var objOrgId = await context.Board.SingleOrDefaultAsync(x => x.OrgId == orgId, token);
        if (objOrgId is null)
            return null;
        
        await context.Board.AddAsync(boardModel, token);
        await context.SaveChangesAsync(token);
        return boardModel;
    }

    public async Task<Boards?> UpdateAsync(Guid id, Boards boardModel, CancellationToken token)
    {
        var board = await GetByIdAsync(id, token);
        
        if (board is null)
            return null;
        
        board.OrgId = boardModel.OrgId;
        board.Title = boardModel.Title;
        board.ImageId = boardModel.ImageId;
        board.ImageThumbUrl = boardModel.ImageThumbUrl;
        board.ImageFullUrl = boardModel.ImageFullUrl;
        board.ImageUserName = boardModel.ImageUserName;
        board.ImageLinkHTML = boardModel.ImageLinkHTML;
        board.LastModifyTime = boardModel.LastModifyTime;
        
        await context.SaveChangesAsync(token);
        return board;
    }

    public async Task<Boards?> DeleteAsync(Guid id, CancellationToken token)
    {
        var board = await GetByIdAsync(id, token);
        
        if (board is null)
            return null;
        
        context.Board.Remove(board);
        await context.SaveChangesAsync(token);
        return board;
    }

    public Task<bool> ExistAsync(Guid id, CancellationToken token)
    {
        return context.Board.AnyAsync(x => x.Id == id, token);
    }
}