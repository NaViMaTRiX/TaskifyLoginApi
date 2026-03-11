namespace TaskifyApi.Domain.Dtos.OrgLimit;

public record UpdateOrgLimitDto
{
    public int Limit { get; init; }
}