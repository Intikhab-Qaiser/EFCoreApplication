using Microsoft.EntityFrameworkCore;
using EFCoreHeirarchyTPH.Models;

namespace EFCoreHeirarchyTPH.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=EPLVRIGW000F\\SQLEXPRESS;Database=EfCoreHeirarchyTPH;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
           .HasDiscriminator<string>("PersonType")
           .HasValue<Student>("Student")
           .HasValue<Teacher>("Teacher");
        }
    }
}
