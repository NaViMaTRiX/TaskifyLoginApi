using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Cards>
{
    public void Configure(EntityTypeBuilder<Cards> builder)
    {
        builder.ToTable("cards").HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("id");
        builder.Property(c => c.Title).HasMaxLength(100).IsRequired(false).HasColumnName("title");
        builder.Property(c => c.Description).HasMaxLength(3000).IsRequired(false).HasColumnName("description");
        builder.Property(c => c.TimeStart).IsRequired(false).HasColumnName("time_start");
        builder.Property(c => c.TimeEnd).IsRequired(false).HasColumnName("time_end");
        builder.Property(c => c.ListId).IsRequired(false).HasColumnName("list_id");
        builder.Property(c => c.Position).IsRequired(false).HasColumnName("position");
        builder.Property(c => c.Timer).IsRequired(false).HasColumnName("timer");
        builder.Property(c => c.Completed).IsRequired(false).HasColumnName("completed");
        builder.Property(c => c.CreatedTime).IsRequired().HasColumnName("created_time");
        builder.Property(c => c.LastModifyTime).IsRequired().HasColumnName("last_modify_time");
        builder.Property(c => c.CreatedUser).IsRequired(false).HasColumnName("created_user");
        builder.Property(c => c.LastModifyUser).IsRequired(false).HasColumnName("last_modify_user");
        
        builder.HasOne(c => c.List)
            .WithMany(l => l.Cards)
            .HasForeignKey("ListId") // Теневое свойство (не существует в модели)
            .OnDelete(DeleteBehavior.Cascade);
    }
}