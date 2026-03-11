using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Models;

public class Organizations : BaseModel
{
    public string Id { get; init; }
    public string Name { get; set; }
    public string? Description{ get; set; }
    public string Logo{ get; set; }
    public string? Website{ get; set; }
    public string? Email{ get; set; }
    public string? Phone{ get; set; }
    public string? Address { get; set; }
    public string? State { get; set; }
}