using BurnoutinhosProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BurnoutinhosProject.Connection
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TimeBlock> TimeBlock { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Analytics> Analytics { get; set; }
        public DbSet<Suggestion> Suggestion { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Todo> Todo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
