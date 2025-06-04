using TaskifyApi.Domain.Interface;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

using Data;
using Microsoft.EntityFrameworkCore;

public class OrgSubscriptionRepository(AppDbContext context) : IOrgSubscriptionRepository
{
    public async Task<List<OrgSubscriptions>> GetAllAsync(int page, int pageSize, CancellationToken token)
    {
        return await context.OrgSubscription
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
    }

    public async Task<OrgSubscriptions?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await context.OrgSubscription
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<OrgSubscriptions?> CreateAsync(OrgSubscriptions listModel, CancellationToken token)
    {
        await context.OrgSubscription.AddAsync(listModel, token);
        await context.SaveChangesAsync(token);
        return listModel;
    }

    public async Task<OrgSubscriptions?> DeleteAsync(Guid id, CancellationToken token)
    {
        var listModel = await GetByIdAsync(id, token);

        if (listModel is null)
            return null;
        
        context.OrgSubscription.Remove(listModel);
        await context.SaveChangesAsync(token);
        return listModel;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken token)
    {
        return await context.OrgSubscription.AnyAsync(x => x.Id == id, token);
    }
}