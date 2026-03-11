using FluentValidation;
using TaskifyApi.Domain.Dtos.Board;

namespace TaskifyApi.Application.Validation.Board;

public class CreateBoardValidator : AbstractValidator<CreateBoardDto>
{
    public CreateBoardValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(100).WithMessage("Title cannot be longer than 50 characters");

        RuleFor(x => x.ImageFullUrl).Must(BeValidUrl)
            .WithMessage("'{PropertyName}' is not a valid URL.");
        
        RuleFor(x => x.ImageThumbUrl).Must(BeValidUrl)
            .WithMessage("'{PropertyName}' is not a valid URL.");;
        
        RuleFor(x => x.ImageLinkHtml).Must(BeValidUrl)
            .WithMessage("'{PropertyName}' is not a valid URL.");;
        
        RuleFor(x => x.ImageId).NotEmpty().WithMessage("ImageId cannot be empty");
    }
    
    private static bool BeValidUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
            return false;

        return uriResult.IsWellFormedOriginalString() &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps) &&
               !string.IsNullOrEmpty(uriResult.Host); // чтобы отсечь "http://"
    }
}

