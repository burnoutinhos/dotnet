using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class TodoMapper : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todo");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");
            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("title");
            builder.Property(t => t.Description)
                .HasColumnName("description");
            builder.Property(t => t.IsCompleted)
                .HasDefaultValue(false)
                .IsRequired()
                .HasColumnName("is_completed");
            builder.Property(t => t.Start)
                .HasColumnName("due_date");
            builder.Property(t => t.End)
                .HasColumnName("end_date");
            builder.Property(t => t.Type)
                .IsRequired()
                .HasColumnName("type");
            builder.Property(t => t.IdSugestion)
                .HasColumnName("id_suggestion");
            builder.HasOne<Todo>()
                .WithMany()
                .HasForeignKey(t => t.IdSugestion)
                .HasConstraintName("sugestao_todo");
            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("user_id");
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("usuario_todo");
            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasColumnName("updated_at");
        }
    }
}
