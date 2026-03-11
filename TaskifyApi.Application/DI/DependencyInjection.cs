using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskifyApi.Application.Validation.Board;
using TaskifyApi.Application.Validation.Card;

namespace TaskifyApi.Application.DI;

public static class DependencyInjection
{
    public static void InitValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CardDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateBoardValidator>();
    }
}