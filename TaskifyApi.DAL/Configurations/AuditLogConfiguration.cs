using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyApi.Domain.Models;
using TaskifyApi.Domain.Models.Enum;

namespace TaskifyApi.DAL.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLogs>
{
    public void Configure(EntityTypeBuilder<AuditLogs> builder)
    {
        builder.ToTable("audit_logs").HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("id");
        
        builder.Property(c => c.Action)
            .IsRequired(false)
            .HasColumnName("action")
            .HasConversion(
                v => v.ToString(),
                v => (ACTION)Enum.Parse(typeof(ACTION), v!));

        builder.Property(c => c.EntityId).IsRequired(false).HasColumnName("entity_id").HasMaxLength(100);
        builder.Property(c => c.EntityTitle).IsRequired(false).HasColumnName("entity_title").HasMaxLength(100);
        
        builder.Property(c => c.EntityType)
            .IsRequired(false)
            .HasColumnName("entity_type")
            .HasConversion(
                v => v.ToString(),
                v => (ENTITY_TYPE)Enum.Parse(typeof(ENTITY_TYPE), v!));

        builder.Property(c => c.OrgId).IsRequired(false).HasColumnName("org_id").HasMaxLength(100);
        builder.Property(c => c.UserId).IsRequired(false).HasColumnName("user_id").HasMaxLength(100);
        builder.Property(c => c.UserName).IsRequired(false).HasMaxLength(100).HasColumnName("user_name");
        builder.Property(c => c.UserImage).IsRequired(false).HasMaxLength(300).HasColumnName("user_image");
        builder.Property(c => c.CreatedTime).IsRequired().HasColumnName("created_time");
        builder.Property(c => c.LastModifyTime).IsRequired().HasColumnName("last_modify_time");
        builder.Property(c => c.CreatedUser).IsRequired(false).HasColumnName("created_user");
        builder.Property(c => c.LastModifyUser).IsRequired(false).HasColumnName("last_modify_user");
    }
}