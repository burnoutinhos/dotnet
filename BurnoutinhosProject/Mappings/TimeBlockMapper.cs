using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class TimeBlockMapper : IEntityTypeConfiguration<TimeBlock>
    {
        public void Configure(EntityTypeBuilder<TimeBlock> builder)
        {
            builder.ToTable("T_BURNOUTINHOS_TIMEBLOCK");
            builder.HasKey(tb => tb.Id);
            builder.Property(tb => tb.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("ID_TIMEBK");
            builder.Property(tb => tb.Name)
                .IsRequired()
                .HasColumnName("NAME_TIMEBK");
            builder.Property(tb => tb.Start)
                .HasColumnName("START_TIMEBK");
            builder.Property(tb => tb.TypeTime)
                .IsRequired()
                .HasColumnName("TYPE_TIMEBK");
            builder.Property(tb => tb.TimeCount)
                .IsRequired()
                .HasColumnName("TIME_COUNT");
            builder.Property(tb => tb.End)
                .IsRequired()
                .HasColumnName("MAX_TIMEBK");
            builder.Property(tb => tb.UserId)
                .HasColumnName("ID_USER");
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(tb => tb.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Property(tb => tb.TodoId)
                .HasColumnName("ID_TODO");
            builder.HasOne<Todo>()
                .WithMany()
                .HasForeignKey(tb => tb.TodoId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Property(tb => tb.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("CREATED_AT");
            builder.Property(tb => tb.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("UPDATED_AT");
        }
    }
}
