namespace TaskifyApi.Domain.Dtos.List;

public record UpdateListDto
{
    public string Title { get; init; }
    public int Order { get; init; }
}