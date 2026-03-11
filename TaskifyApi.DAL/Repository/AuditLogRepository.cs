using Microsoft.EntityFrameworkCore;
using TaskifyApi.DAL.Data;
using TaskifyApi.Domain.Interface;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

public class AuditLogRepository(AppDbContext context) : IAuditLogRepository
{
    public async Task<List<AuditLogs>> GetAllAsync(CancellationToken token)
    {
        var lists = await context.AuditLog.AsNoTracking().ToListAsync(token);
        
        if (lists is null)
            throw new ArgumentNullException($"{nameof(lists)} does not exist");
        
        return lists;
    }

    public async Task<List<AuditLogs>> GetAllByUserAsync(string userId, CancellationToken token)
    {
        //TODO: Implement check for user by api
        
        var logs = await context.AuditLog.Where(x => x.UserId == userId).AsNoTracking().ToListAsync(token);
        
        if (logs is null)
            throw new ArgumentNullException($"{nameof(logs)} does not exist");
        
        return logs;
    }

    public async Task<AuditLogs?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var auditLogs = await context.AuditLog
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, token);
        
        if (auditLogs is null)
            throw new ArgumentNullException($"{nameof(auditLogs)} does not exist");
        
        return auditLogs;
    }

    public async Task<AuditLogs?> CreateAsync(AuditLogs boardsModel, CancellationToken token)
    {
        await context.AuditLog.AddAsync(boardsModel, token);
        await context.SaveChangesAsync(token);
        return boardsModel;
    }

    public async Task<AuditLogs?> DeleteAsync(Guid id, CancellationToken token)
    {
        var audit = await context.AuditLog.SingleOrDefaultAsync(x => x.Id == id, token);
        if (audit is null)
            throw new ArgumentNullException($"{nameof(audit)} does not exist");
        
        context.AuditLog.Remove(audit);
        await context.SaveChangesAsync(token);
        return audit;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken token)
    {
        var exist = await context.AuditLog.AnyAsync(x => x.Id == id, token);
        
        return exist;
    }
}