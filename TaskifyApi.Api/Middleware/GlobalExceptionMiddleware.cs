using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskifyApi.Application.Exceptions;

namespace TaskifyApi.Api.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = context.TraceIdentifier;
        var (statusCode, title, logLevel) = exception switch
        {
            ValidationException => (HttpStatusCode.BadRequest, "Ошибка валидации", LogLevel.Warning),
            NotFoundException   => (HttpStatusCode.NotFound, "Запрашиваемый ресурс не найден", LogLevel.Information),
            _                   => (HttpStatusCode.InternalServerError, "Произошла внутренняя ошибка сервера", LogLevel.Error)
        };

        logger.Log(logLevel, exception, "Произошла ошибка (TraceID: {TraceId}) для запроса {Path}", traceId, context.Request.Path);

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Instance = context.Request.Path,
            Detail = env.IsDevelopment() ? exception.ToString() : null,
        };
        problemDetails.Extensions["traceId"] = traceId;
        
        // Очищаем любые предыдущие данные в ответе и устанавливаем статус код
        context.Response.Clear(); 
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        try
        {
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true; // Указываем, что исключение обработано
        }
        catch (Exception fallbackEx)
        {
            logger.LogError(fallbackEx, "Произошла ошибка при формировании fallback-ответа.");
            
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync($"Ошибка: {title}. TraceId: {traceId}", cancellationToken);
            return true; // Указываем, что исключение обработано (пусть и с fallback'ом)
        }
    }
}