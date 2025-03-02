using TaskifyApi.Domain.Models.Libs;

namespace WebApiTaskify.Dtos.OrgLimit;

public class OrgLimitDto : BaseModel
{
    public Guid Id { get; set; }
    public string? OrgId { get; set; }
    public int? Limit { get; set; }
}