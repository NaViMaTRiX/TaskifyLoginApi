using TaskifyApi.Domain.Dtos.OrgLimit;
using TaskifyApi.Domain.Models;
using WebApiTaskify.Dtos.OrgLimit;

namespace TaskifyApi.Application.Mappers;

public static class OrgMappers
{
    public static OrgLimitDto ToOrgLimitDto(this OrgLimits orgLimit)
    {
        return new OrgLimitDto
        {
            Id = orgLimit.Id,
            OrgId = orgLimit.OrgId,
            Limit = orgLimit.Count,
            LastModifyTime = orgLimit.LastModifyTime,
            CreatedTime = orgLimit.CreatedTime,
            LastModifyUser = orgLimit.LastModifyUser,
            CreatedUser = orgLimit.CreatedUser,
        };
    }

    public static OrgLimits ToCreateFromOrgLimitDto(this CreateOrgLimitDto createOrgLimitDto, string orgId)
    {
        return new OrgLimits
        {
            Id = Guid.NewGuid(),
            OrgId = orgId,
            Count = createOrgLimitDto.Limit,
        };
    }

    public static OrgLimits ToUpdateFromOrgLimitDto(this UpdateOrgLimitDto updateOrgLimitDto)
    {
        return new OrgLimits
        {
            Count = updateOrgLimitDto.Limit,
        };
    }
}