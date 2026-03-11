using TaskifyApi.Domain.Interface;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

using Data;
using Microsoft.EntityFrameworkCore;

public class OrgSubscriptionRepository(AppDbContext context) : IOrgSubscriptionRepository
{
    public async Task<List<OrgSubscriptions>> GetAllAsync(CancellationToken token)
    {
        var subs = await context.OrgSubscription.AsNoTracking().ToListAsync(token);
        
        if (subs is null)
            throw new ArgumentNullException($"{nameof(subs)} does not exist");
        
        return subs;
    }

    public async Task<OrgSubscriptions?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var sub = await context.OrgSubscription
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, token);
        
        if (sub is null)
            throw new ArgumentNullException($"{nameof(sub)} does not exist, id: {id}");
        return sub;
    }

    public async Task<OrgSubscriptions?> CreateAsync(OrgSubscriptions listModel, CancellationToken token)
    {
        await context.OrgSubscription.AddAsync(listModel, token);
        await context.SaveChangesAsync(token);
        return listModel;
    }

    public async Task<OrgSubscriptions?> DeleteAsync(Guid id, CancellationToken token)
    {
        var sub = await context.OrgSubscription
            .SingleOrDefaultAsync(x => x.Id == id, token);
        
        if (sub is null)
            throw new ArgumentNullException($"{nameof(sub)} does not exist, id: {id}");
        
        context.OrgSubscription.Remove(sub);
        await context.SaveChangesAsync(token);
        return sub;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken token)
    {
        return await context.OrgSubscription.AnyAsync(x => x.Id == id, token);
    }
}