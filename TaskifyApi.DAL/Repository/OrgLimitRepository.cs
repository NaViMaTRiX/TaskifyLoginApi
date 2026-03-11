using Microsoft.EntityFrameworkCore;
using TaskifyApi.DAL.Data;
using TaskifyApi.Domain.Interface;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

public class OrgLimitRepository(AppDbContext context) : IOrgLimitRepository
{
    public async Task<List<OrgLimits>> GetAllAsync(CancellationToken token)
    {
        var limits = await context.OrgLimit.AsNoTracking().ToListAsync(token);
        
        if(limits is null)
            throw new ArgumentNullException($"{nameof(limits)} is null");
        
        return limits;
    }

    public async Task<OrgLimits?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var limit = await context.OrgLimit
            .AsNoTracking()     //TODO: сделать еще один метод без noTracking и встаить его в методы Create, Update, Delete.
            .SingleOrDefaultAsync(x =>x.Id == id, token);
        
        if(limit is null)
            throw new ArgumentNullException($"{nameof(limit)} is null, id: {id}");
        
        return limit;
    }

    public async Task<OrgLimits?> CreateAsync(OrgLimits listModel, CancellationToken token)
    {
        await context.OrgLimit.AddAsync(listModel, token);
        await context.SaveChangesAsync(token);
        return listModel;
    }

    public async Task<OrgLimits?> UpdateAsync(Guid id, OrgLimits listModel, CancellationToken token)
    {
        var orgLimit = await context.OrgLimit.SingleOrDefaultAsync(x => x.Id == id, token);
        
        if (orgLimit is null)
            throw new ArgumentNullException($"{nameof(orgLimit)} is null, id: {id}");
        
        orgLimit.Count = listModel.Count;
        orgLimit.LastModifyTime = listModel.LastModifyTime;
        await context.SaveChangesAsync(token);
        return orgLimit;
    }

    public async Task<OrgLimits?> DeleteAsync(Guid id, CancellationToken token)
    {
        var orgLimit = await context.OrgLimit.SingleOrDefaultAsync(x => x.Id == id, token);
        
        if (orgLimit is null)
            throw new ArgumentNullException($"{nameof(orgLimit)} is null, id: {id}");
        
        context.OrgLimit.Remove(orgLimit);
        await context.SaveChangesAsync(token);
        return orgLimit;
    }

    public Task<bool> ExistAsync(Guid id, CancellationToken token)
    {
        return context.OrgLimit.AnyAsync(x => x.Id == id, token);
    }
}