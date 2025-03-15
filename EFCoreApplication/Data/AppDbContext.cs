using EFCoreApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApplication.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            
            optionsBuilder.UseSqlServer("Server=EPLVRIGW000F\\SQLEXPRESS;Database=EfCoreAdvancedApp;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Student>()
                .Property(s => s.RowVersion)
                .IsRowVersion();

            // Apply a Global Query Filter to exclude soft-deleted records
            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
        }

        // Compiled Query (Performance Optimization)
        public static readonly Func<AppDbContext, int, Student?> GetStudentByIdCompiled =
            EF.CompileQuery((AppDbContext context, int studentId) =>
                context.Students.FirstOrDefault(s => s.Id == studentId));

    }
}
