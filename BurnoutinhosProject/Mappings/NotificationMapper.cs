using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class NotificationMapper : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("t_burnoutinhos_notification");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id_notif");
            builder.Property(n => n.UserId)
                .HasColumnName("id_user")
                .IsRequired();
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(n => n.Message)
                .HasColumnName("message_notif")
                .IsRequired();
            builder.Property(n => n.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder.Property(n => n.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder.Property(n => n.IsRead)
                .HasDefaultValue(false)
                .IsRequired();
        }
    }
}
