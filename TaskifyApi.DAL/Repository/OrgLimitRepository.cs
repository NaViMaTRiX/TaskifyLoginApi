using Microsoft.EntityFrameworkCore;
using TaskifyApi.DAL.Data;
using TaskifyApi.Domain.Interface;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

public class OrgLimitRepository(AppDbContext context) : IOrgLimitRepository
{
    public async Task<List<OrgLimits>> GetAllAsync(int page, int pageSize, CancellationToken token)
    {
        return await context.OrgLimit
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
    }

    public async Task<OrgLimits?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await context.OrgLimit
            .AsNoTracking()     //TODO: сделать еще один метод без noTracking и встаить его в методы Create, Update, Delete.
            .SingleOrDefaultAsync(x =>x.Id == id, token);
    }

    public async Task<OrgLimits?> CreateAsync(OrgLimits listModel, CancellationToken token)
    {
        await context.OrgLimit.AddAsync(listModel, token);
        await context.SaveChangesAsync(token);
        return listModel;
    }

    public async Task<OrgLimits?> UpdateAsync(Guid id, OrgLimits listModel, CancellationToken token)
    {
        var orgLimit = await GetByIdAsync(id, token);
        
        if (orgLimit is null)
            return null;
        
        orgLimit.Count = listModel.Count;
        orgLimit.LastModifyTime = listModel.LastModifyTime;
        await context.SaveChangesAsync(token);
        return orgLimit;
    }

    public async Task<OrgLimits?> DeleteAsync(Guid id, CancellationToken token)
    {
        var orgLimit = await GetByIdAsync(id, token);
        if (orgLimit is null)
            return null;
        
        context.OrgLimit.Remove(orgLimit);
        await context.SaveChangesAsync(token);
        return orgLimit;
    }

    public Task<bool> ExistAsync(Guid id, CancellationToken token)
    {
        return context.OrgLimit.AnyAsync(x => x.Id == id, token);
    }
}