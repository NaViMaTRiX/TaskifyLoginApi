namespace TaskifyApi.Domain.Dtos.OrgSubscription;

public record CreateOrgSubscriptionDto
{
    public string StripeCustomerId { get; init; }
    public string StripeSubscriptionId { get; init; }
    public string StripePriseId { get; init; } // это пока не подключил Юкассу
}