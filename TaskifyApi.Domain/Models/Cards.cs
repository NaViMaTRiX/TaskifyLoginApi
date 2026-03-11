using System.Text.Json.Serialization;
using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Models;

public class Cards : BaseModel
{
    public Guid Id { get; init; }
    public string? Title { get; set; }
    public int? Position { get; set; } // TODO Position
    public Guid? ListId { get; init; }
    public string? Description { get; set; }
    public bool? Timer { get; set; } // таймер есть или нет
    public bool? Completed { get; set; } // TODO Completed
    public DateTime? TimeStart { get; set; } 
    public DateTime? TimeEnd { get; set; }
    
    [JsonIgnore]
    public Lists? List { get; init; }
}