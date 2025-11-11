using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");
            builder.Property(u => u.Name)
                .IsRequired()
                .HasColumnName("username");
            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email");
            builder.Property(u => u.Password)
                .IsRequired()
                .HasColumnName("password");
            builder.Property(u => u.PreferredLanguage)
                .IsRequired()
                .HasColumnName("preferred_language");
            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("created_at");
            builder.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("updated_at");
        }

    }
}
