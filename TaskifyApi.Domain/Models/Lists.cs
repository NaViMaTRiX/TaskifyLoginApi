using System.Text.Json.Serialization;
using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Models;

public class Lists : BaseModel
{
    public Guid Id { get; init; }
    public Guid? BoardId { get; init; }
    public string? Title { get; set; }
    public int? Order { get; set; }
    
    public ICollection<Cards>? Cards { get; init; }
    [JsonIgnore]
    public Boards? Board { get; init; }
}