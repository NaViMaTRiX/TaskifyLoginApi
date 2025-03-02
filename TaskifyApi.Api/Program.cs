using Asp.Versioning;
using TaskifyApi.DAL.Data;
using TaskifyApi.DAL.Repository;
using TaskifyApi.Domain.Interface;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
builder.Configuration.AddUserSecrets<Program>();

services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


services.AddDbContextPool<AppDbContext>(options =>
{
    AppDbContext.ConfigureOptions(options, builder.Configuration.GetConnectionString(nameof(AppDbContext)));
});

services.AddScoped<IBoardRepository, BoardRepository>();
services.AddScoped<IListRepository, ListRepository>();
services.AddScoped<ICardRepository, CardRepository>();
services.AddScoped<IOrgLimitRepository, OrgLimitRepository>();
services.AddScoped<IOrgSubscriptionRepository, OrgSubscriptionRepository>();
services.AddScoped<IAuditLogRepository, AuditLogRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.Run();