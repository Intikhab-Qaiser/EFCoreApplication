using Microsoft.EntityFrameworkCore;
using EFCoreInterceptor.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using EFCoreCustomConventions.Encryption;

namespace EFCoreInterceptor.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=EPLVRIGW000F\\SQLEXPRESS;Database=EfCoreCustomConventions;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(s => s.Id);

            // Apply property based convention
            // modelBuilder.Entity<Student>().Property(c => c.Name).HasMaxLength(100);

            // Apply a global convention: All string properties get a max length of 100
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // String type lenght is 100
                foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(string)))
                {
                    property.SetMaxLength(100);
                }

                // Decimal type precision
                foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(decimal)))
                {
                    property.SetPrecision(18);
                    property.SetScale(2);
                }
            }

            // Convert Enum to String
            modelBuilder.Entity<Student>()
                .Property(s => s.Status)
                .HasConversion<string>(); // Store as VARCHAR instead of INT

            // Convertion of IsEnrolled property
            var boolToYesNoConverter = new ValueConverter<bool, string>(
                    v => v ? "Yes" : "No", // Convert true → "Yes", false → "No"
                    v => v == "Yes"); // Convert "Yes" → true, "No" → false

            modelBuilder.Entity<Student>()
                .Property(s => s.IsEnrolled)
                .HasConversion(boolToYesNoConverter);


            // Encrption of SSN Property
            var encryptConverter = new ValueConverter<string, string>(
                    v => EncryptionHelper.Encrypt(v),  // Encrypt before saving
                    v => EncryptionHelper.Decrypt(v)); // Decrypt when retrieving

            modelBuilder.Entity<Student>()
                .Property(s => s.SSN)
                .HasConversion(encryptConverter);
        }
    }
}
