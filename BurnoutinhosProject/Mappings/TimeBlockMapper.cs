using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class TimeBlockMapper : IEntityTypeConfiguration<TimeBlock>
    {
        public void Configure(EntityTypeBuilder<TimeBlock> builder)
        {
            builder.ToTable("TimeBlock");
            builder.HasKey(tb => tb.Id);
            builder.Property(tb => tb.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");
            builder.Property(tb => tb.Name)
                .IsRequired()
                .HasColumnName("name");
            builder.Property(tb => tb.Start)
                .HasColumnName("start");
            builder.Property(tb => tb.TypeTime)
                .IsRequired()
                .HasColumnName("type_time");
            builder.Property(tb => tb.TodoId)
                .HasColumnName("todo_id");
            builder.HasOne<Todo>()
                .WithMany()
                .HasForeignKey(tb => tb.TodoId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Property(tb => tb.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("created_at");
            builder.Property(tb => tb.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("updated_at");
        }
    }
}
