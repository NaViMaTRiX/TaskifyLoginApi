using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Configurations;

public class OrgLimitConfiguration : IEntityTypeConfiguration<OrgLimits>
{
    public void Configure(EntityTypeBuilder<OrgLimits> builder)
    {
        builder.ToTable("org_limits").HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("id");
        builder.Property(c => c.Count).IsRequired(false).HasColumnName("count");
        builder.Property(c => c.OrgId).IsRequired(false).HasColumnName("org_id").HasMaxLength(100);
        builder.Property(c => c.CreatedTime).IsRequired().HasColumnName("created_time");
        builder.Property(c => c.LastModifyTime).IsRequired().HasColumnName("last_modify_time");
        builder.Property(c => c.CreatedUser).IsRequired(false).HasColumnName("created_user");
        builder.Property(c => c.LastModifyUser).IsRequired(false).HasColumnName("last_modify_user");
    }
}