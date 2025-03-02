using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Boards>
{
    public void Configure(EntityTypeBuilder<Boards> builder)
    {
        builder.ToTable("boards").HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("id");
        builder.Property(c => c.Title).HasMaxLength(100).IsRequired(false).HasColumnName("title");
        builder.Property(c => c.ImageFullUrl).IsRequired(false).HasMaxLength(300).HasColumnName("image_full_url");
        builder.Property(c => c.ImageThumbUrl).IsRequired(false).HasMaxLength(300).HasColumnName("image_thumb_url");
        builder.Property(c => c.ImageFullUrl).IsRequired(false).HasMaxLength(300).HasColumnName("image_full_url");
        builder.Property(c => c.ImageUserName).IsRequired(false).HasMaxLength(300).HasColumnName("image_user_name");
        builder.Property(c => c.ImageLinkHTML).IsRequired(false).HasMaxLength(300).HasColumnName("image_link_html");
        builder.Property(c => c.ImageId).IsRequired(false).HasMaxLength(300).HasColumnName("image_id");
        builder.Property(c => c.OrgId).IsRequired(false).HasColumnName("org_id").HasMaxLength(100);
        builder.Property(c => c.CreatedTime).IsRequired().HasColumnName("created_time");
        builder.Property(c => c.LastModifyTime).IsRequired().HasColumnName("last_modify_time");
        builder.Property(c => c.CreatedUser).IsRequired(false).HasColumnName("created_user");
        builder.Property(c => c.LastModifyUser).IsRequired(false).HasColumnName("last_modify_user");
    }
}