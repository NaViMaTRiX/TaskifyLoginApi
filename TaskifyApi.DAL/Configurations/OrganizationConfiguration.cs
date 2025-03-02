using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organizations>
{
    public void Configure(EntityTypeBuilder<Organizations> builder)
    {
        builder.ToTable("organizations").HasKey(x => x.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("id");
        builder.Property(c => c.Email).HasMaxLength(75).IsRequired(false).HasColumnName("email");
        builder.Property(c => c.Description).IsRequired(false).HasMaxLength(900).HasColumnName("description");
        builder.Property(c => c.Address).IsRequired(false).HasMaxLength(300).HasColumnName("address");
        builder.Property(c => c.Name).IsRequired(false).HasMaxLength(100).HasColumnName("name");
        builder.Property(c => c.Logo).IsRequired(false).HasMaxLength(100).HasColumnName("logo");
        builder.Property(c => c.Phone).IsRequired(false).HasMaxLength(15).HasColumnName("phone");
        builder.Property(c => c.State).IsRequired(false).HasColumnName("state");
        builder.Property(c => c.Website).IsRequired(false).HasMaxLength(100).HasColumnName("website");
        builder.Property(c => c.CreatedTime).IsRequired().HasColumnName("created_time");
        builder.Property(c => c.LastModifyTime).IsRequired().HasColumnName("last_modify_time");
        builder.Property(c => c.CreatedUser).IsRequired(false).HasColumnName("created_user");
        builder.Property(c => c.LastModifyUser).IsRequired(false).HasColumnName("last_modify_user");
    }
}