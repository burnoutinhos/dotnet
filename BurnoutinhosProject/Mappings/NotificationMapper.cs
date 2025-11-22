using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class NotificationMapper : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("T_BURNOUTINHOS_NOTIFICATION");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("ID_NOTIF");
            builder.Property(n => n.UserId)
                .HasColumnName("ID_USER")
                .IsRequired();
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(n => n.Message)
                .HasColumnName("MESSAGE_NOTIF")
                .IsRequired();
            builder.Property(n => n.CreatedAt)
                .HasColumnName("CREATED_AT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder.Property(n => n.UpdatedAt)
                .HasColumnName("UPDATED_AT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        }
    }
}
