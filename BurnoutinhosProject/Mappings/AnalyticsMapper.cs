using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class AnalyticsMapper : IEntityTypeConfiguration<Analytics>
    {
        public void Configure(EntityTypeBuilder<Analytics> builder)
        {
            builder.ToTable("Analytics");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");
            builder.Property(a => a.UserId)
                .HasColumnName("user_id")
                .IsRequired();
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(a => a.Metric)
                .IsRequired();
            builder.Property(a => a.Value)
                .IsRequired();
            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        }
    }
}
