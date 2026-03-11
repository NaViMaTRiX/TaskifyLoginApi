using Microsoft.Extensions.Configuration;
using TaskifyApi.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskifyApi.DAL.DI;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        IConfiguration configuration = new ConfigurationBuilder().Build();

        optionsBuilder.UseNpgsql(
            "Server=ep-little-frog-a2qm5av7-pooler.eu-central-1.aws.neon.tech;Port=5432;Database=Taskify;User Id=NaViMaTRiX;Password=2VL5AWzZrYvI",

            sqlOptions => sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        );

        return new AppDbContext(optionsBuilder.Options, configuration);
    }
}