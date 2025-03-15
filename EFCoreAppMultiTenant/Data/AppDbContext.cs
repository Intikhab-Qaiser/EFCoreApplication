using EFCoreAppMultiTenant.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EFCoreAppMultiTenant.Data
{
    public class AppDbContext : DbContext
    {
        private readonly string _tenantSchema;

        public AppDbContext(string tenantSchema)
        {
            _tenantSchema = tenantSchema;
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=EPLVRIGW000F\\SQLEXPRESS;Database=EfCoreMultitenantApp;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(_tenantSchema); // Dynamic Schema Per Tenant
            modelBuilder.Entity<Student>().ToTable("Students", _tenantSchema);
        }
    }
}
