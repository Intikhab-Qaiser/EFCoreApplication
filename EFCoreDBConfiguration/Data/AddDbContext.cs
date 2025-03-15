using EFCoreDBConfiguration.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=FluentAPIvsDataAnnotations;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table Name
        modelBuilder.Entity<Student>().ToTable("Students");

        // Primary Key
        modelBuilder.Entity<Student>().HasKey(s => s.Id);

        // Column Constraints
        modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Student>()
            .Property(s => s.Age)
            .HasDefaultValue(18);

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Email)  // Create an index on Email
            .IsUnique();

        // One-to-One Relationship
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Address)
            .WithOne()
            .HasForeignKey<Student>(s => s.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}