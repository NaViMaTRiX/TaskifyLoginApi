namespace WebApiTaskify.Dtos.OrgSubscription;

using System.ComponentModel.DataAnnotations;

public class CreateOrgSubscriptionDto
{
    public string StripeCustomerId { get; set; }
    public string StripeSubscriptionId { get; set; }
    public string StripePriseId { get; set; } // это пока не подключил Юкассу
}