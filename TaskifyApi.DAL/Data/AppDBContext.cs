using Microsoft.Extensions.Configuration;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using TaskifyApi.Domain.Models;
using TaskifyApi.Domain.Models.Enum;

namespace TaskifyApi.DAL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    public static void ConfigureOptions(DbContextOptionsBuilder optionsBuilder, string? connectionString)
    {
        // Получаем строку и создаём объект для того чтобы его сконфигурировать
        var builder = new NpgsqlDataSourceBuilder(connectionString);
        
        // Настраиваем сопоставление типов Enum
        builder.MapEnum<ACTION>("action");
        builder.MapEnum<ENTITY_TYPE>("entity_type");

        // Строим DataSource и передаем его в UseNpgsql
        var dataSource = builder.Build();
        optionsBuilder.UseNpgsql(dataSource, o => o.CommandTimeout(7));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            ConfigureOptions(optionsBuilder, configuration.GetConnectionString("AppDbContext"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Регистрируем Enum в модели
        modelBuilder.HasPostgresEnum<ACTION>("action");
        modelBuilder.HasPostgresEnum<ENTITY_TYPE>("entity_type");
        
        //Подключаем комфигурацию db
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Boards> Board { get; init; }
    public DbSet<Lists> List { get; init; }
    public DbSet<Cards> Card { get; init; }
    public DbSet<Organizations> Organization { get; init; }
    public DbSet<AuditLogs> AuditLog { get; init; }
    public DbSet<OrgLimits> OrgLimit { get; init; }
    public DbSet<OrgSubscriptions> OrgSubscription { get; init; }
}