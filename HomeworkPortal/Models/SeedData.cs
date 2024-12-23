using Microsoft.EntityFrameworkCore;

namespace HomeworkPortal.Models
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>().HasData(
                   new Lesson() { Id = 1, Name = "İnternet Programcılığı", IsActive = true },
                   new Lesson() { Id = 2, Name = "Bilgisayar Donanımı", IsActive = false },
                   new Lesson() { Id = 3, Name = "Veritabanı Yönetimi", IsActive = true }
                );



        }
    }
}
