using Microsoft.EntityFrameworkCore;

namespace HomeworkPortal.Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
