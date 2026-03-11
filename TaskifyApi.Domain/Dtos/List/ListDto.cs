using TaskifyApi.Domain.Dtos.Card;
using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Dtos.List;

public class ListDto : BaseModel
{
    public Guid Id { get; set; } 
    public Guid? BoardId { get; set; } 
    public string Title { get; set; } 
    public int? Order { get; set; }
    public ICollection<CardDto?> Cards { get; set; }

}