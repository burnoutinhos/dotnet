using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurnoutinhosProject.Mappings
{
    public class SuggestionMapper : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            builder.ToTable("T_BURNOUTINHOS_SUGGESTION");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("ID_SUGGESTION");
            builder.Property(s => s.SuggestionDescription)
                .HasColumnName("SUGGESTION_DESC")
                .IsRequired();
            builder.Property(s => s.TodoId)
                .HasColumnName("ID_TODO")
                .IsRequired();
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.TodoId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(s => s.CreatedAt)
                .HasColumnName("CREATED_AT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        }
    }
}
