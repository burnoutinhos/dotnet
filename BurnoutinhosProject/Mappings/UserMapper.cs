using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("t_burnoutinhos_user");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id_user");
            builder.Property(u => u.Name)
                .IsRequired()
                .HasColumnName("name_user");
            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email_user");
            builder.Property(u => u.Password)
                .IsRequired()
                .HasColumnName("password");
            builder.Property(u => u.PreferredLanguage)
                .IsRequired()
                .HasColumnName("language");
            builder.Property(u => u.ProfileImage)
                .HasColumnName("profile_image");
        }

    }
}
