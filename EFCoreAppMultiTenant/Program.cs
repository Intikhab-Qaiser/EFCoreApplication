// See https://aka.ms/new-console-template for more information

using EFCoreAppMultiTenant.Data;
using EFCoreAppMultiTenant.Models;
using Microsoft.EntityFrameworkCore;

//EnsureSchemasAndMigrate("TenantA");
//EnsureSchemasAndMigrate("TenantB");

SeedDataForTenant("TenantA");
SeedDataForTenant("TenantB");

//FetchStudentsForTenant("TenantA");
//FetchStudentsForTenant("TenantB");

static void EnsureSchemasAndMigrate(string tenantSchema)
{
    using var context = new AppDbContext(tenantSchema);

    // Create Schema if Not Exists
    context.Database.ExecuteSqlRaw($"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{tenantSchema}') EXEC('CREATE SCHEMA {tenantSchema}')");

    // Apply Migrations
    context.Database.Migrate();
}

static void SeedDataForTenant(string tenantSchema)
{
    using var context = new AppDbContext(tenantSchema);

    if (!context.Students.Any())
    {
        var students = new List<Student>
        {
            new Student { Name = $"Alice {tenantSchema}", Age = 22, Email = $"alice@{tenantSchema}.com" },
            new Student { Name = $"Bob {tenantSchema}", Age = 24, Email = $"bob@{tenantSchema}.com" }
        };

        context.Students.AddRange(students);
        context.SaveChanges();
        Console.WriteLine($"Students added for tenant: {tenantSchema}");
    }
}

static void FetchStudentsForTenant(string tenantSchema)
{
    using var context = new AppDbContext(tenantSchema);

    Console.WriteLine($"\nFetching students for tenant: {tenantSchema}");
    foreach (var student in context.Students)
    {
        Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Email: {student.Email}");
    }
}
