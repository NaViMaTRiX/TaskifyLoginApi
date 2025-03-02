using TaskifyApi.Domain.Dtos.OrgSubscription;
using TaskifyApi.Domain.Models;
using WebApiTaskify.Dtos.OrgSubscription;

namespace TaskifyApi.Application.Mappers;

public static class OrgSubscriptionMappers
{
    public static OrgSubscriptionDto ToOrgSubscriptionDto(this OrgSubscriptions orgSubscription)
    {
        return new OrgSubscriptionDto
        {
            Id = orgSubscription.Id,
            OrgId = orgSubscription.OrgId,
            StripePriseId = orgSubscription.StripePriceId,
            StripeCustomerId = orgSubscription.StripeCustomerId,
            StripeSubscriptionId = orgSubscription.StripeSubscriptionId,
            StripeCurrentPeriodEnd = orgSubscription.StripeCurrentPeriodEnd,
            CreatedUser = orgSubscription.CreatedUser,
            LastModifyUser = orgSubscription.LastModifyUser,
            CreatedTime = orgSubscription.CreatedTime,
            LastModifyTime = orgSubscription.LastModifyTime,
        };
    }

    public static OrgSubscriptions ToOrgSubscriptionDtoFromCreate(this CreateOrgSubscriptionDto createOrgSubscription, string orgId)
    {
        return new OrgSubscriptions
        {
            Id = Guid.NewGuid(),
            OrgId = orgId,
            StripePriceId = createOrgSubscription.StripePriseId,
            StripeCustomerId = createOrgSubscription.StripeCustomerId,
            StripeSubscriptionId = createOrgSubscription.StripeSubscriptionId,
        };
    }
}