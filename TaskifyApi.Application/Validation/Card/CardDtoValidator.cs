using FluentValidation;
using TaskifyApi.Domain.Dtos.Card;

namespace TaskifyApi.Application.Validation.Card;

public class CardDtoValidator : AbstractValidator<CreateCardDto>
{
    public CardDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description too long.");
        
        RuleFor(x => x.Order).NotEmpty().WithMessage("Order is required.");

        RuleFor(x => x.TimeStart)
            .LessThanOrEqualTo(x => x.TimeEnd)
            .When(x => x.TimeStart.HasValue && x.TimeEnd.HasValue)
            .WithMessage("TimeStart must be less than or equal to TimeEnd.");
    }
}