namespace TaskifyApi.Domain.Dtos.Card;

using System.ComponentModel.DataAnnotations;

public record CreateCardDto
{
    public string Title { get; init; }
    public int Order { get; init; }
    public string Description { get; init; }
    public bool TimeChecked { get; init; } //хз
    public bool ReadyChecked { get; init; } //хз
    public DateTime? TimeStart { get; init; }
    public DateTime? TimeEnd { get; init; }
}