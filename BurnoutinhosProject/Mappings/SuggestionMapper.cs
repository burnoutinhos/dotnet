using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class SuggestionMapper : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.ToTable("t_burnoutinhos_suggestion");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id_suggestion");
            builder.Property(s => s.SuggestionDescription)
                .HasColumnName("suggestion_desc")
                .IsRequired();
            builder.Property(s => s.TodoId)
                .HasColumnName("id_todo")
                .IsRequired();
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.TodoId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(s => s.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        }
    }
}
