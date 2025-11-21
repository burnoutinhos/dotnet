using BurnoutinhosProject.Enums;
using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_BURNOUTINHOS_USER");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnType("NUMBER(10)")
                .HasColumnName("ID_USER");
            builder.Property(u => u.Name)
                .IsRequired()
                .HasColumnName("NAME_USER");
            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("EMAIL_USER");
            builder.Property(u => u.Password)
                .IsRequired()
                .HasColumnName("PASSWORD");
            builder.Property(u => u.PreferredLanguage)
                .IsRequired()
                .HasColumnName("LANGUAGE")
                .HasConversion(
                    v => v.ToString().Replace("_", "-"),
                    v => (LanguageEnum)Enum.Parse(typeof(LanguageEnum), v.Replace("-", "_"))
                );
            builder.Property(u => u.ProfileImage)
                .HasColumnName("PROFILE_IMAGE");
        }

    }
}
