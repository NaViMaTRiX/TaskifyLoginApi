using TaskifyApi.Domain.Models.Libs;

namespace TaskifyApi.Domain.Dtos.OrgSubscription;

public class OrgSubscriptionDto : BaseModel
{
    public Guid Id { get; set; }
    public string? OrgId { get; set; } = string.Empty;
    public string? StripeCustomerId { get; set; } = string.Empty;
    public string? StripeSubscriptionId { get; set; } = string.Empty;
    public string? StripePriseId { get; set; } = string.Empty; // это пока не подключил Юкассу
    public DateTime? StripeCurrentPeriodEnd { get; set; } = DateTime.UtcNow; //есть только потому что лень задавать где-то пусть лучше тут
}