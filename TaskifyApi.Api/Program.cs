using System.Text;
using Asp.Versioning;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskifyApi.Api.Middleware;
using TaskifyApi.Application.DI;
using TaskifyApi.Application.Validation;
using TaskifyApi.Application.Validation.Card;
using TaskifyApi.DAL.Data;
using TaskifyApi.DAL.DI;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
Console.OutputEncoding = Encoding.UTF8;

services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Configuration.AddUserSecrets<Program>();

// 1. Настройка Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

services.AddProblemDetails();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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

services.AddDataAccessLayer(builder.Configuration);
services.InitValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Log.Information("🚀Application started!!!");
}

app.UseCorrelation();
app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.MapControllers();
app.Run();

