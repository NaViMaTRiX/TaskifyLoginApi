using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskifyApi.DAL.Data;
using TaskifyApi.DAL.Repository;
using TaskifyApi.Domain.Interface;

namespace TaskifyApi.DAL.DI;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(AppDbContext));
        
        if(connectionString is null) 
            throw new Exception($"Database connection string is null, please check your configuration, {nameof(AppDbContext)}");

        services.AddDbContextPool<AppDbContext>(options =>
        {
            AppDbContext.ConfigureOptions(options, connectionString);
        });
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<IListRepository, ListRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IOrgLimitRepository, OrgLimitRepository>();
        services.AddScoped<IOrgSubscriptionRepository, OrgSubscriptionRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
    }
}