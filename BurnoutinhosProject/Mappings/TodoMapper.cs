using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class TodoMapper : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("T_BURNOUTINHOS_TODO");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("ID_TODO");
            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("NAME_TODO");
            builder.Property(t => t.Description)
                .HasColumnName("DESCRIPTION");
            builder.Property(t => t.IsCompleted)
                .HasDefaultValue(false)
                .IsRequired()
                .HasColumnName("IS_COMPLETED");
            builder.Property(t => t.Start)
                .HasColumnName("START_TODO");
            builder.Property(t => t.End)
                .HasColumnName("END_TODO");
            builder.Property(t => t.Type)
                .IsRequired()
                .HasColumnName("TYPE");
            builder.Property(t => t.IdSugestion)
                .HasColumnName("ID_SUGGESTION");
            builder.HasOne<Todo>()
                .WithMany()
                .HasForeignKey(t => t.IdSugestion)
                .HasConstraintName("SUGESTAO_TODO");
            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("USER_ID");
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("USUARIO_TODO");
            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("CREATED_AT");
            builder.Property(t => t.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("UPDATED_AT");
        }
    }
}
