using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Configurations;

public class ListConfiguration : IEntityTypeConfiguration<Lists>
{
    public void Configure(EntityTypeBuilder<Lists> builder)
    {
        builder.ToTable("lists").HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnName("id");
        builder.Property(c => c.Title).HasMaxLength(100).IsRequired(false).HasColumnName("title");
        builder.Property(c => c.Order).IsRequired(false).HasColumnName("order");
        builder.Property(c => c.BoardId).IsRequired(false).HasColumnName("board_id");
        builder.Property(c => c.CreatedTime).IsRequired().HasColumnName("created_time");
        builder.Property(c => c.LastModifyTime).IsRequired().HasColumnName("last_modify_time");
        builder.Property(c => c.CreatedUser).IsRequired(false).HasColumnName("created_user");
        builder.Property(c => c.LastModifyUser).IsRequired(false).HasColumnName("last_modify_user");
        
        builder.HasOne(l => l.Board)
            .WithMany(l => l.Lists)
            .HasForeignKey("BoardId") // Теневое свойство (не существует в модели)
            .OnDelete(DeleteBehavior.Cascade);
    }
}