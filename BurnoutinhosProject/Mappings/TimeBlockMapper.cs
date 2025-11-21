using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class TimeBlockMapper : IEntityTypeConfiguration<TimeBlock>
    {
        public void Configure(EntityTypeBuilder<TimeBlock> builder)
        {
            builder.ToTable("t_burnoutinhos_timeblock");
            builder.HasKey(tb => tb.Id);
            builder.Property(tb => tb.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id_timebk");
            builder.Property(tb => tb.Name)
                .IsRequired()
                .HasColumnName("name_timebk");
            builder.Property(tb => tb.Start)
                .HasColumnName("start_timebk");
            builder.Property(tb => tb.TypeTime)
                .IsRequired()
                .HasColumnName("type_timebk");
            builder.Property(tb => tb.TimeCount)
                .IsRequired()
                .HasColumnName("time_count");
            builder.Property(tb => tb.End)
                .IsRequired()
                .HasColumnName("max_timebk");
            builder.Property(tb => tb.UserId)
                .HasColumnName("id_user");
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(tb => tb.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Property(tb => tb.TodoId)
                .HasColumnName("id_todo");
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
