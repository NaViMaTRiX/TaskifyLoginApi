using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Configurations;

public class OrgSubscriptionConfiguration : IEntityTypeConfiguration<OrgSubscriptions>
{
    public void Configure(EntityTypeBuilder<OrgSubscriptions> builder)
    {
        builder.ToTable("org_subscriptions").HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("id");
        builder.Property(c => c.StripeCustomerId).HasMaxLength(300).IsRequired(false).HasColumnName("stripe_customer_id");
        builder.Property(c => c.StripePriceId).IsRequired(false).HasColumnName("stripe_price_id").HasMaxLength(300);
        builder.Property(c => c.StripeSubscriptionId).IsRequired(false).HasColumnName("stripe_subscription_id").HasMaxLength(300);
        builder.Property(c => c.StripeCurrentPeriodEnd).IsRequired(false).HasColumnName("stripe_current_period_end");
        builder.Property(c => c.OrgId).IsRequired(false).HasColumnName("org_id").HasMaxLength(100);
        builder.Property(c => c.CreatedTime).IsRequired().HasColumnName("created_time");
        builder.Property(c => c.LastModifyTime).IsRequired().HasColumnName("last_modify_time");
        builder.Property(c => c.CreatedUser).IsRequired(false).HasColumnName("created_user");
        builder.Property(c => c.LastModifyUser).IsRequired(false).HasColumnName("last_modify_user");
        
    }
}