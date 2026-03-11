namespace TaskifyApi.Domain.Dtos.Board;

public record CreateBoardDto
{
    public string Title { get; init; }
    public string ImageId { get; init; }
    public string ImageFullUrl { get; init; }
    public string ImageThumbUrl { get; init; }
    public string ImageUserName { get; init; }
    public string ImageLinkHtml { get; init; }
}